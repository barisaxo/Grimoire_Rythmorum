using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

public class NPCSailAway_State : State
{
    public NPCSailAway_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    NPCShip NPC => Scene.Io.NearestNPC;

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
            while (Vector3.Distance(NPC.GO.transform.position, Scene.Io.Ship.GO.transform.position) < Scene.Io.Board.Center() + 1)
            {
                Vector3 posDelta = Time.deltaTime * 4 * NPC.GO.transform.forward;
                NPC.GO.transform.position += posDelta;
                yield return null;
            }

            Scene.Io.UnusedShips.Add(NPC.GO);
            Scene.Io.UsedShips.Remove(NPC.GO);
            Scene.Io.RockTheBoat.RemoveBoat(NPC.GO.transform);

            NPC.GO.SetActive(false);
            NPC.GO = null;
            NPC.PatrolIndex = i;
            NPC.Pos = NPC.PatrolPath[NPC.PatrolIndex];

            SetStateDirectly(SubsequentState);
        }
    }

    bool IsOnGridOrOccupied(Vector2Int v2)
    {
        if (v2.x.IsPOM(Scene.Io.Board.Center() + 1, Scene.Io.Ship.GlobalCoord.x) ||
            v2.y.IsPOM(Scene.Io.Board.Center() + 1, Scene.Io.Ship.GlobalCoord.y))
            return true;

        var localRegions = Scene.Io.Map.AdjacentRegions(Scene.Io.Ship);
        foreach (Region region in localRegions)
            foreach (NPCShip npc in region.NPCs) if (npc.LocalCoords == v2) return true;

        return false;
    }

}
