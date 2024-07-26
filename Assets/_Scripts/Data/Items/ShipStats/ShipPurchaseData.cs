using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class ShipPurchaseData : IData
    {
        private Dictionary<IShipPurchase, int> _datum;
        private Dictionary<IShipPurchase, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IShipPurchase, int> SetUpDatum()
        {
            Dictionary<IShipPurchase, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IShipPurchase)Items[i], 0);

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
                    var enums = Enumeration.All<ShipPurchaseEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = ShipPurchaseEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IShipPurchase)
                throw new Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IShipPurchase)
                throw new Exception(item.GetType().ToString());
            return Datum[(IShipPurchase)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not IShipPurchase)
                throw new Exception(item.GetType().ToString());
            return Datum[(IShipPurchase)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IShipPurchase || i < 0)
                throw new Exception(item.GetType().ToString() + " " + i);
            Datum[(IShipPurchase)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IShipPurchase)
                throw new Exception(item.GetType().ToString());
            Datum[(IShipPurchase)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not IShipPurchase)
                throw new Exception(item.GetType().ToString());
            Datum[(IShipPurchase)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private ShipPurchaseData() { }

        public static ShipPurchaseData GetData()
        {
            ShipPurchaseData data = new();
            if (data.PersistentData.TryLoadData() is not ShipPurchaseData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(ShipPurchaseData));
    }

    public interface IShipPurchase : IItem
    {
        ShipPurchaseEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct SloopPurchase : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.SloopPurchase; }

    // [Serializable] public struct SSSS : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.SSSS; }
    // [Serializable] public struct Q : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.Q; }
    // [Serializable] public struct EE : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.EE; }
    // [Serializable] public struct ESS : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.ESS; }
    // [Serializable] public struct SSE : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.SSE; }
    // [Serializable] public struct DES : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.DES; }
    // [Serializable] public struct SDE : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.SDE; }
    // [Serializable] public struct SES : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.SES; }

    [Serializable]
    public class ShipPurchaseEnum : Enumeration
    {
        public ShipPurchaseEnum() : base(0, null) { }
        public ShipPurchaseEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static ShipPurchaseEnum SloopPurchase = new(0, "");
        // public static ShipPurchaseEnum Q = new(1, "Q");
        // public static ShipPurchaseEnum EE = new(2, "EE");
        // public static ShipPurchaseEnum ESS = new(3, "ESS");
        // public static ShipPurchaseEnum SSE = new(4, "SSE");
        // public static ShipPurchaseEnum DES = new(5, "E.S");
        // public static ShipPurchaseEnum SDE = new(6, "SE.");
        // public static ShipPurchaseEnum SES = new(7, "SES");

        internal static IItem ToItem(ShipPurchaseEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == SloopPurchase => new SloopPurchase(),
                // _ when @enum == SSSS => new SSSS(),
                // _ when @enum == Q => new Q(),
                // _ when @enum == EE => new EE(),
                // _ when @enum == ESS => new ESS(),
                // _ when @enum == SSE => new SSE(),
                // _ when @enum == DES => new DES(),
                // _ when @enum == SDE => new SDE(),
                // _ when @enum == SES => new SES(),
                _ => throw new ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}