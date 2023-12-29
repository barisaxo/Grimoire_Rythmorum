
using MusicTheory;
using System;

public class EndBatterie_State : State
{
    public EndBatterie_State(BatteriePack pack)
    {
        Pack = pack;
    }

    readonly BatteriePack Pack;

    int damage => Pack.TotalErrors;
    int coins = 0;
    int mats = 0;
    int rations = 0;
    bool map;

    int level => Data.GamePlay.CurrentLevel switch
    {
        RegionalMode.Lydian => 2,
        RegionalMode.Aeolian => 2,
        RegionalMode.Phrygian => 3,
        RegionalMode.Locrian => 3,
        _ => 1,
    };

    protected override void PrepareState(Action callback)
    {
        // Pack.BHUD.SelfDestruct();

        // coins = (int)((level + 25f) * 5.55f * UnityEngine.Random.Range(.15f, 1) * (float)((100 - Result()) * .01f));
        // mats = (int)(((level + 15f) * 2.55f) * UnityEngine.Random.Range(.15f, 1) * (float)((100 - Result()) * .01f));
        // rations = (int)(1 + ((level + 5f) * UnityEngine.Random.Range(.5f, 1) * (float)((100 - Result()) * .01f)));

        UnityEngine.GameObject.Destroy(Pack.NME);
        UnityEngine.GameObject.Destroy(Pack.Ship);
        UnityEngine.GameObject.Destroy(Pack.NMEFire);
        UnityEngine.GameObject.Destroy(Pack.ShipFire);

        map = FoundMap();

        callback();
        return;

        int Result()
        {
            if (Pack.Spammed) return -99;
            else return Pack.TotalErrors;
        }

        bool FoundMap()
        {
            if (Data.CharacterData.Map) return false;
            if (Data.GamePlay.Batterie_Difficulty == 0) return true;

            float chance = UnityEngine.Random.value;
            float percent = ((float)(6 - (int)Data.GamePlay.Batterie_Difficulty));//TODO / (float)(Pack.SeaScene.NPC.Ships.Count + 1);
            //Debug.Log(nameof(chance) + ": " + chance + ", " + nameof(percent) + ": " + percent + ", num of Pirate ships: " + (Board.PirateShipCount() + 1));
            return (chance < percent);
        }
    }

    protected override void EngageState()
    {
        // Board.BoardHUD.UpdatePlayerHealth(Data.CharacterData);
        // SetStateDirectly(new SeaSceneTest_State());
        // return;
        switch (Pack.ResultType)
        {
            case BatterieResultType.NMESurrender:
                Data.CharacterData.Materials += mats /= 2;
                Data.CharacterData.Rations += rations /= 2;
                Data.CharacterData.Coins += coins /= 2;
                SetStateDirectly(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, damage, false, BatterieResultType.NMESurrender))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Surrender:
                Data.CharacterData.Materials -= mats = Data.CharacterData.Materials /= 2;
                Data.CharacterData.Rations -= rations = Data.CharacterData.Rations /= 2;
                Data.CharacterData.Coins -= coins = Data.CharacterData.Coins /= 2;
                SetStateDirectly(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, damage, false, BatterieResultType.Surrender))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Spam:
                SetStateDirectly(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, damage, false, BatterieResultType.Spam))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Fled:
                SetStateDirectly(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, damage, false, BatterieResultType.Fled)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            3)));

                return;

            case BatterieResultType.NMEscaped:
                SetStateDirectly(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, damage, false, BatterieResultType.NMEscaped))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Lost://TODO
                SetStateDirectly(
                    new CameraPan_State(
                        new DialogStart_State(
                            new EndBatterie_Dialogue(0, 0, 0, damage, false, BatterieResultType.NMEscaped)),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));

                return;

            case BatterieResultType.Won:
                if (map) Data.CharacterData.Map = true;
                Data.CharacterData.Materials += mats;
                Data.CharacterData.Rations += rations;
                Data.CharacterData.Coins += coins;

                SetStateDirectly(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, damage, map, BatterieResultType.Won)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            3)));
                return;
        }

    }


}


