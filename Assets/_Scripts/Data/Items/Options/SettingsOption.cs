
namespace Data
{
    public interface SettingsOption : IItem
    {
        SettingsOptionEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => null;
    }

    [System.Serializable]
    public class SettingsOptionEnum : Enumeration
    {
        public SettingsOptionEnum() : base(0, null) { }
        public SettingsOptionEnum(int id, string name) : base(id, name) { }

        public static SettingsOptionEnum VolumeSetting = new(0, "Volume");
        public static SettingsOptionEnum GameplaySetting = new(1, "Options");

        internal static SettingsOption ToItem(SettingsOptionEnum i)
        {
            return i switch
            {
                _ when i == VolumeSetting => new VolumeSetting(),
                _ when i == GameplaySetting => new GameplaySetting(),
                _ => throw new System.ArgumentOutOfRangeException(i.Name)
            };
        }
    }

    [System.Serializable] public struct VolumeSetting : SettingsOption { public readonly SettingsOptionEnum Enum => SettingsOptionEnum.VolumeSetting; }
    [System.Serializable] public struct GameplaySetting : SettingsOption { public readonly SettingsOptionEnum Enum => SettingsOptionEnum.GameplaySetting; }
}