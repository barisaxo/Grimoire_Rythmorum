namespace OldMenus.OptionsMenu
{
    public class OptionsMenu : Menu<OptionsMenu.OptionsItem, OptionsMenu>
    {
        private BackButton _back;

        public OptionsMenu() :
            base(
                nameof(OptionsMenu),
                new Header<OptionsItem>())
        { }

        public BackButton Back => _back ??= new BackButton(Parent);

        public override Menu<OptionsItem, OptionsMenu> Initialize(OptionsItem selection)
        {
            _ = Back;
            return base.Initialize(selection);
        }

        public class OptionsItem : DataEnum
        {
            public static readonly OptionsItem Volume = new(0, "VOLUME");
            public static readonly OptionsItem GamePlay = new(1, "GAME PLAY");
            public static OptionsItem Controls = new(2, "CONTROLS");

            public OptionsItem() : base(0, "") { }
            private OptionsItem(int id, string name) : base(id, name) { }
        }
    }
}