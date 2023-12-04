using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMESailApproach_State : State
{
    public NMESailApproach_State(State subsequentState)
    {
        SubsequentState = subsequentState;
    }

    readonly State SubsequentState;
    NPCShip NMEShip;

    protected override void EngageState()
    {
        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        NMEShip = SeaScene.Io.NearNPCShip;
        NMEShip.GO.transform.LookAt(SeaScene.Io.Ship.GO.transform);
        SailToward().StartCoroutine();

        IEnumerator SailToward()
        {
            while (Vector3.Distance(NMEShip.GO.transform.position, SeaScene.Io.Ship.GO.transform.position) > .75f)
            {
                yield return null;

                Vector3 posDelta = Time.deltaTime * 4 * NMEShip.GO.transform.forward;

                NMEShip.GO.transform.position += posDelta;
            }

            SetStateDirectly(SubsequentState);
        }
    }
}
