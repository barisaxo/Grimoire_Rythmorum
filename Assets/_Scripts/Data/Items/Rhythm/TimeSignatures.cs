using System;
using System.Collections.Generic;
using UnityEngine;
using Datum;
using MusicTheory.Rhythms;

namespace Datum.Rhythm
{
    public class TimeSignatures : IData
    {

        private IItem[] _items;
        public IItem[] Items
        {
            get
            {
                return _items ??= SetUp();
                IItem[] SetUp()
                {
                    var enums = Enumeration.All<TimeSignatureEnum>();
                    var temp = new IItem[enums.Length];
                    for (int i = 0; i < temp.Length; i++)
                    {
                        temp[i] = TimeSignatureEnum.ToItem(enums[i]);
                    }
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

    public interface ITimeSignatureData : IItem
    {
        TimeSignatureEnum Enum { get; }
        int IItem.Id => Enum.Id;
        string IItem.Name => Enum.Name;
        string IItem.Description => Enum.Meter.Name.StartCase() + " Time";
    }

    // [Serializable] public struct TwoTwo : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.TwoTwo; }
    // [Serializable] public struct ThreeTwo : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.ThreeTwo; }
    [Serializable] public struct TwoFour : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.TwoFour; }
    [Serializable] public struct ThreeFour : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.ThreeFour; }
    [Serializable] public struct FourFour : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.FourFour; }
    [Serializable] public struct FiveFour23 : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.FiveFour23; }
    [Serializable] public struct FiveFour32 : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.FiveFour32; }
    [Serializable] public struct SixFour : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.SixFour; }
    [Serializable] public struct SevenFour43 : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.SevenFour43; }
    [Serializable] public struct SevenFour34 : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.SevenFour34; }
    // [Serializable] public struct ThreeEight : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.ThreeEight; }
    // [Serializable] public struct FiveEight23 : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.FiveEight23; }
    // [Serializable] public struct FiveEight32 : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.FiveEight32; }
    [Serializable] public struct SixEight : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.SixEight; }
    // [Serializable] public struct SevenEight43 : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.SevenEight43; }
    // [Serializable] public struct SevenEight34 : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.SevenEight34; }
    [Serializable] public struct NineEight : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.NineEight; }
    [Serializable] public struct TwelveEight : ITimeSignatureData { public readonly TimeSignatureEnum Enum => TimeSignatureEnum.TwelveEight; }

}

namespace Menus
{
    public class TimeSignaturesMenu : IMenu
    {

        public TimeSignaturesMenu(State subsequentState)
        {
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
        public IData Data { get; } = new Datum.Rhythm.TimeSignatures();
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(IItem item)
        {
            return item.Name;//+ ": " + Data.GetDisplayLevel(item);
        }

        public string GetDescription => Selection.Item.Description;

        public IInputHandler Input => new MenuInputHandler()
        {
            Up = new ButtonInput(() =>
            {
                Selection = Layout.ScrollMenuItems(Dir.Up, this);
                UpdateButtons();
            }),
            Down = new ButtonInput(() =>
            {
                Selection = Layout.ScrollMenuItems(Dir.Down, this);
                UpdateButtons();
            }),
            East = new ButtonInput(() => ConsequentState = GetNextState),
            South = new ButtonInput(() => ConsequentState = SubsequentState),
        };

        private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new BatteriePracticeMenuScene();

        public State ConsequentState { get; set; }

        public State GetNextState =>
                new BatteryTutorial_State(null, GetSpecs(), Manager.Io.TimeSignatures, Selection.Item, new MenuState(this), false);



        void UpdateButtons()
        {
            // if (BatterieUnlocked) ((BatteriePracticeMenuScene)Scene).ShowButtons();
            // else ((BatteriePracticeMenuScene)Scene).HideButtons();
        }

        private RhythmSpecs GetSpecs()
        {
            return new RhythmSpecs()
                .SetTime((MusicTheory.Rhythms.Time)Enumeration.All<TimeSignatureEnum>()[Selection.Item.Id])
                .SetNumberOfMeasures(4)
                .SetTies(true)
                .SetRests(true)
                .SetTempo(((MusicTheory.Rhythms.Time)Enumeration.All<TimeSignatureEnum>()[Selection.Item.Id]).GetTempo())
                ;
        }
    }
}