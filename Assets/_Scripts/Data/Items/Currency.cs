namespace Data
{
    public interface ICurrency : IItem
    {
        CurrencyEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => null;
    }

    [System.Serializable] public readonly struct Gold : ICurrency { public readonly CurrencyEnum Enum => CurrencyEnum.Gold; }
    [System.Serializable] public readonly struct Material : ICurrency { public readonly CurrencyEnum Enum => CurrencyEnum.Material; }
    [System.Serializable] public readonly struct Ration : ICurrency { public readonly CurrencyEnum Enum => CurrencyEnum.Ration; }
    [System.Serializable] public readonly struct StarChart : ICurrency { public readonly CurrencyEnum Enum => CurrencyEnum.StarChart; }
    [System.Serializable] public readonly struct Gramophone : ICurrency { public readonly CurrencyEnum Enum => CurrencyEnum.Gramophone; }

    [System.Serializable]
    public class CurrencyEnum : Enumeration
    {
        public CurrencyEnum() : base(0, null) { }
        public CurrencyEnum(int id, string name) : base(id, name) { }

        public readonly static CurrencyEnum Gold = new(0, "Gold");
        public readonly static CurrencyEnum Material = new(1, "Materials");
        public readonly static CurrencyEnum Ration = new(2, "Ration");
        public readonly static CurrencyEnum StarChart = new(3, "Star Chart");
        public readonly static CurrencyEnum Gramophone = new(4, "Gramophone");

        internal static IItem ToItem(WoodEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == Gold => new Gold(),
                _ when @enum == Material => new Material(),
                _ when @enum == Ration => new Ration(),
                _ when @enum == StarChart => new StarChart(),
                _ when @enum == Gramophone => new Gramophone(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}

