using UnityEngine;

namespace Sea
{
    public interface IUpdatePosition
    {
        public Vector3 NewPosition(WorldMapScene scene, Region localRegion, Vector2 cellCoord);
    }
    public class UpdateRockPosition : IUpdatePosition
    {
        public Vector3 NewPosition(WorldMapScene scene, Region localRegion, Vector2 cellCoord)
        {
            Vector3 v3 = new(
             (scene.Board.Center() + .3f + (((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalLoc.x)).Smod(scene.Map.GlobalSize),
             -.5f,
             (scene.Board.Center() + .3f + (((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalLoc.y)).Smod(scene.Map.GlobalSize));

            // Debug.Log(localRegion.Coord + " " + cellCoord + " " + ((localRegion.Coord * localRegion.Size) + cellCoord) + " " + scene.Ship.GlobalLoc + " " + v3);
            return v3;
        }
    }

    public class UpdateFishPosition : IUpdatePosition
    {
        public Vector3 NewPosition(WorldMapScene scene, Region localRegion, Vector2 cellCoord) => new(
            (scene.Board.Center() + .3f + (((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalLoc.x)).Smod(scene.Map.GlobalSize),
            -.35f,
            (scene.Board.Center() + .3f + (((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalLoc.y)).Smod(scene.Map.GlobalSize));
    }

    public class UpdateCovePosition : IUpdatePosition
    {
        public Vector3 NewPosition(WorldMapScene scene, Region localRegion, Vector2 cellCoord) => new(
            (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalLoc.x),
            .05f,
            (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalLoc.y));
    }

    public class UpdateLighthousePosition : IUpdatePosition
    {
        public Vector3 NewPosition(WorldMapScene scene, Region localRegion, Vector2 cellCoord)
        {
            return new(
            (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalLoc.x),
            0f,
            (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalLoc.y));
        }
    }

    public class UpdateNPCShipPosition : IUpdatePosition
    {
        public Vector3 NewPosition(WorldMapScene scene, Region localRegion, Vector2 npcGlobalPos) => new(
                    (scene.Board.Center() + .3f + npcGlobalPos.x - scene.Ship.GlobalLoc.x).Smod(scene.Map.GlobalSize),
                    0,
                    (scene.Board.Center() + .3f + npcGlobalPos.y - scene.Ship.GlobalLoc.y).Smod(scene.Map.GlobalSize));
    }

    public class UpdateBorderPosition : IUpdatePosition
    {
        public Vector3 NewPosition(WorldMapScene scene, Region localRegion, Vector2 cellCoord)
        {
            return new(
           (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).x - scene.Ship.GlobalLoc.x),
           0f,
           (scene.Board.Center() + .3f + ((localRegion.Coord * localRegion.Size) + cellCoord).y - scene.Ship.GlobalLoc.y));

        }
    }
}
