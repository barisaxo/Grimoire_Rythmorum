using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Menus.QuitMenu
{
    public class QuitAetherMenu : Menu<QuitAetherMenu.QuitMenuItem, QuitAetherMenu>
    {
        public QuitAetherMenu() : base(nameof(QuitAetherMenu)) { }

        private BackButton _back;
        public BackButton Back => _back ??= new BackButton(Parent);
        public override MenuLayoutStyle Style => MenuLayoutStyle.AlignRight;

        public override Menu<QuitMenuItem, QuitAetherMenu> Initialize()
        {
            _ = Back;
            return base.Initialize();
        }

        public override void SelfDestruct()
        {
            base.SelfDestruct();
        }

        public class QuitMenuItem : DataEnum
        {
            public static readonly QuitMenuItem QuitToMainMenu = new(0, nameof(QuitToMainMenu).CapsCase());
            public static readonly QuitMenuItem QuitGame = new(1, nameof(QuitGame).CapsCase());

            public QuitMenuItem() : base(0, "") { }
            private QuitMenuItem(int id, string name) : base(id, name) { }
        }


    }
}