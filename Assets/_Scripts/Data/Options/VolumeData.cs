using System.Collections.Generic;

namespace Data.Options
{
    [System.Serializable]
    public class VolumeData : IData
    {
        private Dictionary<DataItem, int> _volumeLevels;
        private Dictionary<DataItem, int> VolumeLevels => _volumeLevels ??= SetUpVolLvls();
        Dictionary<DataItem, int> SetUpVolLvls()
        {
            Dictionary<DataItem, int> levels = new();
            var items = DataItems;

            for (int i = 0; i < items.Length; i++)
                if (i == DataItem.BGMusic) levels.TryAdd((DataItem)items[i], 35);
                else if (i == DataItem.Drums) levels.TryAdd((DataItem)items[i], 75);
                else if (i == DataItem.Chords) levels.TryAdd((DataItem)items[i], 95);
                else if (i == DataItem.Bass) levels.TryAdd((DataItem)items[i], 95);
                else if (i == DataItem.SoundFX) levels.TryAdd((DataItem)items[i], 95);
                else levels.TryAdd((DataItem)items[i], 20);

            return levels;
        }

        public void Reset() => _volumeLevels = SetUpVolLvls();
        public string GetDisplayLevel(DataEnum item) => VolumeLevels[(DataItem)item].ToString();
        public float GetScaledLevel(DataEnum item) => (float)GetLevel(item) * .01f;
        public int GetLevel(DataEnum item) => VolumeLevels[(DataItem)item];

        public void IncreaseLevel(DataEnum item)
        {
            VolumeLevels[(DataItem)item] = (VolumeLevels[(DataItem)item] + 5) % 105;
            PersistentData.Save(this);
        }

        public void DecreaseLevel(DataEnum item)
        {
            VolumeLevels[(DataItem)item] = (VolumeLevels[(DataItem)item] - 5).Smod(105);
            PersistentData.Save(this);
        }

        public void SetLevel(DataEnum item, int newVolumeLevel)
        {
            VolumeLevels[(DataItem)item] = newVolumeLevel;
            PersistentData.Save(this);
        }

        public DataEnum[] DataItems => Enumeration.All<DataItem>();
        public bool InventoryIsFull(int i) => false;

        [System.Serializable]
        public class DataItem : DataEnum
        {
            public DataItem() : base(0, "") { }
            public DataItem(int id, string name) : base(id, name) { }
            public DataItem(int id, string name, string description) : base(id, name) => Description = description;
            // public DataItem(int id, string name, string description, UnityEngine.Sprite img) : base(id, name)
            // {
            //     Description = description;
            //     Image = img;
            // }
            public static DataItem BGMusic = new(0, "BG MUSIC", "Background music volume level");//, Assets.White);//
            public static DataItem SoundFX = new(1, "SOUND FX", "Sound effects volume level");//, Assets.LydianFlag);
            public static DataItem Click = new(2, "CLICK", "Batterie metronome volume level");//, Assets.LocrianFlag);
            public static DataItem Batterie = new(3, "BATTERIE", "Batterie snare drum volume level");//, Assets.AeolianFlag);
            public static DataItem Chords = new(4, "CHORDS", "Puzzle chords volume level");//, Assets.PhrygianFlag);
            public static DataItem Bass = new(5, "BASS", "Puzzle bass volume level");//, Assets.IonianFlag);
            public static DataItem Drums = new(6, "DRUMS", "Puzzle drums volume level");//, Assets.DorianFlag);
        }

        private VolumeData() { }

        public static VolumeData GetData()
        {
            VolumeData data = new();
            if (data.PersistentData.TryLoadData() is not VolumeData loadData) return data;
            for (int i = 0; i < data.DataItems.Length; i++)
                try { data.SetLevel(data.DataItems[i], loadData.GetLevel(data.DataItems[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Volume.Data");
    }
}