using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeRoom
{
    public PracticeRoom(Transform parent)
    {
        Parent = parent;
        _ = Tiles;
    }

    public void SelfDestruct()
    {
        if (Parent)
            GameObject.Destroy(Parent.gameObject);
    }

    public const int boardSize = 33;
    readonly Transform Parent;
    private GameObject _tileParent;
    public GameObject TileParent => _tileParent ? _tileParent : _tileParent = SetUpTileParent();

    private GameObject SetUpTileParent()
    {
        GameObject go = new(nameof(Tiles));
        go.transform.SetParent(Parent);
        return go;
    }

    private List<GameObject> _tiles;
    public List<GameObject> Tiles => _tiles ??= SetUpPracticeRoom();

    private List<GameObject> SetUpPracticeRoom()
    {
        List<GameObject> tiles = new();
        List<Vector2> floorTiles = new();
        List<Vector2> wallTiles = new();
        AssignTilePos();

        CreateFloorTiles();
        CreateWallTiles();

        return tiles;

        void AssignTilePos()
        {
            List<Vector2> extraWallTiles = new();
            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {
                    if (IsWall()) wallTiles.Add(new Vector2(x, z));
                    else floorTiles.Add(new Vector2(x, z));

                    bool IsWall() =>
                        (x - boardSize) * (z - boardSize) < boardSize ||
                        (x - boardSize) * -z < boardSize ||
                        (-x * (z - boardSize)) < boardSize ||
                        (x * z) < boardSize;
                }
            }

            foreach (Vector2 v in wallTiles) if (IsExtraWall(v)) extraWallTiles.Add(v);
            foreach (Vector2 v in extraWallTiles) wallTiles.Remove(v);

            bool IsExtraWall(Vector2 v)
            {
                if (floorTiles.Contains(v + Vector2.up) || floorTiles.Contains(v + Vector2.down) ||
                   floorTiles.Contains(v + Vector2.left) || floorTiles.Contains(v + Vector2.right)) return false;
                return true;
            }
        }

        void CreateFloorTiles()
        {
            foreach (Vector2 v in floorTiles)
            {
                GameObject t = GameObject.CreatePrimitive(PrimitiveType.Cube);
                t.name = "Tile: " + v.x + ", " + v.y;
                t.transform.localScale = Vector3.one * Random.Range(1f, 1.15f);
                // t.transform.SetPositionAndRotation(
                t.transform.position = new Vector3(v.x, 0, v.y);
                //     Quaternion.Euler(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f))));
                t.transform.SetParent(TileParent.transform);
                //var mr = go.GetComponent<MeshRenderer>();
                tiles.Add(t);
            }
        }

        void CreateWallTiles()
        {
            foreach (Vector2 v in wallTiles)
            {
                GameObject t = GameObject.CreatePrimitive(PrimitiveType.Cube);
                t.name = "WallTile: " + v.x + ", " + v.y;
                t.transform.localScale = new Vector3(1, 2.5f, 1) * Random.Range(1.15f, 1.666f);
                t.transform.position = new Vector3(v.x, 0, v.y);
                // t.transform.SetPositionAndRotation(
                //     new Vector3(v.x, Random.value * .08f, v.y),
                //     Quaternion.Euler(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f))));
                t.transform.SetParent(TileParent.transform);
                //var mr = go.GetComponent<MeshRenderer>();
                tiles.Add(t);
            }
        }
    }

}