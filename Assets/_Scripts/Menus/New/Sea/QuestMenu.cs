using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;


namespace Menus
{
    public class QuestsMenu : IMenu
    {
        public QuestsMenu(QuestData questData) { Data = questData; QuestData = questData; }
        public IData Data { get; }
        readonly QuestData QuestData;
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string GetDescription { get => Selection.Item.Description; }
        public string DisplayData(IItem item)
        {
            // Debug.Log(item.Name);
            // Debug.Log(Data.GetDisplayLevel(item));
            // Debug.Log(QuestData.GetQuest(item));
            // Debug.Log(QuestData.GetQuest(item)?.QuestLocation);
            if (QuestData.GetQuest(item) is null) return item.Name + ": (none active)";

            if (QuestData.GetQuest(item).Complete) return
                item.Name + ": " +
                "Claim bounty from a " +
                ((Quests.BountyQuest)QuestData.GetQuest(item)).Standing.ToRegionalName() + " ship";

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
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
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

        public State ConsequentState => null; private IMenuScene _scene;
        public IMenuScene Scene => _scene ??= new MenuScene();

        public class MenuScene : IMenuScene
        {
            // public InventoryMenuScene() { Initialize(); }
            public string Name { get; } = nameof(MenuScene);
            public void Initialize()
            {
                South.SetTextString("Back").SetImageColor(Color.white);
                North.SetTextString("Increase").SetImageColor(Color.clear).SetTextColor(Color.clear);
                North.SetTextString("Decrease").SetImageColor(Color.clear).SetTextColor(Color.clear);
                ((IMenuScene)this).SetCardPos1(South);
                ((IMenuScene)this).SetCardPos2(East);
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
}
