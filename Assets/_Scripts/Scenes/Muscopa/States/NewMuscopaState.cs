using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Muscopa;
using Datum;
using MusicTheory.RomanNumerals.Diatonic;

public class NewMuscopaState : State
{
    readonly State SubsequentState;
    readonly bool IsPractice;
    readonly MusicTheory.RegionalMode Mode;

    public NewMuscopaState(State subsequentState, IRegion mode, bool isPractice)
    {
        SubsequentState = subsequentState;
        IsPractice = isPractice;
        Mode = mode switch
        {
            Ios => MusicTheory.RegionalMode.Ionian,
            Doria => MusicTheory.RegionalMode.Dorian,
            Phrygia => MusicTheory.RegionalMode.Phrygian,
            Lydia => MusicTheory.RegionalMode.Lydian,
            MixoLydia => MusicTheory.RegionalMode.MixoLydian,
            Aeolia => MusicTheory.RegionalMode.Aeolian,
            Locria => MusicTheory.RegionalMode.Locrian,
            _ => throw new System.Exception(mode.Name)
        };
    }

    public NewMuscopaState(State subsequentState, Sea.RegionEnum mode, bool isPractice)
    {
        SubsequentState = subsequentState;
        IsPractice = isPractice;
        Mode = mode switch
        {
            Sea.RegionEnum.Ionian => MusicTheory.RegionalMode.Ionian,
            Sea.RegionEnum.Dorian => MusicTheory.RegionalMode.Dorian,
            Sea.RegionEnum.Phrygian => MusicTheory.RegionalMode.Phrygian,
            Sea.RegionEnum.Lydian => MusicTheory.RegionalMode.Lydian,
            Sea.RegionEnum.MixoLydian => MusicTheory.RegionalMode.MixoLydian,
            Sea.RegionEnum.Aeolian => MusicTheory.RegionalMode.Aeolian,
            Sea.RegionEnum.Locrian => MusicTheory.RegionalMode.Locrian,
            _ => throw new System.Exception(mode.ToString())
        };
    }

    protected override void EngageState()
    {
        Cam.Io.Camera.transform.SetPositionAndRotation(new Vector3(0, 0, 95), Quaternion.identity);
        var scene = new MuscopaScene(SubsequentState, Mode, IsPractice);
        (scene as IScene).Initialize();


        scene.CardManager.CurrentAnswerSpot = AnswerSpot.Off;
        scene.CardManager.CurrentHandSpot = HandSpot.Off;
        scene.HighlightAnswerSpot();
        scene.HighlightHandSpot();

        SetState(new CardSelectionState(scene));
    }
}
