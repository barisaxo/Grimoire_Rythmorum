using System;
using System.Collections.Generic;

namespace Datum
{
    [Serializable]
    public class ShipPurchaseData : IData
    {
        private Dictionary<IShipPurchase, bool> _datum;
        private Dictionary<IShipPurchase, bool> Datum => _datum ??= SetUpDatum();

        private Dictionary<IShipPurchase, bool> SetUpDatum()
        {
            Dictionary<IShipPurchase, bool> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IShipPurchase)Items[i], false);

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
            return Datum[(IShipPurchase)item] ? 1 : 0;
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IShipPurchase || i < 0)
                throw new Exception(item.GetType().ToString() + " " + i);
            Datum[(IShipPurchase)item] = i > 0;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IShipPurchase)
                throw new Exception(item.GetType().ToString());
            Datum[(IShipPurchase)item] = level > 0;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not IShipPurchase)
                throw new Exception(item.GetType().ToString());
            Datum[(IShipPurchase)item] = value > 0;
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
        int IItem.Id => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Cost.ToString();
        int Cost => Enum.Cost;
    }

    [Serializable] public struct SloopPurchase : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.SloopPurchase; }
    [Serializable] public struct CutterPurchase : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.CutterPurchase; }
    [Serializable] public struct SchoonerPurchase : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.SchoonerPurchase; }
    [Serializable] public struct BrigPurchase : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.BrigPurchase; }
    [Serializable] public struct FrigatePurchase : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.FrigatePurchase; }
    [Serializable] public struct BarquePurchase : IShipPurchase { public readonly ShipPurchaseEnum Enum => ShipPurchaseEnum.BarquePurchase; }

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
        public ShipPurchaseEnum(int id, string name, int cost) : base(id, name) { Cost = cost; }

        public readonly int Cost;

        public static ShipPurchaseEnum SloopPurchase = new(0, nameof(SloopPurchase), 250);
        public static ShipPurchaseEnum CutterPurchase = new(1, nameof(CutterPurchase), 5000);
        public static ShipPurchaseEnum SchoonerPurchase = new(3, nameof(SchoonerPurchase), 10000);
        public static ShipPurchaseEnum BrigPurchase = new(4, nameof(BrigPurchase), 20000);
        public static ShipPurchaseEnum FrigatePurchase = new(5, nameof(FrigatePurchase), 50000);
        public static ShipPurchaseEnum BarquePurchase = new(6, nameof(BarquePurchase), 100000);

        internal static IItem ToItem(ShipPurchaseEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == SloopPurchase => new SloopPurchase(),
                _ when @enum == CutterPurchase => new CutterPurchase(),
                _ when @enum == SchoonerPurchase => new SchoonerPurchase(),
                _ when @enum == BrigPurchase => new BrigPurchase(),
                _ when @enum == FrigatePurchase => new FrigatePurchase(),
                _ when @enum == BarquePurchase => new BarquePurchase(),
                _ => throw new ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}
/*
CutterPurchase  
SchoonerPurchase
BrigPurchase    
FrigatePurchase 
BarquePurchase  




*/