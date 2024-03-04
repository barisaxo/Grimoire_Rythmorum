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
    NPCShip NPC => WorldMapScene.Io.NearestNPC;

    protected override void EngageState()
    {
        int i = (NPC.PatrolIndex + (NPC.PatrolPath.Length / 2)) % NPC.PatrolPath.Length;
        int n = 0;
        while (IsOnGridOrOccupied(NPC.PatrolPath[i]))
        {
            i = (i + 1) % NPC.PatrolPath.Length;
            if (++n == NPC.PatrolPath.Length)
            {
                i = (NPC.PatrolIndex + (NPC.PatrolPath.Length / 2)) % NPC.PatrolPath.Length;
                break;
            }
        }

        SailAway().StartCoroutine();

        IEnumerator SailAway()
        {
            while (Vector3.Distance(NPC.SceneObject.GO.transform.position, WorldMapScene.Io.Ship.GO.transform.position) < WorldMapScene.Io.Board.Center() + 1)
            {
                Vector3 posDelta = Time.deltaTime * 4 * NPC.SceneObject.GO.transform.forward;
                NPC.SceneObject.GO.transform.position += posDelta;
                yield return null;
            }

            WorldMapScene.Io.NPCShips.Remove(NPC);
            WorldMapScene.Io.RockTheBoat.RemoveBoat(NPC.SceneObject.GO.transform);

            NPC.DestroySceneObject();
            NPC.PatrolIndex = i;
            NPC.Pos = NPC.PatrolPath[NPC.PatrolIndex];

            SetState(SubsequentState);
        }
    }

    bool IsOnGridOrOccupied(Vector2Int v2)
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
