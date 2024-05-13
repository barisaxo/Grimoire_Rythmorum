using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoveScene_State : State
{
    CoveScene Cove => CoveScene.Io;
    Vector2 lStick, rStick;

    protected override void PrepareState(Action callback)
    {
        MonoHelper.ToFixedUpdate += HandleInput;

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        Data.Two.Manager.Io.Lighthouse.Reset();

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
        Cove.Player.RotatePlayer(rStick.x + (lStick.x * .7f));
        Cove.Player.MovePlayer(new Vector2(-lStick.x * .7f, -lStick.y));
        Cove.Player.MoveCamera(rStick.y);

        CheckDistances();
    }

    private void CheckDistances()
    {
        bool bark = false;
        if (NearShip || NearPatterns) bark = true;

        if (bark) Cove.Player.Bark.SetTextString("...");
        else Cove.Player.Bark.SetTextString("");
    }

    protected override void EastPressed()
    {
        if (NearShip)
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearPatterns)
        {
            SetState(new CoveToMenuTransition_State(
                new Menus.Two.SkillsMenu(Data.Two.Manager.Io.Skill, Data.Two.Manager.Io.Player,
                    new CameraPan_State(
                    subsequentState: this,
                    pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    speed: 5))));
            return;
        }
    }

    protected override void SelectPressed()
    {
        // SetState(new CameraPan_State(
        //     new CoveToMenuTransition_State(new Menus.Options.OptionsMenu(Data, Audio, this) as Menus.IHeaderMenu),
        //         pan: new Vector3(
        //             -50,
        //             Cam.Io.Camera.transform.rotation.eulerAngles.y,
        //             Cam.Io.Camera.transform.rotation.eulerAngles.z),
        //         strafe: Cam.Io.Camera.transform.position,
        //         speed: 3));

        SetState(new CoveToMenuTransition_State(
            new Menus.Two.OptionsMenu(
                DataManager,
                Audio,
                new CameraPan_State(
                    subsequentState: this,
                    pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    speed: 5))));
    }

    bool NearShip => Dist(Cove.Player.GO, Cove.Ship) < 2.5f;
    bool NearPatterns => Dist(Cove.Player.GO, Cove.SkillSheet) < 2.5f;
    float Dist(GameObject a, GameObject b) => Vector2.Distance(
      new Vector2(a.transform.position.x, a.transform.position.z),
      new Vector2(b.transform.position.x, b.transform.position.z));
}