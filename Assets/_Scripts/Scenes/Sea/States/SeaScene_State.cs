using System;
using System.Collections;
using UnityEngine;
using Sea;
using Sea.Maps;

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
        Scene.HUD.Enable();
        Scene.HUD.Show();
        Scene.Ship.ConfirmPopup.GO.SetActive(true);
        Scene.MiniMap.Card.GO.SetActive(true);
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

        Scene.HUD.UpdateHealthBar(
            DataManager.CharData.GetLevel(Data.Player.CharacterData.DataItem.CurrentHP),
            DataManager.CharData.GetLevel(Data.Player.CharacterData.DataItem.MaxHP));
        Scene.HUD.Hud.GO.SetActive(true);

        _ = Scene.UpdateMap(this, DataManager, ShipVelocity);


        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        MonoHelper.OnUpdate += Tick;
        MonoHelper.OnFixedUpdate += FixedTick;
    }

    protected override void DisengageState()
    {
        Scene.HUD.Hide(TimeSinceLastL);
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

        ShipVelocity = Scene.UpdateMap(this, DataManager, ShipVelocity);

        // if (TimeSinceLastL < 1.5f) { Scene.HUD.Hide(TimeSinceLastL); }
        // else { Scene.HUD.Show(); }
        Scene.HUD.Hide(TimeSinceLastL);
        TimeSinceLastL += Time.deltaTime;
    }

    protected override void NorthPressed()
    {
        SetState(new SeaToMenuTransition_State(
            new Menus.Inventory.InventoryMenu(DataManager,
                new CameraPan_State(
                    subsequentState: this,
                    pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    speed: 3))));
        // SetState(
        // SetState(new CameraPan_State(
        //     new InventoryMenu_State<
        //         OldMenus.InventoryMenu.InventoryMenu.InventoryItem,
        //         OldMenus.InventoryMenu.InventoryMenu,
        //         MaterialsData.DataItem,
        //         OldMenus.Inventory.Materials.MaterialsMenu>
        //     (new OldMenus.InventoryMenu.InventoryMenu(),
        //      new OldMenus.Inventory.Materials.MaterialsMenu()),
        //      // SetState(new CameraPan_State(
        //      //      new MaterialsMenu_State(new MenuToSeaTransition_State()),
        //      pan: new Vector3(
        //          -50,
        //          Cam.Io.Camera.transform.rotation.eulerAngles.y,
        //          Cam.Io.Camera.transform.rotation.eulerAngles.z),
        //      strafe: Cam.Io.Camera.transform.position,
        //      speed: 3));
    }

    protected override void EastPressed()
    {
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
        SetState(new SeaToMenuTransition_State(
            new Menus.Options.OptionsMenu(
                DataManager,
                Audio,
                new CameraPan_State(
                    subsequentState: this,
                    pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    speed: 3))));
    }

    protected override void SelectPressed()
    {
        // SetState(new SeaToQuitMenuTransition_State());
    }

    protected override void LStickInput(Vector2 v2)
    {
        ShipVelocity.y = Mathf.Clamp(ShipVelocity.y + (Time.deltaTime * v2.y * .9f), -.15f, .8f);
        ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * -v2.x * 3, -1f, 1f);
        if (v2 != Vector2.zero) { TimeSinceLastL -= Time.deltaTime * 2; }
    }

    protected override void RStickInput(Vector2 v2)
    {
        ShipVelocity.x = Mathf.Clamp(ShipVelocity.x - Time.deltaTime * -v2.x * 5f, -1f, 1f);
        CameraFollow.MoveCamera(v2.y);
    }

    protected override void WestPressed()
    {
        SetState(
            new CameraPan_State(
                new SeaInspection_State(),
                new Vector3(45, 180, 0),
                new Vector3(5.5f, 5, 13),
                3.5f
            ));
    }

    void Tick()
    {
        Scene.MiniMap.BlinkMiniMap(Scene.Ship.RegionCoord, (int)Scene.Map.Size);

        Scene.HUD.SetCompassRotation(Scene.Ship.RotY);

        CheckDirectionalInput();

        Scene.HUD.UpdateCoords(Scene.Ship.GlobalCoord.GlobalCoordsToLatLongs(Scene.Map.GlobalSize));

        if ((Scene.NearestNPC = Scene.CheckNMETriggers()) != null)
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
