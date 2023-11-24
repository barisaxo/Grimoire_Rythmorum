using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using System;

public class SeaScene
{
    public void SelfDestruct()
    {
        Swells.DisableSwells();
        RockTheBoat.SelfDestruct();
        Object.Destroy(TheSea);
        Resources.UnloadUnusedAssets();
    }

    #region FIELDS

    public bool ShowShipCoords = false;
    public bool ShowIslandCoords = false;

    public GameObject TheSea = new(nameof(TheSea));

    public PlayerShip Ship;

    public List<NPCShip> NPCShips = new();

    public bool Sailing;
    public bool haveMoved = false;

    public Swells Swells;

    public RockTheBoat RockTheBoat = new();
    public CameraFollow CameraFollow;

    public List<GameObject> UnusedRocks = new();
    public List<GameObject> UnusedShips = new();

    public List<SeaGridTile> Board;
    public SeaMapTile[] Map;

    public NPCShip NearNPCShip;

    private bool IsOccupied(Vector3Int value)
    {
        foreach (var ship in NPCShips) if (ship.Coords == value) return true;
        return false;
    }

    private bool IsOpen(Vector3Int value)
    {
        if (Map[PureMapIndex(value.x, value.z)].Type == SeaMapTileType.Rocks) return false;
        return true;
    }

    bool IsInRange(Vector3Int v) => v.x > -1 && v.x < MapSize && v.z > -1 && v.z < MapSize;
    int PureMapIndex(int x, int z) => new Vector2(x, z).Vec2ToInt(MapSize);

    public Vector3Int BoardCenter => new(BoardOffset, 0, BoardOffset);

    public int BoardOffset => Mathf.FloorToInt(BoardSize * .5f);
    public Vector3Int restoreMapLoc;

    public int BoardSize { get; private set; } = 11;
    public int SeaGridSize => BoardSize * 3;
    public int MapSize { get; private set; }

    // public Vector2 IslandLoc;

    public Color SeaColor;
    public Color MyGrey => new Color(.45f, .45f, .45f, .35f);
    public Color MyRed => new Color(.85f, .15f, .35f, .35f);
    public Color MyGreen => new Color(.25f, .55f, .25f, .35f);
    public Color MyBlue => new Color(.15f, .35f, .85f, .35f);
    public Color MyCyan => new Color(.15f, .65f, .55f, .35f);
    public Color MyYellow => new Color(.75f, .65f, .15f, .35f);
    public Color MyMagenta => new Color(.85f, .15f, .75f, .35f);

    public Color OverlayWhite => new Color(.3f, .3f, .3f, .8f);
    public Color OverlayGrey => new Color(.5f, .5f, .1f, .2f);
    public Color OverlayGreen => new Color(0, 1f, .0f, .2f);
    public Color OverlayRed => new Color(1f, 0f, 0f, .2f);

    public readonly string _shipString = "SHIP ";
    public readonly string _islandString = "ISLAND ";
    public readonly string _commaSpaceString = ", ";

    #endregion

    #region INITIALIZATION

    public SeaScene()
    {
        SeaColor = MyCyan;

        BoardSize = 11;
        MapSize = 77;

        Board = SetUpBoard();
        Map = SetUpMap();

        _ = DayCounterText;

        Ship = new(this);
        Ship.Parent.transform.SetParent(TheSea.transform);
        Ship.Parent.transform.position = Board[(int)(Board.Count * .5f)].Loc + (Vector3.up * .3f);
        Ship.Pos = new Vector3(1, 0, 1) * (int)(MapSize * .5f);

        SetUpSeaCam();

        Swells = new(this);
        Swells.EnableSwells();

        RockTheBoat.AddBoat(Ship.GO.transform, .08f, 1, 0);
        RockTheBoat.Rocking = true;
        AddShips();
    }

    private void AddShips()
    {
        int numShips = Random.Range((int)(MapSize * .3f), (int)(MapSize * .6f));

        for (int i = 0; i < numShips; i++)
        {
            Vector3Int randA = Rand(i + BoardSize);
            Vector3Int randB = Rand(i + MapSize);
            Vector3Int randC = Rand(i + BoardSize + MapSize);
            Vector3Int randD = Rand(i + BoardSize + MapSize * 2);

            List<Vector3Int> path = new() { };

            var ab = Map[randA.Vec3ToInt(MapSize)].NewSailingPath(
                 Map[randB.Vec3ToInt(MapSize)],
                 Map,
                 MapSize);
            for (int n = 0; n < ab.Length; n++)
                path.Add(new Vector3Int(ab[n].Coord.x, 0, ab[n].Coord.y));

            var bc = Map[randB.Vec3ToInt(MapSize)].NewSailingPath(
                 Map[randC.Vec3ToInt(MapSize)],
                 Map,
                 MapSize);
            for (int n = 0; n < bc.Length; n++)
                path.Add(new Vector3Int(bc[n].Coord.x, 0, bc[n].Coord.y));

            var cd = Map[randC.Vec3ToInt(MapSize)].NewSailingPath(
                  Map[randD.Vec3ToInt(MapSize)],
                  Map,
                  MapSize);
            for (int n = 0; n < cd.Length; n++)
                path.Add(new Vector3Int(cd[n].Coord.x, 0, cd[n].Coord.y));

            var da = Map[randD.Vec3ToInt(MapSize)].NewSailingPath(
                 Map[randA.Vec3ToInt(MapSize)],
                 Map,
                 MapSize);
            for (int n = 0; n < da.Length; n++)
                path.Add(new Vector3Int(da[n].Coord.x, 0, da[n].Coord.y));

            NPCShip ship = new(randA, path.ToArray());
            foreach (var p in path) { Debug.Log(p); }
            NPCShips.Add(ship);
        }

        Vector3Int Rand(int i)
        {
            int r = RandInt();
            while (!Map[r].IsOpen || LocTaken(Map[r].Loc))
            {
                r = (r + Random.Range(BoardSize + i * BoardSize, MapSize + i * MapSize)) % (Map.Length - 1);
            }

            return Map[r].Loc;
        }

        bool LocTaken(Vector3Int loc)
        {
            foreach (var ship in NPCShips) if (ship.Coords == loc) return true;
            return false;
        }

        int RandInt()
        {
            int r = Random.Range(1, (int)((Map.Length - 1) * .5f));
            r = Random.value > .5f ? r : Map.Length - r;
            return r;
        }
    }


    List<SeaGridTile> SetUpBoard()
    {
        List<SeaGridTile> boardTiles = new();
        for (int x = 0; x < SeaGridSize; x++)
        {
            for (int z = 0; z < SeaGridSize; z++)
            {
                boardTiles.Add(new SeaGridTile(new Vector3Int(x, 0, z), this));
                // if (!IsWall())
                // bool IsWall() =>
                //        (x - SeaGridSize) * (z - SeaGridSize) < SeaGridSize ||
                //        (x - SeaGridSize) * -z < SeaGridSize ||
                //        (-x * (z - SeaGridSize)) < SeaGridSize ||
                //        (x * z) < SeaGridSize;
            }
        }
        return boardTiles;
    }

    SeaMapTile[] SetUpMap()
    {
        List<SeaMapTile> mapTiles = new();
        for (int x = 0; x < MapSize; x++)
            for (int z = 0; z < MapSize; z++)
                mapTiles.Add(new SeaMapTile(new Vector3Int(x, 0, z))
                {
                    Type = SeaMapTileType.OpenSea
                });


        for (int i = 0; i < mapTiles.Count; i++)
        {
            if (mapTiles[i].Loc.x < BoardOffset || mapTiles[i].Loc.x > MapSize - BoardOffset ||
                mapTiles[i].Loc.z < BoardOffset || mapTiles[i].Loc.z > MapSize - BoardOffset)
            {
                mapTiles[i].Type = SeaMapTileType.Rocks;
                continue;
            }

            mapTiles[i].Type = Random.Range(0, 10) switch
            {
                0 => SeaMapTileType.Rocks,
                _ => SeaMapTileType.OpenSea,
            };

            if (ClearCenter(mapTiles[i].Loc.x, mapTiles[i].Loc.z))
            {
                mapTiles[i].Type = SeaMapTileType.OpenSea;
                continue;
            }

            if (mapTiles[i].Type == SeaMapTileType.Rocks)
                for (int nX = -1; nX < 2; nX++)
                    for (int nZ = -1; nZ < 2; nZ++)
                    {
                        if (nX == nZ) continue;

                        Vector3Int loc = mapTiles[i].Loc + new Vector3Int(nX, 0, nZ);
                        if (!IsInRange(loc)) continue;

                        if (!mapTiles[loc.Vec3ToInt(MapSize)].IsOpen) continue;

                        if (!mapTiles[(int)(mapTiles.Count * .5f)].IsTileReachable(mapTiles[loc.Vec3ToInt(MapSize)], mapTiles.ToArray(), MapSize))
                        {
                            mapTiles[i].Type = SeaMapTileType.OpenSea;
                            break;
                        }
                    }
        }

        return mapTiles.ToArray();

        bool ClearCenter(int x, int z)
        {
            for (int i = -2; i < 3; i++)
                for (int j = -2; j < 3; j++)
                    if (x == Mathf.FloorToInt(MapSize * .5f) + i &&
                        z == Mathf.FloorToInt(MapSize * .5f) + j)
                        return true;

            return false;
        }
    }

    void SetUpSeaCam()
    {
        // Cam.Io.Camera.transform.SetParent(null);
        // Cam.Io.Camera.transform.SetPositionAndRotation(
        //        new Vector3(2, 1, 3.75f),
        //        Quaternion.Euler(new Vector3(17, 90, 0)));


        // Cam.Io.Camera.transform.SetParent(Ship.Parent.transform);
        Cam.Io.Camera.transform.rotation = Ship.Parent.transform.rotation;
        Cam.Io.Camera.transform.position = Ship.Parent.transform.parent.position;
        Cam.Io.Camera.transform.Translate(Vector3.up - (Cam.Io.Camera.transform.forward * 2));
        Cam.Io.Camera.transform.LookAt(Ship.GO.transform, Vector3.up);
        // Cam.Io.Camera.transform.Translate(new Vector3(0, .5f, -2), Ship.GO.transform);
        Cam.Io.Camera.transform.Rotate(new Vector3(-10, 0, 0));


        CameraFollow = new(Ship.Parent.transform)
        {
            LockYPos = true,
            LockXRot = true,
            LockWRot = true,
            LockZRot = true,
        };

        var l = new GameObject(nameof(Light)).AddComponent<Light>();
        l.type = LightType.Directional;
        l.color = new Color(.9f, .8f, .65f);
        l.shadows = LightShadows.None;
        l.transform.SetPositionAndRotation(Cam.Io.Camera.transform.position, Cam.Io.Camera.transform.rotation);
        l.transform.Rotate(new Vector3(45, 0, 0));
        l.intensity = 1.95f;

        // float CamPosZ() => 1.5f;
        // float CamPosY() => 17 / Cam.Io.Camera.aspect;
        // float CamRotX() => 75;
        // Vector3 CenterPos() => SeaGrid[(int)(SeaGrid.Count * .5f)].Loc;
    }

    private Card _dayCounterText;
    public Card DayCounterText => _dayCounterText ??= new Card(nameof(DayCounterText), TheSea.transform)
        .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, Cam.UIOrthoY - 1))
        .SetTMPSize(new Vector2(1, 1))
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetFontScale(.55f, .55f)
        .AutoSizeFont(true);

    private Card _sextantText;
    public Card SextantText => _sextantText ??= new Card(nameof(SextantText), TheSea.transform)
        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1))
        .SetTMPSize(new Vector2(3, 1))
        .SetTextAlignment(TextAlignmentOptions.Center)
        .SetFontScale(.55f, .55f)
        .AutoSizeFont(true);

    private Card _islandCoordText;
    public Card IslandCoordText => _islandCoordText ??= new Card(nameof(IslandCoordText), TheSea.transform)
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, Cam.UIOrthoY - 1))
        .SetTMPSize(new Vector2(3, 1))
        .SetTextAlignment(TextAlignmentOptions.Left)
        .SetFontScale(.55f, .55f)
        .AutoSizeFont(true);

    #endregion
}
