using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class LighthouseData : IData
    {
        private Dictionary<Lighthouse, int> _lighthouses;
        private Dictionary<Lighthouse, int> Lighthouses => _lighthouses ??= SetUpLightHouses();
        Dictionary<Lighthouse, int> SetUpLightHouses()
        {
            Dictionary<Lighthouse, int> ship = new();
            foreach (var item in Items) ship.TryAdd((Lighthouse)item, 0);
            return ship;
        }

        public void Reset() => _lighthouses = SetUpLightHouses();

        public string GetDisplayLevel(IItem item) => Lighthouses[(Lighthouse)item] == 1 ? "Active" : "Not active";

        public int GetLevel(IItem item) => Lighthouses[(Lighthouse)item];// Lighthouses.GetValueOrDefault((Lighthouse)item);


        public void AdjustLevel(IItem item, int i)
        {
            Lighthouses[(Lighthouse)item] = i > 0 ? 1 : 0;
            PersistentData.Save(this);
        }
        // public void IncreaseLevel(IItem item)
        // {
        //     Lighthouses[(Lighthouse)item] = 1;
        //     PersistentData.Save(this);
        // }
        // public void IncreaseLevel(IItem item, int i) => IncreaseLevel(item);

        // public void DecreaseLevel(IItem item)
        // {
        //     Lighthouses[(Lighthouse)item] = 0;
        //     PersistentData.Save(this);
        // }
        // public void DecreaseLevel(IItem item, int i) => DecreaseLevel(item);

        public void SetLevel(IItem item, int count)
        {
            Lighthouses[(Lighthouse)item] = count;
            PersistentData.Save(this);
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<LighthouseEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = LighthouseEnum.ToItem(enums[i]);
                    return items;
                }
            }
        }

        public bool InventoryIsFull(int i) => false;

        string IData.GetDescription(IItem item)
        {
            return null;
            // throw new System.NotImplementedException();
        }


        // private LighthouseData() { }

        // public static LighthouseData GetData()
        // {
        //     LighthouseData data = new();
        //     if (data.PersistentData.TryLoadData() is not LighthouseData loadData) return data;
        //     for (int i = 0; i < data.Lighthouses.Length; i++)
        //         try { data.SetLevel(data.Lighthouses[i], loadData.GetLevel(data.Lighthouses[i])); }
        //         catch { }
        //     return data;
        // }

        public IPersistentData PersistentData { get; } = new NotPersistentData();
        // new SaveData("Lighthouse.Data");
    }

    public interface Lighthouse : IItem
    {
        LighthouseEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable] public readonly struct IonianLighthouse : Lighthouse { public readonly LighthouseEnum Enum => LighthouseEnum.IonianLighthouse; }
    [System.Serializable] public readonly struct DorianLighthouse : Lighthouse { public readonly LighthouseEnum Enum => LighthouseEnum.DorianLighthouse; }
    [System.Serializable] public readonly struct PhrygianLighthouse : Lighthouse { public readonly LighthouseEnum Enum => LighthouseEnum.PhrygianLighthouse; }
    [System.Serializable] public readonly struct LydianLighthouse : Lighthouse { public readonly LighthouseEnum Enum => LighthouseEnum.LydianLighthouse; }
    [System.Serializable] public readonly struct MixolydianLighthouse : Lighthouse { public readonly LighthouseEnum Enum => LighthouseEnum.MixolydianLighthouse; }
    [System.Serializable] public readonly struct AeolianLighthouse : Lighthouse { public readonly LighthouseEnum Enum => LighthouseEnum.AeolianLighthouse; }
    [System.Serializable] public readonly struct LocrianLighthouse : Lighthouse { public readonly LighthouseEnum Enum => LighthouseEnum.LocrianLighthouse; }
    [System.Serializable] public readonly struct ChromaticLighthouse : Lighthouse { public readonly LighthouseEnum Enum => LighthouseEnum.ChromaticLighthouse; }

    [System.Serializable]
    public class LighthouseEnum : Enumeration
    {
        public LighthouseEnum() : base(0, null) { }
        public LighthouseEnum(int id, string name) : base(id, name) { }
        public LighthouseEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public readonly static LighthouseEnum IonianLighthouse = new(0, "Ionian Lighthouse");
        public readonly static LighthouseEnum DorianLighthouse = new(1, "Dorian Lighthouse");
        public readonly static LighthouseEnum PhrygianLighthouse = new(2, "Phrygian Lighthouse");
        public readonly static LighthouseEnum LydianLighthouse = new(3, "Lydian Lighthouse");
        public readonly static LighthouseEnum MixolydianLighthouse = new(4, "Mixolydian Lighthouse");
        public readonly static LighthouseEnum AeolianLighthouse = new(5, "Aeolian Lighthouse");
        public readonly static LighthouseEnum LocrianLighthouse = new(6, "Locrian Lighthouse");
        public readonly static LighthouseEnum ChromaticLighthouse = new(7, "Chromatic Lighthouse");

        internal static IItem ToItem(LighthouseEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == IonianLighthouse => new IonianLighthouse(),
                _ when @enum == DorianLighthouse => new DorianLighthouse(),
                _ when @enum == PhrygianLighthouse => new PhrygianLighthouse(),
                _ when @enum == LydianLighthouse => new LydianLighthouse(),
                _ when @enum == MixolydianLighthouse => new MixolydianLighthouse(),
                _ when @enum == AeolianLighthouse => new AeolianLighthouse(),
                _ when @enum == LocrianLighthouse => new LocrianLighthouse(),
                _ when @enum == ChromaticLighthouse => new ChromaticLighthouse(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}
