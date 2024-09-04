using System;

namespace MusicTheory.ScaleDegrees
{

    public interface IFunction : IMusicalElement
    {
        FunctionEnum Enum { get; }
        string IMusicalElement.Name => Enum.Name;
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
    }

    [Serializable] public readonly struct Tonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Tonic; }
    [Serializable] public readonly struct LateralSubDominantMinor : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralSubDominantMinor; }
    [Serializable] public readonly struct LateralSubDominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralSubDominant; }
    [Serializable] public readonly struct LateralSubDominantAugmented : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralSubDominantAugmented; }
    [Serializable] public readonly struct MediantTonicMinor : IFunction { public readonly FunctionEnum Enum => FunctionEnum.MediantTonicMinor; }
    [Serializable] public readonly struct MediantTonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.MediantTonic; }
    [Serializable] public readonly struct SubDominantDiminished : IFunction { public readonly FunctionEnum Enum => FunctionEnum.SubDominantDiminished; }
    [Serializable] public readonly struct SubDominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.SubDominant; }
    [Serializable] public readonly struct SubDominantAugmented : IFunction { public readonly FunctionEnum Enum => FunctionEnum.SubDominantAugmented; }
    [Serializable] public readonly struct Tritone : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Tritone; }
    [Serializable] public readonly struct DominantDiminished : IFunction { public readonly FunctionEnum Enum => FunctionEnum.DominantDiminished; }
    [Serializable] public readonly struct Dominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Dominant; }
    [Serializable] public readonly struct DominantAugmented : IFunction { public readonly FunctionEnum Enum => FunctionEnum.DominantAugmented; }
    [Serializable] public readonly struct SubMediantTonicMinor : IFunction { public readonly FunctionEnum Enum => FunctionEnum.SubMediantTonicMinor; }
    [Serializable] public readonly struct SubMediantTonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.SubMediantTonic; }
    [Serializable] public readonly struct LateralDominantMinor : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralDominantMinor; }
    [Serializable] public readonly struct LateralDominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralDominant; }

    public class FunctionEnum : Enumeration
    {
        public FunctionEnum() : base(0, "") { }
        public FunctionEnum(int sn, int id, string name) : base(sn: sn, id: id, name) { }

        public static FunctionEnum Tonic = new(0, 0, nameof(Tonic));
        public static FunctionEnum LateralSubDominantMinor = new(1, 1, nameof(LateralSubDominantMinor));
        public static FunctionEnum LateralSubDominant = new(2, 2, nameof(LateralSubDominant));
        public static FunctionEnum LateralSubDominantAugmented = new(3, 3, nameof(LateralSubDominantAugmented));
        public static FunctionEnum MediantTonicMinor = new(4, 3, nameof(MediantTonicMinor));
        public static FunctionEnum MediantTonic = new(5, 4, nameof(MediantTonic));
        public static FunctionEnum SubDominantDiminished = new(6, 4, nameof(SubDominantDiminished));
        public static FunctionEnum SubDominant = new(7, 5, nameof(SubDominant));
        public static FunctionEnum SubDominantAugmented = new(8, 6, nameof(SubDominantAugmented));
        public static FunctionEnum Tritone = new(9, 6, nameof(Tritone));
        public static FunctionEnum DominantDiminished = new(10, 6, nameof(DominantDiminished));
        public static FunctionEnum Dominant = new(11, 7, nameof(Dominant));
        public static FunctionEnum DominantAugmented = new(12, 8, nameof(DominantAugmented));
        public static FunctionEnum SubMediantTonicMinor = new(13, 8, nameof(SubMediantTonicMinor));
        public static FunctionEnum SubMediantTonic = new(14, 9, nameof(SubMediantTonic));
        public static FunctionEnum LateralDominantMinor = new(15, 10, nameof(LateralDominantMinor));
        public static FunctionEnum LateralDominant = new(16, 11, nameof(LateralDominant));
    }
}