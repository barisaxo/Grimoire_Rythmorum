// using System;
// using UnityEngine;

// namespace OldMenus
// {
//     public interface IMenu<T> where T : DataEnum, new()
//     {
//         public MenuItem<T> Selection { get; set; }
//         public Card ItemDescription { get; }
//         public MenuItem<T>[] MenuItems { get; }
//         public IMenuLayout<T> Layout { get; }
//         public T[] DataItems { get; }
//         public IButtonInput North { get; }
//         public IButtonInput East { get; }
//         public IButtonInput West { get; }
//         public IButtonInput Up { get; }
//         public IButtonInput Down { get; }
//         public IButtonInput Left { get; }
//         public IButtonInput Right { get; }
//         public IButtonInput R1 { get; }
//         public IButtonInput L1 { get; }
//     }

//     public interface IButtonInput
//     {
//         public void Action();
//     }

//     public class ButtonInput : IButtonInput
//     {
//         private event Action Action;
//         void IButtonInput.Action() => Action?.Invoke();
//         public ButtonInput(Action action) => Action = action;
//     }
// }