using UnityEngine;

namespace Menus
{
    public interface IMenuLayout
    {
        public Card Card { get; }

        public TMPro.TextAlignmentOptions ItemTextAlignment { get; }
        public TMPro.TextAlignmentOptions DescTextAlignment { get; }
        public Vector2 ItemTMPRectPivot { get; }
        public Vector2 DescTMPRectPivot { get; }
        public Vector2 GetDescPosition();
        public Vector2 GetTextPosition(int i, int length);
        public Vector2 GetImagePosition(int i, int length);

        public MenuItem ScrollMenuItems(Dir dir, IMenu menu);
        public void UpdateText(IMenu menu);
    }

    public class AlignLeft : IMenuLayout
    {
        public Card Card { get; set; }
        public Vector2 GetDescPosition() => new(Cam.UIOrthoX - 1f, -Cam.UIOrthoY * .5f);
        public Vector2 GetTextPosition(int i, int length) => new(-Cam.UIOrthoX + 2.5f, 1.8f - (i * .8f));
        public Vector2 GetImagePosition(int i, int length) => new(0, -Cam.UIOrthoY * .25f);
        public Vector2 ItemTMPRectPivot => new(0, .5f);
        public Vector2 DescTMPRectPivot => new(1, .5f);
        public TMPro.TextAlignmentOptions ItemTextAlignment => TMPro.TextAlignmentOptions.Left;
        public TMPro.TextAlignmentOptions DescTextAlignment => TMPro.TextAlignmentOptions.Left;

        public MenuItem ScrollMenuItems(Dir dir, IMenu menu)
        {
            menu.Selection = dir switch
            {
                Dir.Up => PrevItem(menu.Selection, menu.MenuItems),
                Dir.Down => NextItem(menu.Selection, menu.MenuItems),
                _ => menu.Selection,
            };
            UpdateText(menu);
            return menu.Selection;
        }

        MenuItem NextItem(MenuItem current, MenuItem[] menuItems) =>
            current == menuItems[^1] ? current : menuItems[current + 1];

        MenuItem PrevItem(MenuItem current, MenuItem[] menuItems) =>
            current <= 0 ? current : menuItems[current - 1];

        public void UpdateText(IMenu menu)
        {
            for (int i = 0; i < menu.MenuItems.Length; i++)
                if (menu.MenuItems[i].Card.GO.activeInHierarchy)
                    menu.MenuItems[i].Card
                        .SetTextColor(menu.Selection == i ? Color.white : Color.gray)
                        .SetTextString(menu.DisplayData(menu.MenuItems[i].Item));

            menu?.Description?.SetTextString(menu?.GetDescription);
        }

        // if (menu.MenuItems[i].Card.Children is not null)
        //     foreach (Card child in menu.MenuItems[i].Card.Children)
        //     {
        //         if (child.ImageExists) child.SetImageColor(menu.Selection == i ? Color.white : Color.clear);
        //         if (child.TMPExists) child.SetTextColor(menu.Selection == i ? Color.white : Color.clear);
        //     }


    }

    public class LeftScroll : IMenuLayout
    {
        public Card Card { get; set; }

        public Vector2 GetDescPosition() => new(Cam.UIOrthoX - 1f, 2);
        public Vector2 GetTextPosition(int i, int length) => new(-Cam.UIOrthoX + 2.5f, -(i * .8f));
        public Vector2 GetImagePosition(int i, int length) => new(Cam.UIOrthoX - 2.5f, -2);
        public Vector2 ItemTMPRectPivot => new(0, .5f);
        public Vector2 DescTMPRectPivot => new(1, .5f);
        public TMPro.TextAlignmentOptions ItemTextAlignment => TMPro.TextAlignmentOptions.Left;
        public TMPro.TextAlignmentOptions DescTextAlignment => TMPro.TextAlignmentOptions.Right;

        public MenuItem ScrollMenuItems(Dir dir, IMenu menu)
        {
            menu.Selection = dir switch
            {
                Dir.Up => PrevItem(menu.Selection, menu.MenuItems),
                Dir.Down => NextItem(menu.Selection, menu.MenuItems),
                _ => menu.Selection,
            };
            UpdateText(menu);
            return menu.Selection;
        }

        MenuItem NextItem(MenuItem current, MenuItem[] menuItems) =>
            current == menuItems[^1] ? current : menuItems[current + 1];

        MenuItem PrevItem(MenuItem current, MenuItem[] menuItems) =>
            current <= 0 ? current : menuItems[current - 1];

        // public void UpdateText(MenuItem[] MenuItems, MenuItem Selection)
        // {
        //     for (int i = 0; i < MenuItems.Length; i++)
        //     {
        //         if (MenuItems[i].Card.GO.activeInHierarchy)
        //             MenuItems[i].Card.SetTextColor(Selection == i ? Color.white : Color.gray);

        //         if (MenuItems[i].Card.Children is not null)
        //             foreach (Card child in MenuItems[i].Card.Children)
        //             {
        //                 if (child.ImageExists) child.SetImageColor(Selection == i ? Color.white : Color.clear);
        //                 if (child.TMPExists) child.SetTextColor(Selection == i ? Color.white : Color.clear);
        //             }

        //        menu.Description?.SetTextString(menu.GetDescription);
        //     }
        // }

        public void UpdateText(IMenu menu)
        {
            for (int i = 0; i < menu.MenuItems.Length; i++)
            {
                menu.MenuItems[i].Card.SetTMPPosition(
                    new(-Cam.UIOrthoX + 2.5f, (menu.Selection * .8f) - (i * .8f)));

                if (menu.MenuItems[i].Card.GO.activeInHierarchy)
                    menu.MenuItems[i].Card
                        .SetTextColor(menu.Selection == i ? Color.white : Color.gray)
                        .SetTextString(menu.DisplayData(menu.MenuItems[i].Item));
            }
            menu?.Description?.SetTextString(menu.GetDescription);
        }

    }

    public class ScrollingHeader : IMenuLayout
    {
        const int spacing = 4;
        public Card Card { get; set; }
        public Vector2 GetDescPosition() => Vector2.zero;
        public Vector2 GetTextPosition(int i, int length) => new(i * spacing, Cam.UIOrthoY - 1);
        public Vector2 GetImagePosition(int i, int length) => Vector2.zero;
        public Vector2 ItemTMPRectPivot => new(.5f, .5f);
        public Vector2 DescTMPRectPivot => new(.5f, .5f);
        public TMPro.TextAlignmentOptions ItemTextAlignment => TMPro.TextAlignmentOptions.Left;
        public TMPro.TextAlignmentOptions DescTextAlignment => TMPro.TextAlignmentOptions.Left;

        public MenuItem ScrollMenuItems(Dir dir, IMenu menu)
        {
            menu.Selection = dir switch
            {
                Dir.Left => PrevItem(menu.Selection, menu.MenuItems),
                Dir.Right => NextItem(menu.Selection, menu.MenuItems),
                _ => menu.Selection,
            };
            UpdateText(menu);
            return menu.Selection;
        }

        MenuItem NextItem(MenuItem current, MenuItem[] menuItems) =>
            current == menuItems[^1] ? current : menuItems[current + 1];

        MenuItem PrevItem(MenuItem current, MenuItem[] menuItems) =>
            current <= 0 ? current : menuItems[current - 1];

        public void UpdateText(IMenu menu)
        {
            for (int i = 0; i < menu.MenuItems.Length; i++)
            {
                menu.MenuItems[i].Card.SetTMPPosition(new(
                    -(menu.Selection * spacing) + (i * spacing), Cam.UIOrthoY - 1));

                if (menu.MenuItems[i].Card.GO.activeInHierarchy)
                    menu.MenuItems[i].Card.SetTextColor(menu.Selection == i ? Color.white : Color.gray)
                    .SetTextString(menu.DisplayData(menu.MenuItems[i].Item));

                // if (MenuItems[i].Card.Children is not null)
                //     foreach (Card child in MenuItems[i].Card.Children)
                //     {
                //         if (child.ImageExists) child.SetImageColor(Selection == i ? Color.white : Color.clear);
                //         if (child.TMPExists) child.SetTextColor(Selection == i ? Color.white : Color.clear);
                //     }
                menu?.Description?.SetTextString(menu.GetDescription);
            }
        }
    }

    public class AlignRight : IMenuLayout
    {
        public Vector2 GetDescPosition() => new(0, -Cam.UIOrthoY * .5f);
        public Vector2 GetTextPosition(int i, int length) => new(Cam.UIOrthoX - 2.5f, 1.8f - (i * .8f));
        public Vector2 GetImagePosition(int i, int length) => new(0, Cam.UIOrthoY * .5f);
        public Vector2 ItemTMPRectPivot => new(1, .5f);
        public Vector2 DescTMPRectPivot => new(.5f, .5f);
        public TMPro.TextAlignmentOptions ItemTextAlignment => TMPro.TextAlignmentOptions.Right;
        public TMPro.TextAlignmentOptions DescTextAlignment => TMPro.TextAlignmentOptions.Left;
        public Card Card { get; set; }

        public MenuItem ScrollMenuItems(Dir dir, IMenu menu)
        {
            menu.Selection = dir switch
            {
                Dir.Up => PrevItem(menu.Selection, menu.MenuItems),
                Dir.Down => NextItem(menu.Selection, menu.MenuItems),
                _ => menu.Selection,
            };
            UpdateText(menu);
            return menu.Selection;
        }

        MenuItem NextItem(MenuItem current, MenuItem[] menuItems) =>
            current == menuItems[^1] ? current : menuItems[current + 1];

        MenuItem PrevItem(MenuItem current, MenuItem[] menuItems) =>
            current <= 0 ? current : menuItems[current - 1];

        public void UpdateText(IMenu menu)
        {
            for (int i = 0; i < menu.MenuItems.Length; i++)
            {
                if (menu.MenuItems[i].Card.GO.activeInHierarchy)
                    menu.MenuItems[i].Card
                        .SetTextColor(menu.Selection == i ? Color.white : Color.gray)
                        .SetTextString(menu.DisplayData(menu.MenuItems[i].Item));

                // if (MenuItems[i].Card.Children is not null)
                //     foreach (Card child in MenuItems[i].Card.Children)
                //     {
                //         if (child.ImageExists) child.SetImageColor(Selection == i ? Color.white : Color.clear);
                //         if (child.TMPExists) child.SetTextColor(Selection == i ? Color.white : Color.clear);
                //     }
            }
            menu?.Description?.SetTextString(menu.GetDescription);
        }
    }

    public class Header : IMenuLayout
    {
        public Vector2 GetDescPosition() => Vector2.zero;
        public Vector2 GetTextPosition(int i, int length) => new(
            2 - Cam.UIOrthoX + (2 * (Cam.UIOrthoX - 2) / (length - 1) * i),
            Cam.UIOrthoY - 1);
        public Vector2 GetImagePosition(int i, int length) => Vector2.zero;
        public Vector2 ItemTMPRectPivot => new(.5f, .5f);
        public Vector2 DescTMPRectPivot => new(.5f, .5f);
        public TMPro.TextAlignmentOptions ItemTextAlignment => TMPro.TextAlignmentOptions.Center;
        public TMPro.TextAlignmentOptions DescTextAlignment => TMPro.TextAlignmentOptions.Left;
        public Card Card { get; set; }

        public MenuItem ScrollMenuItems(Dir dir, IMenu menu)
        {
            menu.Selection = dir switch
            {
                Dir.Left => PrevItem(menu.Selection, menu.MenuItems),
                Dir.Right => NextItem(menu.Selection, menu.MenuItems),
                _ => menu.Selection,
            };
            UpdateText(menu);
            return menu.Selection;
        }

        MenuItem NextItem(MenuItem current, MenuItem[] menuItems) =>
            current == menuItems[^1] ? current : menuItems[current + 1];

        MenuItem PrevItem(MenuItem current, MenuItem[] menuItems) =>
            current <= 0 ? current : menuItems[current - 1];

        public void UpdateText(IMenu menu)
        {
            for (int i = 0; i < menu.MenuItems.Length; i++)
            {
                if (menu.MenuItems[i].Card.GO.activeInHierarchy)
                    menu.MenuItems[i].Card
                        .SetTextColor(menu.Selection == i ? Color.white : Color.gray)
                        .SetTextString(menu.DisplayData(menu.MenuItems[i].Item));

                // if (menu.MenuItems[i].Card.Children is not null)
                //     foreach (Card child in menu.MenuItems[i].Card.Children)
                //     {
                //         if (child.ImageExists) child.SetImageColor(Selection == i ? Color.white : Color.clear);
                //         if (child.TMPExists) child.SetTextColor(Selection == i ? Color.white : Color.clear);
                //     }
            }
            menu?.Description?.SetTextString(menu.GetDescription);
        }
    }

    public class TwoColumns : IMenuLayout
    {
        public Vector2 GetDescPosition() => new(0, -Cam.UIOrthoY * .5f);
        public Vector2 GetImagePosition(int i, int length) => new(0, -Cam.UIOrthoY * .25f);
        public Vector2 GetTextPosition(int i, int length) => new(
                i < length * .5f ? -Cam.UIOrthoX + 2.5f : 2,
                -1.8f - (i % Mathf.CeilToInt(length * .5f) * .8f) + (length * .5f));
        public Vector2 ItemTMPRectPivot => new(0, .5f);
        public Vector2 DescTMPRectPivot => new(.5f, .5f);
        public TMPro.TextAlignmentOptions ItemTextAlignment => TMPro.TextAlignmentOptions.Left;
        public TMPro.TextAlignmentOptions DescTextAlignment => TMPro.TextAlignmentOptions.Left;
        public Card Card { get; set; }

        public MenuItem ScrollMenuItems(Dir dir, IMenu menu)
        {
            menu.Selection = dir switch
            {
                Dir.Up => PrevItem(menu.Selection, menu.MenuItems),
                Dir.Down => NextItem(menu.Selection, menu.MenuItems),
                Dir.Left => ScrollLeft(menu.Selection, menu.MenuItems),
                Dir.Right => ScrollRight(menu.Selection, menu.MenuItems),
                _ => menu.Selection,
            };
            UpdateText(menu);
            return menu.Selection;
        }

        MenuItem NextItem(MenuItem current, MenuItem[] menuItems) =>
            (current == Mathf.FloorToInt((menuItems.Length - .5f) * .5f) ||
             current == menuItems[^1]) ?
             current : menuItems[current + 1];

        MenuItem PrevItem(MenuItem current, MenuItem[] menuItems) =>
            (current == Mathf.CeilToInt((menuItems.Length - .5f) * .5f) ||
             current <= 0) ?
             current : menuItems[current - 1];

        MenuItem ScrollRight(MenuItem current, MenuItem[] menuItems) => current + Mathf.CeilToInt((menuItems.Length - .5f) * .5f) < menuItems.Length ?
            menuItems[current + Mathf.CeilToInt((menuItems.Length - .5f) * .5f)] : current;

        MenuItem ScrollLeft(MenuItem current, MenuItem[] menuItems) => current - Mathf.CeilToInt((menuItems.Length - .5f) * .5f) >= 0 ?
            menuItems[current - Mathf.CeilToInt((menuItems.Length - .5f) * .5f)] : current;

        public void UpdateText(IMenu menu)
        {
            for (int i = 0; i < menu.MenuItems.Length; i++)
            {
                if (menu.MenuItems[i].Card.GO.activeInHierarchy)
                    menu.MenuItems[i].Card
                        .SetTextColor(menu.Selection == i ? Color.white : Color.gray)
                        .SetTextString(menu.DisplayData(menu.MenuItems[i].Item));

                // if (menu.MenuItems[i].Card.Children is not null)
                //     foreach (Card child in menu.MenuItems[i].Card.Children)
                //     {
                //         if (child.ImageExists) child.SetImageColor(menu.Selection == i ? Color.white : Color.clear);
                //         if (child.TMPExists) child.SetTextColor(Selection == i ? Color.white : Color.clear);
                //     }
            }
            menu?.Description?.SetTextString(menu.GetDescription);
        }
    }
}
