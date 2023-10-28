//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SeaMap
//{
//    //private SeaMap() { }

//    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//    //static void AutoInit()
//    //{
//    //    Rewards.FoundIslandEvent += AssignCaveTile;
//    //    Ship_Movement.SailingStart += ShowTileTypesOffset;
//    //}

//    //internal static void MakeANewMap(SaveSlot _)
//    //{
//    //    MakeNewMap();
//    //}

//    public static void ClearCardShip(this SeaScene sea)
//    {
//        MapTile().Type = SeaMapTileType.OpenSea;
//        sea.NumOfCardShips -= sea.NumOfCardShips - 1 == 0 ? 0 : 1;
//        ShowTileTypes();
//        Debug.Log("num of ships " + sea.NumOfCardShips);

//        SeaMapTile MapTile() => GetMapTileFromSeaTile(
//            sea.SeaGrid[SeaTileToInteractWith()],
//            sea.SeaGridCenter,
//            sea.MapLoc,
//            sea.MapSize,
//            sea.MapGrid);

//        static SeaMapTile GetMapTileFromSeaTile(
//        SeaTile t,
//        Vector3 center,
//        Vector3 shipMapLoc,
//        int mapSize,
//        List<SeaMapTile> mapGrid)
//        {
//            return mapGrid[Index()];

//            Vector3 SeaLoc() => t.Loc - center;
//            Vector3 MapLoc() => shipMapLoc + SeaLoc();
//            int Index() => Helper.Vec3ToInt(MapLoc(), mapSize);
//        }

//        int SeaTileToInteractWith() => Overlay_Manager.cursorLocaction.Vec3ToInt(sea.SeaGridSize);
//    }

//    public static void ShowTileTypesOffset(this SeaScene sea) => ShowTileTypes(sea.SeaLoc - sea.SeaGridCenter);

//    private static void ShowTileTypes() => ShowTileTypes(Vector3.zero);

//    private static void ShowTileTypes(Vector3 offset)
//    {
//        for (int x = 0; x < Entities.Io.SeaGridSize; x++)
//        {
//            for (int z = 0; z < Entities.Io.SeaGridSize; z++)
//            {
//                Entities.Io.SeaGrid[SeaIndex(x, z)].GameShip.SetActive(false);
//                Entities.Io.SeaGrid[SeaIndex(x, z)].TradeShip.SetActive(false);
//                Entities.Io.SeaGrid[SeaIndex(x, z)].Rocks.SetActive(false);
//                Entities.Io.SeaGrid[SeaIndex(x, z)].Island.SetActive(false);

//                switch (TypeOfMapTile(x, z))
//                {
//                    case MapTileType.OpenSea:
//                        continue;

//                    case MapTileType.Rocks:
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].Rocks.SetActive(true);
//                        continue;

//                    case MapTileType.Trader:
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].TradeShip.SetActive(true);
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].TradeShipSway.ShipRotY =
//                            Entities.Io.MapGrid[MapIndex(x, z)].ShipRotY;
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].TradeShipSway.SwayAmp =
//                            Entities.Io.MapGrid[MapIndex(x, z)].SwayAmp;
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].TradeShipSway.SwayPeriod =
//                            Entities.Io.MapGrid[MapIndex(x, z)].SwayPeriod;
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].TradeShipSway.SetNewSwayPos();

//                        continue;

//                    case MapTileType.Cave:
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].Island.SetActive(true);
//                        continue;

//                    case MapTileType.CardGame:
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].GameShip.SetActive(true);
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].GameShipSway.ShipRotY =
//                           Entities.Io.MapGrid[MapIndex(x, z)].ShipRotY;
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].GameShipSway.SwayAmp =
//                            Entities.Io.MapGrid[MapIndex(x, z)].SwayAmp;
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].GameShipSway.SwayPeriod =
//                             Entities.Io.MapGrid[MapIndex(x, z)].SwayPeriod;
//                        Entities.Io.SeaGrid[SeaIndex(x, z)].GameShipSway.SetNewSwayPos();
//                        continue;
//                }
//            }
//        }
//        #region INTERNAL
//        MapTileType TypeOfMapTile(int x, int z) => IsInBounds(x, z) ?
//                    Entities.Io.MapGrid[MapIndex(x, z)].Type : MapTileType.Rocks;

//        Vector3 OffsetMapLoc() => Entities.Io.MapLoc - offset;

//        int MapIndex(int x, int z) => Helper.Vec2ToInt(
//                      x + (int)OffsetMapLoc().x - HalfBoard(),
//                      z + (int)OffsetMapLoc().z - HalfBoard(),
//                      Entities.Io.MapSize);

//        int SeaIndex(int x, int z) => Helper.Vec2ToInt(x, z, Entities.Io.SeaGridSize);

//        int HalfBoard() => (int)(Entities.Io.SeaGridSize * .5f);

//        bool IsInBounds(int x, int z) => MapIndex(x, z) < Entities.Io.MapGrid.Count - 1 && MapIndex(x, z) > -1;
//        #endregion
//    }

//    internal static void MakeNewMap()
//    {
//        Entities.Io.NumOfCardShips = 0;
//        AssignTileTypes();
//        ClearCenter();
//        PlaceShip();
//        Entities.Io.SeaColor = ColorByLevel();
//        ShowTileTypes();
//        Entities.Io.StartingShips = Entities.Io.NumOfCardShips;

//        #region INTERNAL
//        void AssignTileTypes()
//        {
//            foreach (MapTile t in Entities.Io.MapGrid)
//            {
//                t.Type = IsBorder() == true ? AssignRockBorder() : AssignRandomType();
//                Entities.Io.NumOfCardShips += t.Type == MapTileType.CardGame ? 1 : 0;
//                continue;

//                bool IsBorder()
//                {
//                    return t.Loc.x == 0 ||
//                           t.Loc.z == 0 ||
//                           t.Loc.x == Entities.Io.MapSize - 1 ||
//                           t.Loc.z == Entities.Io.MapSize - 1;
//                }
//                MapTileType AssignRockBorder() { return MapTileType.Rocks; }
//                MapTileType AssignRandomType()
//                {
//                    return UnityEngine.Random.value switch
//                    {
//                        float r when r < .015f => MapTileType.Trader,
//                        float r when r < .05f => MapTileType.CardGame,
//                        float r when r < .09f => MapTileType.Rocks,
//                        _ => MapTileType.OpenSea
//                    };
//                }
//            }
//        }

//        void ClearCenter()
//        {
//            for (int x = -1; x < 2; x++)
//            {
//                for (int z = -1; z < 2; z++)
//                {
//                    Entities.Io.MapGrid[IndexOf(x, z)].Type = MapTileType.OpenSea;
//                }
//            }
//            return;

//            int HalfBoard() { return (int)(Entities.Io.MapSize * .5f); }
//            int IndexOf(int x, int z) { return new Vector3(x + HalfBoard(), 0, z + HalfBoard()).Vec3ToInt(Entities.Io.MapSize); }
//        }

//        void PlaceShip()
//        {
//            Entities.Io.Ship.transform.parent = Entities.Io.SeaGrid[(int)(Entities.Io.SeaGrid.Count * .5f)].GO.transform;
//            Ship_Movement.InitialShipLocation(Entities.Io.SeaGrid[(int)(Entities.Io.SeaGrid.Count * .5f)].Loc);
//            Ship_Movement.SetInitialMapLoc(new Vector3(
//                 (int)(Entities.Io.MapSize * .5f), 0,
//                 (int)(Entities.Io.MapSize * .5f)));
//            Entities.Io.Ship.transform.position = Entities.Io.SeaLoc + (Vector3.up * .6f);
//            Entities.Io.Ship.transform.localScale = Vector3.one * .4f;
//        }

//        Color ColorByLevel()
//        {
//            return Puzzle_Data.CurrentLevel switch
//            {
//                Level.c251 => Color.cyan,
//                Level.c145 => Color.blue,
//                Level.c1456 => Color.green,
//                Level.c3625 => Color.yellow,
//                Level.cAll => Color.magenta,
//                _ => Color.grey,
//            };
//        }
//        #endregion
//    }

//    private static void AssignCaveTile()
//    {
//        Entities.Io.IslandLoc = RandLoc();
//        Entities.Io.MapGrid[Entities.Io.IslandLoc.Vec2ToInt(Entities.Io.MapSize)].Type = MapTileType.Cave;
//        ClearAdjacentTiles();

//        #region INTERNAL
//        void ClearAdjacentTiles()
//        {
//            for (int x = -2; x < 3; x++)
//            {
//                for (int z = -2; z < 3; z++)
//                {
//                    if (x == 0 || z == 0) { continue; }
//                    Entities.Io.MapGrid[IndexOf(Entities.Io.IslandLoc, x, z)].Type = MapTileType.OpenSea;
//                }
//            }
//        }

//        int IndexOf(Vector2 i, int x, int z) { return (i + new Vector2(x, z)).Vec2ToInt(Entities.Io.MapSize); }
//        Vector2 RandLoc() { return new Vector2(randINTInBy4(), randINTInBy4()); }
//        int randINTInBy4() => Random.Range(4, Entities.Io.MapSize - 4);
//        #endregion
//    }

//}
