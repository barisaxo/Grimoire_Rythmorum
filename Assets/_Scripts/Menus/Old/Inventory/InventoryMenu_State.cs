
// using OldMenus;
// using System;

// public class InventoryMenu_State<T1, T1Menu, T2, T2Menu> : State
//     where T1 : DataEnum, new()
//     where T1Menu : Menu<T1, T1Menu>
//     where T2 : DataEnum, new()
//     where T2Menu : Menu<T2, T2Menu>
// {
//     public Menu<T1, T1Menu> Header;
//     public Menu<T2, T2Menu> Sub;

//     public InventoryMenu_State(Menu<T1, T1Menu> header, Menu<T2, T2Menu> sub)
//     {
//         Header = header;
//         Sub = sub;
//     }

//     public InventoryMenu_State(Menu<T2, T2Menu> sub)
//     {
//         Sub = sub;
//     }

//     protected override void PrepareState(Action callback)
//     {
//         Header?.Initialize();
//         Sub?.Initialize();
//         base.PrepareState(callback);
//     }

//     protected override void DirectionPressed(Dir dir)
//     {
//         switch (dir)
//         {
//             case Dir.Up: Sub?.Up?.Action(); break;
//             case Dir.Down: Sub?.Down?.Action(); break;
//             case Dir.Left: Sub?.Left?.Action(); break;
//             case Dir.Right: Sub?.Right?.Action(); break;
//         }
//     }

//     protected override void R1Pressed() => Sub?.R1?.Action();
//     protected override void L1Pressed() => Sub?.L1?.Action();
//     protected override void EastPressed() => Sub?.East?.Action();
//     protected override void NorthPressed() => Sub?.North?.Action();
//     protected override void WestPressed() => Sub?.West?.Action();
//     protected override void SouthPressed()
//     {
//         //todo leave scene or back out
//     }

// }
