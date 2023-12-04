using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNPCOffScreen_State : State
{
    public MoveNPCOffScreen_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    NPCShip NPC => SeaScene.Io.NearNPCShip;

    protected override void EngageState()
    {
        int i = NPC.PatrolIndex + 1;
        int z = 0;

        while (IsOccupiedAndOnGrid(NPC.PatrolPath[i]))
        {
            i = (i + 1) % NPC.PatrolPath.Length;
            if (++z == NPC.PatrolPath.Length) { i = NPC.PatrolIndex + (NPC.PatrolPath.Length / 2); break; }
        }

        SeaScene.Io.UnusedShips.Add(NPC.GO);
        NPC.GO.SetActive(false);
        NPC.GO = null;
        NPC.PatrolIndex = i;
        NPC.Pos = NPC.PatrolPath[NPC.PatrolIndex];

        SetStateDirectly(SubsequentState);
    }

    bool IsOccupiedAndOnGrid(Vector3Int v3)
    {
        if (v3.x.IsPOM(SeaScene.Io.BoardOffset + 1, SeaScene.Io.Ship.Coord.x) ||
            v3.z.IsPOM(SeaScene.Io.BoardOffset + 1, SeaScene.Io.Ship.Coord.z))
            return true;

        foreach (var npc in SeaScene.Io.NPCShips) if (npc.Coords == v3) return true;

        return false;
    }

}
