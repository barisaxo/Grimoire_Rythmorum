using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sea.Maps;

namespace Sea
{
    public sealed class WorldMapScene
    {
        public float DebugTimer;
        public readonly float DebugInterval = 5f;

        #region  INSTANCE

        private WorldMapScene()
        {
            Board = new(17, TheSea.transform)
            {
                Swells = new(this)
            };
            Board.Swells.EnableSwells();

            Ship = new PlayerShip(this);
            // Ship.GO.transform.SetPositionAndRotation(
            //     Ship.Parent.transform.position,
            //     Quaternion.Euler(new Vector3(0, 180, 0)));

            RockTheBoat.AddBoat(Ship.GO.transform, (.08f, 1, 0));
            RockTheBoat.Rocking = true;

            _ = HUD;
            _ = MiniMap;
            this.SetUpSeaCam();
        }

        public static WorldMapScene Io => Instance.Io;

        private class Instance
        {
            static Instance() { }
            static WorldMapScene _io;
            internal static WorldMapScene Io => _io ??= new WorldMapScene();
            internal static void Destruct() => _io = null;
        }

        public void SelfDestruct()
        {
            MiniMap.SelfDestruct();
            HUD.SelfDestruct();
            Board.Swells.DisableSwells();
            RockTheBoat.SelfDestruct();
            Object.Destroy(TheSea);
            Resources.UnloadUnusedAssets();
            Instance.Destruct();
        }

        #endregion  INSTANCE

        public GameObject TheSea = new(nameof(TheSea));

        private MiniMap _miniMap;
        public MiniMap MiniMap => _miniMap ??= new(Map);

        public Board Board;

        public Vector3Int restoreMapLoc;
        private WorldMap _map;
        public WorldMap Map => _map ??= new();

        public HUD _hud;
        public HUD HUD => _hud ??= new(DataManager.Io.CharacterData);

        public PlayerShip Ship;
        public RockTheBoat RockTheBoat = new();
        public NPCShip NearestNPC;
        // public Fish NearestFish;

        // public GameObject Fog;
        // public Material FogMat;

        public Cell NearestInteractableCell;

        // public List<GameObject> UsedRocks = new();
        // public List<GameObject> UnusedRocks = new();
        // public List<GameObject> UsedShips = new();
        // public List<GameObject> UnusedShips = new();
        // public List<GameObject> UsedFish = new();
        // public List<GameObject> UnusedFish = new();

        public List<ISceneObject> SceneObjects = new();
        public List<NPCShip> NPCShips = new();
        public Region[] LocalRegions = new Region[] { };

    }

}