using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

public class NMESailApproach_State : State
{
    public NMESailApproach_State(NPCShip npcShip) => NMEShip = npcShip;
    readonly NPCShip NMEShip;

    protected override void PrepareState(Action callback)
    {
        WorldMapScene.Io.HUD.Disable();
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        NMEShip.SceneObject.GO.transform.LookAt(WorldMapScene.Io.Ship.GO.transform);

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SailToward().StartCoroutine();

        IEnumerator SailToward()
        {
            while (Vector3.Distance(NMEShip.SceneObject.GO.transform.position, WorldMapScene.Io.Ship.GO.transform.position) > .75f)
            {
                yield return null;

                Vector3 posDelta = Time.deltaTime * 4 * NMEShip.SceneObject.GO.transform.forward;

                NMEShip.SceneObject.GO.transform.position += posDelta;
            }

            SetStateDirectly(new DialogStart_State(new PirateEncounter_Dialogue()));
        }
    }
}
