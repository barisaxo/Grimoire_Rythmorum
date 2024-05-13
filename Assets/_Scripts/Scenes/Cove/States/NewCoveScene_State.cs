using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCoveScene_State : State
{
    public NewCoveScene_State() { Fade = true; }

    bool ready = true;

    protected override void PrepareState(Action callback)
    {
        Audio.BGMusic.PlayClip(Assets.BGMus2);

        Data.Two.Manager.Io.PlayerShip.SetLevel(new Data.Two.MaxHitPoints(),
            Data.Two.Manager.Io.PlayerShip.ShipStats.HullStrength);

        Data.Two.Manager.Io.PlayerShip.SetLevel(new Data.Two.CurrentHitPoints(),
            Data.Two.Manager.Io.PlayerShip.ShipStats.HullStrength);

        Cam.Io.Camera.transform.SetPositionAndRotation(
            CoveScene.Io.Player.GO.transform.position,
            CoveScene.Io.Player.GO.transform.rotation);
        Cam.Io.Camera.transform.SetParent(CoveScene.Io.Player.GO.transform);
        Cam.Io.Camera.transform.Translate(new Vector3(0, 8, 5));
        Cam.Io.Camera.transform.Rotate(new Vector3(-35, 0, 0));

        PanCamera().StartCoroutine();
        IEnumerator PanCamera()
        {
            while (ready)
            {
                CoveScene.Io.Player.MoveCamera(-.333f);
                yield return null;
            }
            SetState(new CoveScene_State());
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
        ready = false;
    }
}
