using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;

namespace Menus.Two
{
    public class SkillsMenu : IMenu
    {
        public SkillsMenu(SkillData data, PlayerData playerData, State subsequentState)
        {
            Data = data;
            PlayerData = playerData;
            SubsequentState = subsequentState;

            // ConsequentState = new Menu_State(this, Selection);
        }

        readonly State SubsequentState;

        public PlayerData PlayerData;
        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(IItem item)
        {
            return item.Name;
        }

        public string GetDescription
        {
            get
            {
                return ((ISkill)Selection.Item).Description +
                    "\n" + ((ISkill)Selection.Item).Per + "% bonus per level." +
                    "\nCurrent Level: " + Data.GetLevel(Selection.Item) + " / " + ((ISkill)Selection.Item).MaxLevel +
                    "\nCurrent Bonus: " + (int)(Data.GetLevel(Selection.Item) * ((ISkill)Selection.Item).Per) + "%" +
                    "\nCost: " + ((SkillData)Data).GetSkillCost(Selection.Item) + " patterns." +
                    "\n(" + PlayerData.GetLevel(new PatternsAvailable()) + " patterns available)"
                     ;
            }
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            North = new ButtonInput(IncreaseItem),
            // West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
            South = new ButtonInput(SouthPressed),
        };

        private void SouthPressed()
        {
            ConsequentState = SubsequentState;
        }

        private void IncreaseItem()
        {
            if (((SkillData)Data).GetSkillCost(Selection.Item) >
                PlayerData.GetLevel(new PatternsAvailable()) &&
                Data.GetLevel(Selection.Item) < ((ISkill)Selection.Item).MaxLevel)
                return;

            PlayerData.AdjustLevel(new PatternsSpent(), +
                ((SkillData)Data).GetSkillCost(Selection.Item));

            Data.AdjustLevel(Selection.Item, 1);

            Layout.UpdateText(this);
            // Layout.ScrollMenuItems(Dir.Reset, Selection, MenuItems);
            // Selection.Card.SetTextString(DisplayData(Selection.Item));
        }


        public State ConsequentState { get; private set; }
        public IMenuScene Scene => null;


    }
}