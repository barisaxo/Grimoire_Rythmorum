using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SeaScene_State : State
{
    protected override void PrepareState(Action callback)
    {
        _ = SeaScene;

        MonoHelper.OnUpdate += CheckInput;
        MonoHelper.OnUpdate += FPS;
        MonoHelper.OnUpdate += CheckNMETriggers;
        MonoHelper.OnFixedUpdate += Movement;

        CameraFollow = new(SeaScene.Ship.Parent.transform)
        {
            LockYPos = true,
            LockXRot = true,
            LockWRot = true,
            LockZRot = true,
        };

        SeaScene.RockTheBoat.Rocking = true;

        SeaScene.Swells.EnableSwells();

        if (Audio.BGMusic.AudioSources[0].clip == Assets.BGMus4)
            Audio.BGMusic.Resume();
        else Audio.BGMusic.PlayClip(Assets.BGMus4);

        Audio.Ambience.PlayClip(Assets.SailAmbience);

        PanCamera().StartCoroutine();
        IEnumerator PanCamera()
        {
            var v3 = new Vector3(0, .333f);
            while (RStick.y == 0)
            {
                RStickInput(v3);
                yield return null;
            }
        }

        base.PrepareState(callback);
    }
    protected override void EngageState()
    {

    }

    protected override void DisengageState()
    {
        RStick.y = .01f;

        Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles;
        Cam.StoredCamPos = Cam.Io.Camera.transform.position;
        CameraFollow.SelfDestruct();

        MonoHelper.OnUpdate -= CheckInput;
        MonoHelper.OnUpdate -= FPS;
        MonoHelper.OnUpdate -= CheckNMETriggers;
        MonoHelper.OnFixedUpdate -= Movement;

        // SeaScene.RockTheBoat.Rocking = false;
        // SeaScene.Swells.DisableSwells();

        // Audio.Ambience.Stop();
        // Audio.BGMusic.Pause();

        // SeaScene.Io.Ship.GO.transform.rotation = Quaternion.Euler(
        //     new Vector3(
        //         SeaScene.Io.Ship.GO.transform.rotation.eulerAngles.x,
        //         SeaScene.Io.Ship.GO.transform.rotation.eulerAngles.y,
        //         0));
    }

    SeaScene SeaScene => SeaScene.Io;
    Vector3 ShipVelocity = Vector3.zero;
    public CameraFollow CameraFollow;

    bool up, down, left, right;

    protected override void DirectionPressed(Dir dir)
    {
        switch (dir)
        {
            case Dir.Up: up = true; break;
            case Dir.Up_Off: up = false; break;
            case Dir.Down: down = true; break;
            case Dir.Down_Off: down = false; break;
            case Dir.Left: left = true; break;
            case Dir.Left_Off: left = false; break;
            case Dir.Right: right = true; break;
            case Dir.Right_Off: right = false; break;
        }

    }

    protected void CheckInput()
    {
        if (UnityEngine.InputSystem.Keyboard.current.wKey.wasPressedThisFrame) DirectionPressed(Dir.Up);
        else if (UnityEngine.InputSystem.Keyboard.current.wKey.wasReleasedThisFrame) DirectionPressed(Dir.Up_Off);

        if (UnityEngine.InputSystem.Keyboard.current.sKey.wasPressedThisFrame) DirectionPressed(Dir.Down);
        else if (UnityEngine.InputSystem.Keyboard.current.sKey.wasReleasedThisFrame) DirectionPressed(Dir.Down_Off);

        if (UnityEngine.InputSystem.Keyboard.current.aKey.wasPressedThisFrame) DirectionPressed(Dir.Left);
        else if (UnityEngine.InputSystem.Keyboard.current.aKey.wasReleasedThisFrame) DirectionPressed(Dir.Left_Off);

        if (UnityEngine.InputSystem.Keyboard.current.dKey.wasPressedThisFrame) DirectionPressed(Dir.Right);
        else if (UnityEngine.InputSystem.Keyboard.current.dKey.wasReleasedThisFrame) DirectionPressed(Dir.Right_Off);
    }

    void Movement()
    {
        if (up) ShipVelocity.z = Mathf.Clamp(ShipVelocity.z + (Time.deltaTime * .8f), -.3f, .8f);
        else if (down) ShipVelocity.z = Mathf.Clamp(ShipVelocity.z - Time.deltaTime, -.3f, .8f);
        else ShipVelocity.z *= .99f;

        if (left) ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * 2, -1f, 1);
        else if (right) ShipVelocity.x = Mathf.Clamp(ShipVelocity.x + Time.deltaTime * 2, -1f, 1);
        else ShipVelocity.x *= .9f;

        ShipVelocity = SeaScene.UpdateMap(ShipVelocity);
    }


    protected override void InteractPressed()
    {
        if (SeaScene.NearNPCShip == null) return;
        Audio.SFX.PlayOneShot(SeaScene.Io.NearNPCShip.RegionalSound);
        SetStateDirectly(new SeaToDialogueTransition_State(new HailShip_Dialogue()));
    }

    protected override void StartPressed()
    {
        SetStateDirectly(new SeaToMenuTransition_State());
    }

    protected override void SelectPressed()
    {
        SetStateDirectly(new SeaToQuitMenuTransition_State());
    }

    protected override void LStickInput(Vector2 v2)
    {
        ShipVelocity.z = Mathf.Clamp(ShipVelocity.z + (Time.deltaTime * v2.y * .8f), -.3f, .8f);
        ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * -v2.x * 3, -1f, 1f);
    }

    protected override void RStickInput(Vector2 v2)
    {
        ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * -v2.x * 5f, -1f, 1f);
        CameraFollow.MoveCamera(v2.y);
    }


    int frames = 0;
    float elapsed = 0;
    const float interval = 1;
    float timer = 0;

    void FPS()
    {
        frames++;
        elapsed += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer -= interval;
            SeaScene.DayCounterText.TextString = ((int)(frames / elapsed)).ToString();
            frames = 0;
            elapsed = 0;
        }
    }

    void CheckNMETriggers()
    {
        foreach (NPCShip npc in SeaScene.Io.NPCShips)
            if (npc.ShipType == NPCShipType.Pirate && Vector3.Distance(npc.Coords, SeaScene.Io.Ship.Coord) < npc.ThreatRange)
            {
                SeaScene.Io.NearNPCShip = npc;
                SetStateDirectly(new NMESailApproach_State(
                    new SeaToDialogueTransition_State(
                        new PirateEncounter_Dialogue())));
                return;
            }
    }
}
