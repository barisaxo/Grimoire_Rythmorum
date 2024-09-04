using MusicTheory.Scales;
using System;

namespace MusicTheory.Modes
{
    public enum ModeDegree { Prime, Second, Third, Fourth, Fifth, Sixth, Seventh }

    public interface IMode : IMusicalElement
    {
        ModeEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct Ionian : IMode { public readonly ModeEnum Enum => ModeEnum.Ionian; }
    [Serializable] public readonly struct Dorian : IMode { public readonly ModeEnum Enum => ModeEnum.Dorian; }
    [Serializable] public readonly struct Phrygian : IMode { public readonly ModeEnum Enum => ModeEnum.Phrygian; }
    [Serializable] public readonly struct Lydian : IMode { public readonly ModeEnum Enum => ModeEnum.Lydian; }
    [Serializable] public readonly struct MixoLydian : IMode { public readonly ModeEnum Enum => ModeEnum.MixoLydian; }
    [Serializable] public readonly struct Aeolian : IMode { public readonly ModeEnum Enum => ModeEnum.Aeolian; }
    [Serializable] public readonly struct Locrian : IMode { public readonly ModeEnum Enum => ModeEnum.Locrian; }

    [Serializable] public readonly struct HarmonicI : IMode { public readonly ModeEnum Enum => ModeEnum.HarmonicI; }
    [Serializable] public readonly struct HarmonicII : IMode { public readonly ModeEnum Enum => ModeEnum.HarmonicII; }
    [Serializable] public readonly struct HarmonicIII : IMode { public readonly ModeEnum Enum => ModeEnum.HarmonicIII; }
    [Serializable] public readonly struct HarmonicIV : IMode { public readonly ModeEnum Enum => ModeEnum.HarmonicIV; }
    [Serializable] public readonly struct HarmonicV : IMode { public readonly ModeEnum Enum => ModeEnum.HarmonicV; }
    [Serializable] public readonly struct HarmonicVI : IMode { public readonly ModeEnum Enum => ModeEnum.HarmonicVI; }
    [Serializable] public readonly struct HarmonicVII : IMode { public readonly ModeEnum Enum => ModeEnum.HarmonicVII; }

    [Serializable] public readonly struct JazzI : IMode { public readonly ModeEnum Enum => ModeEnum.JazzI; }
    [Serializable] public readonly struct JazzII : IMode { public readonly ModeEnum Enum => ModeEnum.JazzII; }
    [Serializable] public readonly struct JazzIII : IMode { public readonly ModeEnum Enum => ModeEnum.JazzIII; }
    [Serializable] public readonly struct JazzIV : IMode { public readonly ModeEnum Enum => ModeEnum.JazzIV; }
    [Serializable] public readonly struct JazzV : IMode { public readonly ModeEnum Enum => ModeEnum.JazzV; }
    [Serializable] public readonly struct JazzVI : IMode { public readonly ModeEnum Enum => ModeEnum.JazzVI; }
    [Serializable] public readonly struct JazzVII : IMode { public readonly ModeEnum Enum => ModeEnum.JazzVII; }

    [Serializable] public readonly struct Diminished : IMode { public readonly ModeEnum Enum => ModeEnum.Diminished; }
    [Serializable] public readonly struct Octatonic : IMode { public readonly ModeEnum Enum => ModeEnum.Octatonic; }

    [Serializable] public readonly struct PentatonicMajor : IMode { public readonly ModeEnum Enum => ModeEnum.PentatonicMajor; }
    [Serializable] public readonly struct PentatonicII : IMode { public readonly ModeEnum Enum => ModeEnum.PentatonicII; }
    [Serializable] public readonly struct PentatonicIII : IMode { public readonly ModeEnum Enum => ModeEnum.PentatonicIII; }
    [Serializable] public readonly struct PentatonicIV : IMode { public readonly ModeEnum Enum => ModeEnum.PentatonicIV; }
    [Serializable] public readonly struct PentatonicMinor : IMode { public readonly ModeEnum Enum => ModeEnum.PentatonicMinor; }

    [Serializable] public readonly struct Diminished6thI : IMode { public readonly ModeEnum Enum => ModeEnum.Diminished6thI; }
    [Serializable] public readonly struct Diminished6thII : IMode { public readonly ModeEnum Enum => ModeEnum.Diminished6thII; }

    [Serializable] public readonly struct Blues : IMode { public readonly ModeEnum Enum => ModeEnum.Blues; }
    [Serializable] public readonly struct MajorBlues : IMode { public readonly ModeEnum Enum => ModeEnum.MajorBlues; }

    [Serializable] public readonly struct WholeTone : IMode { public readonly ModeEnum Enum => ModeEnum.WholeTone; }

    [Serializable] public readonly struct Chromatic : IMode { public readonly ModeEnum Enum => ModeEnum.Chromatic; }

    public class ModeEnum : Enumeration
    {
        public ModeEnum() : base(0, "") { }
        public ModeEnum(
            int sn,
            int id,
            string name,
            ScaleEnum parent,
            ModeDegreeEnum modeDegree) : base(sn: sn, id: id, name)
        {
            ParentScale = parent;
            ModeDegreeEnum = modeDegree;
        }

        public readonly ScaleEnum ParentScale;
        public readonly ModeDegreeEnum ModeDegreeEnum;
        public static ModeEnum Ionian = new(0, 0, nameof(Ionian), ScaleEnum.Major, ModeDegreeEnum.Prime);
        public static ModeEnum Dorian = new(1, 1, nameof(Dorian), ScaleEnum.Major, ModeDegreeEnum.Second);
        public static ModeEnum Phrygian = new(2, 2, nameof(Phrygian), ScaleEnum.Major, ModeDegreeEnum.Third);
        public static ModeEnum Lydian = new(3, 3, nameof(Lydian), ScaleEnum.Major, ModeDegreeEnum.Fourth);
        public static ModeEnum MixoLydian = new(4, 4, nameof(MixoLydian), ScaleEnum.Major, ModeDegreeEnum.Fifth);
        public static ModeEnum Aeolian = new(5, 5, nameof(Aeolian), ScaleEnum.Major, ModeDegreeEnum.Sixth);
        public static ModeEnum Locrian = new(6, 6, nameof(Locrian), ScaleEnum.Major, ModeDegreeEnum.Seventh);
        public static ModeEnum HarmonicI = new(7, 0, nameof(HarmonicI), ScaleEnum.HarmonicMinor, ModeDegreeEnum.Prime);
        public static ModeEnum HarmonicII = new(8, 1, nameof(HarmonicII), ScaleEnum.HarmonicMinor, ModeDegreeEnum.Second);
        public static ModeEnum HarmonicIII = new(9, 2, nameof(HarmonicIII), ScaleEnum.HarmonicMinor, ModeDegreeEnum.Third);
        public static ModeEnum HarmonicIV = new(10, 3, nameof(HarmonicIV), ScaleEnum.HarmonicMinor, ModeDegreeEnum.Fourth);
        public static ModeEnum HarmonicV = new(11, 4, nameof(HarmonicV), ScaleEnum.HarmonicMinor, ModeDegreeEnum.Fifth);
        public static ModeEnum HarmonicVI = new(12, 5, nameof(HarmonicVI), ScaleEnum.HarmonicMinor, ModeDegreeEnum.Sixth);
        public static ModeEnum HarmonicVII = new(13, 6, nameof(HarmonicVII), ScaleEnum.HarmonicMinor, ModeDegreeEnum.Seventh);
        public static ModeEnum JazzI = new(14, 0, nameof(JazzI), ScaleEnum.JazzMinor, ModeDegreeEnum.Prime);
        public static ModeEnum JazzII = new(15, 1, nameof(JazzII), ScaleEnum.JazzMinor, ModeDegreeEnum.Second);
        public static ModeEnum JazzIII = new(16, 2, nameof(JazzIII), ScaleEnum.JazzMinor, ModeDegreeEnum.Third);
        public static ModeEnum JazzIV = new(17, 3, nameof(JazzIV), ScaleEnum.JazzMinor, ModeDegreeEnum.Fourth);
        public static ModeEnum JazzV = new(18, 4, nameof(JazzV), ScaleEnum.JazzMinor, ModeDegreeEnum.Fifth);
        public static ModeEnum JazzVI = new(19, 5, nameof(JazzVI), ScaleEnum.JazzMinor, ModeDegreeEnum.Sixth);
        public static ModeEnum JazzVII = new(20, 6, nameof(JazzVII), ScaleEnum.JazzMinor, ModeDegreeEnum.Seventh);
        public static ModeEnum Diminished = new(21, 0, nameof(Diminished), ScaleEnum.Diminished, ModeDegreeEnum.Prime);
        public static ModeEnum Octatonic = new(22, 1, nameof(Octatonic), ScaleEnum.Diminished, ModeDegreeEnum.Second);
        public static ModeEnum PentatonicMajor = new(23, 0, nameof(PentatonicMajor), ScaleEnum.Pentatonic, ModeDegreeEnum.Prime);
        public static ModeEnum PentatonicII = new(24, 1, nameof(PentatonicII), ScaleEnum.Pentatonic, ModeDegreeEnum.Second);
        public static ModeEnum PentatonicIII = new(25, 2, nameof(PentatonicIII), ScaleEnum.Pentatonic, ModeDegreeEnum.Third);
        public static ModeEnum PentatonicIV = new(26, 3, nameof(PentatonicIV), ScaleEnum.Pentatonic, ModeDegreeEnum.Fourth);
        public static ModeEnum PentatonicMinor = new(27, 4, nameof(PentatonicMinor), ScaleEnum.Pentatonic, ModeDegreeEnum.Fifth);
        public static ModeEnum Diminished6thI = new(28, 0, nameof(Diminished6thI), ScaleEnum.Diminished6th, ModeDegreeEnum.Prime);
        public static ModeEnum Diminished6thII = new(29, 1, nameof(Diminished6thII), ScaleEnum.Diminished6th, ModeDegreeEnum.Second);
        public static ModeEnum Blues = new(30, 0, nameof(Blues), ScaleEnum.Blues, ModeDegreeEnum.Prime);
        public static ModeEnum MajorBlues = new(31, 1, nameof(MajorBlues), ScaleEnum.Blues, ModeDegreeEnum.Second);
        public static ModeEnum WholeTone = new(32, 0, nameof(WholeTone), ScaleEnum.WholeTone, ModeDegreeEnum.Prime);
        public static ModeEnum Chromatic = new(33, 0, nameof(Chromatic), ScaleEnum.Chromatic, ModeDegreeEnum.Prime);
    }

    public class ModeDegreeEnum : Enumeration
    {
        public ModeDegreeEnum() : base(0, "") { }
        public ModeDegreeEnum(int snId, string name) : base(snId, name) { }

        public static ModeDegreeEnum Prime = new(0, nameof(Prime));
        public static ModeDegreeEnum Second = new(1, nameof(Second));
        public static ModeDegreeEnum Third = new(2, nameof(Third));
        public static ModeDegreeEnum Fourth = new(3, nameof(Fourth));
        public static ModeDegreeEnum Fifth = new(4, nameof(Fifth));
        public static ModeDegreeEnum Sixth = new(5, nameof(Sixth));
        public static ModeDegreeEnum Seventh = new(6, nameof(Seventh));

        public static int Count => Length<ModeDegreeEnum>();
    }
}