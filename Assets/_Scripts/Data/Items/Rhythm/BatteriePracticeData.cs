using System;
using Menus;
using UnityEngine;
using System.Collections.Generic;
namespace Data
{
    [Serializable]
    public class BatteriePracticeData : IData
    {
        private Dictionary<IBatteriePracticeOption, int> _datum;
        private Dictionary<IBatteriePracticeOption, int> Datum => _datum ??= SetUpDatum();

        private Dictionary<IBatteriePracticeOption, int> SetUpDatum()
        {
            Dictionary<IBatteriePracticeOption, int> datum = new();
            for (int i = 0; i < Items.Length; i++)
                datum.TryAdd((IBatteriePracticeOption)Items[i], 0);

            return datum;
        }

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                IItem[] SetUp()
                {
                    var enums = Enumeration.All<BatteriePracticeOptionEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                        temp[i] = BatteriePracticeOptionEnum.ToItem(enums[i]);

                    return temp;
                }
            }
        }

        public string GetDescription(IItem item)
        {
            if (item is not IBatteriePracticeOption)
                throw new System.Exception(item.GetType().ToString());

            // if (item is ERhythmCell)
            // {
            //     if (!(Manager.Io.QRhythmCellData.GetLevel(new QHQ()) > 0))
            //     {
            //         return "Complete Quarter Notes to Unlock";
            //     }
            // }

            // if (item is SRhythmCell)
            // {
            //     if (!(Manager.Io.ERhythmCellData.GetLevel(new EQE()) > 0))
            //     {
            //         return "Complete Eighth Notes to Unlock";
            //     }
            // }

            return "";
        }

        public string GetDisplayLevel(IItem item)
        {
            // if (item is not MainOption) throw new System.Exception(item.GetType().ToString());
            // return Datum[(MainOption)item].ToString();
            return null;
        }

        public int GetLevel(IItem item)
        {
            if (item is not IBatteriePracticeOption)
                throw new System.Exception(item.GetType().ToString());
            return Datum[(IBatteriePracticeOption)item];
        }

        public void AdjustLevel(IItem item, int i)
        {
            if (item is not IBatteriePracticeOption || i < 0)
                throw new System.Exception(item.GetType().ToString() + " " + i);
            Datum[(IBatteriePracticeOption)item] += i;
            PersistentData.Save(this);
        }

        public void SetLevel(IItem item, int level)
        {
            if (item is not IBatteriePracticeOption)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IBatteriePracticeOption)item] = level;
            PersistentData.Save(this);
        }

        private void LoadValue(IItem item, int value)
        {
            if (item is not IBatteriePracticeOption)
                throw new System.Exception(item.GetType().ToString());
            Datum[(IBatteriePracticeOption)item] = value;
        }

        public bool InventoryIsFull(int space) => false;

        public void Reset()
        {
            _datum = SetUpDatum();
            PersistentData.Save(this);
        }

        private BatteriePracticeData() { }

        public static BatteriePracticeData GetData()
        {
            BatteriePracticeData data = new();
            if (data.PersistentData.TryLoadData() is not BatteriePracticeData loadData) return data;
            for (int i = 0; i < data.Items.Length; i++)
                try { data.LoadValue(data.Items[i], loadData.GetLevel(data.Items[i])); }
                catch { }
            return data;
        }

        public IPersistentData PersistentData { get; } = new SaveData(nameof(BatteriePracticeData));
    }

    public interface IBatteriePracticeOption : IItem
    {
        BatteriePracticeOptionEnum Enum { get; }
        int IItem.ID => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Description;
    }

    [Serializable] public struct QRhythmCell : IBatteriePracticeOption { public readonly BatteriePracticeOptionEnum Enum => BatteriePracticeOptionEnum.QRhythmCell; }
    [Serializable] public struct ERhythmCell : IBatteriePracticeOption { public readonly BatteriePracticeOptionEnum Enum => BatteriePracticeOptionEnum.ERhythmCell; }
    [Serializable] public struct SRhythmCell : IBatteriePracticeOption { public readonly BatteriePracticeOptionEnum Enum => BatteriePracticeOptionEnum.SRhythmCell; }

    [Serializable]
    public class BatteriePracticeOptionEnum : Enumeration
    {

        public BatteriePracticeOptionEnum() : base(0, null) { }
        public BatteriePracticeOptionEnum(int id, string name) : base(id, name) { }

        public readonly string Description;
        public static BatteriePracticeOptionEnum QRhythmCell = new(0, "Quarter Note Rhythms");
        public static BatteriePracticeOptionEnum ERhythmCell = new(1, "Eighth Note Rhythms");
        public static BatteriePracticeOptionEnum SRhythmCell = new(2, "Sixteenth Note Rhythms");

        internal static IItem ToItem(BatteriePracticeOptionEnum @enum)
        {
            return @enum switch
            {
                _ when @enum == QRhythmCell => new QRhythmCell(),
                _ when @enum == ERhythmCell => new ERhythmCell(),
                _ when @enum == SRhythmCell => new SRhythmCell(),
                _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
            };
        }

    }

    public class BatteriePracticeMenuScene : IMenuScene
    {
        public string Name { get; } = nameof(PracticeMenuScene);

        public void Initialize()
        {
            South.SetTextString("Back").SetImageColor(Color.white);
            East.SetTextString("Confirm").SetImageColor(Color.white);
            ((IMenuScene)this).SetCardPos1(South);
            ((IMenuScene)this).SetCardPos2(East);
        }
        public void ShowButtons()
        {
            East.SetTextString("Confirm").SetImageColor(Color.white);
        }
        public void HideButtons()
        {
            East.SetTextString("").SetImageColor(Color.clear);
        }

        public void SelfDestruct()
        {
            Hud?.SelfDestruct();
            Hud = null;
            South = null;
            West = null;
            East = null;
            North = null;
            L1 = null;
            R1 = null;
        }

        public Transform TF => null;

        public Card Hud { get; set; }
        public Card North { get; set; }
        public Card East { get; set; }
        public Card South { get; set; }
        public Card West { get; set; }
        public Card L1 { get; set; }
        public Card R1 { get; set; }
    }
}