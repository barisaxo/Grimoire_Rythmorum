using System.Collections.Generic;

namespace Data
{
    [System.Serializable]
    public class VolumeData : IData
    {
        private Dictionary<IVolume, int> _datum;
        private Dictionary<IVolume, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IVolume, int> SetUpDatum()
        {
            Dictionary<IVolume, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                if (Items[i] is BGMusic) datum.TryAdd((IVolume)Items[i], 35);
                else if (Items[i] is Drums) datum.TryAdd((IVolume)Items[i], 75);
                else if (Items[i] is Chords) datum.TryAdd((IVolume)Items[i], 95);
                else if (Items[i] is Bass) datum.TryAdd((IVolume)Items[i], 95);
                else if (Items[i] is SoundFX) datum.TryAdd((IVolume)Items[i], 95);
                else datum.TryAdd((IVolume)Items[i], 20);

            return datum;
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<VolumeEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = VolumeEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IVolume) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IVolume) throw new System.Exception(item.GetType().ToString());
            return Datum[(IVolume)item].ToString();
        }

        // public void DecreaseLevel(IItem item)
        // {
        //     if (item is not Volume) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Volume)item] -= Datum[(Volume)item] - 5 < 0 ? 0 : 5;
        //     PersistentData.Save(this);
        // }

        // public void DecreaseLevel(IItem item, int i) => DecreaseLevel(item);

        public float GetScaledLevel(IItem item) => (float)GetLevel(item) * .01f;

        public int GetLevel(IItem item)
        {
            if (item is not IVolume) throw new System.Exception(item.GetType().ToString());
            return Datum[(IVolume)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IVolume) throw new System.Exception(item.GetType().ToString());
            Datum[(IVolume)item] =
                Datum[(IVolume)item] + i > 100 ? 100 :
                Datum[(IVolume)item] + i < 0 ? 0 :
                Datum[(IVolume)item] + i;
            PersistentData.Save(this);
        }

        // public void IncreaseLevel(IItem item)
        // {
        //     if (item is not Volume) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Volume)item] += Datum[(Volume)item] + 5 > 100 ? 0 : 5;
        //     PersistentData.Save(this);
        // }

        // public void IncreaseLevel(IItem item, int i) => IncreaseLevel(item);


        public void SetLevel(IItem item, int level)
        {
            if (item is not IVolume) throw new System.Exception(item.GetType().ToString());
            Datum[(IVolume)item] = level;
            PersistentData.Save(this);
        }
        private void LoadLevel(IItem item, int level)
        {
            if (item is not IVolume) throw new System.Exception(item.GetType().ToString());
            Datum[(IVolume)item] = level;
            while (Datum[(IVolume)item] % 5 != 0) Datum[(IVolume)item]++;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private VolumeData() { }

        public static VolumeData GetData()
        {
            VolumeData data = new();
            if (data.PersistentData.TryLoadData() is not VolumeData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Volume.Data");
    }

}