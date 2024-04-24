namespace Data.Two
{
    public interface StarChart : IItem
    {
        public static StarChartEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    public struct NotesT : StarChart { public static StarChartEnum Enum => StarChartEnum.NotesT; }
    public struct NotesA : StarChart { public static StarChartEnum Enum => StarChartEnum.NotesA; }
    public struct StepsT : StarChart { public static StarChartEnum Enum => StarChartEnum.StepsT; }
    public struct StepsA : StarChart { public static StarChartEnum Enum => StarChartEnum.StepsA; }
    public struct ScalesT : StarChart { public static StarChartEnum Enum => StarChartEnum.ScalesT; }
    public struct ScalesA : StarChart { public static StarChartEnum Enum => StarChartEnum.ScalesA; }
    public struct IntervalsT : StarChart { public static StarChartEnum Enum => StarChartEnum.IntervalsT; }
    public struct IntervalsA : StarChart { public static StarChartEnum Enum => StarChartEnum.IntervalsA; }
    public struct TriadsT : StarChart { public static StarChartEnum Enum => StarChartEnum.TriadsT; }
    public struct TriadsA : StarChart { public static StarChartEnum Enum => StarChartEnum.TriadsA; }
    public struct InversionsT : StarChart { public static StarChartEnum Enum => StarChartEnum.InversionsT; }
    public struct InversionsA : StarChart { public static StarChartEnum Enum => StarChartEnum.InversionsA; }
    public struct InvertedTriadsT : StarChart { public static StarChartEnum Enum => StarChartEnum.InvertedTriadsT; }
    public struct InvertedTriadsA : StarChart { public static StarChartEnum Enum => StarChartEnum.InvertedTriadsA; }
    public struct SeventhChordsT : StarChart { public static StarChartEnum Enum => StarChartEnum.SeventhChordsT; }
    public struct SeventhChordsA : StarChart { public static StarChartEnum Enum => StarChartEnum.SeventhChordsA; }
    public struct ModesT : StarChart { public static StarChartEnum Enum => StarChartEnum.ModesT; }
    public struct ModesA : StarChart { public static StarChartEnum Enum => StarChartEnum.ModesA; }
    public struct Inverted7thChordsT : StarChart { public static StarChartEnum Enum => StarChartEnum.Inverted7thChordsT; }
    public struct Inverted7thChordsA : StarChart { public static StarChartEnum Enum => StarChartEnum.Inverted7thChordsA; }

    [System.Serializable]
    public class StarChartEnum : Enumeration
    {
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
    }
}