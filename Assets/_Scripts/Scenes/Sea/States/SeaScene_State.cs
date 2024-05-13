using System;
using System.Collections;
using UnityEngine;
using Sea;
using Sea.Maps;
using Data.Two;

public class SeaScene_State : State
{
    Sea.WorldMapScene Scene => Sea.WorldMapScene.Io;

    Vector2 ShipVelocity = Vector2.zero;
    public CameraFollow CameraFollow;

    bool up, down, left, right;
    float _timeSinceLastL = 2.5f;
    float TimeSinceLastL
    {
        get => _timeSinceLastL;
        set => _timeSinceLastL = value > 2.5f ? 2.5f : value < -2.5f ? -2.5f : value;
    }

    protected override void PrepareState(Action callback)
    {
        Fade = false;
        _ = Scene;

        CameraFollow = new(Scene.Ship.Parent.transform)
        {
            LockYPos = true,
            LockXRot = true,
            LockWRot = true,
            LockZRot = true,
        };

        Scene.RockTheBoat.Rocking = true;
        Scene.Board.Swells.EnableSwells();

        if (Audio.BGMusic.AudioSources[0].clip == Assets.BGMus4)
            Audio.BGMusic.Resume();
        else Audio.BGMusic.PlayClip(Assets.BGMus4);

        if (Audio.Ambience.AudioSources[0].clip == Assets.SailAmbience)
            Audio.Ambience.Resume();
        else
        {
            Audio.Ambience.AudioSources[0].clip = Assets.SailAmbience;
            Audio.Ambience.AudioSources[0].time = UnityEngine.Random.Range(0, Audio.Ambience.AudioSources[0].clip.length);
            Audio.Ambience.Play(false);
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


        _ = Scene.UpdateMap(this, DataManager, ShipVelocity);

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        Scene.HUD.Enable();
        Scene.HUD.UpdateHealthBar(
            DataManager.PlayerShip.GetLevel(new CurrentHitPoints()),
            DataManager.PlayerShip.GetLevel(new MaxHitPoints()));
        Scene.HUD.Hud.GO.SetActive(true);
        // Scene.HUD.Show();
        Scene.HUD.Hide(-1);
        Scene.Ship.ConfirmPopup.GO.SetActive(false);
        Scene.Ship.AttackPopup.GO.SetActive(false);
        Scene.MiniMap.Card.GO.SetActive(true);

        MonoHelper.OnUpdate += Tick;
        MonoHelper.OnFixedUpdate += FixedTick;
    }

    protected override void DisengageState()
    {
        Scene.HUD.Hide(TimeSinceLastL);

        RStick = Vector2.one * Mathf.Epsilon;
        LStick = Vector2.zero;

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
            // case Dir.Up: up = true; break;
            // case Dir.Up_Off: up = false; break;
            // case Dir.Down: down = true; break;
            // case Dir.Down_Off: down = false; break;
            // case Dir.Left: left = true; break;
            // case Dir.Left_Off: left = false; break;
            // case Dir.Right: right = true; break;
            // case Dir.Right_Off: right = false; break;
        }

    }

    protected void CheckDirectionalInput()
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
        //todotodo ship speeds for different ships. this below is normally .8f, but trying the outrigger at lower speeds
        if (up) ShipVelocity.y = Mathf.Lerp(ShipVelocity.y, .8f, Time.fixedDeltaTime);
        else if (down) ShipVelocity.y = Mathf.Lerp(ShipVelocity.y, -.15f, Time.fixedDeltaTime);
        else ShipVelocity.y = Mathf.Lerp(ShipVelocity.y, 0, Time.fixedDeltaTime);

        if (left) ShipVelocity.x = Mathf.Lerp(ShipVelocity.x, -1, Time.fixedDeltaTime);
        else if (right) ShipVelocity.x = Mathf.Lerp(ShipVelocity.x, 1, Time.fixedDeltaTime);
        else ShipVelocity.x = Mathf.Lerp(ShipVelocity.x, 0, Time.fixedDeltaTime);

        // Debug.Log();
        ShipVelocity = Scene.UpdateMap(this, DataManager, ShipVelocity);
        // if (TimeSinceLastL < 1.5f) { Scene.HUD.Hide(TimeSinceLastL); }
        // else { Scene.HUD.Show(); }
        Scene.HUD.Hide(TimeSinceLastL);
        TimeSinceLastL += Time.deltaTime;
    }

    protected override void NorthPressed()
    {
        Debug.Log("North pressed;");
        _ = Scene.NearestNPC;
        // + " Scene.NearestNPC is not null: " +
        //         (Scene.NearestNPC is not null) +
        //         ", Scene.NearestNPC.SceneObject.Interactable is not NoInteraction: " +
        //         (Scene.NearestNPC.SceneObject.Interactable is not NoInteraction));

        if (Scene.NearestNPC is not null &&
            Scene.NearestNPC.SceneObject.Interactable is not NoInteraction)
        {
            Scene.NearestNPC.HideTimer = Scene.NearestNPC.HideTime;
            // SetState(Scene.NearestNPC.SceneObject.Interactable.SubsequentState);
            SetState(new SeaToBatteryTransition_State());
            Debug.Log("battery transition state setting");
            return;
        }
    }

    protected override void EastPressed()
    {
        Debug.Log("East pressed;" + " " + Scene.NearestNPC + " " + Scene.NearestNPC?.SceneObject.Interactable.GetType() + " " + Scene.NearestNPC?.SceneObject.Interactable.SubsequentState);
        if (Scene.NearestNPC is not null &&
            Scene.NearestNPC.SceneObject.Interactable is not NoInteraction)
        {
            Scene.NearestNPC.HideTimer = Scene.NearestNPC.HideTime;
            SetState(Scene.NearestNPC.SceneObject.Interactable.SubsequentState);
            return;
        }

        if (Scene.NearestInteractableCell is not null)
            SetState(Scene.NearestInteractableCell.SceneObject.Interactable.SubsequentState);
    }

    protected override void StartPressed()
    {
        SetState(new SeaToNewMenuTransition_State(
        new Menus.Two.SeaMenu(
               Manager.Io,
               this
                   // new CameraPan_State(
                   //     subsequentState: this,
                   //     pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                   //     strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                   //     speed: 3)
                   )));

        // SetState(new SeaToMenuTransition_State(
        //     new Menus.Inventory.InventoryMenu(DataManager,
        //     this
        //             // new CameraPan_State(
        //             //     subsequentState: this,
        //             //     pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
        //             //     strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
        //             //     speed: 3)
        //             )));
    }

    protected override void SelectPressed()
    {
        SetState(new SeaToNewMenuTransition_State(
         new Menus.Two.OptionsMenu(
                Manager.Io,
                Audio,
                this
                    // new CameraPan_State(
                    //     subsequentState: this,
                    //     pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    //     strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    //     speed: 3)
                    )));
    }

    protected override void LStickInput(Vector2 v2)
    {
        // Debug.Log(v2);
        ShipVelocity.y = Mathf.Clamp(ShipVelocity.y + (Time.deltaTime * v2.y * .9f), -.15f, .8f);
        ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * -v2.x * 2, -1f, 1f);
        if (v2 != Vector2.zero) { TimeSinceLastL -= Time.deltaTime * 2; }
    }

    protected override void RStickInput(Vector2 v2)
    {
        ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * -v2.x * 5f, -1f, 1f);
        CameraFollow.MoveCamera(v2.y);
    }

    protected override void WestPressed()
    {
        SetState(new SeaToInspection_State(this));
    }

    void Tick()
    {
        Scene.MiniMap.BlinkMiniMap(Scene.Ship.RegionCoord, (int)Scene.Map.RegionResolution);

        Scene.HUD.SetCompassRotation(Scene.Ship.RotY);

        CheckDirectionalInput();

        Scene.HUD.UpdateCoords(Scene.Ship.GlobalCoord.GlobalCoordsToLatLongs(Scene.Map.GlobalSize));

        if ((Scene.NearestNPC = Scene.CheckNMETriggers()) is not null)
        {
            Scene.NearestNPC.HideTimer = Scene.NearestNPC.HideTime;
            SetState(Scene.NearestNPC.SceneObject.Triggerable.SubsequentState);
        }
    }

    void FixedTick()
    {
        Movement();
    }

}
