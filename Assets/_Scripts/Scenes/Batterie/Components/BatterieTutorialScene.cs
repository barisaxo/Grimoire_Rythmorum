using System;
using UnityEngine;
using Batterie;
using MusicTheory.Rhythms;

public class BatterieTutorialScene
{
    public BatterieTutorialScene(Action tick, RhythmSpecs specs, Measure measure)
    {
        Pack = new(specs);
        Tick = tick;
        BatterieAudio = Audio.AudioManager.Io.Batterie;
    }

    public void Initialize()
    {
        BatterieFeedback = new();
        Pack.Initialize(HandleHit, BatterieFeedback, Tick);
        CountOffFeedBack = new(Pack.MusicSheet.RhythmSpecs.Time.GetCounts());
        BatterieFeedback.UpdateLoop();
        CountOffFeedBack.UpdateLoop();
    }

    public void SelfDestruct()
    {
        Background.SelfDestruct();
    }

    public Action Tick;
    readonly Audio.Batterie_AudioSystem BatterieAudio;
    public BatteriePack Pack;

    public BatterieFeedback BatterieFeedback;
    public CountOffFeedback CountOffFeedBack;

    Card _background;
    Card Background => _background ??= new Card(nameof(Background), null)
        .SetImagePosition(Vector2.zero)
        .SetImageSize(new Vector2(Cam.Io.UICamera.scaledPixelWidth * .8f, Cam.Io.UICamera.scaledPixelHeight * .8f))
        .SetCanvasSortingOrder(0)
        .SetImageColor(new Color(0, 0, 0, .25f));

    private void HandleHit(Batterie.Hit hit)
    {
        switch (hit)
        {
            case Hit.Hit:
                Pack.GoodHits++;
                BatterieAudio.Hit();
                break;
            case Hit.Miss:
                Pack.MissedHits++;
                BatterieAudio.Miss();
                break;
            case Hit.BadHit:
                Pack.ErroneousAttacks++;
                Audio.AudioManager.Io.Batterie.MissStick();
                break;
            case Hit.Break:
                break;
        }
    }

}