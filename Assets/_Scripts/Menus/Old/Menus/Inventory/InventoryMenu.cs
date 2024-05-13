// using System;
// using Data;
// using Data.Inventory;
// using Menus.One;


// namespace Menus.Inventory
// {
//     public class InventoryMenu : IMenu, IHeaderMenu
//     {
//         public InventoryMenu(DataManager dm, State subsequentState)
//         {
//             ConsequentState = subsequentState;
//             DataManager = dm;
//             Data = new InventoryData();
//             CurrentSub = SubMenus[0];
//             UnityEngine.Debug.Log(subsequentState);
//         }

//         readonly DataManager DataManager;
//         public IData Data { get; }
//         public Card Description { get; set; }
//         private MenuItem _selection;
//         public MenuItem Selection
//         {
//             get => _selection;
//             set
//             {
//                 _selection = value;
//                 CurrentSub = SubMenus[_selection.Item.Id];
//             }
//         }
//         public string GetDescription => null;
//         public MenuItem[] MenuItems { get; set; }
//         public IMenu CurrentSub { get; private set; }
//         private IMenu[] _subMenus;
//         public IMenu[] SubMenus => _subMenus ??= new IMenu[] {
//             new QuestsMenu(DataManager.QuestsData),
//             new MaterialsMenu(DataManager.MaterialsData),
//             new FishMenu(DataManager.FishData),
//             new StarChartsMenu(DataManager.starChartsData, DataManager.QuestsData, ConsequentState, this),
//             new GramophoneMenu(DataManager.GramophoneData),
//             new LighthousesMenu(DataManager.LighthousesData),
//             new ShipMenu(DataManager.ShipData),
//             new PlayerMenu(DataManager.PlayerData),
//         };

//         public IMenuLayout Layout { get; } = new ScrollingHeader();

//         public IInputHandler Input => new MenuInputHandler()
//         {
//             R1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Right, this)),
//             L1 = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Left, this)),
//         };

//         public string DisplayData(DataEnum item)
//         {
//             return item is not null ? item.Name : "";
//         }

//         public State ConsequentState { get; }
//         public IMenuScene Scene => null;
//     }

// }
