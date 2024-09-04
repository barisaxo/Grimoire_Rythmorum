using System;

namespace MusicTheory.Functions.Harmonic
{
    public interface IFunction : IMusicalElement
    {
        FunctionEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct Tonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Tonic; }
    [Serializable] public readonly struct Subdominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Subdominant; }
    [Serializable] public readonly struct Dominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Dominant; }

    public class FunctionEnum : Enumeration
    {
        public FunctionEnum() : base(0, "") { }
        public FunctionEnum(int id, string name) : base(id, name) { }

        public static FunctionEnum Tonic = new(0, nameof(Tonic));
        public static FunctionEnum Subdominant = new(1, nameof(Subdominant));
        public static FunctionEnum Dominant = new(2, nameof(Dominant));

        public static IFunction GetRandomFunction =>
            UnityEngine.Random.Range(0, FunctionCount) switch
            {
                0 => new Tonic(),
                1 => new Subdominant(),
                2 => new Dominant(),
                _ => throw new System.Exception()
            };

        public static int FunctionCount => Length<FunctionEnum>();
    }
}

namespace MusicTheory.Functions.Diatonic
{
    public interface IFunction : IMusicalElement
    {
        FunctionEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct Tonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Tonic; }
    [Serializable] public readonly struct LateralSubDominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralSubdominant; }
    [Serializable] public readonly struct MediantTonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.MediantTonic; }
    [Serializable] public readonly struct Subdominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Subdominant; }
    [Serializable] public readonly struct Dominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Dominant; }
    [Serializable] public readonly struct SubmediantTonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.SubmediantTonic; }
    [Serializable] public readonly struct LateralDominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralDominant; }

    public class FunctionEnum : Enumeration
    {
        public FunctionEnum() : base(0, "") { }
        public FunctionEnum(int id, string name) : base(id, name) { }

        public static FunctionEnum Tonic = new(0, nameof(Tonic));
        public static FunctionEnum LateralSubdominant = new(1, nameof(LateralSubdominant));
        public static FunctionEnum MediantTonic = new(2, nameof(MediantTonic));
        public static FunctionEnum Subdominant = new(3, nameof(Subdominant));
        public static FunctionEnum Dominant = new(4, nameof(Dominant));
        public static FunctionEnum SubmediantTonic = new(5, nameof(SubmediantTonic));
        public static FunctionEnum LateralDominant = new(6, nameof(LateralDominant));


    }
}

namespace MusicTheory.Functions.Chromatic
{
    public interface IFunction : IMusicalElement
    {
        FunctionEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
    }

    [Serializable] public readonly struct Tonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Tonic; }
    [Serializable] public readonly struct LateralSubDominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralSubdominant; }
    [Serializable] public readonly struct MediantTonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.MediantTonic; }
    [Serializable] public readonly struct SubDominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Subdominant; }
    [Serializable] public readonly struct Dominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.Dominant; }
    [Serializable] public readonly struct SubmediantTonic : IFunction { public readonly FunctionEnum Enum => FunctionEnum.SubmediantTonic; }
    [Serializable] public readonly struct LateralDominant : IFunction { public readonly FunctionEnum Enum => FunctionEnum.LateralDominant; }

    public class FunctionEnum : Enumeration
    {
        public FunctionEnum() : base(0, "") { }
        public FunctionEnum(int id, string name) : base(id, name) { }

        public static FunctionEnum Tonic = new(0, nameof(Tonic));
        public static FunctionEnum ChromaticDominant = new(1, nameof(ChromaticDominant));
        public static FunctionEnum LateralSubdominant = new(2, nameof(LateralSubdominant));
        public static FunctionEnum ChromaticMediant = new(3, nameof(ChromaticMediant));
        public static FunctionEnum MediantTonic = new(4, nameof(MediantTonic));
        public static FunctionEnum Tritone = new(5, nameof(Tritone));
        public static FunctionEnum Subdominant = new(6, nameof(Subdominant));
        public static FunctionEnum Dominant = new(7, nameof(Dominant));
        public static FunctionEnum ChromaticSubmediant = new(8, nameof(ChromaticSubmediant));
        public static FunctionEnum SubmediantTonic = new(9, nameof(SubmediantTonic));
        public static FunctionEnum ModalDominant = new(10, nameof(ModalDominant));
        public static FunctionEnum LateralDominant = new(11, nameof(LateralDominant));
    }
}

