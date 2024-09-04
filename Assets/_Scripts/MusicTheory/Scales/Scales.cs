using System;
using MusicTheory.ScaleDegrees;
using MusicTheory.Steps;
using MusicTheory.Modes;

namespace MusicTheory.Scales
{
    public interface IScale : IMusicalElement
    {
        ScaleEnum Enum { get; }
        IMode[] Modes => Enum.Modes;
        IStep[] Steps => Enum.Steps;
        IScaleDegree[] ScaleDegrees => Enum.ScaleDegrees;

        string Description => Enum.Description;
        string IMusicalElement.Name => Enum.Name;
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
    }

    [Serializable] public readonly struct Major : IScale { public readonly ScaleEnum Enum => ScaleEnum.Major; }
    [Serializable] public readonly struct JazzMinor : IScale { public readonly ScaleEnum Enum => ScaleEnum.JazzMinor; }
    [Serializable] public readonly struct HarmonicMinor : IScale { public readonly ScaleEnum Enum => ScaleEnum.HarmonicMinor; }
    [Serializable] public readonly struct WholeTone : IScale { public readonly ScaleEnum Enum => ScaleEnum.WholeTone; }
    [Serializable] public readonly struct Diminished : IScale { public readonly ScaleEnum Enum => ScaleEnum.Diminished; }
    [Serializable] public readonly struct Diminished6th : IScale { public readonly ScaleEnum Enum => ScaleEnum.Diminished6th; }
    [Serializable] public readonly struct Chromatic : IScale { public readonly ScaleEnum Enum => ScaleEnum.Chromatic; }
    [Serializable] public readonly struct Pentatonic : IScale { public readonly ScaleEnum Enum => ScaleEnum.Pentatonic; }
    [Serializable] public readonly struct Blues : IScale { public readonly ScaleEnum Enum => ScaleEnum.Blues; }

    public class ScaleEnum : Enumeration
    {
        public ScaleEnum() : base(0, "") { }
        public ScaleEnum(
            int snId,
            string name,
            string desc,
            IMode[] modes,
            IStep[] steps,
            IScaleDegree[] degrees) : base(snId, name)
        {
            Description = desc;
            Modes = modes;
            Steps = steps;
            ScaleDegrees = degrees;
        }

        public readonly string Description;
        public readonly IMode[] Modes;
        public readonly IStep[] Steps;
        public readonly IScaleDegree[] ScaleDegrees;

        public static ScaleEnum Major = new(0, "∆", nameof(Major),
            modes: new IMode[] { new Ionian(), new Dorian(), new Phrygian(), new Lydian(), new MixoLydian(), new Aeolian(), new Locrian() },
            steps: new IStep[] { new Whole(), new Whole(), new Half(), new Whole(), new Whole(), new Whole(), new Half() },
            degrees: new IScaleDegree[] { new _1(), new _2(), new _3(), new P4(), new P5(), new _6(), new _7(), });

        public static ScaleEnum JazzMinor = new(1, "-∆", nameof(JazzMinor),
            modes: new IMode[] { new JazzI(), new JazzII(), new JazzIII(), new JazzIV(), new JazzV(), new JazzVI(), new JazzVII() },
            steps: new IStep[] { new Whole(), new Half(), new Whole(), new Whole(), new Whole(), new Whole(), new Half() },
            degrees: new IScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new P5(), new _6(), new _7() });

        public static ScaleEnum HarmonicMinor = new(2, "±∆", nameof(HarmonicMinor),
            modes: new IMode[] { new HarmonicI(), new HarmonicII(), new HarmonicIII(), new HarmonicIV(), new HarmonicV(), new HarmonicVI(), new HarmonicVII() },
            steps: new IStep[] { new Whole(), new Half(), new Whole(), new Whole(), new Half(), new Skip(), new Half() },
            degrees: new IScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new P5(), new b6(), new _7() });

        public static ScaleEnum WholeTone = new(3, "+", nameof(WholeTone),
            modes: new IMode[] { new Modes.WholeTone() },
            steps: new IStep[] { new Whole(), new Whole(), new Whole(), new Whole(), new Whole(), new Whole() },
            degrees: new IScaleDegree[] { new _1(), new _2(), new _3(), new s4(), new s5(), new b7() });

        public static ScaleEnum Diminished = new(4, "º", nameof(Diminished),
            modes: new IMode[] { new Modes.Diminished(), new Octatonic() },
            steps: new IStep[] { new Whole(), new Half(), new Whole(), new Half(), new Whole(), new Half(), new Whole(), new Half() },
            degrees: new IScaleDegree[] { new _1(), new _2(), new b3(), new P4(), new b5(), new b6(), new _6(), new _7() });

        public static ScaleEnum Diminished6th = new(5, "º6", nameof(Diminished6th),
            modes: new IMode[] { new Modes.Diminished6thI(), new Diminished6thII() },
            steps: new IStep[] { new Whole(), new Whole(), new Half(), new Whole(), new Half(), new Half(), new Whole(), new Half() },
            degrees: new IScaleDegree[] { new _1(), new _2(), new _3(), new P4(), new P5(), new b6(), new _6(), new _7(), });

        public static ScaleEnum Chromatic = new(6, "∞", nameof(Chromatic),
            modes: new IMode[] { new Modes.Chromatic() },
            steps: new IStep[] { new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), new Half(), },
            degrees: new IScaleDegree[] { new _1(), new b2(), new _2(), new b3(), new _3(), new P4(), new b5(), new P5(), new b6(), new _6(), new b7(), new _7() });

        public static ScaleEnum Pentatonic = new(7, "ε", nameof(Pentatonic),
            modes: new IMode[] { new PentatonicMajor(), new PentatonicII(), new PentatonicIII(), new PentatonicIV(), new PentatonicMinor() },
            steps: new IStep[] { new Whole(), new Whole(), new Skip(), new Whole(), new Skip() },
            degrees: new IScaleDegree[] { new _1(), new _2(), new _3(), new P5(), new _6() });

        public static ScaleEnum Blues = new(8, "Bl", nameof(Blues),
            modes: new IMode[] { new Modes.Blues(), new MajorBlues() },
            steps: new IStep[] { new Skip(), new Whole(), new Half(), new Half(), new Skip(), new Whole() },
            degrees: new IScaleDegree[] { new _1(), new b3(), new P4(), new b5(), new P5(), new b7(), });



        // public static implicit operator IScale(ScaleEnum s) => s switch
        // {
        //     _ when s == Major => new Major(),
        //     _ when s == JazzMinor => new JazzMinor(),
        //     _ when s == HarmonicMinor => new HarmonicMinor(),
        //     _ when s == Diminished => new Diminished(),
        //     _ when s == WholeTone => new WholeTone(),
        //     _ when s == Diminished6th => new Diminished6th(),
        //     _ when s == Chromatic => new Chromatic(),
        //     _ when s == Pentatonic => new Pentatonic(),
        //     _ => throw new ArgumentOutOfRangeException()
        // };
    }

}