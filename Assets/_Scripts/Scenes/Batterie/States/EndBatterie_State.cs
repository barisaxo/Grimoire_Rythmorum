
using MusicTheory;
using System;

public class EndBatterie_State : State
{
    public EndBatterie_State(BatterieScene scene, BatterieResultType result)
    {
        Scene = scene;
        Scene.Pack.SetResultType(result);
    }
    readonly BatterieScene Scene;

    // int damage => Scene.Pack.TotalErrors;
    int coins = 0;
    int mats = 0;
    int rations = 0;
    int patterns = 0;
    // bool map;

    int level => (Scene.NMEShipStats.HullStats.Hull.ID * 4) + 1;
    // DataManager.GamePlay.CurrentLevel switch
    // {
    //     RegionalMode.Lydian => 2,
    //     RegionalMode.Aeolian => 2,
    //     RegionalMode.Phrygian => 3,
    //     RegionalMode.Locrian => 3,
    //     _ => 1,
    // };

    protected override void PrepareState(Action callback)
    {
        coins = (int)((level + 35f) * 5.55f * UnityEngine.Random.Range(.35f, 1) * (float)((100 - Result()) * .01f));
        mats = (int)((level + 25f) * 2.55f * UnityEngine.Random.Range(.35f, 1) * (float)((100 - Result()) * .01f));
        rations = (int)(1 + ((level + 5f) * UnityEngine.Random.Range(.5f, 1) * (float)((100 - Result()) * .01f)));
        patterns = (int)((Scene.NMEShipStats.HullStrength + Scene.NMEShipStats.VolleyDamage) *
                        (Scene.Pack.HasCritThisBattery ? .45f : .25f));
        Results();

        Sea.WorldMapScene.Io.Ship.GO.transform.SetPositionAndRotation(
            Sea.WorldMapScene.Io.Ship.SeaPos,
            Sea.WorldMapScene.Io.Ship.SeaRot
        );

        UnityEngine.GameObject.Destroy(Scene.NMEFire);
        UnityEngine.GameObject.Destroy(Scene.ShipFire);
        Scene.SelfDestruct();

        callback();
        return;

        int Result()
        {
            if (Scene.Pack.Spammed) return -99;
            else return Scene.Pack.TotalErrors;
        }

    }

    void Results()
    {
        // Board.BoardHUD.UpdatePlayerHealth(Data.CharacterData);
        // SetState(new SeaSceneTest_State());
        // return;
        switch (Scene.Pack.ResultType)
        {
            case BatterieResultType.NMESurrender:
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), mats /= 2);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), rations /= 2);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), coins /= 2);
                DataManager.Player.AdjustLevel(new Data.PatternsFound(), patterns);
                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, patterns, BatterieResultType.NMESurrender))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Surrender:
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), mats /= -2);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), rations /= -2);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), coins /= -2);
                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, 0, BatterieResultType.Surrender))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Spam:
                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, 0, BatterieResultType.Spam))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Fled:
                SetState(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, 0, BatterieResultType.Fled)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            3)));

                return;

            case BatterieResultType.NMEscaped:
                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, 0, BatterieResultType.NMEscaped))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Lost://TODO
                SetState(new MenuState(new Menus.MainMenu(Data.Manager.Io, Audio))); ;

                return;

            case BatterieResultType.Won:
                //  DataManager.CharacterData.Map = map;
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), mats);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), rations);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), coins);
                DataManager.Player.AdjustLevel(new Data.PatternsFound(), patterns);

                SetState(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, patterns, BatterieResultType.Won)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            3)));
                return;
        }

    }


}


