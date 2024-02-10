using UnityEngine;

namespace Sea
{

    // (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + shipCoord).x - scene.Ship.GlobalLoc.x),
    // 0f,
    // (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + shipCoord).y - scene.Ship.GlobalLoc.y));

    // (scene.Board.Center() + .3f + cellCoord.x - scene.Ship.GlobalLoc.x),
    // 0,
    // (scene.Board.Center() + .3f + cellCoord.y - scene.Ship.GlobalLoc.y));
    // new Vector3(
    //     (scene.Board.Center() + .3f + cellCoord.x - scene.Ship.GlobalPos.x).Smod(scene.Map.GlobalSize),
    //     0,
    //     (scene.Board.Center() + .3f + cellCoord.y - scene.Ship.GlobalPos.y).Smod(scene.Map.GlobalSize));
}

// Region localRegion = scene.RegionFromOffsetGlobalCoord(offsetGlobalCoord);

// switch (cell.Type)
// {
//     case CellType.Rocks:
//         Vector3 rockPos = new(
//             (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalPos.x),
//             -.5f,
//             (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalPos.y));

//         cell.GO.transform.SetPositionAndRotation(rockPos,
//             Quaternion.Euler(new Vector3(0, cell.RotY, 0)));
//         break;

//     case CellType.Fish:
//         Vector3 fishPos = new(
//             (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalPos.x),
//             -.35f,
//             (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalPos.y));

//         cell.GO.transform.SetPositionAndRotation(fishPos,
//             Quaternion.Euler(new Vector3(0, cell.RotY, 0)));

//         break;
//     case CellType.Lighthouse:

//         Vector3 lhPos = new(
//             (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalPos.x),
//             -.35f,
//             (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalPos.y));

//         cell.GO.transform.SetPositionAndRotation(lhPos,
//             Quaternion.Euler(new Vector3(0, cell.RotY, 0)));
//         break;

//     case CellType.Cave:

//         Vector3 cavePos = new(
//             (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalPos.x),
//             .5f,
//             (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalPos.y));

//         cell.GO.transform.SetPositionAndRotation(cavePos,
//             Quaternion.Euler(new Vector3(0, cell.RotY, 0)));
//         break;
// }