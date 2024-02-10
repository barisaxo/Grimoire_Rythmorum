using UnityEngine;

// public static class SeaCollisionSystems
// {
//     public static (Vector2 dir, int sign) ShipCollision(this Sea.NPCShip ship, Vector2 dir, int sign, Collision hit)
//     {
//         switch (ship.RotY)
//         {
//             case < 90:
//                 dir.x += ((sign == 1 && ship.GO.transform.position.x < hit.transform.position.x) ||
//                           (sign == -1 && ship.GO.transform.position.z < hit.transform.position.z)) ?
//                     sign * -1 : sign;
//                 break;

//             case < 180:
//                 dir.x += ((sign == 1 && ship.GO.transform.position.x > hit.transform.position.x) ||
//                           (sign == -1 && ship.GO.transform.position.z < hit.transform.position.z)) ?
//                     sign * -1 : sign;
//                 break;

//             case < 270:
//                 dir.x += ((sign == 1 && ship.GO.transform.position.x > hit.transform.position.x) ||
//                           (sign == -1 && ship.GO.transform.position.z > hit.transform.position.z)) ?
//                     sign * -1 : sign;
//                 break;

//             case < 361:
//                 dir.x += ((sign == 1 && ship.GO.transform.position.x < hit.transform.position.x) ||
//                           (sign == -1 && ship.GO.transform.position.z > hit.transform.position.z)) ?
//                     sign *= -1 : sign;
//                 break;
//         }

//         return (dir, sign);
//     }
// }

// public static int OffsetRegionCellIndex(this Scene scene, int x, int z) => new Vector2(
//         (x + ship.GlobalPos.x - scene.Board.Center()).Smod(scene.Map.RegionSize),
//         (z + ship.GlobalPos.y - scene.Board.Center()).Smod(scene.Map.RegionSize))
//     .Vec2ToInt(scene.Map.RegionSize);

// public static int NewMapRegionIndex(this Scene scene, Vector2 posDelta) =>
//    new Vector2Int((int)((posDelta + ship.GlobalPos).x.Smod(scene.Map.GlobalSize) / scene.Map.RegionSize),
//                   (int)((posDelta + ship.GlobalPos).y.Smod(scene.Map.GlobalSize) / scene.Map.RegionSize))
//     .Vec2ToInt(scene.Map.Size);

// public static int RegionCellIndex(this Scene scene, Vector2 deltaPos, Region region) =>
//     (deltaPos + ship.LocalCoord(region.Size)).Vec2ToInt(region.Size);

// public static int BoardIndex(this Scene scene, int x, int y) =>
//     new Vector2(x, y).Vec2ToInt(scene.Board.Size);

// public static bool IsInBounds(this Scene scene, int x, int y) =>
//     x + ship.GlobalCoord.x - scene.Board.Center() > -1 &&
//     x + ship.GlobalCoord.x - scene.Board.Center() < scene.Map.RegionSize &&
//     y + ship.GlobalCoord.y - scene.Board.Center() > -1 &&
//     y + ship.GlobalCoord.y - scene.Board.Center() < scene.Map.RegionSize;

// public static bool IsInBounds(this Scene scene, float x, float y) =>
//     x + ship.GlobalPos.x - scene.Board.Center() > -1 &&
//     x + ship.GlobalPos.x - scene.Board.Center() < scene.Map.RegionSize &&
//     y + ship.GlobalPos.y - scene.Board.Center() > -1 &&
//     y + ship.GlobalPos.y - scene.Board.Center() < scene.Map.RegionSize;


// private static bool NewCoordIsOpen(this Scene scene, Vector2 posDelta)
// {
//     Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(posDelta)];
//     Cell dCell = localRegion.Cells[scene.RegionCellIndex(posDelta, localRegion)];

//     return dCell.Type == CellType.OpenSea || dCell.Type == CellType.Center;
// }

// private static CellType NewCoordCellType(this Scene scene, Vector2 posDelta)
// {
//     Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(posDelta)];
//     Cell dCell = localRegion.Cells[scene.RegionCellIndex(posDelta, localRegion)];

//     return dCell.Type;
// }

// private static Cell NewCoordCell(this Scene scene, Vector2 posDelta)
// {
//     Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(posDelta)];
//     Cell dCell = localRegion.Cells[scene.RegionCellIndex(posDelta, localRegion)];

//     return dCell;
// }

// private static CellType NewCoordYCellType(this Scene scene, Vector2 posDelta)
// {
//     Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(new Vector2(0, posDelta.y))];
//     Cell dCell = localRegion.Cells[scene.RegionCellIndex(new Vector2(0, posDelta.y), localRegion)];
//     return dCell.Type;
// }

// private static CellType NewCoordXCellType(this Scene scene, Vector2 posDelta)
// {
//     Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(new Vector2(posDelta.x, 0))];
//     Cell dCell = localRegion.Cells[scene.RegionCellIndex(new Vector2(posDelta.x, 0), localRegion)];
//     return dCell.Type;
// }

// private static bool NewCoordYIsOpen(this Scene scene, Vector2 posDelta)
// {
//     Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(new Vector2(0, posDelta.y))];
//     Cell dCell = localRegion.Cells[scene.RegionCellIndex(new Vector2(0, posDelta.y), localRegion)];
//     return dCell.Type == CellType.OpenSea || dCell.Type == CellType.Center;
// }

// private static bool NewCoordXIsOpen(this Scene scene, Vector2 posDelta)
// {
//     Region localRegion = scene.Map.Regions[scene.NewMapRegionIndex(new Vector2(posDelta.x, 0))];
//     Cell dCell = localRegion.Cells[scene.RegionCellIndex(new Vector2(posDelta.x, 0), localRegion)];
//     return dCell.Type == CellType.OpenSea || dCell.Type == CellType.Center;
// }


// private static bool IsBorder(int x, int y, Scene scene, Vector2Int offsetGlobalCoord, Cell cell, Region[] localRegions)
// {
//     if (x == -1 || y == -1 || x == scene.Board.Size || y == scene.Board.Size)
//     {
//         DeactivateCellBorderObject(scene, cell);
//         foreach (Region region in localRegions)
//             foreach (NPCShip npc in region.NPCs)
//                 if (DeactivateNPCBorderObject(scene, npc, offsetGlobalCoord))
//                     return true;

//         return true;
//     }
//     return false;
// }
