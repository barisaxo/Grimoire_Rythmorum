using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Sea
{
    public sealed class Scene
    {

        public float DebugTimer;
        public readonly float DebugInterval = 5f;

        #region  INSTANCE

        private Scene()
        {
            SeaColor = this.MyCyan();
            Board = new(11, TheSea.transform, SeaColor);

            Swells = new(this);
            Swells.EnableSwells();

            Ship = new PlayerShip(this);
            RockTheBoat.AddBoat(Ship.GO.transform, (.08f, 1, 0));
            RockTheBoat.Rocking = true;

            _ = HUD;
            this.SetUpSeaCam();
        }

        public static Scene Io => Instance.Io;

        private class Instance
        {
            static Instance() { }
            static Scene _io;
            internal static Scene Io => _io ??= new Scene();
            internal static void Destruct() => _io = null;
        }

        public void SelfDestruct()
        {
            HUD.SelfDestruct();
            Swells.DisableSwells();
            RockTheBoat.SelfDestruct();
            Object.Destroy(TheSea);
            Resources.UnloadUnusedAssets();
            Instance.Destruct();
        }

        #endregion  INSTANCE

        public GameObject TheSea = new(nameof(TheSea));

        public Color SeaColor;
        public Board Board;
        public Swells Swells;

        public Vector3Int restoreMapLoc;
        private Map _map;
        public Map Map => _map ??= new(30, 20);

        public HUD _hud;
        public HUD HUD => _hud ??= new(DataManager.Io.CharacterData);

        public PlayerShip Ship;
        public RockTheBoat RockTheBoat = new();
        public NPCShip NearestNPC;

        public List<GameObject> UsedRocks = new();
        public List<GameObject> UnusedRocks = new();
        public List<GameObject> UsedShips = new();
        public List<GameObject> UnusedShips = new();



    }
}