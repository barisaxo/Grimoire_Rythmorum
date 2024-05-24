using System.Collections.Generic;

namespace Data.Two
{
    [System.Serializable]
    public class StandingData : IData
    {
        private Dictionary<Standing, int> _Standings;
        private Dictionary<Standing, int> Standings => _Standings ??= SetUpStandings();
        Dictionary<Standing, int> SetUpStandings()
        {
            Dictionary<Standing, int> standing = new();
            foreach (var item in Items) standing.TryAdd((Standing)item, item switch
            {
                IonianStanding or LydianStanding => 5,
                MixolydianStanding => 4,
                DorianStanding or AeolianStanding => 3,
                LocrianStanding or PhrygianStanding => 2,
                ChromaticStanding => 0,
                _ => throw new System.ArgumentOutOfRangeException(item.Name)
            });

            return standing;
        }

        public void Reset() => _Standings = SetUpStandings();

        public string GetDisplayLevel(IItem item) => Standings[(Standing)item] switch
        {
            10 => "Cordial",
            9 or 8 => "Civil",
            7 or 5 or 6 => "Tolerant",
            4 or 2 or 3 => "Confrontational",
            _ => "Hostile",
        };

        public int GetLevel(IItem item) => Standings[(Standing)item];// Standings.GetValueOrDefault((Standing)item);

        public int GetLevel(Sea.RegionEnum RegionalMode) =>
            GetLevel(ModeToStanding(RegionalMode));

        public void AdjustLevel(IItem item, int i)
        {
            foreach (var s in Standings) UnityEngine.Debug.Log(s.Value + " " + s.Key.Name);

            Standings[(Standing)item] =
                Standings[(Standing)item] + i < 0 ? 0 :
                Standings[(Standing)item] + i > 9 ? 9 :
                Standings[(Standing)item] + i;

            if (i > 0) AdjustGroup(1, (Standing)item);

            foreach (var s in Standings) UnityEngine.Debug.Log("after: " + s.Value + " " + s.Key.Name);


            PersistentData.Save(this);
        }

        void AdjustGroup(int add, Standing standing)
        {
            Standing[][] groups = new Standing[][]{
                new Standing[]{ new IonianStanding(), new LydianStanding(), new MixolydianStanding()},
                new Standing[]{ new DorianStanding(), new PhrygianStanding(), new AeolianStanding()},
                new Standing[]{ new LocrianStanding(), new ChromaticStanding()}};

            for (int i = 0; i < groups.Length; i++)
            {
                if (groups[i].Contains(standing))
                {
                    UnityEngine.Debug.Log(groups[i].Contains(standing) + " " + i + " " + standing);
                    foreach (Standing s in groups[i])
                    {
                        Standings[s] =
                            Standings[s] + add < 0 ? 0 :
                            Standings[s] + add > 9 ? 9 :
                            Standings[s] + add;
                    }

                    foreach (Standing s in groups[(i + 1) % groups.Length])
                    {
                        Standings[s] =
                            Standings[s] - add < 0 ? 0 :
                            Standings[s] - add > 9 ? 9 :
                            Standings[s] - add;
                    }

                }
            }

        }

        public void AdjustLevel(Sea.RegionEnum RegionalMode, int i) =>
            AdjustLevel(ModeToStanding(RegionalMode), i);

        public IItem ModeToStanding(Sea.RegionEnum mode) => mode switch
        {
            Sea.RegionEnum.Aeolian => new AeolianStanding(),
            Sea.RegionEnum.Dorian => new DorianStanding(),
            Sea.RegionEnum.Ionian => new IonianStanding(),
            Sea.RegionEnum.Lydian => new LydianStanding(),
            Sea.RegionEnum.Phrygian => new PhrygianStanding(),
            Sea.RegionEnum.MixoLydian => new MixolydianStanding(),
            Sea.RegionEnum.Locrian => new LocrianStanding(),
            Sea.RegionEnum.Chromatic => new ChromaticStanding(),
            _ => throw new System.Exception(mode.ToString()),
        };

        // public void IncreaseLevel(IItem item)
        // {
        //     Standings[(Standing)item] = 1;
        //     PersistentData.Save(this);
        // }
        // public void IncreaseLevel(IItem item, int i) => IncreaseLevel(item);

        // public void DecreaseLevel(IItem item)
        // {
        //     Standings[(Standing)item] = 0;
        //     PersistentData.Save(this);
        // }
        // public void DecreaseLevel(IItem item, int i) => DecreaseLevel(item);

        public void SetLevel(IItem item, int level)
        {
            Standings[(Standing)item] = level;
            PersistentData.Save(this);
        }

        public void LoadLevel(IItem item, int level)
        {
            Standings[(Standing)item] = level;
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                static IItem[] SetUp()
                {
                    var enums = Enumeration.All<StandingEnum>();
                    var items = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        items[i] = StandingEnum.ToItem(enums[i]);
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

        // private StandingData() { }

        // public static StandingData GetData()
        // {
        //     StandingData data = new();
        //     if (data.PersistentData.TryLoadData() is not StandingData loadData) return data;
        //     for (int i = 0; i < data.Items.Length; i++)
        //         try { data.LoadLevel(data.Items[i], loadData.GetLevel(data.Items[i])); }
        //         catch { }
        //     return data;
        // }

        public IPersistentData PersistentData { get; } = new NotPersistentData();
        // new SaveData("Standing.Data");
    }

    public interface Standing : IItem
    {
        StandingEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [System.Serializable] public readonly struct IonianStanding : Standing { public readonly StandingEnum Enum => StandingEnum.IonianStanding; }
    [System.Serializable] public readonly struct DorianStanding : Standing { public readonly StandingEnum Enum => StandingEnum.DorianStanding; }
    [System.Serializable] public readonly struct PhrygianStanding : Standing { public readonly StandingEnum Enum => StandingEnum.PhrygianStanding; }
    [System.Serializable] public readonly struct LydianStanding : Standing { public readonly StandingEnum Enum => StandingEnum.LydianStanding; }
    [System.Serializable] public readonly struct MixolydianStanding : Standing { public readonly StandingEnum Enum => StandingEnum.MixolydianStanding; }
    [System.Serializable] public readonly struct AeolianStanding : Standing { public readonly StandingEnum Enum => StandingEnum.AeolianStanding; }
    [System.Serializable] public readonly struct LocrianStanding : Standing { public readonly StandingEnum Enum => StandingEnum.LocrianStanding; }
    [System.Serializable] public readonly struct ChromaticStanding : Standing { public readonly StandingEnum Enum => StandingEnum.ChromaticStanding; }

    [System.Serializable]
    public class StandingEnum : Enumeration
    {
        public StandingEnum() : base(0, null) { }
        public StandingEnum(int id, string name) : base(id, name) { }
        public StandingEnum(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }

        public readonly string Description;

        public readonly static StandingEnum IonianStanding = new(0, "Ios Standings");
        public readonly static StandingEnum DorianStanding = new(1, "Doria Standings");
        public readonly static StandingEnum PhrygianStanding = new(2, "Phrygia Standings");
        public readonly static StandingEnum LydianStanding = new(3, "Lydia Standings");
        public readonly static StandingEnum MixolydianStanding = new(4, "MixoLydia Standings");
        public readonly static StandingEnum AeolianStanding = new(5, "Aeolia Standings");
        public readonly static StandingEnum LocrianStanding = new(6, "Locria Standings");
        public readonly static StandingEnum ChromaticStanding = new(7, "Chromatica Standings");

        internal static IItem ToItem(StandingEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == IonianStanding => new IonianStanding(),
                _ when @enum == DorianStanding => new DorianStanding(),
                _ when @enum == PhrygianStanding => new PhrygianStanding(),
                _ when @enum == LydianStanding => new LydianStanding(),
                _ when @enum == MixolydianStanding => new MixolydianStanding(),
                _ when @enum == AeolianStanding => new AeolianStanding(),
                _ when @enum == LocrianStanding => new LocrianStanding(),
                _ when @enum == ChromaticStanding => new ChromaticStanding(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }

    public static class StandingToRegionConverter
    {
        public static string ToRegionalName(this Standing standing) => standing switch
        {
            IonianStanding => "Ionians",
            AeolianStanding => "Aeolians",
            DorianStanding => "Dorians",
            PhrygianStanding => "Phrygians",
            LydianStanding => "Lydians",
            MixolydianStanding => "MixoLydians",
            LocrianStanding => "Locrians",
            _ => throw new System.ArgumentOutOfRangeException(standing.Name)
        };
    }
}
