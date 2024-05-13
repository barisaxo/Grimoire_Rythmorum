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
        var _RhythmSpecs = new RhythmSpecs()
        {
            Time = new FourFour(),
            NumberOfMeasures = 4,
            SubDivisionTier = SubDivisionTier.D1Only,
            HasTies = UnityEngine.Random.value > .5f,
            HasRests = UnityEngine.Random.value > .5f,
            HasTriplets = false,
            Tempo = 90
        };

        SetState(new BatterieAndCadence_State(_RhythmSpecs) { Fade = true });
        // new CameraPan_State(
        // subsequentState: new BatterieAndCadence_State(_RhythmSpecs),
        // pan: new UnityEngine.Vector3(-20f, Cam.Io.Camera.transform.rotation.eulerAngles.y + 180, Cam.Io.Camera.transform.rotation.eulerAngles.z),
        // strafe: Cam.Io.Camera.transform.position + (UnityEngine.Vector3.up * 7),
        // speed: 5));
    }
}
