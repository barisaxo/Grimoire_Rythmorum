using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

public class MoveNPCOffScreen_State : State
{
    public MoveNPCOffScreen_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    NPCShip NPC => WorldMapScene.Io.NearestNPC;

    protected override void EngageState()
    {
        if (NPC is null) { SetState(SubsequentState); return; }


        int i = NPC.PatrolIndex + 1;
        int z = 0;

        while (IsOccupiedAndOnGrid(NPC.PatrolPath[i]))
        {
            i = (i + 1) % NPC.PatrolPath.Length;
            if (++z == NPC.PatrolPath.Length) { i = NPC.PatrolIndex + (NPC.PatrolPath.Length / 2); break; }
        }

        WorldMapScene.Io.NPCShips.Remove(NPC);
        WorldMapScene.Io.RockTheBoat.RemoveBoat(NPC.SceneObject.GO.transform);
        NPC.DestroySceneObject();
        NPC.PatrolIndex = i;
        NPC.Pos = NPC.PatrolPath[NPC.PatrolIndex];

        SetState(SubsequentState);
    }

    bool IsOccupiedAndOnGrid(Vector2Int v2)
    {
        if (v2.x.IsPOM(WorldMapScene.Io.Board.Center() + 1, WorldMapScene.Io.Ship.GlobalCoord.x) ||
            v2.y.IsPOM(WorldMapScene.Io.Board.Center() + 1, WorldMapScene.Io.Ship.GlobalCoord.y))
            return true;

        var localRegions = WorldMapScene.Io.Map.RegionsAdjacentTo(WorldMapScene.Io.Ship);
        foreach (Region region in localRegions)
            foreach (NPCShip npc in region.NPCs) if (npc.LocalCoords == v2) return true;

        return false;
    }

}
