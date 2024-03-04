using UnityEngine;
using SheetMusic;
using Batterie;
using MusicTheory.Rhythms;
using System;

public class Batterie_State : State
{
    public Batterie_State(RhythmSpecs specs)
    {
        Specs = specs;
    }

    RhythmSpecs Specs;
    MusicSheet MusicSheet;
    Synchronizer Synchro;
    CountOffFeedback CountOffFeedBack;
    Note[] CountOffNotes;
    MappedBeat[] CountOffBeatmap;
    BatterieInputAnalyzer Analyzer;
    BatterieFeedback BatterieFeedback;

    public int Counter { get; private set; }
    bool CountingOff = true;
    bool Playing = false;

    protected override void PrepareState(Action callback)
    {
        MusicSheet = new MusicSheet() { RhythmSpecs = Specs };
        MusicSheet.RhythmSpecs.Time.GenerateRhythmCells(MusicSheet);
        MusicSheet.GetNotes();
        MusicSheet.DrawRhythms();
        MusicSheet.BeatMap = MusicSheet.Notes.MapBeats(Specs.Tempo);
        Synchro = new(Specs.Time.GetQuantizement(), Specs.Tempo);
        CountOffNotes = CountOff.GetNotes(Specs.Time);
        CountOffBeatmap = CountOffNotes.MapBeats(Specs.Tempo);
        CountOffFeedBack = new(Specs.Time.GetCounts());
        BatterieFeedback = new();
        Analyzer = new(BatterieFeedback.CreateCard, HandleHit, 5, MusicSheet.BeatMap);
        Analyzer.SetUp();

        BatterieFeedback.UpdateLoop();
        CountOffFeedBack.UpdateLoop();
        Synchro.TickEvent += Tick;
        Counter = 1;
        MonoHelper.OnUpdate += SpaceBar;
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        Synchro.KeepTime();
    }

    protected override void DisengageState()
    {
        Synchro.TickEvent -= Tick;
        Synchro.BeatEvent -= Click;
        MonoHelper.OnUpdate -= SpaceBar;

        BatterieFeedback.Running = false;
        Audio.Batterie.Stop();
        MusicSheet.SelfDestruct();
    }

    void Tick()
    {
        if (CountingOff)
        {
            CountOffTimeEvent();
            if (++Counter == CountOffBeatmap.Length - 1)
            {
                CountingOff = false; Playing = true; Counter = 0;
                MonoHelper.OnUpdate += Analyzer.Tick;
                Synchro.BeatEvent += Click;
                Analyzer.Start();
                Audio.Batterie.Miss();
            }
            return;
        }

        if (Playing)
        {
            BatterieTimeEvent();
            Playing = !(++Counter >= MusicSheet.BeatMap.Length);
        }

        if (!Playing && !CountingOff)
        {
            Audio.Batterie.Stop();
            Synchro.Stop();
            MonoHelper.OnUpdate -= Analyzer.Tick;
        }
    }

    void CountOffTimeEvent()
    {
        switch (CountOffBeatmap[Counter].NoteFunction)
        {
            case NoteFunction.Attack:
                CountOffFeedBack.ReadCountOff();
                Audio.Batterie.PlayClick();
                break;

            case NoteFunction.Hold:
                Audio.Batterie.PlayClick();
                break;
        }
    }

    void BatterieTimeEvent()
    {
        switch (MusicSheet.BeatMap[Counter].NoteFunction)
        {
            case NoteFunction.Attack:
                Audio.Batterie.PlaySnareRoll();
                break;

            case NoteFunction.Rest:
                Audio.Batterie.RestSnareRoll();
                break;

            case NoteFunction.Hold:
                break;
        }
    }

    void Click()
    {
        Audio.Batterie.PlayClick();
    }

    protected override void Clicked(MouseAction action, Vector3 mousePos)
    {
        switch (action)
        {
            case MouseAction.LUp:
                Analyzer.InputUpAction();
                break;

            case MouseAction.LDown:
                Analyzer.InputDownAction();
                break;
        }
    }

    protected void SpaceBar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Analyzer.InputDownAction();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Analyzer.InputUpAction();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetState(new Batterie_State(Specs));
        }
    }

    private void HandleHit(Batterie.Hit hit)
    {
        switch (hit)
        {
            case Hit.Hit:
                Audio.Batterie.Hit();
                break;
            case Hit.Miss:
                Audio.Batterie.Miss();
                break;
            case Hit.BadHit:
                Audio.Batterie.MissStick();
                break;
            case Hit.Break:
                break;
        }
    }
}
