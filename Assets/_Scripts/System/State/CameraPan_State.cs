using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan_State : State
{
    Vector3 Pan;
    Vector3 Strafe;
    readonly float Speed;
    readonly State SubsequentState;

    public CameraPan_State(State subsequentState, Vector3 pan, Vector3 strafe, float speed)
    {
        SubsequentState = subsequentState;
        Pan = pan;
        Strafe = strafe;
        Speed = speed;
    }

    protected override void EngageState()
    {
        InitiatePan().StartCoroutine();
    }

    IEnumerator InitiatePan()
    {
        Debug.Log(nameof(InitiatePan));
        DisableInput();
        DisengageState();
        yield return null;
        PanCamera().StartCoroutine();
    }

    IEnumerator PanCamera()
    {
        Pan = new Vector3(Pan.x.Smod(360), Pan.y.Smod(360), Pan.z.Smod(360));

        while (!Cam.Io.Camera.transform.rotation.eulerAngles.IsPOM(1, Pan) ||
               !Cam.Io.Camera.transform.position.IsPOM(1, Strafe))
        {
            Cam.Io.Camera.transform.SetPositionAndRotation(
                Vector3.Lerp(Cam.Io.Camera.transform.position, Strafe, Time.deltaTime * Speed),
                Quaternion.Slerp(Cam.Io.Camera.transform.rotation, Quaternion.Euler(Pan), Time.deltaTime * Speed));
            yield return null;
        }

        Cam.Io.Camera.transform.SetPositionAndRotation(Strafe, Quaternion.Euler(Pan));
        SetState(SubsequentState);
    }
}
