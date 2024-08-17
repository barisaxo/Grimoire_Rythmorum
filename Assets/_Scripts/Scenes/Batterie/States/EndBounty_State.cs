
using MusicTheory;
using System;
using System.Collections.Generic;

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
                               * Datum.Manager.Io.Skill.GetBonusRatio(new Datum.Apophenia()));

        Sea.NPCShip NPC = Sea.WorldMapScene.Io.NearestNPC;

        if (NPC is not null && NPC.SceneObject.GO == null)
        {
            if (Sea.WorldMapScene.Io.NPCShips.Contains(NPC))
                Sea.WorldMapScene.Io.NPCShips.Remove(NPC);

            Sea.WorldMapScene.Io.RockTheBoat.RemoveBoat(NPC.SceneObject.GO.transform);
            NPC.DestroySceneObject();
        }

        List<Sea.ISceneObject> sos = new();

        foreach (var so in Sea.WorldMapScene.Io.SceneObjects)
            if (so is Sea.BountyShip)
            {
                UnityEngine.Debug.Log("removing " + so.GO.name);
                sos.Add(so);
            }

        foreach (var so in sos)
            Sea.WorldMapScene.Io.SceneObjects.Remove(so);

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
                // DataManager.Standings.AdjustLevel(Quest.Standing, 1);
                DataManager.Quests.GetQuest(new Datum.Bounty()).Complete = true;

                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Material(), mats /= 2);
                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Ration(), rations /= 2);
                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), coins /= 2);
                DataManager.Player.AdjustLevel(new Datum.PatternsFound(), patterns);

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
                DataManager.Quests.AdjustLevel(new Datum.Bounty(), 0);
                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Material(), mats /= -2);
                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Ration(), rations /= -2);
                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), coins /= -2);

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
                DataManager.Quests.AdjustLevel(new Datum.Bounty(), 0);
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
                DataManager.Quests.AdjustLevel(new Datum.Bounty(), 0);
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

                SetState(//TODO 7/5/24 - is this still todo?
                    new CameraPan_State(
                        new DialogStart_State(
                            new EndBounty_Dialogue(0, 0, 0, 0, BatterieResultType.NMEscaped)),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        5));

                return;

            case BatterieResultType.Won:
                // DataManager.Standings.AdjustLevel(Quest.Standing, 1);
                DataManager.Quests.GetQuest(new Datum.Bounty()).Complete = true;

                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Material(), mats);
                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Ration(), rations);
                Datum.Manager.Io.Inventory.AdjustLevel(new Datum.Gold(), coins);

                DataManager.Player.AdjustLevel(new Datum.PatternsFound(), patterns);
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


