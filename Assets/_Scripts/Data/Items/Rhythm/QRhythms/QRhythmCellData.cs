using System;
using System.Collections.Generic;

namespace Datum.QRhythm
{
    [System.Serializable]
    public class QRhythmCellData : IData
    {
        private Dictionary<IQRhythmCell, int> _datum;
        private Dictionary<IQRhythmCell, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IQRhythmCell, int> SetUpDatum()
        {
            Dictionary<IQRhythmCell, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IQRhythmCell)Items[i], 0);

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
                    var enums = Enumeration.All<QRhythmCellEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = QRhythmCellEnum.ToItem(enums[i]);
                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IQRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            if (item is not IQRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IQRhythmCell)item].ToString();
        }

        public int GetLevel(IItem item)
        {
            if (item is not IQRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IQRhythmCell)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IQRhythmCell || i < 0)
                throw new System.Exception(item.GetType().ToString() + " " + i);
            Datum[(IQRhythmCell)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IQRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IQRhythmCell)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not IQRhythmCell)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IQRhythmCell)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private QRhythmCellData() { }

        public static QRhythmCellData GetData()
        {
            QRhythmCellData data = new();
            if (data.PersistentData.TryLoadData() is not QRhythmCellData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(QRhythmCellData));
    }

    public interface IQRhythmCell : IItem
    {
        QRhythmCellEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct QQQQ : IQRhythmCell { public readonly QRhythmCellEnum Enum => QRhythmCellEnum.QQQQ; }
    [Serializable] public struct W : IQRhythmCell { public readonly QRhythmCellEnum Enum => QRhythmCellEnum.W; }
    [Serializable] public struct HH : IQRhythmCell { public readonly QRhythmCellEnum Enum => QRhythmCellEnum.HH; }
    [Serializable] public struct HQQ : IQRhythmCell { public readonly QRhythmCellEnum Enum => QRhythmCellEnum.HQQ; }
    [Serializable] public struct QQH : IQRhythmCell { public readonly QRhythmCellEnum Enum => QRhythmCellEnum.QQH; }
    [Serializable] public struct DHQ : IQRhythmCell { public readonly QRhythmCellEnum Enum => QRhythmCellEnum.DHQ; }
    [Serializable] public struct QDH : IQRhythmCell { public readonly QRhythmCellEnum Enum => QRhythmCellEnum.QDH; }
    [Serializable] public struct QHQ : IQRhythmCell { public readonly QRhythmCellEnum Enum => QRhythmCellEnum.QHQ; }

    [Serializable]
    public class QRhythmCellEnum : Enumeration
    {
        public QRhythmCellEnum() : base(0, null) { }
        public QRhythmCellEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static QRhythmCellEnum QQQQ = new(0, "QQQQ");
        public static QRhythmCellEnum W = new(1, "W");
        public static QRhythmCellEnum HH = new(2, "HH");
        public static QRhythmCellEnum HQQ = new(3, "HQQ");
        public static QRhythmCellEnum QQH = new(4, "QQH");
        public static QRhythmCellEnum DHQ = new(5, "H.Q");
        public static QRhythmCellEnum QDH = new(6, "QH.");
        public static QRhythmCellEnum QHQ = new(7, "QHQ");

        internal static IItem ToItem(QRhythmCellEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == QQQQ => new QQQQ(),
                _ when @enum == W => new W(),
                _ when @enum == HH => new HH(),
                _ when @enum == HQQ => new HQQ(),
                _ when @enum == QQH => new QQH(),
                _ when @enum == DHQ => new DHQ(),
                _ when @enum == QDH => new QDH(),
                _ when @enum == QHQ => new QHQ(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }
    }
}