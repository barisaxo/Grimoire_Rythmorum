using System;
using System.Collections.Generic;
namespace Data.SRhythm
{
    public class SRhythmCellMenuOptionData : IData
    {
        // public SRhythmCellMenuOptionData(IData data) { Data = data; }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                IItem[] SetUp()
                {
                    var enums = Enumeration.All<SRhythmCellMenuOptionDataEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = SRhythmCellMenuOptionDataEnum.ToItem(enums[i]);

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

    public interface ISRhythmCellMenuOptionData : IItem
    {
        SRhythmCellMenuOptionDataEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct About : ISRhythmCellMenuOptionData { public readonly SRhythmCellMenuOptionDataEnum Enum => SRhythmCellMenuOptionDataEnum.About; }
    [Serializable] public struct RhythmCells : ISRhythmCellMenuOptionData { public readonly SRhythmCellMenuOptionDataEnum Enum => SRhythmCellMenuOptionDataEnum.RhythmCells; }
    [Serializable] public struct BatteriePractice : ISRhythmCellMenuOptionData { public readonly SRhythmCellMenuOptionDataEnum Enum => SRhythmCellMenuOptionDataEnum.BatteriePractice; }

    [Serializable]
    public class SRhythmCellMenuOptionDataEnum : Enumeration
    {

        public SRhythmCellMenuOptionDataEnum() : base(0, null) { }
        public SRhythmCellMenuOptionDataEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static SRhythmCellMenuOptionDataEnum About = new(0, "About");
        public static SRhythmCellMenuOptionDataEnum RhythmCells = new(1, "Rhythm Cells");
        public static SRhythmCellMenuOptionDataEnum BatteriePractice = new(2, "Batterie Practice");

        internal static IItem ToItem(SRhythmCellMenuOptionDataEnum @enum)
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