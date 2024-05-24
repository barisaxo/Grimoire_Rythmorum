
using MusicTheory;
using System;

public class EndBounty_State : State
{
    public EndBounty_State(BatterieScene scene, BatterieResultType result, Quests.BountyQuest quest)
    {
        Scene = scene;
        Scene.Pack.SetResultType(result);
        Quest = quest;
    }
    readonly BatterieScene Scene;
    readonly Quests.BountyQuest Quest;
    // int damage => Scene.Pack.TotalErrors;
    int coins = 0;
    int mats = 0;
    int rations = 0;
    // bool map;

    int level => (Scene.NMEShipStats.HullStats.Hull.ID * Scene.NMEShipStats.HullStats.Hull.ID) + 1;
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
        Scene.SelfDestruct();
        // Pack.BHUD.SelfDestruct();

        coins = (int)((level + 25f) * 5.55f * UnityEngine.Random.Range(.15f, 1) * (float)((100 - Result()) * .01f));
        mats = (int)(((level + 15f) * 2.55f) * UnityEngine.Random.Range(.15f, 1) * (float)((100 - Result()) * .01f));
        rations = (int)(1 + ((level + 5f) * UnityEngine.Random.Range(.5f, 1) * (float)((100 - Result()) * .01f)));

        // UnityEngine.GameObject.Destroy(Pack.NME);
        // UnityEngine.GameObject.Destroy(Pack.Ship);
        Sea.WorldMapScene.Io.Ship.GO.transform.SetPositionAndRotation(
            Sea.WorldMapScene.Io.Ship.SeaPos,
            Sea.WorldMapScene.Io.Ship.SeaRot
        );
        UnityEngine.Object.Destroy(Scene.NMEFire);
        UnityEngine.Object.Destroy(Scene.ShipFire);

        callback();
        return;

        int Result()
        {
            if (Scene.Pack.Spammed) return -99;
            else return Scene.Pack.TotalErrors;
        }
    }

    protected override void EngageState()
    {
        // Board.BoardHUD.UpdatePlayerHealth(Data.CharacterData);
        // SetState(new SeaSceneTest_State());
        // return;
        switch (Scene.Pack.ResultType)
        {
            case BatterieResultType.NMESurrender:
                DataManager.StandingData.AdjustLevel(Quest.Standing, 1);

                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Material(), mats /= 2);
                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Ration(), rations /= 2);
                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), coins /= 2);

                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBounty_Dialogue(
                                coins, mats, rations, BatterieResultType.NMESurrender))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Surrender:
                DataManager.StandingData.AdjustLevel(Quest.Standing, -1);

                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Material(), mats /= -2);
                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Ration(), rations /= -2);
                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), coins /= -2);

                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBounty_Dialogue(
                                coins, mats, rations, BatterieResultType.Surrender))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Spam:
                DataManager.StandingData.AdjustLevel(Quest.Standing, -1);

                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBounty_Dialogue(
                                0, 0, 0, BatterieResultType.Spam))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Fled:
                DataManager.StandingData.AdjustLevel(Quest.Standing, -1);

                SetState(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBounty_Dialogue(
                                0, 0, 0, BatterieResultType.Fled)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            3)));

                return;

            case BatterieResultType.NMEscaped:
                DataManager.StandingData.AdjustLevel(Quest.Standing, -1);

                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBounty_Dialogue(
                                0, 0, 0, BatterieResultType.NMEscaped))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Lost:

                SetState(//TODO
                    new CameraPan_State(
                        new DialogStart_State(
                            new EndBounty_Dialogue(0, 0, 0, BatterieResultType.NMEscaped)),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));

                return;

            case BatterieResultType.Won:
                DataManager.StandingData.AdjustLevel(Quest.Standing, 1);

                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Material(), mats);
                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Ration(), rations);
                Data.Two.Manager.Io.Inventory.AdjustLevel(new Data.Two.Gold(), coins);

                SetState(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBounty_Dialogue(
                                coins, mats, rations, BatterieResultType.Won)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            3)));
                return;
        }

    }


}


