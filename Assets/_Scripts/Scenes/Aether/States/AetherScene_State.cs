using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherScene_State : State
{
    AetherScene Aether => AetherScene.Io;
    Vector2 lStick, rStick;

    protected override void PrepareState(Action callback)
    {
        MonoHelper.ToFixedUpdate += HandleInput;

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        if (!Audio.BGMusic.AudioSources[0].isPlaying)
            Audio.BGMusic.Resume();
    }

    protected override void DisengageState()
    {
        Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles;
        Cam.StoredCamPos = Cam.Io.Camera.transform.position;

        MonoHelper.ToFixedUpdate -= HandleInput;

        Audio.BGMusic.Pause();
    }

    protected override void LStickInput(Vector2 v2)
    {
        if (v2.y > .5f) v2.y = 1;
        else if (v2.y > 0) v2.y = .5f;
        lStick = v2;
    }

    protected override void RStickInput(Vector2 v2)
    {
        rStick = v2;
    }

    void HandleInput()
    {
        Aether.Player.RotatePlayer(rStick.x + (lStick.x * .7f));
        Aether.Player.MovePlayer(new Vector2(-lStick.x * .7f, -lStick.y));
        Aether.Player.MoveCamera(rStick.y);

        CheckDistances();
    }

    private void CheckDistances()
    {
        bool bark = false;
        if (NearShip) bark = true;

        if (bark) Aether.Player.Bark.SetTextString("...");
        else Aether.Player.Bark.SetTextString("");

        foreach (var x in Aether.Player.Bark.TMP.material.enabledKeywords)
            Debug.Log(x.name);
    }

    protected override void ConfirmPressed()
    {
        if (NearShip) { SetStateDirectly(new AetherToSeaTransition_State()); return; }
    }

    protected override void SelectPressed()
    {
        SetStateDirectly(new AetherToQuitMenuTransition_State());
    }

    bool NearShip => Dist(Aether.Player.GO, Aether.Ship) < 2.5f;
    float Dist(GameObject a, GameObject b) => Vector2.Distance(
      new Vector2(a.transform.position.x, a.transform.position.z),
      new Vector2(b.transform.position.x, b.transform.position.z));
}