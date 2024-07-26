using System;
using System.Collections.Generic;
namespace Data.QRhythm
{
    public class QRhythmCellMenuOptionData : IData
    {
        // public QRhythmCellMenuOptionData(IData data) { Data = data; }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                IItem[] SetUp()
                {
                    var enums = Enumeration.All<QRhythmCellMenuOptionDataEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = QRhythmCellMenuOptionDataEnum.ToItem(enums[i]);

                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not BatteriePracticeOptionEnum) throw new System.Exception(item.GetType().ToString());

            return item.Description;
        }

        public string GetDisplayLevel(IItem item)
        {
            // if (item is not MainOption) throw new System.Exception(item.GetType().ToString());
            // return Datum[(MainOption)item].ToString();
            return null;
        }

        public int GetLevel(IItem item)
        {
            // if (item is not MainOption) throw new System.Exception(item.GetType().ToString());
            // return Datum[(MainOption)item];
            return 0;
        }

        public void AdjustLevel(IItem item, int i) { }

        public void SetLevel(IItem item, int level)
        {
            // if (item is not MainOption) throw new System.Exception(item.GetType().ToString());
            // Datum[(MainOption)item] = level;
            // PersistentData.Save(this);
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset() { }

        public IPersistentData PersistentData { get; } = new NotPersistentData();
    }

    public interface IQRhythmCellMenuOptionData : IItem
    {
        QRhythmCellMenuOptionDataEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct About : IQRhythmCellMenuOptionData { public readonly QRhythmCellMenuOptionDataEnum Enum => QRhythmCellMenuOptionDataEnum.About; }
    [Serializable] public struct RhythmCells : IQRhythmCellMenuOptionData { public readonly QRhythmCellMenuOptionDataEnum Enum => QRhythmCellMenuOptionDataEnum.RhythmCells; }
    [Serializable] public struct BatteriePractice : IQRhythmCellMenuOptionData { public readonly QRhythmCellMenuOptionDataEnum Enum => QRhythmCellMenuOptionDataEnum.BatteriePractice; }

    [Serializable]
    public class QRhythmCellMenuOptionDataEnum : Enumeration
    {

        public QRhythmCellMenuOptionDataEnum() : base(0, null) { }
        public QRhythmCellMenuOptionDataEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static QRhythmCellMenuOptionDataEnum About = new(0, "About");
        public static QRhythmCellMenuOptionDataEnum RhythmCells = new(1, "Rhythm Cells");
        public static QRhythmCellMenuOptionDataEnum BatteriePractice = new(2, "Batterie Practice");

        internal static IItem ToItem(QRhythmCellMenuOptionDataEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == About => new About(),
                _ when @enum == RhythmCells => new RhythmCells(),
                _ when @enum == BatteriePractice => new BatteriePractice(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }

    }

}