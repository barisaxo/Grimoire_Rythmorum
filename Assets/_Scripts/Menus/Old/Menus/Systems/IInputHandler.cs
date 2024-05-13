using System;

namespace Menus.One
{
    public interface IInputHandler
    {
        public IButtonInput North { get; set; }
        public IButtonInput South { get; set; }
        public IButtonInput East { get; set; }
        public IButtonInput West { get; set; }
        public IButtonInput Up { get; set; }
        public IButtonInput Down { get; set; }
        public IButtonInput Left { get; set; }
        public IButtonInput Right { get; set; }
        public IButtonInput R1 { get; set; }
        public IButtonInput L1 { get; set; }
    }

    public class MenuInputHandler : IInputHandler
    {
        public IButtonInput North { get; set; }
        public IButtonInput South { get; set; }
        public IButtonInput East { get; set; }
        public IButtonInput West { get; set; }
        public IButtonInput Up { get; set; }
        public IButtonInput Down { get; set; }
        public IButtonInput Left { get; set; }
        public IButtonInput Right { get; set; }
        public IButtonInput R1 { get; set; }
        public IButtonInput L1 { get; set; }
    }

    public interface IButtonInput
    {
        public void Action();
    }

    public class ButtonInput : IButtonInput
    {
        private event Action _action;
        void IButtonInput.Action() => _action?.Invoke();
        public ButtonInput(Action action) => _action = action;
    }

}
