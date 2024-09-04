using System;
using System.Collections.Generic;
namespace Datum
{
    public interface IRegion : IItem
    {
        RegionEnum Enum { get; }
        int IItem.Id => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Name;
    }

    [Serializable] public readonly struct Ios : IRegion { public readonly RegionEnum Enum => RegionEnum.Ios; }
    [Serializable] public readonly struct Doria : IRegion { public readonly RegionEnum Enum => RegionEnum.Doria; }
    [Serializable] public readonly struct Phrygia : IRegion { public readonly RegionEnum Enum => RegionEnum.Phrygia; }
    [Serializable] public readonly struct Lydia : IRegion { public readonly RegionEnum Enum => RegionEnum.Lydia; }
    [Serializable] public readonly struct MixoLydia : IRegion { public readonly RegionEnum Enum => RegionEnum.MixoLydia; }
    [Serializable] public readonly struct Aeolia : IRegion { public readonly RegionEnum Enum => RegionEnum.Aeolia; }
    [Serializable] public readonly struct Locria : IRegion { public readonly RegionEnum Enum => RegionEnum.Locria; }

    [Serializable]
    public class RegionEnum : Enumeration
    {
        public RegionEnum() : base(0, null) { }
        public RegionEnum(int id, string name) : base(id, name) { }

        public readonly static RegionEnum Ios = new(0, nameof(Ios));
        public readonly static RegionEnum Doria = new(1, nameof(Doria));
        public readonly static RegionEnum Phrygia = new(2, nameof(Phrygia));
        public readonly static RegionEnum Lydia = new(3, nameof(Lydia));
        public readonly static RegionEnum MixoLydia = new(4, nameof(MixoLydia));
        public readonly static RegionEnum Aeolia = new(5, nameof(Aeolia));
        public readonly static RegionEnum Locria = new(6, nameof(Locria));

        public static IRegion GetRandomRegion() =>
            All<RegionEnum>()[UnityEngine.Random.Range(0, Length<RegionEnum>())].ToRegion();
    }

    public static class RegionSystems
    {
        public static IRegion ToRegion(this RegionEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == RegionEnum.Ios => new Ios(),
                _ when @enum == RegionEnum.Doria => new Doria(),
                _ when @enum == RegionEnum.Phrygia => new Phrygia(),
                _ when @enum == RegionEnum.Lydia => new Lydia(),
                _ when @enum == RegionEnum.MixoLydia => new MixoLydia(),
                _ when @enum == RegionEnum.Aeolia => new Aeolia(),
                _ when @enum == RegionEnum.Locria => new Locria(),
                _ => throw new ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }

    public class RegionData : IData
    {
        private Dictionary<IRegion, int> _datum;
        private Dictionary<IRegion, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IRegion, int> SetUpDatum()
        {
            Dictionary<IRegion, int> datum = new();
            foreach (IItem item in Items) datum.TryAdd(item as IRegion, 0);
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
                    var enums = Enumeration.All<RegionEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < enums.Length; i++)
                        temp[i] = enums[i].ToRegion();
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IRegion) throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }
        public int GetLevel(IItem item)
        {
            return 1;
            // if (item is not IRegion) throw new System.Exception(item.GetType().ToString());
            // return Datum[(IRegion)item];
        }


        public string GetDisplayLevel(IItem item)
        {
            return "";
            // if (item is not IRegion) throw new System.Exception(item.GetType().ToString());
            // return Datum[(IRegion)item].ToString();
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IRegion) throw new System.Exception(item.GetType().ToString());
            Datum[(IRegion)item] =
                Datum[(IRegion)item] + i > 999 ? 999 :
                Datum[(IRegion)item] + i < 0 ? 0 :
                Datum[(IRegion)item] + i;
        }

        // public void DecreaseLevel(IItem item)
        // {
        //     if (item is not Region) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Region)item] = Datum[(Region)item] - 1 < 0 ? 0 : Datum[(Region)item] - 1;
        // }
        // public void DecreaseLevel(IItem item, int i)
        // {
        //     if (item is not Region) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Region)item] = Datum[(Region)item] - i < 0 ? 0 : Datum[(Region)item] - i;
        // }
        // public void IncreaseLevel(IItem item)
        // {
        //     if (item is not Region) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Region)item] = Datum[(Region)item] + 1 > 999 ? 999 : Datum[(Region)item] + 1;
        // }
        // public void IncreaseLevel(IItem item, int i)
        // {
        //     if (item is not Region) throw new System.Exception(item.GetType().ToString());
        //     Datum[(Region)item] = Datum[(Region)item] + i > 999 ? 999 : Datum[(Region)item] + i;
        // }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IRegion) throw new System.Exception(item.GetType().ToString());
            Datum[(IRegion)item] = level;
        }

        // public bool IsFull(int i)
        // {
        //     return false;
        // }

        public void Reset()
        {
            _datum = SetUpDatum();
        }

        bool IData.InventoryIsFull(int space)
        {
            return false;
        }

        public IPersistentData PersistentData { get; } = new NotPersistentData();
    }

}