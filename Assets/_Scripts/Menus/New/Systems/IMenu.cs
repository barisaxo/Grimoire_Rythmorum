using UnityEngine;
using System;
using Data;

namespace Menus
{
    public interface IMenu
    {
        public State ConsequentState { get; }
        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; }
        public IInputHandler Input { get; }
        public IMenuScene Scene { get; }
        public string GetDescription { get; }
        public string DisplayData(IItem item);

        public void SelfDestruct()
        {
            for (int i = 0; i < MenuItems.Length; i++)
                MenuItems[i].Card?.SelfDestruct();
            Description?.SelfDestruct();
            Scene?.Hud?.SelfDestruct();
            Scene?.SelfDestruct();
            Resources.UnloadUnusedAssets();
        }

        public void SetUpMenuCards()
        {
            MenuItem[] items = new MenuItem[Data.Items.Length];
            // Debug.Log(items.Length);

            for (int i = 0; i < items.Length; i++)
            {
                // Debug.Log(Data.Items[i].Name + " " + i);
                items[i] = new()
                {
                    Item = Data.Items[i],
                    Card = new Card(Data.Items[i].Name, null)
                       .SetTextString(DisplayData(Data.Items[i]))
                       .AutoSizeTextContainer(true)
                       .SetTMPPosition(Layout.GetTextPosition(i, items.Length))
                       .AutoSizeFont(true)
                       .SetTextAlignment(Layout.ItemTextAlignment)
                       .AllowWordWrap(false)
                       .SetTMPRectPivot(Layout.ItemTMPRectPivot)
                       .SetFontScale(.6f, .6f)
                    //    .TMPClickable()
                };
            }

            MenuItems = items;
            Selection = MenuItems[0];
            Layout.ScrollMenuItems(Dir.Reset, this);
        }

        public void SetUpDescription()
        {
            Description = new Card(nameof(Description), null)
                .AutoSizeTextContainer(true)
                .SetTMPRectPivot(Layout.DescTMPRectPivot)
                .SetTMPPosition(Layout.GetDescPosition())
                .AutoSizeFont(true)
                .SetTextAlignment(Layout.DescTextAlignment)
                .AllowWordWrap(false)
                .SetFontScale(.5f, .5f);
        }
    }

    // public class EmptyDataItem : IItem
    // {
    //     public EmptyDataItem() : base(0, "") { }
    //     public EmptyDataItem(int id, string name) : base(id, name) { }
    //     public static EmptyDataItem Empty = new(0, nameof(Empty));

    //     string IItem.Name => throw new NotImplementedException();

    //     string IItem.Description => throw new NotImplementedException();

    //     int IItem.ID => throw new NotImplementedException();
    // }

    public interface IHeaderMenu : IMenu
    {
        public IMenu[] SubMenus { get; }
        public IMenu CurrentSub { get; }
    }

    public struct MenuItem
    {
        public IItem Item;
        public Card Card;

        public static int operator +(MenuItem a, int b) => a.Item.ID + b;
        public static int operator -(MenuItem a, int b) => a.Item.ID - b;
        public static int operator +(MenuItem a, MenuItem b) => a.Item.ID + b.Item.ID;
        public static int operator -(MenuItem a, MenuItem b) => a.Item.ID - b.Item.ID;

        public static bool operator ==(MenuItem a, int b) => a.Item.ID == b;
        public static bool operator !=(MenuItem a, int b) => a.Item.ID != b;
        public static bool operator ==(MenuItem a, MenuItem b) => a.Item.ID == b.Item.ID;
        public static bool operator !=(MenuItem a, MenuItem b) => a.Item.ID != b.Item.ID;

        public static bool operator <=(MenuItem a, int b) => a.Item.ID <= b;
        public static bool operator >=(MenuItem a, int b) => a.Item.ID >= b;
        public static bool operator <=(MenuItem a, MenuItem b) => a.Item.ID <= b.Item.ID;
        public static bool operator >=(MenuItem a, MenuItem b) => a.Item.ID >= b.Item.ID;

        public static implicit operator int(MenuItem a) => a.Item.ID;

        public readonly override bool Equals(object obj) =>
            Item != null && obj is MenuItem e &&
            e.Item != null && Item.ID == e.Item.ID;

        public readonly override int GetHashCode() => HashCode.Combine(Item.ID);
    }
}
