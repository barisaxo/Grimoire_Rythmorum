using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAetherScene_State : State
{
    bool ready = true;

    protected override void PrepareState(Action callback)
    {
        Audio.BGMusic.PlayClip(Assets.BGMus2);

        Cam.Io.Camera.transform.position = AetherScene.Io.Player.GO.transform.position;
        Cam.Io.Camera.transform.SetParent(AetherScene.Io.Player.GO.transform);
        Cam.Io.Camera.transform.Translate(new Vector3(0, 8, 5));
        Cam.Io.Camera.transform.Rotate(new Vector3(-35, 0, 0));

        PanCamera().StartCoroutine();
        IEnumerator PanCamera()
        {
            while (ready)
            {
                AetherScene.Io.Player.MoveCamera(-.333f);
                yield return null;
            }
            SetStateDirectly(new AetherScene_State());
        }
        base.PrepareState(callback);
    }

    protected override void DisengageState()
    {
        RStick.y = 0.01f;
    }
    protected override void LStickInput(Vector2 v2)
    {
        if (v2 == Vector2.zero) return;
        Ready();
    }
    protected override void RStickInput(Vector2 v2)
    {
        if (v2 == Vector2.zero) return;
        Ready();
    }
    protected override void GPInput(GamePadButton gpb)
    {
        Ready();
    }

    void Ready()
    {
        if (ready)
        {
            ready = false;
        }
    }
}
