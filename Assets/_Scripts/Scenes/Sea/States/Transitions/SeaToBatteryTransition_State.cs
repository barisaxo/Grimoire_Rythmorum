using System;
using MusicTheory.Rhythms;
public class SeaToBatteryTransition_State : State
{
    public SeaToBatteryTransition_State() { Fade = false; }

    protected override void PrepareState(Action callback)
    {
        Audio.Ambience.Pause();
        Audio.BGMusic.Pause();
        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.AttackPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.SeaPos = Sea.WorldMapScene.Io.Ship.GO.transform.position;
        Sea.WorldMapScene.Io.Ship.SeaRot = Sea.WorldMapScene.Io.Ship.GO.transform.rotation;
        Sea.WorldMapScene.Io.Board.Swells.DisableSwells();


        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        UnityEngine.Debug.Log(Sea.WorldMapScene.Io.NearestNPC.RegionalMode + " " + DataManager.StandingData.GetLevel(Sea.WorldMapScene.Io.NearestNPC.RegionalMode));
        DataManager.StandingData.AdjustLevel(Sea.WorldMapScene.Io.NearestNPC.RegionalMode, -1);
        UnityEngine.Debug.Log(Sea.WorldMapScene.Io.NearestNPC.RegionalMode + " " + DataManager.StandingData.GetLevel(Sea.WorldMapScene.Io.NearestNPC.RegionalMode));
        var _RhythmSpecs = new RhythmSpecs()
        {
            Time = new FourFour(),
            NumberOfMeasures = DataManager.PlayerShip.ShipStats.NumOfCannons switch
            {
                4 => 1,
                8 => 2,
                _ => 4,
            },
            SubDivisionTier = DataManager.PlayerShip.ShipStats.NumOfCannons switch
            {
                4 => SubDivisionTier.BeatOnly,
                8 => SubDivisionTier.BeatOnly,
                16 => SubDivisionTier.BeatOnly,
                24 => SubDivisionTier.BeatAndD1,
                32 => SubDivisionTier.D1Only,
                48 => SubDivisionTier.D1AndD2,
                64 => SubDivisionTier.D2Only,
                _ => throw new Exception("What is going on here? " + DataManager.PlayerShip.ShipStats.NumOfCannons)
            },
            HasTies = UnityEngine.Random.value > .5f,
            HasRests = UnityEngine.Random.value > .5f,
            HasTriplets = false,
            Tempo = 90
        };

        SetState(new BatterieAndCadence_State(_RhythmSpecs) { Fade = true });
    }
}
