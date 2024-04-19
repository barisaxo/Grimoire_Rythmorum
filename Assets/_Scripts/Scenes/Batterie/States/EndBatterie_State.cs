
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
    bool map;

    int level => DataManager.GamePlay.CurrentLevel switch
    {
        RegionalMode.Lydian => 2,
        RegionalMode.Aeolian => 2,
        RegionalMode.Phrygian => 3,
        RegionalMode.Locrian => 3,
        _ => 1,
    };

    protected override void PrepareState(Action callback)
    {
        Scene.SelfDestruct();
        // Pack.BHUD.SelfDestruct();

        // coins = (int)((level + 25f) * 5.55f * UnityEngine.Random.Range(.15f, 1) * (float)((100 - Result()) * .01f));
        // mats = (int)(((level + 15f) * 2.55f) * UnityEngine.Random.Range(.15f, 1) * (float)((100 - Result()) * .01f));
        // rations = (int)(1 + ((level + 5f) * UnityEngine.Random.Range(.5f, 1) * (float)((100 - Result()) * .01f)));

        // UnityEngine.GameObject.Destroy(Pack.NME);
        // UnityEngine.GameObject.Destroy(Pack.Ship);
        Sea.WorldMapScene.Io.Ship.GO.transform.SetPositionAndRotation(
            Sea.WorldMapScene.Io.Ship.SeaPos,
            Sea.WorldMapScene.Io.Ship.SeaRot
        );
        UnityEngine.GameObject.Destroy(Scene.NMEFire);
        UnityEngine.GameObject.Destroy(Scene.ShipFire);

        map = FoundMap();

        callback();
        return;

        int Result()
        {
            if (Scene.Pack.Spammed) return -99;
            else return Scene.Pack.TotalErrors;
        }

        bool FoundMap()
        {
            if (DataManager.CharacterData.Map) return false;
            if (DataManager.GamePlay.Batterie_Difficulty == 0) return true;

            float chance = UnityEngine.Random.value;
            float percent = ((float)(6 - (int)DataManager.GamePlay.Batterie_Difficulty));//TODO / (float)(Pack.SeaScene.NPC.Ships.Count + 1);
            //Debug.Log(nameof(chance) + ": " + chance + ", " + nameof(percent) + ": " + percent + ", num of Pirate ships: " + (Board.PirateShipCount() + 1));
            return (chance < percent);
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
                DataManager.CharacterData.Materials += mats /= 2;
                DataManager.CharacterData.Rations += rations /= 2;
                DataManager.CharacterData.Coins += coins /= 2;
                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, false, BatterieResultType.NMESurrender))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Surrender:
                DataManager.CharacterData.Materials -= mats = DataManager.CharacterData.Materials /= 2;
                DataManager.CharacterData.Rations -= rations = DataManager.CharacterData.Rations /= 2;
                DataManager.CharacterData.Coins -= coins = DataManager.CharacterData.Coins /= 2;
                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, false, BatterieResultType.Surrender))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Spam:
                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, false, BatterieResultType.Spam))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Fled:
                SetState(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, false, BatterieResultType.Fled)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            3)));

                return;

            case BatterieResultType.NMEscaped:
                SetState(
                    new CameraPan_State(
                        new NPCSailAway_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                0, 0, 0, false, BatterieResultType.NMEscaped))),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));
                return;

            case BatterieResultType.Lost://TODO
                SetState(
                    new CameraPan_State(
                        new DialogStart_State(
                            new EndBatterie_Dialogue(0, 0, 0, false, BatterieResultType.NMEscaped)),
                        Cam.StoredCamRot,
                        Cam.StoredCamPos,
                        3));

                return;

            case BatterieResultType.Won:
                if (map) DataManager.CharacterData.Map = true;
                DataManager.CharacterData.Materials += mats;
                DataManager.CharacterData.Rations += rations;
                DataManager.CharacterData.Coins += coins;

                SetState(
                    new MoveNPCOffScreen_State(
                        new CameraPan_State(
                            new DialogStart_State(new EndBatterie_Dialogue(
                                coins, mats, rations, map, BatterieResultType.Won)),
                            Cam.StoredCamRot,
                            Cam.StoredCamPos,
                            3)));
                return;
        }

    }


}


