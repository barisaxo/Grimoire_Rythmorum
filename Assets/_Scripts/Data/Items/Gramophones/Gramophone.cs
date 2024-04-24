namespace Data.Two
{
    public interface Gramophone : IItem
    {
        static GramophoneEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable]
    public struct Gramo1 : Gramophone { public static GramophoneEnum Enum => GramophoneEnum.Gramo1; }

    [System.Serializable]
    public struct Gramo2 : Gramophone { public static GramophoneEnum Enum => GramophoneEnum.Gramo2; }

    [System.Serializable]
    public struct Gramo3 : Gramophone { public static GramophoneEnum Enum => GramophoneEnum.Gramo3; }

    [System.Serializable]
    public struct Gramo4 : Gramophone { public static GramophoneEnum Enum => GramophoneEnum.Gramo4; }

    [System.Serializable]
    public struct Gramo5 : Gramophone { public static GramophoneEnum Enum => GramophoneEnum.Gramo5; }

    [System.Serializable]
    public class GramophoneEnum : Enumeration
    {
        public GramophoneEnum(int id, string name) : base(id, name) { }
        public GramophoneEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public static GramophoneEnum Gramo1 = new(0, "II- V7 I", "Difficulty: *");
        public static GramophoneEnum Gramo2 = new(1, "I IV V", "Difficulty: *");
        public static GramophoneEnum Gramo3 = new(2, "I VI- II- V", "Difficulty: **");
        public static GramophoneEnum Gramo4 = new(3, "III- VI- II- V", "Difficulty: ***");
        public static GramophoneEnum Gramo5 = new(4, "I II- III- IV V7 VI- VIIø", "Difficulty: ***");
    }
}