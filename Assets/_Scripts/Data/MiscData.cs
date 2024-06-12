using System.Collections.Generic;
using System;
namespace Data
{
    [System.Serializable]
    public class MiscData : IData
    {
        private Dictionary<IMisc, bool> _datum;
        private Dictionary<IMisc, bool> Datum => _datum ??= SetUpDatum();

        private Dictionary<IMisc, bool> SetUpDatum()
        {
            Dictionary<IMisc, bool> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IMisc)Items[i], false);

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
                    var enums = Enumeration.All<MiscEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = MiscEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IMisc) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IMisc) throw new System.Exception(item.GetType().ToString());
            return Datum[(IMisc)item].ToString();
        }

        public float GetScaledLevel(IItem item) => (float)GetLevel(item) * .01f;

        public int GetLevel(IItem item)
        {
            throw new System.Exception(item.GetType().ToString());
            // return Datum[(IMisc)item];
        }

        public bool Get(IItem item)
        {
            if (item is not IMisc) throw new System.Exception(item.GetType().ToString());
            return Datum[(IMisc)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IMisc) throw new System.Exception(item.GetType().ToString());
            Datum[(IMisc)item] = i > 0;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            // if (item is not IMisc) 
            throw new System.Exception(item.GetType().ToString());
            // Datum[(IMisc)item] = level;
            // PersistentData.Save(this);
        }
        public void SetValue(IItem item, bool value)
        {
            if (item is not IMisc)
                throw new System.Exception(item.GetType().ToString());

            Datum[(IMisc)item] = value;
            PersistentData.Save(this);
        }
        private void LoadValue(IItem item, bool value)
        {
            if (item is not IMisc) throw new System.Exception(item.GetType().ToString());
            Datum[(IMisc)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private MiscData() { }

        public static MiscData GetData()
        {
            MiscData data = new();
            if (data.PersistentData.TryLoadData() is not MiscData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.Get(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData("Misc.Data");
    }
    public interface IMisc : IItem
    {
        MiscEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct FirstInstantiateDialogue : IMisc { public readonly MiscEnum Enum => MiscEnum.FirstInstantiateDialogue; }

    [Serializable]
    public class MiscEnum : Enumeration
    {
        public MiscEnum() : base(0, "") { }
        public MiscEnum(int id, string name) : base(id, name) { }
        public string Description;
        public static MiscEnum FirstInstantiateDialogue = new(0, "Instantiate Dialogue");
        // public static MiscEnum SoundFX = new(1, "SOUND FX", "Sound effects Misc level");
        // public static MiscEnum Click = new(2, "CLICK", "Metronome click Misc level");
        // public static MiscEnum Batterie = new(3, "BATTERIE", "Batterie snare drum Misc level");
        // public static MiscEnum Chords = new(4, "CHORDS", "Puzzle chords Misc level");
        // public static MiscEnum Bass = new(5, "BASS", "Puzzle Bass Misc level");
        // public static MiscEnum Drums = new(6, "DRUMS", "Puzzle drums Misc level");

        public static IItem ToItem(MiscEnum @enum) => @enum switch
        {
            _ when @enum == FirstInstantiateDialogue => new FirstInstantiateDialogue(),
            // _ when @enum == SoundFX => new SoundFX(),
            // _ when @enum == Click => new Click(),
            // _ when @enum == Batterie => new BatterieMisc(),
            // _ when @enum == Chords => new Chords(),
            // _ when @enum == Bass => new Bass(),
            // _ when @enum == Drums => new Drums(),
            _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
        };
    }
}