using System;
using System.Collections;
using UnityEngine;

public class SeaScene_State : State
{
    SeaScene SeaScene => SeaScene.Io;

    Vector3 ShipVelocity = Vector3.zero;
    public CameraFollow CameraFollow;

    bool up, down, left, right;
    float TimeSinceLastL;

    protected override void PrepareState(Action callback)
    {
        _ = SeaScene;
        // return;
        SeaScene.SeaHUD.HUD.GO.SetActive(true);
        SeaScene.SeaHUD.Show();
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

        SeaScene.Io.SeaHUD.HUD.GO.SetActive(true);
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        MonoHelper.OnUpdate += CheckInput;
        MonoHelper.OnUpdate += ShipCoords;
        MonoHelper.OnUpdate += CheckNMETriggers;
        MonoHelper.OnFixedUpdate += Movement;
    }

    protected override void DisengageState()
    {
        SeaScene.SeaHUD.Hide();
        RStick.y = .01f;

        Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles;
        Cam.StoredCamPos = Cam.Io.Camera.transform.position;
        CameraFollow.SelfDestruct();

        MonoHelper.OnUpdate -= CheckInput;
        MonoHelper.OnUpdate -= ShipCoords;
        MonoHelper.OnUpdate -= CheckNMETriggers;
        MonoHelper.OnFixedUpdate -= Movement;
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
        if (up) ShipVelocity.z = Mathf.Clamp(ShipVelocity.z + (Time.deltaTime * .8f), -.3f, .8f);
        else if (down) ShipVelocity.z = Mathf.Clamp(ShipVelocity.z - Time.deltaTime, -.3f, .8f);
        else ShipVelocity.z *= .99f;

        if (left) ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * 2, -1f, 1);
        else if (right) ShipVelocity.x = Mathf.Clamp(ShipVelocity.x + Time.deltaTime * 2, -1f, 1);
        else ShipVelocity.x *= .9f;

        ShipVelocity = SeaScene.UpdateMap(ShipVelocity);

        if (TimeSinceLastL < 1.5f) { SeaScene.SeaHUD.Hide(); }//Debug.Log("Hide");
        else { SeaScene.SeaHUD.Show(); }//Debug.Log("Show");

        TimeSinceLastL += Time.deltaTime;
    }


    protected override void EastPressed()
    {
        if (SeaScene.NearNPCShip == null) return;
        Audio.SFX.PlayOneShot(SeaScene.Io.NearNPCShip.RegionalSound);
        SetStateDirectly(new SeaToDialogueTransition_State(new HailShip_Dialogue()));
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
        ShipVelocity.z = Mathf.Clamp(ShipVelocity.z + (Time.deltaTime * v2.y * .8f), -.3f, .8f);
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


    void ShipCoords()
    {
        SeaScene.DayCounterText.TextString = SeaScene.Ship.Coord.x.ToString() + " : " + SeaScene.Ship.Coord.z.ToString();
    }

    void CheckNMETriggers()
    {
        foreach (NPCShip npc in SeaScene.Io.NPCShips)
            if (npc.ShipType == NPCShipType.Pirate && Vector3.Distance(npc.Coords, SeaScene.Io.Ship.Coord) < npc.ThreatRange)
            {
                SeaScene.Io.NearNPCShip = npc;
                SetStateDirectly(new NMESailApproach_State());
                return;
            }
    }
}
