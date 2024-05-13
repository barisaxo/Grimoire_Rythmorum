// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Data;
// using Data.Player;

// namespace Menus.One
// {
//     public class SkillsMenu : IMenu
//     {
//         public SkillsMenu(SkillsData data, Data.Player.PlayerData playerData, State subsequentState)
//         {
//             Data = data;
//             PlayerData = playerData;
//             SubsequentState = subsequentState;

//             // ConsequentState = new Menu_State(this, Selection);
//         }

//         readonly State SubsequentState;

//         public Data.Player.PlayerData PlayerData;
//         public IData Data { get; }
//         public MenuItem Selection { get; set; }
//         public MenuItem[] MenuItems { get; set; }
//         public Card Description { get; set; }
//         public IMenuLayout Layout { get; } = new LeftScroll();

//         public string DisplayData(DataEnum item)
//         {
//             return Data.GetDisplayLevel(item);
//         }

//         public string GetDescription
//         {
//             get
//             {
//                 return Selection.Item.Description + "\n+ " +
//                    ((SkillsData.DataItem)Selection.Item).Per + "% bonus per level." +
//                     "\nCurrent Level: " + Data.GetLevel(Selection.Item) + " / " + ((SkillsData.DataItem)Selection.Item).MaxLevel +
//                     "\nCurrent Bonus: " + (int)(Data.GetLevel(Selection.Item) * ((SkillsData.DataItem)Selection.Item).Per)
//                      + "%\nCost: " + ((Data.Player.SkillsData)Data).GetSkillCost(Selection.Item) + " patterns." +
//                      "\n(" + PlayerData.GetLevel(PlayerData.DataItem.Patterns) + " patterns available)";
//             }
//         }

//         public IInputHandler Input => new MenuInputHandler()
//         {
//             North = new ButtonInput(IncreaseItem),
//             // West = new ButtonInput(DecreaseItem),
//             Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
//             Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
//             South = new ButtonInput(SouthPressed),
//         };

//         private void SouthPressed()
//         {
//             ConsequentState = SubsequentState;
//         }

//         private void IncreaseItem()
//         {
//             if (((Data.Player.SkillsData)Data).GetSkillCost(Selection.Item) >
//                 PlayerData.GetLevel(PlayerData.DataItem.Patterns) &&
//                 Data.GetLevel(Selection.Item) < ((SkillsData.DataItem)Selection.Item).MaxLevel)
//                 return;

//             PlayerData.SetLevel(
//                 PlayerData.DataItem.Patterns,
//                 PlayerData.GetLevel(PlayerData.DataItem.Patterns) -
//                 ((Data.Player.SkillsData)Data).GetSkillCost(Selection.Item));

//             Data.IncreaseLevel(Selection.Item);

//             Layout.UpdateText(this);
//             // Layout.ScrollMenuItems(Dir.Reset, Selection, MenuItems);
//             // Selection.Card.SetTextString(DisplayData(Selection.Item));
//         }

//         private void DecreaseItem()
//         {
//             Data.DecreaseLevel(Selection.Item);
//             // Selection.Card.SetTextString(DisplayData(Selection.Item));
//         }

//         public State ConsequentState { get; private set; }
//         public IMenuScene Scene => null;


//     }
// }