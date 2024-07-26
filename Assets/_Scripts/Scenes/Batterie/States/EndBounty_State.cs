
using MusicTheory;
using System;

public class EndBounty_State : State
{
    public EndBounty_State(BatterieScene scene, BatterieResultType result, Quests.BountyQuest quest)
    {
        Scene = scene;
        Scene.Pack.SetResultType(result);
        Quest = quest;
        Quest.Complete = true;
    }
    readonly BatterieScene Scene;
    readonly Quests.BountyQuest Quest;
    // int damage => Scene.Pack.TotalErrors;
    int coins = 0;
    int mats = 0;
    int rations = 0;
    int patterns = 0;
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

        coins = (int)((level + 50f) * 5.55f * UnityEngine.Random.Range(.15f, 1) * (float)((100 - Result()) * .01f));
        mats = (int)(((level + 30f) * 2.55f) * UnityEngine.Random.Range(.15f, 1) * (float)((100 - Result()) * .01f));
        rations = (int)(1 + ((level + 10f) * UnityEngine.Random.Range(.5f, 1) * (float)((100 - Result()) * .01f)));
        patterns = (int)((Scene.NMEShipStats.HullStrength + Scene.NMEShipStats.VolleyDamage) *
                               (Scene.Pack.HasCritThisBattery ? 1.45f : 1f)
                               * Data.Manager.Io.Skill.GetBonusRatio(new Data.Apophenia()));

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
                DataManager.Standings.AdjustLevel(Quest.Standing, 1);

                Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), mats /= 2);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), rations /= 2);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), coins /= 2);
                DataManager.Player.AdjustLevel(new Data.PatternsFound(), patterns);

                SetState(
                  new MoveNPCOffScreen_State(
                      new CameraPan_State(
                          new DialogStart_State(new EndBounty_Dialogue(
                              coins, mats, rations, patterns, BatterieResultType.Won)),
                          Cam.StoredCamRot,
                          Cam.StoredCamPos,
                          5)));
                // SetState(
                //     new CameraPan_State(
                //         new NPCSailAway_State(
                //             new DialogStart_State(new EndBounty_Dialogue(
                //                 coins, mats, rations, BatterieResultType.NMESurrender))),
                //         Cam.StoredCamRot,
                //         Cam.StoredCamPos,
                //         3));
                return;

            case BatterieResultType.Surrender:
                DataManager.Standings.AdjustLevel(Quest.Standing, -1);

                Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), mats /= -2);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), rations /= -2);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), coins /= -2);

                SetState(
                  new MoveNPCOffScreen_State(
                      new CameraPan_State(
                          new DialogStart_State(new EndBounty_Dialogue(
                              0, 0, 0, 0, BatterieResultType.Won)),
                          Cam.StoredCamRot,
                          Cam.StoredCamPos,
                          5)));
                // SetState(
                //     new CameraPan_State(
                //         new NPCSailAway_State(
                //             new DialogStart_State(new EndBounty_Dialogue(
                //                 coins, mats, rations, BatterieResultType.Surrender))),
                //         Cam.StoredCamRot,
                //         Cam.StoredCamPos,
                //         3));
                return;

            case BatterieResultType.Spam:
                DataManager.Standings.AdjustLevel(Quest.Standing, -1);

                SetState(
                  new MoveNPCOffScreen_State(
                      new CameraPan_State(
                          new DialogStart_State(new EndBounty_Dialogue(
                               0, 0, 0, 0, BatterieResultType.Won)),
                          Cam.StoredCamRot,
                          Cam.StoredCamPos,
                          5)));
                // SetState(
                //     new CameraPan_State(
                //         new NPCSailAway_State(
                //             new DialogStart_State(new EndBounty_Dialogue(
                //                 0, 0, 0, BatterieResultType.Spam))),
                //         Cam.StoredCamRot,
                //         Cam.StoredCamPos,
                //         3));
                return;

            case BatterieResultType.Fled:
                DataManager.Standings.AdjustLevel(Quest.Standing, -1);

                SetState(
                  new MoveNPCOffScreen_State(
                      new CameraPan_State(
                          new DialogStart_State(new EndBounty_Dialogue(
                              0, 0, 0, 0, BatterieResultType.Won)),
                          Cam.StoredCamRot,
                          Cam.StoredCamPos,
                          5)));

                // SetState(
                //     new MoveNPCOffScreen_State(
                //         new CameraPan_State(
                //             new DialogStart_State(new EndBounty_Dialogue(
                //                 0, 0, 0, BatterieResultType.Fled)),
                //             Cam.StoredCamRot,
                //             Cam.StoredCamPos,
                //             3)));

                return;

            case BatterieResultType.NMEscaped:
                DataManager.Standings.AdjustLevel(Quest.Standing, -1);
                SetState(
                  new MoveNPCOffScreen_State(
                      new CameraPan_State(
                          new DialogStart_State(new EndBounty_Dialogue(
                              0, 0, 0, 0, BatterieResultType.Won)),
                          Cam.StoredCamRot,
                          Cam.StoredCamPos,
                          5)));
                // SetState(
                //     new CameraPan_State(
                //         new NPCSailAway_State(
                //             new DialogStart_State(new EndBounty_Dialogue(
                //                 0, 0, 0, BatterieResultType.NMEscaped))),
                //         Cam.StoredCamRot,
                //         Cam.StoredCamPos,
                //         3));
                return;

            case BatterieResultType.Lost:

                SetState(//TODO
                    new CameraPan_State(
                        new DialogStart_State(
                            new EndBounty_Dialogue(0, 0, 0, 0, BatterieResultType.NMEscaped)),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        5));

                return;

            case BatterieResultType.Won:
                DataManager.Standings.AdjustLevel(Quest.Standing, 1);

                Data.Manager.Io.Inventory.AdjustLevel(new Data.Material(), mats);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Ration(), rations);
                Data.Manager.Io.Inventory.AdjustLevel(new Data.Gold(), coins);

                DataManager.Player.AdjustLevel(new Data.PatternsFound(), patterns);
                SetState(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBounty_Dialogue(
                                coins, mats, rations, patterns, BatterieResultType.Won)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            5)));
                return;
        }

    }


}


