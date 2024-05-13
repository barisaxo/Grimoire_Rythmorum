// using System;

// namespace Menus.One
// {
//     public struct MenuItem
//     {
//         public DataEnum Item;
//         public Card Card;

//         public static int operator +(MenuItem a, int b) => a.Item.Id + b;
//         public static int operator -(MenuItem a, int b) => a.Item.Id - b;
//         public static int operator +(MenuItem a, MenuItem b) => a.Item.Id + b.Item.Id;
//         public static int operator -(MenuItem a, MenuItem b) => a.Item.Id - b.Item.Id;

//         public static bool operator ==(MenuItem a, int b) => a.Item.Id == b;
//         public static bool operator !=(MenuItem a, int b) => a.Item.Id != b;
//         public static bool operator ==(MenuItem a, MenuItem b) => a.Item.Id == b.Item.Id;
//         public static bool operator !=(MenuItem a, MenuItem b) => a.Item.Id != b.Item.Id;

//         public static bool operator <=(MenuItem a, int b) => a.Item.Id <= b;
//         public static bool operator >=(MenuItem a, int b) => a.Item.Id >= b;
//         public static bool operator <=(MenuItem a, MenuItem b) => a.Item.Id <= b.Item.Id;
//         public static bool operator >=(MenuItem a, MenuItem b) => a.Item.Id >= b.Item.Id;

//         public static implicit operator int(MenuItem a) => a.Item.Id;

//         public readonly override bool Equals(object obj) =>
//             Item != null && obj is MenuItem e &&
//             e.Item != null && Item.Id == e.Item.Id;
//         public readonly override int GetHashCode() => HashCode.Combine(Item.Id);
//     }
// }
