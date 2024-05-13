// using System;
// using Data;
// using Data.Options;

// namespace Menus.One
// {
//     public class VolumeMenu : IMenu
//     {
//         public VolumeMenu(VolumeData vd, Audio.AudioManager am) { Data = vd; AM = am; }

//         readonly Audio.AudioManager AM;
//         public IData Data { get; }
//         public MenuItem Selection { get; set; }
//         public MenuItem[] MenuItems { get; set; }
//         public Card Description { get; set; }
//         public IMenuLayout Layout { get; } = new TwoColumns();

//         public string GetDescription { get => Selection.Item.Description; }
//         public string DisplayData(DataEnum item)
//         {
//             return item.Name + ": " + Data.GetDisplayLevel(item) + "%";
//         }

//         public IInputHandler Input => new MenuInputHandler()
//         {
//             North = new ButtonInput(IncreaseItem),
//             West = new ButtonInput(DecreaseItem),
//             Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
//             Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
//             Right = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
//             Left = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
//         };

//         private void IncreaseItem()
//         {
//             Data.IncreaseLevel(Selection.Item);
//             Selection.Card.SetTextString(DisplayData(Selection.Item));
//             AM.BGMusic.VolumeLevelSetting = (float)(Data.GetLevel(VolumeData.DataItem.BGMusic) * .01f);
//             AM.SFX.VolumeLevelSetting = (float)(Data.GetLevel(VolumeData.DataItem.SoundFX) * .01f);
//         }

//         private void DecreaseItem()
//         {
//             Data.DecreaseLevel(Selection.Item);
//             Selection.Card.SetTextString(DisplayData(Selection.Item));
//             AM.BGMusic.VolumeLevelSetting = (float)(Data.GetLevel(VolumeData.DataItem.BGMusic) * .01f);
//             AM.SFX.VolumeLevelSetting = (float)(Data.GetLevel(VolumeData.DataItem.SoundFX) * .01f);
//         }

//         public State ConsequentState => null;
//         public IMenuScene Scene => null;
//     }
// }