namespace Data.Two
{
    public interface Cloth : IItem
    {
        static ClothEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable]
    public struct Hemp : Cloth { public static ClothEnum Enum => ClothEnum.Hemp; }

    [System.Serializable]
    public struct Cotton : Cloth { public static ClothEnum Enum => ClothEnum.Cotton; }

    [System.Serializable]
    public struct Linen : Cloth { public static ClothEnum Enum => ClothEnum.Linen; }

    [System.Serializable]
    public struct Silk : Cloth { public static ClothEnum Enum => ClothEnum.Silk; }

    [System.Serializable]
    public class ClothEnum : Enumeration
    {
        public ClothEnum(int id, string name) : base(id, name) { }
        public ClothEnum(int id, string name, string description, float modifier) : base(id, name)
        {
            Description = description;
            Modifier = modifier;
        }

        public readonly float Modifier;
        public readonly string Description;

        public static ClothEnum Hemp = new(0, "Hemp", "Inexpensive but heavy sailcloth", 1);
        public static ClothEnum Cotton = new(1, "Cotton", "Moderately inexpensive, moderately light sailcloth", 1.5f);
        public static ClothEnum Linen = new(2, "Linen", "Expensive, light sailcloth", 2.25f);
        public static ClothEnum Silk = new(3, "Silk", "Very expensive, very light sailcloth", 3f);
    }
}