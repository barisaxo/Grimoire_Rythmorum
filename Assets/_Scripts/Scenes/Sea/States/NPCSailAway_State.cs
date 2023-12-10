using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSailAway_State : State
{
    public NPCSailAway_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    NPCShip NPC => SeaScene.Io.NearNPCShip;

    protected override void EngageState()
    {
        int i = NPC.PatrolIndex + (NPC.PatrolPath.Length / 2);
        int n = 0;
        while (IsOnGridOrOccupied(NPC.PatrolPath[i]))
        {
            i = (i + 1) % NPC.PatrolPath.Length;
            if (++n == NPC.PatrolPath.Length) { i = NPC.PatrolIndex + (NPC.PatrolPath.Length / 2); break; }
        }

        SailAway().StartCoroutine();

        IEnumerator SailAway()
        {
            while (Vector3.Distance(NPC.GO.transform.position, SeaScene.Io.Ship.GO.transform.position) < SeaScene.Io.BoardOffset + 1)
            {
                Vector3 posDelta = Time.deltaTime * 4 * NPC.GO.transform.forward;
                NPC.GO.transform.position += posDelta;
                yield return null;
            }

            SeaScene.Io.UnusedShips.Add(NPC.GO);
            SeaScene.Io.UsedShips.Remove(NPC.GO);
            SeaScene.Io.RockTheBoat.RemoveBoat(NPC.GO.transform);

            NPC.GO.SetActive(false);
            NPC.GO = null;
            NPC.PatrolIndex = i;
            NPC.Pos = NPC.PatrolPath[NPC.PatrolIndex];

            SetStateDirectly(SubsequentState);
        }
    }

    bool IsOnGridOrOccupied(Vector3Int v3)
    {
        if (v3.x.IsPOM(SeaScene.Io.BoardOffset + 1, SeaScene.Io.Ship.Coord.x) ||
            v3.z.IsPOM(SeaScene.Io.BoardOffset + 1, SeaScene.Io.Ship.Coord.z))
            return true;

        foreach (var npc in SeaScene.Io.NPCShips) if (npc.Coords == v3) return true;

        return false;
    }

}
