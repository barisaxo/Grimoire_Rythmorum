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

        while (!Cam.Io.Camera.transform.rotation.eulerAngles.x.IsPOM(1, Pan.x) ||
               !Cam.Io.Camera.transform.rotation.eulerAngles.y.IsPOM(1, Pan.y) ||
               !Cam.Io.Camera.transform.rotation.eulerAngles.z.IsPOM(1, Pan.z) ||
               !Cam.Io.Camera.transform.position.x.IsPOM(1, Strafe.x) ||
               !Cam.Io.Camera.transform.position.y.IsPOM(1, Strafe.y) ||
               !Cam.Io.Camera.transform.position.z.IsPOM(1, Strafe.z))
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
