using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datum;


namespace Menus
{
    public class ShipStatMenu : IMenu
    {
        public ShipStatMenu(ActiveShipData data) { Data = data; }

        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string GetDescription { get => Selection.Item.Description; }
        public string DisplayData(IItem item)
        {
            return item.Name + ": " + Data.GetDisplayLevel(item);
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            // North = new ButtonInput(IncreaseItem),
            // West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
            // Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
            // Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
        };

        private void IncreaseItem()
        {
            Data.AdjustLevel(Selection.Item, 1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        private void DecreaseItem()
        {
            Data.AdjustLevel(Selection.Item, -1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

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