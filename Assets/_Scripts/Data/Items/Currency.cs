namespace Data.Two
{
    public interface Currency : IItem
    {
        static CurrencyEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => null;
    }

    [System.Serializable]
    public struct Gold : Currency { public static CurrencyEnum Enum => CurrencyEnum.Gold; }

    [System.Serializable]
    public struct Pattern : Currency { public static CurrencyEnum Enum => CurrencyEnum.Pattern; }

    [System.Serializable]
    public struct Material : Currency { public static CurrencyEnum Enum => CurrencyEnum.Material; }

    [System.Serializable]
    public struct Ration : Currency { public static CurrencyEnum Enum => CurrencyEnum.Ration; }

    [System.Serializable]
    public class CurrencyEnum : Enumeration
    {
        public CurrencyEnum(int id, string name) : base(id, name) { }
        public static CurrencyEnum Gold = new(0, "Gold");
        public static CurrencyEnum Pattern = new(1, "Pattern");
        public static CurrencyEnum Material = new(2, "Material");
        public static CurrencyEnum Ration = new(3, "Ration");
    }
}

