
namespace Data
{
    public interface MainOption : IItem
    {
        MainOptionEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => null;
    }

    [System.Serializable]
    public class MainOptionEnum : Enumeration
    {
        public MainOptionEnum() : base(0, null) { }
        public MainOptionEnum(int id, string name) : base(id, name) { }

        public static MainOptionEnum Continue = new(0, "Instantiate");
        public static MainOptionEnum Options = new(1, "Options");

        internal static MainOption ToItem(MainOptionEnum i)
        {
            return i switch
            {
                _ when i == Continue => new Continue(),
                _ when i == Options => new Options(),
                _ => throw new System.ArgumentOutOfRangeException(i.Name)
            };
        }
    }

    [System.Serializable] public struct Continue : MainOption { public readonly MainOptionEnum Enum => MainOptionEnum.Continue; }
    [System.Serializable] public struct Options : MainOption { public readonly MainOptionEnum Enum => MainOptionEnum.Options; }
}