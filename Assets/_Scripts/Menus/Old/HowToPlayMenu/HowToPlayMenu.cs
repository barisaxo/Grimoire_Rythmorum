// namespace OldMenus.HowToPlayMenu
// {
//     public class HowToPlayMenu : Menu<HowToPlayMenu.HowToPlayItem, HowToPlayMenu>
//     {
//         private BackButton _back;

//         public HowToPlayMenu() :
//             base(
//                 nameof(HowToPlayMenu),
//                 new AlignRight<HowToPlayItem>())
//         { }

//         public BackButton Back => _back ??= new BackButton(Parent);

//         public override Menu<HowToPlayItem, HowToPlayMenu> Initialize()
//         {
//             _ = Back;
//             return base.Initialize();
//         }

//         public class HowToPlayItem : DataEnum
//         {
//             public static readonly HowToPlayItem Bard = new(0, "About 'The Bards Code'");
//             public static readonly HowToPlayItem Muscopa = new(1, "About 'Muscopa'");
//             public static readonly HowToPlayItem Batterie = new(2, "About 'Battery'");
//             public static readonly HowToPlayItem RhythmCell = new(3, "Rhythm cell tutorial");

//             public HowToPlayItem() : base(0, "") { }

//             private HowToPlayItem(int id, string name) : base(id, name) { }
//         }
//     }
// }
