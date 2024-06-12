using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using TMPro;

namespace Menus
{
    public class GramophoneMenu : IMenu
    {
        public GramophoneMenu(GramophoneInventoryData data, State subsequentState)
        {
            Data = data;
            SubsequentState = subsequentState;
        }

        readonly State SubsequentState;
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

            East = new ButtonInput(Confirm),
            South = new ButtonInput(Back),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
        };

        private void IncreaseItem()
        {
            // Data.AdjustLevel(Selection.Item, 1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        private void DecreaseItem()
        {
            // Data.AdjustLevel(Selection.Item, -1);
            Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        private void Back() { ConsequentState = SubsequentState; }
        private void Confirm() { ConsequentState = new NewGramoPuzzle_State(SubsequentState, true); }

        public State ConsequentState { get; set; }
        public IMenuScene Scene { get; } = new PracticeMenuScene();
    }

    public class PracticeMenuScene : IMenuScene
    {
        public void Initialize()
        {
            South.SetTextString("Back").SetImageColor(Color.white);
            East.SetTextString("Confirm").SetImageColor(Color.white);
            West.SetImageColor(Color.white).SetTextString("Tutorial");
            ((IMenuScene)this).SetCardPos1(South);
            ((IMenuScene)this).SetCardPos2(West);
            ((IMenuScene)this).SetCardPos3(East);
        }

        public void SelfDestruct()
        {
            Hud?.SelfDestruct();
            Resources.UnloadUnusedAssets();
        }

        public Transform TF => null;

        public Card Hud { get; set; }
        public Card North { get; set; }
        public Card East { get; set; }
        public Card South { get; set; }
        public Card West { get; set; }
    }
}