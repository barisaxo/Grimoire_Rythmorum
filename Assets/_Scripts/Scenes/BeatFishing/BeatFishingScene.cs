using System;
using MusicTheory.Rhythms;
using Batterie;
using SheetMusic;
using Datum.BeatFishing;
using UnityEngine;
using Rhythm;

namespace BeatFishing
{
    public class BeatFishingScene : IScene
    {
        public BeatFishingScene(State subsequentState)
        {
            SubsequentState = subsequentState;
        }

        void IScene.SetUp()
        {
            Spool = FishingReel.CreateChild(nameof(Spool), FishingReel.Canvas)
                .SetImageColor(new Color(0, .6f, .9f))
                .SetImageSprite(Resources.Load<Sprite>("Sprites/Fishing/Spool"))
                .SetImagePosition(Vector2.zero)
                .SetImageSize(Vector2.one * 5)
                .SetCanvasSortingOrder(1)
                ;

            Reel = FishingReel.CreateChild(nameof(Reel), FishingReel.Canvas)
                .SetImageColor(new Color(1, .7f, 0))
                .SetImageSprite(Resources.Load<Sprite>("Sprites/Fishing/Reel"))
                .SetImagePosition(Vector2.zero)
                .SetImageSize(Vector2.one * 5)
                .SetCanvasSortingOrder(2)
                ;

            Handle = FishingReel.CreateChild(nameof(Handle), FishingReel.Canvas)
                .SetImageColor(new Color(.7f, .7f, 0))
                .SetImageSprite(Resources.Load<Sprite>("Sprites/Fishing/ReelHandle"))
                .SetImagePosition(Vector2.zero)
                .SetImageSize(Vector2.one * 5)
                .SetCanvasSortingOrder(3)
                ;
        }

        void IScene.Destroy()
        {
            MusicSheet.SelfDestruct();
            FishingReel.SelfDestruct();
        }

        public IHud Hud { get; } = new NoHud();

        public readonly State SubsequentState;
        public RhythmSpecs Specs;
        public Synchronizer Synchro;
        public RhythmInputAnalyzer Analyzer;
        public BatterieFeedback BatterieFeedback;
        public MappedBeat[] BeatMap;
        public MusicSheet MusicSheet;

        public bool Reeling, Spooling;

        public int Score = 20;
        public int Difficulty;
        public int Counter = 1;
        public int count = 3;

        private Card _fishingReel;
        public Card FishingReel => _fishingReel ??= new(nameof(FishingReel), null);
        public Card Reel;
        public Card Handle;
        public Card Spool;

    }
}