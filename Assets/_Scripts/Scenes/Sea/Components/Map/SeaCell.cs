using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sea
{
    public class Cell
    {
        public Vector2Int Coord { get; }

        private CellType _type;
        public CellType Type
        {
            get => _type;
            set
            {
                _type = value;

                IsAStarOpen = value switch
                {
                    CellType.OpenSea or
                    CellType.Fish or
                    CellType.Center or
                    CellType.Bottle
                        => true,

                    _ => false,
                };
            }
        }

        public bool IsAStarOpen { get; private set; }

        public ISceneObject SceneObject;
        public GameObject GO => SceneObject?.GO;
        // public float RotY;

        public ISceneObject InstantiateNewSceneObject(State currentState, Data.Two.Manager data, Region region, Cell cell)
        {
            // Debug.Log("Instantiating new scene object: " + Type.ToString() + " " + Coord);
            return SceneObject = Type switch
            {
                CellType.Cove => new NullCove(),
                CellType.Rocks => new Rocks(),
                CellType.Fish => new Fish(currentState, data),
                CellType.Lighthouse => new Lighthouse(WorldMapScene.Io.Ship.Region, data.Lighthouse, currentState),
                CellType.Bottle => new Bottle(currentState, data),
                CellType.Gramo => new Gramophone(currentState, data.Inventory, data.Quests, data.ActiveShip, region.R),
                // CellType.Border => new Border(region),
                CellType.OpenSea => null,
                CellType.Bounty => new BountyShip(currentState, data.Quests, data.ActiveShip, region, cell),

                _ => throw new System.NotImplementedException(Type.ToString())
            };

        }

        public void DestroySceneObject()
        {
            Object.Destroy(GO);
            SceneObject = null;
        }

        public Cell(Vector2Int coord)
        {
            Coord = coord;
        }

        public Cell(int x, int y)
        {
            Coord = new(x, y);
        }



    }

    public enum CellType { OpenSea, Border, Cove, Rocks, Center, Lighthouse, Fish, Bottle, Gramo, Bounty }
}
