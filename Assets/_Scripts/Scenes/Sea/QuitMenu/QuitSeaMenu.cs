using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menus;

namespace Menus.QuitMenu
{
    public class QuitSeaMenu : Menu<QuitSeaMenu.QuitMenuItem, QuitSeaMenu>
    {
        public QuitSeaMenu() : base(nameof(QuitSeaMenu)) { }

        private BackButton _back;
        public BackButton Back => _back ??= new BackButton(Parent);
        public override MenuLayoutStyle Style => MenuLayoutStyle.AlignRight;

        public override Menu<QuitMenuItem, QuitSeaMenu> Initialize()
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
            public static readonly QuitMenuItem AbandonRun = new(0, nameof(AbandonRun).CapsCase());
            public static readonly QuitMenuItem QuitToMainMenu = new(1, nameof(QuitToMainMenu).CapsCase());
            public static readonly QuitMenuItem QuitGame = new(2, nameof(QuitGame).CapsCase());

            public QuitMenuItem() : base(0, "") { }
            private QuitMenuItem(int id, string name) : base(id, name) { }
        }


    }
}