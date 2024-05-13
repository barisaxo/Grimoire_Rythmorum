namespace Data.Two
{
    public interface Currency : IItem
    {
        CurrencyEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => null;
    }

    [System.Serializable] public readonly struct Gold : Currency { public readonly CurrencyEnum Enum => CurrencyEnum.Gold; }
    // [System.Serializable] public struct Pattern : Currency { public readonly CurrencyEnum Enum => CurrencyEnum.Pattern; }
    [System.Serializable] public readonly struct Material : Currency { public readonly CurrencyEnum Enum => CurrencyEnum.Material; }
    [System.Serializable] public readonly struct Ration : Currency { public readonly CurrencyEnum Enum => CurrencyEnum.Ration; }

    [System.Serializable]
    public class CurrencyEnum : Enumeration
    {
        public CurrencyEnum() : base(0, null) { }
        public CurrencyEnum(int id, string name) : base(id, name) { }

        public readonly static CurrencyEnum Gold = new(0, "Gold");
        public readonly static CurrencyEnum Material = new(1, "Materials");
        public readonly static CurrencyEnum Ration = new(2, "Rations");
        // public static CurrencyEnum Pattern = new(3, "Pattern");

        internal static IItem ToItem(WoodEnum i)
        {
            return i switch
            {
                _ when i == Gold => new Gold(),
                // _ when i == Pattern => new Pattern(),
                _ when i == Material => new Material(),
                _ when i == Ration => new Ration(),
                _ => throw new System.ArgumentOutOfRangeException(i.Name)
            };
        }
    }
}

