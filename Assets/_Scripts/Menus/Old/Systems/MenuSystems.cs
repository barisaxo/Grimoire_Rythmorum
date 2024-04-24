// using UnityEngine;
// using TMPro;

// namespace OldMenus
// {
//     public static class Menu_Systems
//     {
//         public static void UpdateTextColors<T>(this IMenu<T> menu) where T : DataEnum, new()
//         {
//             for (int i = 0; i < menu.MenuItems.Length; i++)
//                 if (menu.MenuItems[i].Card.GO.activeInHierarchy)
//                     menu.MenuItems[i].Card.SetTextColor(menu.Selection == i ? Color.white : Color.gray);
//         }

//         public static MenuItem<T>[] SetUpMenuCards<T>(this IMenu<T> menu, Transform parent, IMenuLayout<T> layout) where T : DataEnum, new()
//         {
//             MenuItem<T>[] items = new MenuItem<T>[menu.DataItems.Length];
//             for (int i = 0; i < menu.DataItems.Length; i++)
//             {
//                 items[i] = new()
//                 {
//                     Item = menu.DataItems[i],
//                     Card = new Card(menu.DataItems[i].Name, parent)
//                        .SetTextString(menu.DataItems[i].Name)
//                        //.SetTMPSize(new Vector2(7, .7f))
//                        .AutoSizeTextContainer(true)
//                        .SetTMPPosition(layout.GetPosition(i, menu.DataItems.Length))
//                        .AutoSizeFont(true)
//                        .SetTextAlignment(layout.TextAlignment)
//                        .AllowWordWrap(false)
//                        .SetTMPRectPivot(layout.TMPRectPivot)
//                        .SetFontScale(.6f, .6f)
//                        .TMPClickable()
//                 };
//             }

//             return items;
//         }


//     }
// }


// // public static MenuItem<T> PrevItem(this IMenu<T> menu) => menu.Layout switch
// // {
// //     MenuLayoutStyle.TwoColumns => (
// //         menu.Selection == Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f) ||
// //         menu.Selection <= 0) ?
// //             menu.Selection : menu.MenuItems[menu.Selection - 1],

// //     _ => menu.Selection <= 0 ? menu.Selection : menu.MenuItems[menu.Selection - 1]
// // };
// // public static void ScrollMenuItems<T>(this IMenu<T> menu, Dir dir) where T : DataEnum, new()
// // {
// //     switch (menu.Layout)
// //     {
// //         case MenuLayoutStyle.Header:
// //             switch (dir)
// //             {
// //                 case Dir.Left: menu.Selection = PrevItem(); break;
// //                 case Dir.Right: menu.Selection = NextItem(); break;
// //             }; break;

// //         case MenuLayoutStyle.TwoColumns:
// //             switch (dir)
// //             {
// //                 case Dir.Up: menu.Selection = PrevItem(); break;
// //                 case Dir.Down: menu.Selection = NextItem(); break;
// //                 case Dir.Left: if (menu.Layout == MenuLayoutStyle.TwoColumns) menu.Selection = ScrollLeft(); break;
// //                 case Dir.Right: if (menu.Layout == MenuLayoutStyle.TwoColumns) menu.Selection = ScrollRight(); break;
// //             }; break;

// //         default:
// //             switch (dir)
// //             {
// //                 case Dir.Up: menu.Selection = PrevItem(); break;
// //                 case Dir.Down: menu.Selection = NextItem(); break;
// //             }; break;
// //     }

// //     menu.UpdateTextColors();

// //     MenuItem<T> PrevItem() => menu.Layout switch
// //     {
// //         MenuLayoutStyle.TwoColumns => (
// //             menu.Selection == Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f) ||
// //             menu.Selection <= 0) ?
// //                 menu.Selection : menu.MenuItems[menu.Selection - 1],

// //         _ => menu.Selection <= 0 ? menu.Selection : menu.MenuItems[menu.Selection - 1]
// //     };

// //     MenuItem<T> NextItem() => menu.Layout switch
// //     {
// //         MenuLayoutStyle.TwoColumns => (
// //          menu.Selection == Mathf.FloorToInt((menu.MenuItems.Length - .5f) * .5f) ||
// //          menu.Selection == menu.MenuItems[^1]) ?
// //             menu.Selection : menu.MenuItems[menu.Selection + 1],

// //         _ => menu.Selection == menu.MenuItems[^1] ? menu.Selection : menu.MenuItems[menu.Selection + 1]
// //     };

// //     MenuItem<T> ScrollRight() => menu.Selection + Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f) < menu.MenuItems.Length ?
// //         menu.MenuItems[menu.Selection + Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f)] : menu.Selection;

// //     MenuItem<T> ScrollLeft() => menu.Selection - Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f) >= 0 ?
// //         menu.MenuItems[menu.Selection - Mathf.CeilToInt((menu.MenuItems.Length - .5f) * .5f)] : menu.Selection;
// // }