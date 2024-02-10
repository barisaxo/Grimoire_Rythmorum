using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        public float RotY;

        public void InstantiateNewSceneObject(State currentState)
        {
            SceneObject = Type switch
            {
                CellType.Cove => new NullCove(),
                CellType.Rocks => new Rocks(),
                CellType.Fish => new Fish(currentState),
                CellType.Lighthouse => new Lighthouse(WorldMapScene.Io.Ship.Region, currentState),
                CellType.Bottle => new Bottle(currentState),
                // CellType.OpenSea => null,

                _ => throw new System.NotImplementedException()
            };
        }

        public void DestroySceneObject()
        {
            GameObject.Destroy(GO);
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



        public static bool operator ==(Cell a, Cell b)
        {
            if ((a is null && b is not null) || (a is not null && b is null)) return false;
            if (a is null && b is null) return true;
            return a._type == b._type && a.Coord == b.Coord;
        }
        public static bool operator !=(Cell a, Cell b)
        {
            if ((a is null && b is not null) || (a is not null && b is null)) return true;
            if (a is null && b is null) return false;
            return a._type != b._type || a.Coord != b.Coord;
        }

        public override bool Equals(object obj) => obj is Cell c && Coord == c.Coord && _type == c._type;
        public override int GetHashCode() => HashCode.Combine(_type, Coord);
    }

    public enum CellType { OpenSea, Cove, Rocks, Center, Lighthouse, Fish, Bottle }
}
