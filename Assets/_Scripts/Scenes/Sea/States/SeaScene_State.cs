using System;
using System.Collections;
using UnityEngine;
using Sea;

public class SeaScene_State : State
{
    Scene Scene => Scene.Io;

    Vector2 ShipVelocity = Vector2.zero;
    public CameraFollow CameraFollow;

    bool up, down, left, right;
    float TimeSinceLastL;

    protected override void PrepareState(Action callback)
    {
        _ = Scene;
        // return;
        Scene.HUD.Hud.GO.SetActive(true);
        Scene.HUD.Show();
        CameraFollow = new(Scene.Ship.Parent.transform)
        {
            LockYPos = true,
            LockXRot = true,
            LockWRot = true,
            LockZRot = true,
        };

        Scene.RockTheBoat.Rocking = true;

        Scene.Swells.EnableSwells();

        if (Audio.BGMusic.AudioSources[0].clip == Assets.BGMus4)
            Audio.BGMusic.Resume();
        else Audio.BGMusic.PlayClip(Assets.BGMus4);

        if (Audio.Ambience.AudioSources[0].clip == Assets.SailAmbience)
            Audio.Ambience.Resume();
        else
        {
            Audio.Ambience.AudioSources[0].time = UnityEngine.Random.Range(0, Assets.SailAmbience.length);
            Audio.Ambience.PlayClip(Assets.SailAmbience);
        }

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

        Scene.HUD.Hud.GO.SetActive(true);

        _ = Scene.UpdateMap(ShipVelocity);
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        MonoHelper.OnUpdate += Tick;
        MonoHelper.OnFixedUpdate += FixedTick;
    }

    protected override void DisengageState()
    {
        Scene.HUD.Hide();
        RStick.y = .01f;

        Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles;
        Cam.StoredCamPos = Cam.Io.Camera.transform.position;
        CameraFollow.SelfDestruct();

        MonoHelper.OnUpdate -= Tick;
        MonoHelper.OnFixedUpdate -= FixedTick;
    }

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
        if (up) ShipVelocity.y = Mathf.Lerp(ShipVelocity.y, .8f, Time.fixedDeltaTime);
        else if (down) ShipVelocity.y = Mathf.Lerp(ShipVelocity.y, -.3f, Time.fixedDeltaTime);
        else ShipVelocity.y = Mathf.Lerp(ShipVelocity.y, 0, Time.fixedDeltaTime);

        if (left) ShipVelocity.x = Mathf.Lerp(ShipVelocity.x, -1, Time.fixedDeltaTime);
        else if (right) ShipVelocity.x = Mathf.Lerp(ShipVelocity.x, 1, Time.fixedDeltaTime);
        else ShipVelocity.x = Mathf.Lerp(ShipVelocity.x, 0, Time.fixedDeltaTime);

        ShipVelocity = Scene.UpdateMap(ShipVelocity);

        // if (TimeSinceLastL < 1.5f) { Scene.HUD.Hide(); }//Debug.Log("Hide");
        // else { Scene.HUD.Show(); }//Debug.Log("Show");

        // TimeSinceLastL += Time.deltaTime;
    }


    protected override void EastPressed()
    {
        if (Scene.NearestNPC == null) return;
        Audio.SFX.PlayOneShot(Scene.NearestNPC.RegionalSound);
        SetStateDirectly(new SeaToDialogueTransition_State(new HailShip_Dialogue(
            new Speaker(
                Scene.NearestNPC.Flag,
                Scene.NearestNPC.Name,
                Scene.NearestNPC.FlagColor)
        )));
    }

    protected override void StartPressed()
    {
        Debug.Log("Start");
        SetStateDirectly(new CameraPan_State(
             new SeaToMenuTransition_State(),
                 pan: new Vector3(
                     -50,
                     Cam.Io.Camera.transform.rotation.eulerAngles.y,
                     Cam.Io.Camera.transform.rotation.eulerAngles.z),
                 strafe: Cam.Io.Camera.transform.position,
                 speed: 3));
    }

    protected override void SelectPressed()
    {
        SetStateDirectly(new SeaToQuitMenuTransition_State());
    }

    protected override void LStickInput(Vector2 v2)
    {
        ShipVelocity.y = Mathf.Clamp(ShipVelocity.y + (Time.deltaTime * v2.y * .8f), -.3f, .8f);
        ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * -v2.x * 3, -1f, 1f);
        if (v2 != Vector2.zero) { TimeSinceLastL = 0; }
    }

    protected override void RStickInput(Vector2 v2)
    {
        ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * -v2.x * 5f, -1f, 1f);
        CameraFollow.MoveCamera(v2.y);
    }

    protected override void WestPressed()
    {
        SetStateDirectly(
            new CameraPan_State(
                new SeaInspection_State(),
                new Vector3(45, 180, 0),
                new Vector3(5.5f, 5, 13),
                3.5f
            ));
    }

    void Tick()
    {
        CheckInput();
        ShipCoords();
        CheckNMETriggers();
    }

    void FixedTick()
    {
        Movement();
    }

    void ShipCoords()
    {
        Scene.HUD.DayCounterText.TextString =
            Scene.Ship.GlobalCoord.x.ToString()
            + " : " +
            Scene.Ship.GlobalCoord.y.ToString();
    }

    void CheckNMETriggers()
    {
        var localRegions = Scene.Map.AdjacentRegions(Scene.Ship);
        foreach (Region region in localRegions)
            foreach (NPCShip npc in region.NPCs)
                if (npc.ShipType == NPCShipType.Pirate && Vector2.Distance(npc.LocalCoords, Scene.Ship.GlobalCoord) < npc.ThreatRange)
                {
                    Scene.NearestNPC = npc;
                    SetStateDirectly(new NMESailApproach_State());
                    return;
                }
    }
}
