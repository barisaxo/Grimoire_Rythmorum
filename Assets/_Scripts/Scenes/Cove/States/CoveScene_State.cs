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

        Audio.Ambience.Pause();

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        CoveScene.Io.RockTheBoat.Rocking = true;
        Datum.Manager.Io.Lighthouse.Reset();

        DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Sloop());

        if (!Audio.BGMusic.AudioSources[0].isPlaying)
            Audio.BGMusic.Resume();
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

        if (NearObject(Cove.Catboat.transform))
        {
            if (DataManager.BeatFishingPracticeData.GetLevel(new Datum.BeatFishing.QHQ()) == 0)
            {
                Cove.Player.Bark.SetTextString("Complete Beat Fishing to unlock");
                return;
            }
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }

        else if (NearObject(Cove.Sloop.transform))
        {
            if (!ShipUnlocked(new Datum.Sloop()))
            {
                Cove.Player.Bark.SetTextString("Complete Quarter Note Batterie to unlock");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.SloopPurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Sloop: " + Datum.ShipPurchaseEnum.SloopPurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Cutter.transform))
        {
            if (!ShipUnlocked(new Datum.Cutter()))
            {
                Cove.Player.Bark.SetTextString("Complete Quarter Note Batterie to unlock");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.CutterPurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Cutter: " + Datum.ShipPurchaseEnum.CutterPurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Schooner.transform))
        {
            if (!ShipUnlocked(new Datum.Schooner()))
            {
                Cove.Player.Bark.SetTextString("Complete Eighth Note Batterie to unlock");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.SchoonerPurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Schooner: " + Datum.ShipPurchaseEnum.SchoonerPurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }

            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Brig.transform))
        {
            if (!ShipUnlocked(new Datum.Brig()))
            {
                Cove.Player.Bark.SetTextString("Complete Eighth Note Batterie to unlock");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.BrigPurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Brig: " + Datum.ShipPurchaseEnum.BrigPurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Frigate.transform))
        {
            if (!ShipUnlocked(new Datum.Frigate()))
            {
                Cove.Player.Bark.SetTextString("Complete Sixteenth Note Batterie to unlock");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.FrigatePurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Frigate: " + Datum.ShipPurchaseEnum.FrigatePurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.Barque.transform))
        {
            if (!ShipUnlocked(new Datum.Barque()))
            {
                Cove.Player.Bark.SetTextString("Complete Sixteenth Note Batterie to unlock");
                return;
            }

            if (DataManager.ShipPurchaseData.GetLevel(new Datum.BarquePurchase()) == 0)
            {
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Purchase Barque: " + Datum.ShipPurchaseEnum.BarquePurchase.Cost + " patterns\n[" +
                    DataManager.Player.GetLevel(new Datum.PatternsAvailable()) + " patterns available]");
                return;
            }
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Upgrades");
            Cove.HUD.East.SetImageColor(Color.white).SetTextString("Set Sail");
        }
        else if (NearObject(Cove.SkillSheet.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("View Skills");
        }
        else if (NearObject(Cove.Gramo.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Diatonic Cadencing");
        }
        else if (NearObject(Cove.Fish.transform))
        {
            if (DataManager.QRhythmCellData.GetLevel(new Datum.QRhythm.QHQ()) > 0)
                Cove.HUD.North.SetImageColor(Color.white).SetTextString("Beat Fishing");
            else Cove.HUD.North.SetImageColor(Color.white).SetTextString("Complete Quarter Note Rhythm Cells to unlock");
        }
        else if (NearObject(Cove.Bottle.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Celestial Navigation");
        }
        else if (NearObject(Cove.Cannon.transform))
        {
            Cove.HUD.North.SetImageColor(Color.white).SetTextString("Rhythm Cells &\nBatterie Practice");
        }
    }

    protected override void EastPressed()
    {
        Cove.HUD.HideTexts();
        if (NearObject(Cove.Catboat.transform))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.CatBoat());
            SetState(new CoveToSeaTransition_State()); return;
        }
        if (NearObject(Cove.Sloop.transform))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Sloop());
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Cutter.transform) && ShipUnlocked(new Datum.Cutter()))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Cutter());
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Schooner.transform) && ShipUnlocked(new Datum.Schooner()))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Schooner());
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Brig.transform) && ShipUnlocked(new Datum.Brig()))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Brig());
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Frigate.transform) && ShipUnlocked(new Datum.Frigate()))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Frigate());
            SetState(new CoveToSeaTransition_State()); return;
        }
        else if (NearObject(Cove.Barque.transform) && ShipUnlocked(new Datum.Barque()))
        {
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Barque());
            SetState(new CoveToSeaTransition_State()); return;
        }
    }

    protected override void NorthPressed()
    {
        Cove.HUD.HideTexts();
        bool nearShip = false;
        if (NearObject(Cove.Sloop.transform))
        {
            if (DataManager.ShipPurchaseData.GetLevel(new Datum.SloopPurchase()) == 0 &&
                DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                Datum.ShipPurchaseEnum.SloopPurchase.Cost)
            {
                DataManager.ShipPurchaseData.AdjustLevel(new Datum.SloopPurchase(), 1);
                DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.SloopPurchase.Cost);
                return;
            };
            nearShip = true;
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Sloop());
        }
        else if (NearObject(Cove.Cutter.transform) && ShipUnlocked(new Datum.Cutter()))
        {
            if (DataManager.ShipPurchaseData.GetLevel(new Datum.CutterPurchase()) == 0 &&
                DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                Datum.ShipPurchaseEnum.CutterPurchase.Cost)
            {
                DataManager.ShipPurchaseData.AdjustLevel(new Datum.CutterPurchase(), 1);
                DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.CutterPurchase.Cost);
                return;
            };
            nearShip = true;
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Cutter());
        }
        else if (NearObject(Cove.Schooner.transform) && ShipUnlocked(new Datum.Schooner()))
        {
            if (DataManager.ShipPurchaseData.GetLevel(new Datum.SchoonerPurchase()) == 0 &&
                DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                Datum.ShipPurchaseEnum.SchoonerPurchase.Cost)
            {
                DataManager.ShipPurchaseData.AdjustLevel(new Datum.SchoonerPurchase(), 1);
                DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.SchoonerPurchase.Cost);
                return;
            };
            nearShip = true;
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Schooner());
        }
        else if (NearObject(Cove.Brig.transform) && ShipUnlocked(new Datum.Brig()))
        {
            if (DataManager.ShipPurchaseData.GetLevel(new Datum.BrigPurchase()) == 0 &&
                DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                Datum.ShipPurchaseEnum.BrigPurchase.Cost)
            {
                DataManager.ShipPurchaseData.AdjustLevel(new Datum.BrigPurchase(), 1);
                DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.BrigPurchase.Cost);
                return;
            };
            nearShip = true;
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Brig());
        }
        else if (NearObject(Cove.Frigate.transform) && ShipUnlocked(new Datum.Frigate()))
        {
            if (DataManager.ShipPurchaseData.GetLevel(new Datum.FrigatePurchase()) == 0 &&
                DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                Datum.ShipPurchaseEnum.FrigatePurchase.Cost)
            {
                DataManager.ShipPurchaseData.AdjustLevel(new Datum.FrigatePurchase(), 1);
                DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.FrigatePurchase.Cost);
                return;
            };
            nearShip = true;
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Frigate());
        }
        else if (NearObject(Cove.Barque.transform) && ShipUnlocked(new Datum.Barque()))
        {
            if (DataManager.ShipPurchaseData.GetLevel(new Datum.BarquePurchase()) == 0 &&
                DataManager.Player.GetLevel(new Datum.PatternsAvailable()) >=
                Datum.ShipPurchaseEnum.BarquePurchase.Cost)
            {
                DataManager.ShipPurchaseData.AdjustLevel(new Datum.BarquePurchase(), 1);
                DataManager.Player.AdjustLevel(new Datum.PatternsSpent(), Datum.ShipPurchaseEnum.BarquePurchase.Cost);
                return;
            };
            nearShip = true;
            DataManager.ShipStats.ActiveShip = DataManager.ShipStats.GetItem(new Datum.Barque());
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
            if (DataManager.QRhythmCellData.GetLevel(new Datum.QRhythm.QHQ()) > 0)
                SetState(new CoveToMenuTransition_State(
                   new Menus.BeatFishingPracticeMenu(
                       new CameraPan_State(
                       subsequentState: this,
                       pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                       strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                       speed: 5))));

            // SetState(new CoveToAnglingTransition_State(
            //    new AnglingPractice_State(
            //        new CameraPan_State(
            //             subsequentState: this,
            //             pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
            //             strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
            //             speed: 5))));

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
        else if (NearObject(Cove.Cannon.transform))
        {
            SetState(new CoveToMenuTransition_State(
               new Menus.BatteriePracticeMenu(
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
                new Menus.SkillsMenu(Datum.Manager.Io.Skill, Datum.Manager.Io.Player,
                    new CameraPan_State(
                    subsequentState: this,
                    pan: Cam.StoredCamRot = Cam.Io.Camera.transform.rotation.eulerAngles,
                    strafe: Cam.StoredCamPos = Cam.Io.Camera.transform.position,
                    speed: 5))));
            return;
        }

        if (nearShip)
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

    bool NearObject(Transform tf) => Dist(Cove.Player.GO.transform, tf) < 2f;
    float Dist(Transform a, Transform b) => Vector2.Distance(
      new Vector2(a.position.x, a.position.z),
      new Vector2(b.position.x, b.position.z));


    bool ShipUnlocked(Datum.IHull ship)
    {
        switch (ship)
        {
            case Datum.Cutter:
            case Datum.Sloop:
                if (DataManager.BatteriePracticeData.GetLevel(new Datum.QRhythmCell()) < 1) return false;
                break;

            case Datum.Schooner:
            case Datum.Brig:
                if (DataManager.BatteriePracticeData.GetLevel(new Datum.ERhythmCell()) < 1) return false;
                break;

            case Datum.Frigate:
            case Datum.Barque:
                if (DataManager.BatteriePracticeData.GetLevel(new Datum.SRhythmCell()) < 1) return false;
                break;
        }

        // var stats = DataManager.ShipStats.GetItem(prevShip);
        // if (stats.CannonStats.Cannon is not Data.Culverin) return false;
        // if (stats.RiggingStats.ClothType is not Data.Linen) return false;

        return true;
    }
}
