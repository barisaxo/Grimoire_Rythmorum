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
        Data.Manager.Io.Lighthouse.Reset();

        DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Data.Sloop());

        if (!Audio.BGMusic.AudioSources[0].isPlaying)
            Audio.BGMusic.Resume();
    }

    protected override void DisengageState()
    {
        Data.Manager.Io.ActiveShip.SetLevel(new Data.MaxHitPoints(),
            Data.Manager.Io.ActiveShip.ShipStats.HullStrength);

        Data.Manager.Io.ActiveShip.SetLevel(new Data.CurrentHitPoints(),
            Data.Manager.Io.ActiveShip.ShipStats.HullStrength);

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
        Cove.Player.Bark.SetTextString("");
        Cove.HUD.HideTexts();
        if (NearObject(Cove.Sloop.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
            return;
        }
        else if (NearObject(Cove.Cutter.transform))
        {
            if (!ShipUnlocked(new Data.Sloop())) { Cove.Player.Bark.SetTextString("(locked)"); return; }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
            return;
        }
        else if (NearObject(Cove.Schooner.transform))
        {
            if (!ShipUnlocked(new Data.Cutter())) { Cove.Player.Bark.SetTextString("(locked)"); return; }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
            return;
        }
        else if (NearObject(Cove.Brig.transform))
        {
            if (!ShipUnlocked(new Data.Schooner())) { Cove.Player.Bark.SetTextString("(locked)"); return; }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
            return;
        }
        else if (NearObject(Cove.Frigate.transform))
        {
            if (!ShipUnlocked(new Data.Brig())) { Cove.Player.Bark.SetTextString("(locked)"); return; }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
            return;
        }
        else if (NearObject(Cove.Barque.transform))
        {
            if (!ShipUnlocked(new Data.Frigate())) { Cove.Player.Bark.SetTextString("(locked)"); return; }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
            return;
        }
        else if (NearObject(Cove.SkillSheet.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Skills");
            return;
        }
        else if (NearObject(Cove.Gramo.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Diatonic Cadencing");
            return;
        }
        else if (NearObject(Cove.Fish.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Beat Fishing");
            return;
        }
        else if (NearObject(Cove.Bottle.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Celestial Navigation");
            return;
        }
    }

    protected override void EastPressed()
    {
        Cove.HUD.HideTexts();
        if (NearObject(Cove.Sloop.transform))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Data.Sloop());
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Cutter.transform) && ShipUnlocked(new Data.Sloop()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Schooner.transform) && ShipUnlocked(new Data.Cutter()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Brig.transform) && ShipUnlocked(new Data.Schooner()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Frigate.transform) && ShipUnlocked(new Data.Brig()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Barque.transform) && ShipUnlocked(new Data.Frigate()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        // else if (NearObject(Cove.SkillSheet.transform))
        // {
        //     SetState(new CoveToMenuTransition_State(
        //         new Menus.SkillsMenu(Data.Manager.Io.Skill, Data.Manager.Io.Player,
        //             new CameraPan_State(
        //             subsequentState: this,
        //             pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
        //             strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
        //             speed: 5))));
        //     return;
        // }
    }

    protected override void NorthPressed()
    {
        Cove.HUD.HideTexts();
        if (NearObject(Cove.Sloop.transform))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Data.Sloop());
            SetState(new CoveToMenuTransition_State(
                new Menus.ShipUpgradeMenu(DataManager.ShipUpgradeData,
                    DataManager.Player,
                    DataManager.ActiveShip,
                    DataManager.ShipStats.ActiveShip,
                    new CameraPan_State(
                    subsequentState: this,
                    pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    speed: 5))));
            return;
        }
        else if (NearObject(Cove.Cutter.transform) && ShipUnlocked(new Data.Sloop()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Schooner.transform) && ShipUnlocked(new Data.Cutter()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Brig.transform) && ShipUnlocked(new Data.Schooner()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Frigate.transform) && ShipUnlocked(new Data.Brig()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Barque.transform) && ShipUnlocked(new Data.Frigate()))
        {
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Gramo.transform))
        {
            SetState(new CoveToMenuTransition_State(
               new Menus.GramophoneMenu(DataManager.Gramophones,
                   new CameraPan_State(
                   subsequentState: this,
                   pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                   strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                   speed: 5))));
            return;
        }
        else if (NearObject(Cove.Fish.transform))
        {
            SetState(new CoveToAnglingTransition_State(
               new AnglingPractice_State(
                   new CameraPan_State(
                        subsequentState: this,
                        pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                        strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                        speed: 5))));
            return;
        }
        else if (NearObject(Cove.Bottle.transform))
        {
            SetState(new CoveToMenuTransition_State(
               new Menus.StarChartsMenu(DataManager.StarChart,
                   new CameraPan_State(
                   subsequentState: this,
                   pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                   strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                   speed: 5))));
            return;
        }
        else if (NearObject(Cove.SkillSheet.transform))
        {
            SetState(new CoveToMenuTransition_State(
                new Menus.SkillsMenu(Data.Manager.Io.Skill, Data.Manager.Io.Player,
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
        SetState(new CoveToMenuTransition_State(
            new Menus.OptionsMenu(
                DataManager,
                Audio,
                new CameraPan_State(
                    subsequentState: this,
                    pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    speed: 5))));
    }

    bool NearObject(Transform tf) => Dist(Cove.Player.GO.transform, tf) < 2.5f;
    float Dist(Transform a, Transform b) => Vector2.Distance(
      new Vector2(a.position.x, a.position.z),
      new Vector2(b.position.x, b.position.z));


    bool ShipUnlocked(Data.IHull prevShip)
    {
        var stats = DataManager.ShipStats.GetItem(prevShip);
        if (stats.CannonStats.Cannon is not Data.Culverin) return false;
        if (stats.RiggingStats.ClothType is not Data.Linen) return false;
        return true;
    }
}
