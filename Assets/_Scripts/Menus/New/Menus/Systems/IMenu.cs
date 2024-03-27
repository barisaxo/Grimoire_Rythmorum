using UnityEngine;

namespace Menus
{
    public interface IMenu
    {
        public State ConsequentState { get; }
        public Data.IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public IMenuLayout Layout { get; }
        public IInputHandler Input { get; }
        public IMenuScene Scene { get; }
        public string DisplayData(DataEnum item);

        public void SelfDestruct()
        {
            for (int i = 0; i < MenuItems.Length; i++)
                MenuItems[i].Card.SelfDestruct();
            Scene?.SelfDestruct();
            Resources.UnloadUnusedAssets();
        }

        public void SetUpMenuCards()
        {
            MenuItem[] items = new MenuItem[Data.DataItems.Length];//Data.DataItems.Length == 0 ? 1 :

            // if (Data.DataItems.Length == 0)
            // {
            //     items[0] = new MenuItem()
            //     {
            //         Item = EmptyDataItem.Empty,
            //         Card = new Card("Empty", null)
            //            .SetTextString("Empty")
            //            //.SetTMPSize(new Vector2(7, .7f))
            //            .AutoSizeTextContainer(true)
            //            .SetTMPPosition(Layout.GetTextPosition(0, 1))
            //            .AutoSizeFont(true)
            //            .SetTextAlignment(Layout.ItemTextAlignment)
            //            .AllowWordWrap(true)
            //            .SetTMPRectPivot(Layout.ItemTMPRectPivot)
            //            .SetFontScale(.6f, .6f)
            //            .TMPClickable()
            //     };
            // }

            // else
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new()
                {
                    Item = Data.DataItems[i],
                    Card = new Card(Data.DataItems[i].Name, null)
                       .SetTextString(DisplayData(Data.DataItems[i]))
                       //.SetTMPSize(new Vector2(7, .7f))
                       .AutoSizeTextContainer(true)
                       .SetTMPPosition(Layout.GetTextPosition(i, items.Length))
                       .AutoSizeFont(true)
                       .SetTextAlignment(Layout.ItemTextAlignment)
                       .AllowWordWrap(false)
                       .SetTMPRectPivot(Layout.ItemTMPRectPivot)
                       .SetFontScale(.6f, .6f)
                       .TMPClickable()
                };

                // if (Data.DataItems[i].Image != null)
                //     items[i].Card.CreateChild(Data.DataItems[i].Name, items[i].Card.Canvas)
                //         .SetImageSprite(items[i].Item.Image)
                //         .SetImagePosition(Layout.GetImagePosition(i, items.Length));

                if (Data.DataItems[i].Description != null)
                    items[i].Card.CreateChild(Data.DataItems[i].Name, items[i].Card.Canvas)
                       .SetTextString(Data.DataItems[i].Description)
                       //.SetTMPSize(new Vector2(7, .7f))
                       .AutoSizeTextContainer(true)
                       .SetTMPRectPivot(Layout.DescTMPRectPivot)
                       .SetTMPPosition(Layout.GetDescPosition())
                       .AutoSizeFont(true)
                       .SetTextAlignment(Layout.DescTextAlignment)
                       .AllowWordWrap(true)
                       .SetFontScale(.6f, .6f);
            }

            MenuItems = items;
            Selection = MenuItems[0];
            Layout.ScrollMenuItems(Dir.Reset, Selection, MenuItems);
        }

    }

    public class EmptyDataItem : DataEnum
    {
        public EmptyDataItem() : base(0, "") { }
        public EmptyDataItem(int id, string name) : base(id, name) { }
        public static EmptyDataItem Empty = new(0, nameof(Empty));
    }

    public interface IHeaderMenu : IMenu
    {
        public IMenu[] SubMenus { get; }
        public IMenu CurrentSub { get; }
    }
}
