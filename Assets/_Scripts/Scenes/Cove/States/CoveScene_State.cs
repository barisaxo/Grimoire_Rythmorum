using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoveScene_State : State
{
    CoveScene Cove => CoveScene.Io;
    Vector2 lStick, rStick;

    bool readying = true;

    void Readied()
    {
        readying = false;
    }

    protected override void PrepareState(Action callback)
    {
        MonoHelper.ToFixedUpdate += HandleInput;

        Audio.Ambience.Pause();

        PanCamera().StartCoroutine();
        IEnumerator PanCamera()
        {
            while (readying)
            {
                CoveScene.Io.Player.MoveCamera(-.333f);
                yield return null;
            }
        }
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        CoveScene.Io.RockTheBoat.Rocking = true;
        Datum.Manager.Io.Lighthouse.Reset();

        DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.CatBoat());

        if (!Audio.BGMusic.AudioSources[0].isPlaying)
            Audio.BGMusic.Resume();

        MonoHelper.OnUpdate += CheckDirectionalInput;
    }

    protected override void DisengageState()
    {
        Datum.Manager.Io.ActiveShip.SetLevel(new Datum.MaxHitPoints(),
            Datum.Manager.Io.ActiveShip.ShipStats.HullStrength);

        Datum.Manager.Io.ActiveShip.SetLevel(new Datum.CurrentHitPoints(),
            Datum.Manager.Io.ActiveShip.ShipStats.HullStrength);

        Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles;
        Cam.StoredCamPos = Cam.Io.Camera.transform.position;

        MonoHelper.ToFixedUpdate -= HandleInput;

        Audio.BGMusic.Pause();

        MonoHelper.OnUpdate -= CheckDirectionalInput;
    }

    protected override void GPInput(GamePadButton gpb)
    {
        Readied();
        base.GPInput(gpb);
    }

    protected override void LStickInput(Vector2 v2)
    {
        if (v2.y > .5f) v2.y = 1;
        else if (v2.y > 0) v2.y = .5f;
        lStick = v2;

        if (v2 == Vector2.zero) return;
        Readied();
    }

    protected override void RStickInput(Vector2 v2)
    {
        rStick = v2;


        if (v2 == Vector2.zero) return;
        Readied();
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

        if (NearObject(Cove.Catboat.transform))
        {
            if (DataManager.BeatFishingPracticeData.GetLevel(new Datum.BeatFishing.QHQ()) == 0)
            {
                Cove.Player.Bark.SetTextString("Complete Beat Fishing to unlock Catboat");
                return;
            }
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }

        else if (NearObject(Cove.Sloop.transform))
        {
            if (!ShipUnlocked(new Datum.Sloop()))
            {
                Cove.Player.Bark.SetTextString("Complete Quarter Note Batterie 'Rests and Ties' to unlock Sloop");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.SloopPurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Sloop: " + Datum.ShipPurchaseEnum.SloopPurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Upgrade Sloop");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Cutter.transform))
        {
            if (!ShipUnlocked(new Datum.Cutter()))
            {
                Cove.Player.Bark.SetTextString("Complete Quarter Note Batterie 'Rests and Ties' to unlock Cutter");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.CutterPurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Cutter: " + Datum.ShipPurchaseEnum.CutterPurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Upgrade Cutter");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Schooner.transform))
        {
            if (!ShipUnlocked(new Datum.Schooner()))
            {
                Cove.Player.Bark.SetTextString("Complete Eighth Note Batterie 'Rests and Ties' to unlock Schooner");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.SchoonerPurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Schooner: " + Datum.ShipPurchaseEnum.SchoonerPurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }

            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Upgrade Schooner");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Brig.transform))
        {
            if (!ShipUnlocked(new Datum.Brig()))
            {
                Cove.Player.Bark.SetTextString("Complete Eighth Note Batterie 'Rests and Ties' to unlock Brig");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.BrigPurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Brig: " + Datum.ShipPurchaseEnum.BrigPurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Upgrade Brig");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Frigate.transform))
        {
            if (!ShipUnlocked(new Datum.Frigate()))
            {
                Cove.Player.Bark.SetTextString("Complete Sixteenth Note Batterie 'Rests and Ties' to unlock Frigate");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.FrigatePurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Frigate: " + Datum.ShipPurchaseEnum.FrigatePurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Upgrade Frigate");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Barque.transform))
        {
            if (!ShipUnlocked(new Datum.Barque()))
            {
                Cove.Player.Bark.SetTextString("Complete Sixteenth Note Batterie 'Rests and Ties' to unlock Barque");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.BarquePurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Barque: " + Datum.ShipPurchaseEnum.BarquePurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Upgrade Barque");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.SkillSheet.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Skills");
        }
        else if (NearObject(Cove.Gramo.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Diatonic Cadencing Practice");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("About Gramophones");
        }
        else if (NearObject(Cove.Fish.transform))
        {
            if (DataManager.QRhythmCellData.GetLevel(new Datum.QRhythm.QHQ()) > 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Beat Fishing Practice");
                Cove.HUD.East.SetImageColor(Color.white).SetTextString("About Beat Fishing");
            }
            else Cove.HUD.North.SetImageColor(Color.white).SetTextString("Complete Quarter Note Rhythm Cells to unlock");
        }
        else if (NearObject(Cove.AL.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Talk to AL");
        }
        else if (NearObject(Cove.Bottle.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Celestial Navigation Practice");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("About Star Charts");
        }
        else if (NearObject(Cove.Cannon.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Rhythm Cells &\nBatterie Practice");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("About Batterie");
        }
        else if (NearObject(Cove.Card.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Muscopa Practice");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("About Muscopa");
        }
    }

    protected override void EastPressed()
    {
        Cove.HUD.HideTexts();
        InteractWith(GamePadButton.East_Press);
    }

    protected override void NorthPressed()
    {
        Cove.HUD.HideTexts();
        InteractWith(GamePadButton.North_Press);
    }

    void GoToShipUpgradeMenu()
    {
        SetState(new CoveToMenuTransition_State(
                 new Menus.ShipUpgradeMenu(DataManager.ShipUpgrade,
                     DataManager.Player,
                     DataManager.ActiveShip,
                     DataManager.ShipStats.ActiveShip,
                     new CameraPan_State(
                     subsequentState: this,
                     pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                     strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                     speed: 5))));
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

    protected override void StartPressed()
    {
        SetState(new CoveToMenuTransition_State(
            new Menus.SeaMenu(
                DataManager,
                new CameraPan_State(
                    subsequentState: this,
                    pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    speed: 5))));
    }

    void FishInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                if (DataManager.QRhythmCellData.GetLevel(new Datum.QRhythm.QHQ()) > 0)
                    SetState(new CoveToMenuTransition_State(
                        new Menus.BeatFishingPracticeMenu(
                            new CameraPan_State(
                            subsequentState: this,
                            pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                            strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                            speed: 5))));
                return;

            case GamePadButton.East_Press:
                SetState(new DialogStart_State(new BeatFishingTutorial_Dialogue(this)));
                return;
        }
    }
    void ALInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                SetState(new DialogStart_State(new Dialog.Cove.ALFalseStart_Dialogue(this)));
                return;
        }
    }
    void SkillInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                SetState(new CoveToMenuTransition_State(
                    new Menus.SkillsMenu(Datum.Manager.Io.Skill, Datum.Manager.Io.Player,
                        new CameraPan_State(
                        subsequentState: this,
                        pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                        strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                        speed: 5))));
                return;
        }
    }
    void GramoInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                SetState(new CoveToMenuTransition_State(
                new Menus.GramophoneMenu(DataManager.Gramophones,
                    new CameraPan_State(
                        subsequentState: this,
                        pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                        strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                        speed: 5))));
                return;

            case GamePadButton.East_Press:
                SetState(new DialogStart_State(new GramophoneTutorial_Dialogue(this)));
                return;
        }
    }
    void BottleInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                SetState(new CoveToMenuTransition_State(
               new Menus.StarChartsMenu(DataManager.StarChart,
                    new CameraPan_State(
                        subsequentState: this,
                        pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                        strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                        speed: 5))));
                return;

            case GamePadButton.East_Press:
                SetState(new DialogStart_State(new CelestialNavigationTutorial_Dialogue(this)));
                return;
        }
    }
    void CannonInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                SetState(new CoveToMenuTransition_State(
                    new Menus.BatteriePracticeMenu(
                        new CameraPan_State(
                        subsequentState: this,
                        pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                        strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                        speed: 5))));
                return;

            case GamePadButton.East_Press:
                SetState(new DialogStart_State(new Dialog.Cove.AboutBatterie_Dialogue(this)));
                return;
        }
    }
    void CardInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:

                // SetState(new Muscopa.NewMuscopaState(new CameraPan_State(
                //         subsequentState: this,
                //         pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                //         strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                //         speed: 5), true));
                SetState(new CoveToMenuTransition_State(
                    new Menus.MuscopaMenu(
                        new CameraPan_State(
                        subsequentState: this,
                        pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                        strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                        speed: 5))));
                return;

            case GamePadButton.East_Press:
                SetState(new DialogStart_State(new MuscopaTutorial_Dialogue(this)));
                return;
        }
    }
    void CatboatInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.East_Press:
                DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.CatBoat());
                SetState(new CoveToSeaTransition_State());
                return;
        }
    }
    void SloopInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                if (ShipUnlocked(new Datum.Sloop()))
                {
                    if (DataManager.ShipPurchaseData.GetLevel(new Datum.SloopPurchase()) == 0)
                    {
                        if (DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                            Datum.ShipPurchaseEnum.SloopPurchase.Cost)
                        {
                            DataManager.ShipPurchaseData.AdjustLevel(new Datum.SloopPurchase(), 1);
                            DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.SloopPurchase.Cost);
                        }
                        return;
                    }
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Sloop());
                    GoToShipUpgradeMenu();
                }
                return;

            case GamePadButton.East_Press:
                if (DataManager.ShipPurchaseData.GetLevel(new Datum.SloopPurchase()) > 0)
                {
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Sloop());
                    SetState(new CoveToSeaTransition_State());
                }
                return;
        }
    }
    void CutterInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                if (ShipUnlocked(new Datum.Cutter()))
                {
                    if (DataManager.ShipPurchaseData.GetLevel(new Datum.CutterPurchase()) == 0)
                    {
                        if (DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                            Datum.ShipPurchaseEnum.CutterPurchase.Cost)
                        {
                            DataManager.ShipPurchaseData.AdjustLevel(new Datum.CutterPurchase(), 1);
                            DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.CutterPurchase.Cost);
                        }
                        return;
                    }
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Cutter());
                    GoToShipUpgradeMenu();
                }
                return;

            case GamePadButton.East_Press:
                if (DataManager.ShipPurchaseData.GetLevel(new Datum.CutterPurchase()) > 0)
                {
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Cutter());
                    SetState(new CoveToSeaTransition_State());
                }
                return;
        }
    }
    void SchoonerInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                if (ShipUnlocked(new Datum.Schooner()))
                {
                    if (DataManager.ShipPurchaseData.GetLevel(new Datum.SchoonerPurchase()) == 0)
                    {
                        if (DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                            Datum.ShipPurchaseEnum.SchoonerPurchase.Cost)
                        {
                            DataManager.ShipPurchaseData.AdjustLevel(new Datum.SchoonerPurchase(), 1);
                            DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.SchoonerPurchase.Cost);
                        }
                        return;
                    }
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Schooner());
                    GoToShipUpgradeMenu();
                }
                return;

            case GamePadButton.East_Press:
                if (DataManager.ShipPurchaseData.GetLevel(new Datum.SchoonerPurchase()) > 0)
                {
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Schooner());
                    SetState(new CoveToSeaTransition_State());
                }
                return;
        }
    }

    void BrigInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                if (ShipUnlocked(new Datum.Brig()))
                {
                    if (DataManager.ShipPurchaseData.GetLevel(new Datum.BrigPurchase()) == 0)
                    {
                        if (DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                            Datum.ShipPurchaseEnum.BrigPurchase.Cost)
                        {
                            DataManager.ShipPurchaseData.AdjustLevel(new Datum.BrigPurchase(), 1);
                            DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.BrigPurchase.Cost);
                        }
                        return;
                    }
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Brig());
                    GoToShipUpgradeMenu();
                }
                return;

            case GamePadButton.East_Press:
                if (DataManager.ShipPurchaseData.GetLevel(new Datum.BrigPurchase()) > 0)
                {
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Brig());
                    SetState(new CoveToSeaTransition_State());
                }
                return;
        }
    }

    void FrigateInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                if (ShipUnlocked(new Datum.Frigate()))
                {
                    if (DataManager.ShipPurchaseData.GetLevel(new Datum.FrigatePurchase()) == 0)
                    {
                        if (DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                            Datum.ShipPurchaseEnum.FrigatePurchase.Cost)
                        {
                            DataManager.ShipPurchaseData.AdjustLevel(new Datum.FrigatePurchase(), 1);
                            DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.FrigatePurchase.Cost);
                        }
                        return;
                    }

                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Frigate());
                    GoToShipUpgradeMenu();
                }
                return;

            case GamePadButton.East_Press:
                if (DataManager.ShipPurchaseData.GetLevel(new Datum.FrigatePurchase()) > 0)
                {
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Frigate());
                    SetState(new CoveToSeaTransition_State());
                }
                return;
        }
    }

    void BarqueInteraction(GamePadButton gpb)
    {
        switch (gpb)
        {
            case GamePadButton.North_Press:
                if (ShipUnlocked(new Datum.Barque()))
                {
                    if (DataManager.ShipPurchaseData.GetLevel(new Datum.BarquePurchase()) == 0)
                    {
                        if (DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                            Datum.ShipPurchaseEnum.BarquePurchase.Cost)
                        {
                            DataManager.ShipPurchaseData.AdjustLevel(new Datum.BarquePurchase(), 1);
                            DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.BarquePurchase.Cost);
                        }
                        return;
                    };
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Barque());
                    GoToShipUpgradeMenu();
                }
                return;

            case GamePadButton.East_Press:
                if (DataManager.ShipPurchaseData.GetLevel(new Datum.BarquePurchase()) > 0)
                {
                    DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Barque());
                    SetState(new CoveToSeaTransition_State());
                }
                return;
        }
    }

    void InteractWith(GamePadButton gpb)
    {
        if (NearObject(Cove.AL.transform)) ALInteraction(gpb);
        else if (NearObject(Cove.SkillSheet.transform)) SkillInteraction(gpb);
        else if (NearObject(Cove.Fish.transform)) FishInteraction(gpb);
        else if (NearObject(Cove.Gramo.transform)) GramoInteraction(gpb);
        else if (NearObject(Cove.Bottle.transform)) BottleInteraction(gpb);
        else if (NearObject(Cove.Cannon.transform)) CannonInteraction(gpb);
        else if (NearObject(Cove.Card.transform)) CardInteraction(gpb);
        else if (NearObject(Cove.Catboat.transform)) CatboatInteraction(gpb);
        else if (NearObject(Cove.Sloop.transform)) SloopInteraction(gpb);
        else if (NearObject(Cove.Cutter.transform)) CutterInteraction(gpb);
        else if (NearObject(Cove.Schooner.transform)) SchoonerInteraction(gpb);
        else if (NearObject(Cove.Brig.transform)) BrigInteraction(gpb);
        else if (NearObject(Cove.Frigate.transform)) FrigateInteraction(gpb);
        else if (NearObject(Cove.Barque.transform)) BarqueInteraction(gpb);
    }


    bool NearObject(Transform tf) => Dist(Cove.Player.GO.transform, tf) < 2f;
    float Dist(Transform a, Transform b) => Vector2.Distance(
      new Vector2(a.position.x, a.position.z),
      new Vector2(b.position.x, b.position.z));


    bool ShipUnlocked(Datum.IHull ship)
    {
        return ship switch
        {
            Datum.Cutter or Datum.Sloop => DataManager.QBatterieOptionData.GetLevel(new Datum.QRhythm.RestsAndTies()) > 0,
            Datum.Schooner or Datum.Brig => DataManager.EBatterieOptionData.GetLevel(new Datum.ERhythm.RestsAndTies()) > 0,
            Datum.Frigate or Datum.Barque => DataManager.SBatterieOptionData.GetLevel(new Datum.SRhythm.RestsAndTies()) > 0,
            _ => throw new System.Exception(ship.Name)
        };
    }

    protected void CheckDirectionalInput()
    {
        if (UnityEngine.InputSystem.Keyboard.current.wKey.wasPressedThisFrame) LStick = Vector2.up;
        else if (UnityEngine.InputSystem.Keyboard.current.wKey.wasReleasedThisFrame) LStick = Vector2.zero;

        if (UnityEngine.InputSystem.Keyboard.current.sKey.wasPressedThisFrame) LStick = Vector2.down;
        else if (UnityEngine.InputSystem.Keyboard.current.sKey.wasReleasedThisFrame) LStick = Vector2.zero;

        if (UnityEngine.InputSystem.Keyboard.current.aKey.wasPressedThisFrame) RStick = Vector2.left;
        else if (UnityEngine.InputSystem.Keyboard.current.aKey.wasReleasedThisFrame) RStick = Vector2.zero;

        if (UnityEngine.InputSystem.Keyboard.current.dKey.wasPressedThisFrame) RStick = Vector2.right;
        else if (UnityEngine.InputSystem.Keyboard.current.dKey.wasReleasedThisFrame) RStick = Vector2.zero;
    }

}
