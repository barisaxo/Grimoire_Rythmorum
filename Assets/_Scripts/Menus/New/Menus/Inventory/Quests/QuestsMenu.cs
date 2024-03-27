using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Data.Inventory;

namespace Menus.Inventory
{
    public class QuestsMenu : IMenu
    {
        public QuestsMenu(QuestData questData) { Data = questData; QuestData = questData; }
        public IData Data { get; }
        readonly QuestData QuestData;
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(DataEnum item)
        {
            Debug.Log(item.Name);
            Debug.Log(Data.GetDisplayLevel(item));
            Debug.Log(QuestData.GetQuest(item));
            Debug.Log(QuestData.GetQuest(item)?.QuestLocation);
            if (QuestData.GetQuest(item) is null) return item.Name;
            return item.Name + ": " +
                Data.GetDisplayLevel(item) + " " +
                QuestData.GetQuest(item)?.QuestLocation
                    .GlobalCoordsToLatLongs(Sea.WorldMapScene.Io.Map.GlobalSize);
            // return item switch
            // {
            //     _ when item == QuestData.DataItem.StarChart =>
            //         item.Name + ": " + Data.GetDisplayLevel(item) + " " + QuestData.GetQuest(item)?.QuestLocation.GlobalCoordsToLatLongs(Sea.WorldMapScene.Io.Map.GlobalSize),
            //     _ => "stuff",
            // };
            // return item.Name + ": " + Data.GetDisplayLevel(item);
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            // North = new ButtonInput(IncreaseItem),
            // West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, Selection, MenuItems)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, Selection, MenuItems)),
        };

        // private void IncreaseItem()
        // {
        //     Data.IncreaseLevel(Selection.Item);
        //     Selection.Card.SetTextString(DisplayData(Selection.Item));
        // }

        // private void DecreaseItem()
        // {
        //     Data.DecreaseLevel(Selection.Item);
        //     Selection.Card.SetTextString(DisplayData(Selection.Item));
        // }

        public State ConsequentState => null;
        public IMenuScene Scene => null;
    }
}
