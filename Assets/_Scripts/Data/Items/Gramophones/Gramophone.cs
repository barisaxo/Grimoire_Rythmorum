namespace Data.Two
{
    public interface Gramophone : IItem
    {
        GramophoneEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable] public readonly struct Gramo1 : Gramophone { public readonly GramophoneEnum Enum => GramophoneEnum.Gramo1; }
    [System.Serializable] public readonly struct Gramo2 : Gramophone { public readonly GramophoneEnum Enum => GramophoneEnum.Gramo2; }
    [System.Serializable] public readonly struct Gramo3 : Gramophone { public readonly GramophoneEnum Enum => GramophoneEnum.Gramo3; }
    [System.Serializable] public readonly struct Gramo4 : Gramophone { public readonly GramophoneEnum Enum => GramophoneEnum.Gramo4; }
    [System.Serializable] public readonly struct Gramo5 : Gramophone { public readonly GramophoneEnum Enum => GramophoneEnum.Gramo5; }

    [System.Serializable]
    public class GramophoneEnum : Enumeration
    {
        public GramophoneEnum() : base(0, null) { }
        public GramophoneEnum(int id, string name) : base(id, name) { }
        public GramophoneEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public readonly static GramophoneEnum Gramo1 = new(0, "II- V7 I", "Difficulty: *");
        public readonly static GramophoneEnum Gramo2 = new(1, "I IV V", "Difficulty: *");
        public readonly static GramophoneEnum Gramo3 = new(2, "I VI- II- V", "Difficulty: **");
        public readonly static GramophoneEnum Gramo4 = new(3, "III- VI- II- V", "Difficulty: ***");
        public readonly static GramophoneEnum Gramo5 = new(4, "I II- III- IV V7 VI- VIIø", "Difficulty: ***");

        internal static IItem ToItem(GramophoneEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == Gramo1 => new Gramo1(),
                _ when @enum == Gramo2 => new Gramo2(),
                _ when @enum == Gramo3 => new Gramo3(),
                _ when @enum == Gramo4 => new Gramo4(),
                _ when @enum == Gramo5 => new Gramo5(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}