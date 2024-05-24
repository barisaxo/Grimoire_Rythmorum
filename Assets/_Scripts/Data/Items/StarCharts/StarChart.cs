using System;
namespace Data.Two
{
    public interface IStarChart : IItem
    {
        StarChartEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct NotesT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.NotesT; }
    [Serializable] public struct NotesA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.NotesA; }
    [Serializable] public struct StepsT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.StepsT; }
    [Serializable] public struct StepsA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.StepsA; }
    [Serializable] public struct ScalesT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.ScalesT; }
    [Serializable] public struct ScalesA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.ScalesA; }
    [Serializable] public struct IntervalsT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.IntervalsT; }
    [Serializable] public struct IntervalsA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.IntervalsA; }
    [Serializable] public struct TriadsT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.TriadsT; }
    [Serializable] public struct TriadsA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.TriadsA; }
    [Serializable] public struct InversionsT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.InversionsT; }
    [Serializable] public struct InversionsA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.InversionsA; }
    [Serializable] public struct InvertedTriadsT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.InvertedTriadsT; }
    [Serializable] public struct InvertedTriadsA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.InvertedTriadsA; }
    [Serializable] public struct SeventhChordsT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.SeventhChordsT; }
    [Serializable] public struct SeventhChordsA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.SeventhChordsA; }
    [Serializable] public struct ModesT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.ModesT; }
    [Serializable] public struct ModesA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.ModesA; }
    [Serializable] public struct Inverted7thChordsT : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.Inverted7thChordsT; }
    [Serializable] public struct Inverted7thChordsA : IStarChart { public readonly StarChartEnum Enum => StarChartEnum.Inverted7thChordsA; }

    [Serializable]
    public class StarChartEnum : Enumeration
    {
        public StarChartEnum() : base(0, null) { }
        public StarChartEnum(int id, string name) : base(id, name) { }
        public StarChartEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;
        public static StarChartEnum NotesT = new(0, "Notes[Theory]", "Difficulty: *");
        public static StarChartEnum NotesA = new(1, "Notes[Aural]", "Difficulty: !");
        public static StarChartEnum StepsT = new(2, "Steps[Theory]", "Difficulty: **");
        public static StarChartEnum StepsA = new(3, "Steps[Aural]", "Difficulty: !!");
        public static StarChartEnum ScalesT = new(4, "Scales[Theory]", "Difficulty: ***");
        public static StarChartEnum ScalesA = new(5, "Scales[Aural]", "Difficulty: !!!");
        public static StarChartEnum IntervalsT = new(6, "Intervals[Theory]", "Difficulty: ***");
        public static StarChartEnum IntervalsA = new(7, "Intervals[Aural]", "Difficulty: !!!");
        public static StarChartEnum TriadsT = new(8, "Triads[Theory]", "Difficulty: ***");
        public static StarChartEnum TriadsA = new(9, "Triads[Aural]", "Difficulty: !!!");
        public static StarChartEnum InversionsT = new(10, "Inversions[Theory]", "Difficulty: ****");
        public static StarChartEnum InversionsA = new(11, "Inversions[Aural]", "Difficulty: !!!!");
        public static StarChartEnum InvertedTriadsT = new(12, "Inverted Triads[Theory]", "Difficulty: ****");
        public static StarChartEnum InvertedTriadsA = new(13, "Inverted Triads[Aural]", "Difficulty: !!!!");
        public static StarChartEnum SeventhChordsT = new(14, "7th Chords[Theory]", "Difficulty: ****");
        public static StarChartEnum SeventhChordsA = new(15, "7th Chords[Aural]", "Difficulty: !!!!");
        public static StarChartEnum ModesT = new(16, "Modes[Theory]", "Difficulty: *****");
        public static StarChartEnum ModesA = new(17, "Modes[Aural]", "Difficulty: !!!!!");
        public static StarChartEnum Inverted7thChordsT = new(18, "Inverted 7th Chords[Theory]", "Difficulty: *****");
        public static StarChartEnum Inverted7thChordsA = new(19, "Inverted 7th Chords[Aural]", "Difficulty: !!!!!");

        internal static IItem ToItem(StarChartEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == NotesT => new NotesT(),
                _ when @enum == NotesA => new NotesA(),
                _ when @enum == StepsT => new StepsT(),
                _ when @enum == StepsA => new StepsA(),
                _ when @enum == ScalesT => new ScalesT(),
                _ when @enum == ScalesA => new ScalesA(),
                _ when @enum == IntervalsT => new IntervalsT(),
                _ when @enum == IntervalsA => new IntervalsA(),
                _ when @enum == TriadsT => new TriadsT(),
                _ when @enum == TriadsA => new TriadsA(),
                _ when @enum == InversionsT => new InversionsT(),
                _ when @enum == InversionsA => new InversionsA(),
                _ when @enum == InvertedTriadsT => new InvertedTriadsT(),
                _ when @enum == InvertedTriadsA => new InvertedTriadsA(),
                _ when @enum == SeventhChordsT => new SeventhChordsT(),
                _ when @enum == SeventhChordsA => new SeventhChordsA(),
                _ when @enum == ModesT => new ModesT(),
                _ when @enum == ModesA => new ModesA(),
                _ when @enum == Inverted7thChordsT => new Inverted7thChordsT(),
                _ when @enum == Inverted7thChordsA => new Inverted7thChordsA(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}