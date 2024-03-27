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

        public void InstantiateNewSceneObject(State currentState, DataManager data, MusicTheory.RegionalMode region)
        {
            SceneObject = Type switch
            {
                CellType.Cove => new NullCove(),
                CellType.Rocks => new Rocks(),
                CellType.Fish => new Fish(currentState, data),
                CellType.Lighthouse => new Lighthouse(WorldMapScene.Io.Ship.Region, data.LighthousesData, currentState),
                CellType.Bottle => new Bottle(currentState, data, region),
                CellType.Gramo => new Gramophone(currentState, data.GramophoneData, data.QuestsData, data.ShipData, region),
                // CellType.OpenSea => null,

                _ => throw new System.NotImplementedException()
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

    public enum CellType { OpenSea, Cove, Rocks, Center, Lighthouse, Fish, Bottle, Gramo, Bounty }
}
