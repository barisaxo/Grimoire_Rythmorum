
public class VolumeData
{
    private (DataItem volumeItem, int level)[] _volumeLevels;
    private (DataItem volumeItem, int level)[] VolumeLevels => _volumeLevels ??= SetUpVolumeLevels();
    private (DataItem volumeItem, int level)[] SetUpVolumeLevels()
    {
        var items = Enumeration.List<DataItem>();
        var volumeLevels = new (DataItem volumeItem, int level)[items.Count];

        for (int i = 0; i < items.Count; i++)
            if (i == DataItem.BGMusic) volumeLevels[i] = (items[i], 35);
            else if (i == DataItem.Drums) volumeLevels[i] = (items[i], 45);
            else volumeLevels[i] = (items[i], 80);

        return volumeLevels;
    }

    /// <summary>
    /// Give this to set the AudioSources volume level.
    /// </summary>
    /// <returns>a float 0.0f to 1.0f</returns>
    public float GetScaledLevel(DataItem item) => VolumeLevels[item].level * .01f;

    /// <summary>
    /// Give this to the menu objects text to display the current volume level.
    /// </summary>
    /// <returns>an int 0 to 100</returns>
    public string GetDisplayLevel(DataItem item) => VolumeLevels[item].level.ToString();

    public void IncreaseLevel(DataItem item) =>
        VolumeLevels[item].level = VolumeLevels[item].level + 5 > 100 ? 0 : VolumeLevels[item].level + 5;

    public void DecreaseLevel(DataItem item) =>
        VolumeLevels[item].level = VolumeLevels[item].level - 5 < 0 ? 100 : VolumeLevels[item].level - 5;

    public void SetLevel(DataItem item, int newVolumeLevel) => VolumeLevels[item].level = newVolumeLevel;

    public class DataItem : DataEnum
    {
        public DataItem() : base(0, "") { }
        public DataItem(int id, string name) : base(id, name) { }
        public DataItem(int id, string name, string description) : base(id, name) => Description = description;
        public static DataItem BGMusic = new(0, "BG MUSIC", "Background music volume level");
        public static DataItem SoundFX = new(1, "SOUND FX", "Sound effects volume level");
        public static DataItem Chords = new(2, "CHORDS", "Puzzle chords volume level");
        public static DataItem Bass = new(3, "BASS", "Puzzle bass volume level");
        public static DataItem Drums = new(4, "DRUMS", "Puzzle drums volume level");
        public static DataItem Battery = new(5, "BATTERY", "Battery snare drum volume level");
        public static DataItem Click = new(6, "CLICK", "Battery metronome volume level");
    }
}