using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeaScene
{
    #region  INSTANCE

    private SeaScene()
    {
        SeaColor = MyCyan;

        SegmentDivision = 9;
        BoardSize = 11;
        MapSize = BoardSize * SegmentDivision;

        Board = SetUpBoard();
        SegmentedMapLocs = GetMapIndices();

        // return;
        Map = InitializeMapTiles();
        AddMapFeatures();

        _ = DayCounterText;

        Ship = new(this);
        Ship.Parent.transform.SetParent(TheSea.transform);
        Ship.Parent.transform.position = Board[(int)(Board.Count * .5f)].Loc + (Vector3.up * .3f);
        Ship.Pos = new Vector3(1, 0, 1) * (int)(MapSize * .5f);
        Ship.GO.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        SetUpSeaCam();

        Swells = new(this);
        Swells.EnableSwells();

        RockTheBoat.AddBoat(Ship.GO.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;

        AddShips();
    }

    public static SeaScene Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static SeaScene _io;
        internal static SeaScene Io => _io ??= new SeaScene();
        internal static void Destruct() => _io = null;
    }

    public void SelfDestruct()
    {
        SeaHUD.SelfDestruct();
        Swells.DisableSwells();
        RockTheBoat.SelfDestruct();
        Object.Destroy(TheSea);
        Resources.UnloadUnusedAssets();
        Instance.Destruct();
    }

    #endregion  INSTANCE

    #region FIELDS

    public bool ShowShipCoords = false;
    public bool ShowIslandCoords = false;
    public bool Sailing;
    public bool haveMoved = false;

    public GameObject TheSea = new(nameof(TheSea));
    public SeaHUD SeaHUD = new(DataManager.Io.CharacterData);

    public List<GameObject> UnusedRocks = new();
    public List<GameObject> UnusedShips = new();
    public List<GameObject> UsedRocks = new();
    public List<GameObject> UsedShips = new();

    public Swells Swells;

    public PlayerShip Ship;
    public RockTheBoat RockTheBoat = new();

    public NPCShip NearNPCShip;
    public List<NPCShip> NPCShips = new();

    public int BoardSize { get; private set; }
    public int SeaGridSize => BoardSize * 3;
    public int MapSize { get; private set; }
    public int SegmentDivision { get; private set; }
    // public Vector2 IslandLoc;

    public Vector3Int restoreMapLoc;
    public List<SeaGridTile> Board;
    public SeaMapTile[] Map;
    public Vector3Int[][] SegmentedMapLocs;
    public Vector3Int BoardCenter => new(BoardOffset, 0, BoardOffset);
    public int BoardOffset => Mathf.FloorToInt(BoardSize * .5f);

    private bool IsOpen(Vector3Int value)
    {
        if (Map[PureMapIndex(value.x, value.z)].Type == SeaMapTileType.Rocks) return false;
        return true;
    }

    private bool IsOccupied(Vector3Int value)
    {
        foreach (var ship in NPCShips) if (ship.Coords == value) return true;
        return false;
    }

    bool IsInRange(Vector3Int v) => v.x > -1 && v.x < MapSize && v.z > -1 && v.z < MapSize;
    int PureMapIndex(int x, int z) => new Vector2(x, z).Vec2ToInt(MapSize);

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

    private Card _dayCounterText;
    public Card DayCounterText => _dayCounterText ??= new Card(nameof(DayCounterText), TheSea.transform)
        .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, Cam.UIOrthoY - 1))
        .AutoSizeTextContainer(true)
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetFontScale(.45f, .45f)
        .AllowWordWrap(false)
        ;

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

    #region INITIALIZATION

    private void AddShips()
    {
        int numShips = Random.Range((int)(MapSize * .3f), (int)(MapSize * .6f));
        Debug.Log(numShips);
        for (int i = 0; i < numShips; i++)
        {
            Vector3Int randA = Rand(i + BoardSize);
            Vector3Int randB = Rand(i + MapSize);
            Vector3Int randC = Rand(i + BoardSize + MapSize);

            List<Vector3Int> path = new() { };

            var ab = Map[randA.Vec3ToInt(MapSize)].NewSailingPath(
                 Map[randB.Vec3ToInt(MapSize)],
                 Map,
                 MapSize);
            for (int n = 0; n < ab.Length; n++) path.Add(new Vector3Int(ab[n].Coord.x, 0, ab[n].Coord.y));

            var bc = Map[randB.Vec3ToInt(MapSize)].NewSailingPath(
                 Map[randC.Vec3ToInt(MapSize)],
                 Map,
                 MapSize);
            for (int n = 0; n < bc.Length; n++) path.Add(new Vector3Int(bc[n].Coord.x, 0, bc[n].Coord.y));

            var ca = Map[randC.Vec3ToInt(MapSize)].NewSailingPath(
                 Map[randA.Vec3ToInt(MapSize)],
                 Map,
                 MapSize);
            for (int n = 0; n < ca.Length; n++) path.Add(new Vector3Int(ca[n].Coord.x, 0, ca[n].Coord.y));

            NPCShip ship = new(randA, path.ToArray())
            {
                ShipType = NPCShips.Count < numShips / 6 ? NPCShipType.Pirate : NPCShipType.Trade
            };
            NPCShips.Add(ship);
        }

        Vector3Int Rand(int i)
        {
            int r = RandInt();
            int reroll = 0;
            while (!Map[r].IsAStarOpen || LocTaken(Map[r].Loc))
            {
                Debug.Log(++reroll);
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
            for (int z = 0; z < SeaGridSize; z++)
                boardTiles.Add(new SeaGridTile(new Vector3Int(x, 0, z), this));

        return boardTiles;
    }

    Vector3Int[][] GetMapIndices()
    {
        int index = 0;
        int segment = 0;
        Vector3Int[][] mapIndices = new Vector3Int[SegmentDivision * SegmentDivision][];

        for (int u = 0; u < MapSize; u += BoardSize)
        {
            for (int v = 0; v < MapSize; v += BoardSize)
            {
                mapIndices[segment] = MapSegment(u, v);
                segment++;
            }
        }

        return mapIndices;

        Vector3Int[] MapSegment(int u, int v)
        {
            List<Vector3Int> MapSegment = new();

            for (int x = 0; x < BoardSize; x++)
                for (int z = 0; z < BoardSize; z++)
                {
                    MapSegment.Add(new Vector3Int(u + x, index, v + z));
                    index++;
                }

            return MapSegment.ToArray();
        }
    }


    private SeaMapTile[] InitializeMapTiles()
    {
        Vector3Int[] mapIndices = SegmentedMapLocs.Flatten();
        SeaMapTile[] mapTiles = new SeaMapTile[mapIndices.Length];
        int index = 0;
        // foreach (Vector3Int loc in mapIndices)
        //     mapTiles[loc.y] = new SeaMapTile(loc) { Type = SeaMapTileType.OpenSea };

        for (int x = 0; x < MapSize; x++)
            for (int z = 0; z < MapSize; z++)
            {
                mapTiles[index] = new SeaMapTile(new Vector3Int(x, mapIndices[index].y, z)) { Type = SeaMapTileType.OpenSea };
                // Debug.Log(mapIndices[index] + " " + x + " " + z);
                index++;
            }

        return mapTiles;
    }

    void AddMapFeatures()
    {
        for (int u = 0; u < SegmentDivision; u++)
            for (int v = 0; v < SegmentDivision; v++)
            {
                // Debug.Log((u * BoardSize) + v);
                Vector3Int[] locSegment = SegmentedMapLocs[(u * SegmentDivision) + v];
                SeaMapTile[] mapSegment = GetTilesFromSegment(locSegment);

                for (int x = 0; x < BoardSize; x++)
                    for (int z = 0; z < BoardSize; z++)
                    {
                        int segmentIndex = (x * BoardSize) + z;
                        // int segmentIndex = (u * BoardSize * MapSize) + (v * BoardSize * BoardSize) + (x * BoardSize) + z;
                        int mapIndex = new Vector2(locSegment[segmentIndex].x, locSegment[segmentIndex].z).Vec2ToInt(MapSize);
                        // Debug.Log(index);
                        // Vector3Int coord = SegmentedMapCoords[(u * MapSize) + (v * BoardSize)][(x * BoardSize) + z];
                        if (Map[mapIndex].Loc.x < BoardOffset || Map[mapIndex].Loc.x > MapSize - BoardOffset ||
                            Map[mapIndex].Loc.z < BoardOffset || Map[mapIndex].Loc.z > MapSize - BoardOffset)
                        {
                            Map[mapIndex].Type = SeaMapTileType.Rocks;
                            continue;
                        }

                        Map[mapIndex].Type = Random.Range(0, 10) switch
                        {
                            0 => SeaMapTileType.Rocks,
                            _ => SeaMapTileType.OpenSea,
                        };

                        if (ClearCenter(Map[mapIndex].Loc.x, Map[mapIndex].Loc.z))
                        {
                            Map[mapIndex].Type = SeaMapTileType.Center;
                            continue;
                        }

                        if (Map[mapIndex].Type == SeaMapTileType.Rocks)
                            for (int nX = -1; nX < 2; nX++)
                                for (int nZ = -1; nZ < 2; nZ++)
                                {
                                    if (Mathf.Abs(nX) == Mathf.Abs(nZ)) continue;

                                    Vector3Int loc = new(nX + x, 0, nZ + z);
                                    // Debug.Log(mapIndex + " " + Map[mapIndex].Coord + " " + mapSegment[loc.Vec3ToInt(BoardSize)].Coord);
                                    if (!(nX + x > -1 && nX + x < 11 && nZ + z > -1 && nZ + z < 11)) continue;

                                    if (!Map[mapIndex].IsAStarOpen) continue;

                                    if (!mapSegment[(int)(mapSegment.Length * .5f)]
                                        .IsTileReachable(mapSegment[loc.Vec3ToInt(BoardSize)], mapSegment, BoardSize))
                                    {
                                        Map[mapIndex].Type = SeaMapTileType.OpenSea;
                                        break;
                                    }
                                }
                    }
            }


        bool ClearCenter(int x, int z)
        {
            for (int i = -5; i < 6; i++)
                for (int j = -5; j < 6; j++)
                    if (x == Mathf.FloorToInt(MapSize * .5f) + i &&
                        z == Mathf.FloorToInt(MapSize * .5f) + j)
                        return true;

            return false;
        }
    }

    private SeaMapTile[] GetTilesFromSegment(Vector3Int[] locSegment)
    {
        List<SeaMapTile> temp = new();
        foreach (Vector3Int v3i in locSegment)
            temp.Add(Map[v3i.Vec3ToInt(MapSize)]);

        // Debug.Log(temp.Count);
        return temp.ToArray();
    }
    // private SeaMapTile[] GetTilesFromSegment(int u, int v)
    // {
    //     List<SeaMapTile> temp = new();
    //     for (int uu = -1; uu < 2; uu++)
    //         for (int vv = -1; vv < 2; vv++)
    //             if (u + uu > -1 && u + uu < 11 && v + vv > -1 && v + vv < 11)
    //                 foreach (Vector3Int i in SegmentedMap[((u + uu) * BoardSize) + (v + vv)])
    //                     temp.Add(Map[i.y]);

    //     Debug.Log(u + " " + v + " " + temp.Count);
    //     return temp.ToArray();
    // }

    void SetUpSeaCam()
    {
        Cam.Io.Camera.transform.SetPositionAndRotation(Ship.Parent.transform.parent.position, Ship.Parent.transform.rotation);
        Cam.Io.Camera.transform.Translate(Vector3.up - (Cam.Io.Camera.transform.forward * 2));
        Cam.Io.Camera.transform.LookAt(Ship.GO.transform, Vector3.up);
        Cam.Io.Camera.transform.Rotate(new Vector3(-10, 0, 0));

        Light l = new GameObject(nameof(Light)).AddComponent<Light>();
        l.transform.SetParent(TheSea.transform);
        l.type = LightType.Directional;
        l.color = new Color(.9f, .8f, .65f);
        l.shadows = LightShadows.None;
        l.transform.SetPositionAndRotation(Cam.Io.Camera.transform.position, Cam.Io.Camera.transform.rotation);
        l.transform.Rotate(new Vector3(45, 0, 0));
        l.intensity = 1.95f;
    }

    #endregion
}
