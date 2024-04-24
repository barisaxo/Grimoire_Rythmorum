// using System.Collections.Generic;
// using UnityEngine;
// using System;


// namespace OldMenus
// {
//     public abstract class Menu<T, TMenu> : IMenu<T> where T : DataEnum, new() where TMenu : Menu<T, TMenu>
//     {
//         protected Menu(string name, IMenuLayout<T> layout)
//         {
//             Name = name;
//             Layout = layout;
//         }

//         private readonly string Name;
//         private Transform _parent;
//         protected Transform Parent => _parent != null ? _parent : _parent = new GameObject(Name).transform;
//         public virtual IButtonInput North { get; protected set; }
//         public virtual IButtonInput East { get; protected set; }
//         public virtual IButtonInput West { get; protected set; }
//         public virtual IButtonInput Decrease { get; protected set; }
//         public IButtonInput Up { get; protected set; }
//         public IButtonInput Down { get; protected set; }
//         public IButtonInput Left { get; protected set; }
//         public IButtonInput Right { get; protected set; }
//         public IButtonInput R1 { get; protected set; }
//         public IButtonInput L1 { get; protected set; }
//         private MenuItem<T> _selection { get; set; }
//         public MenuItem<T> Selection { get => _selection; set { _selection = value; ItemDescriptionText = value.Item.Description; } }
//         public T[] DataItems => Enumeration.All<T>();
//         private MenuItem<T>[] _menuItems;
//         public MenuItem<T>[] MenuItems => _menuItems ??= this.SetUpMenuCards(Parent, Layout);
//         public IMenuLayout<T> Layout { get; private set; }
//         public string ItemDescriptionText { set => ItemDescription.TMP.text = value; }
//         private Card _itemDescription;
//         public Card ItemDescription => _itemDescription ??= new Card(nameof(ItemDescription), Parent)
//             .SetFontScale(.6f, .6f)
//             .SetTMPPosition(Layout.GetDescPosition())
//             .SetTMPSize(Cam.UIOrthoX * 1.7f, 1)
//             .AllowWordWrap(false)
//             .AutoSizeFont(true);

//         public virtual Menu<T, TMenu> Initialize()
//         {
//             return Initialize(null);
//         }

//         public virtual Menu<T, TMenu> Initialize(T t)
//         {
//             Selection = MenuItems[t ?? 0];
//             Layout.ScrollMenuItems(Dir.Reset, Selection, MenuItems);
//             this.UpdateTextColors();
//             return this;
//         }

//         public virtual void SelfDestruct()
//         {
//             UnityEngine.Object.Destroy(_parent.gameObject);
//             Resources.UnloadUnusedAssets();
//         }
//     }

//     public struct MenuItem<T> where T : DataEnum, new()
//     {
//         public T Item;
//         public Card Card;

//         public static int operator +(MenuItem<T> a, int b) => a.Item.Id + b;
//         public static int operator -(MenuItem<T> a, int b) => a.Item.Id - b;
//         public static int operator +(MenuItem<T> a, MenuItem<T> b) => a.Item.Id + b.Item.Id;
//         public static int operator -(MenuItem<T> a, MenuItem<T> b) => a.Item.Id - b.Item.Id;

//         public static bool operator ==(MenuItem<T> a, int b) => a.Item.Id == b;
//         public static bool operator !=(MenuItem<T> a, int b) => a.Item.Id != b;
//         public static bool operator ==(MenuItem<T> a, MenuItem<T> b) => a.Item.Id == b.Item.Id;
//         public static bool operator !=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id != b.Item.Id;

//         public static bool operator <=(MenuItem<T> a, int b) => a.Item.Id <= b;
//         public static bool operator >=(MenuItem<T> a, int b) => a.Item.Id >= b;
//         public static bool operator <=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id <= b.Item.Id;
//         public static bool operator >=(MenuItem<T> a, MenuItem<T> b) => a.Item.Id >= b.Item.Id;

//         public static implicit operator int(MenuItem<T> a) => a.Item.Id;

//         public override bool Equals(object obj) => obj is MenuItem<T> e && Item.Id == e.Item.Id;
//         public override int GetHashCode() => HashCode.Combine(Item.Id);
//     }
// }