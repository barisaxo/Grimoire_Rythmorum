using System;
using System.Collections;
using System.Collections.Generic;
using Dialog;

public class SeaToPuzzleTransition_State : State
{
    readonly State SubsequentState;
    public SeaToPuzzleTransition_State(State state)
    {
        SubsequentState = state;
    }

    protected override void PrepareState(Action callback)
    {
        Audio.Ambience.Pause();
        Audio.BGMusic.Pause();
        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.AttackPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        // SetStateDirectly();
        // SetStateDirectly(
        //     new CameraPan_State(new NewPuzzleSetup_State(PuzzleDifficulty.Free, SubsequentState),
        //         new UnityEngine.Vector3(Cam.Io.Camera.transform.position.x, 15, Cam.Io.Camera.transform.position.z),
        //         Vector3.up * 90,
        //         3));
    }


}
