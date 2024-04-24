// using OldMenus;
// using OLD;
// public class SaveSlotMenu : Menu<SaveSlotMenu.SaveSlotItem, SaveSlotMenu>
// {
//     private BackButton _back;

//     public SaveSlotMenu() :
//         base(
//             nameof(SaveSlotMenu),
//             new AlignRight<SaveSlotItem>())
//     { }

//     public BackButton Back => _back ??= new BackButton(Parent);

//     public override Menu<SaveSlotItem, SaveSlotMenu> Initialize()
//     {
//         _ = Back;
//         return base.Initialize();
//     }

//     public class SaveSlotItem : DataEnum
//     {
//         public static readonly SaveSlotItem One = new(0, "Save slot one");
//         public static readonly SaveSlotItem Two = new(1, "Save slot two");
//         public static readonly SaveSlotItem Three = new(2, "Save slot three");

//         public SaveSlotItem() : base(0, "")
//         {
//         }

//         private SaveSlotItem(int id, string name) : base(id, name)
//         {
//         }
//     }
// }