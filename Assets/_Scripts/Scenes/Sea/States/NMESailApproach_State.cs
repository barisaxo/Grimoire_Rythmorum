using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;

public class NMESailApproach_State : State
{
    NPCShip NMEShip;

    protected override void PrepareState(Action callback)
    {
        Audio.BGMusic.Pause();
        Audio.Ambience.Pause();

        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        NMEShip = Scene.Io.NearestNPC;
        NMEShip.GO.transform.LookAt(Scene.Io.Ship.GO.transform);

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SailToward().StartCoroutine();

        IEnumerator SailToward()
        {
            while (Vector3.Distance(NMEShip.GO.transform.position, Scene.Io.Ship.GO.transform.position) > .75f)
            {
                yield return null;

                Vector3 posDelta = Time.deltaTime * 4 * NMEShip.GO.transform.forward;

                NMEShip.GO.transform.position += posDelta;
            }

            SetStateDirectly(new DialogStart_State(new PirateEncounter_Dialogue()));
        }
    }
}
