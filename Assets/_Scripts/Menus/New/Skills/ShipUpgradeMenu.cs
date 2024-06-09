using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Two;
using ShipStats;

namespace Menus.Two
{
    public class ShipUpgradeMenu : IMenu
    {
        public ShipUpgradeMenu(ShipUpgradeData data, PlayerData playerData, ActiveShipData activeShipData, ShipStats.ShipStats shipStats, State subsequentState)
        {
            Data = data;
            SubsequentState = subsequentState;
            ShipStats = shipStats;
            ActiveShipData = activeShipData;
            PlayerData = playerData;
            // ConsequentState = new Menu_State(this, Selection);
        }

        readonly State SubsequentState;
        readonly PlayerData PlayerData;
        readonly ActiveShipData ActiveShipData;
        ShipStats.ShipStats ShipStats;
        public IData Data { get; }
        public MenuItem Selection { get; set; }
        public MenuItem[] MenuItems { get; set; }
        public Card Description { get; set; }
        public IMenuLayout Layout { get; } = new LeftScroll();

        public string DisplayData(IItem item)
        {
            return item.Name;
        }

        public string GetDescription
        {
            get
            {
                return Selection.Item switch
                {
                    TimberType => Timber(),
                    RiggingType => Cloth(),
                    CannonType => Cannon(),
                    _ => throw new System.Exception(Selection.Item.Name)
                };
                string Timber()
                {
                    string timber = Selection.Item.Description +
                    "\n\nCurrent " + Selection.Item.Name + ": " + ShipStats.HullStats.Timber.Name +
                    ",\n" + ShipStats.HullStats.Timber.Description +
                    "\nModifier: " + ShipStats.HullStats.Timber.Modifier;

                    if (ShipStats.HullStats.Timber is Teak) return timber;

                    timber += ShipStats.HullStats.Timber switch
                    {
                        Pine =>
                            "\n\nNext Upgrade: " +
                            WoodEnum.Fir.Name +
                            "\n" + WoodEnum.Fir.Description +
                            "\nModifier: " +
                            WoodEnum.Fir.Modifier,
                        Fir =>
                            "\n\nNext Upgrade: " +
                            WoodEnum.Oak.Name +
                            "\n" + WoodEnum.Oak.Description +
                            "\nModifier: " +
                            WoodEnum.Oak.Modifier,
                        Oak =>
                            "\n\nNext Upgrade: " +
                            WoodEnum.Teak.Name +
                            "\n" + WoodEnum.Teak.Description +
                            "\nModifier: " +
                            WoodEnum.Teak.Modifier,
                        _ => throw new System.Exception(ShipStats.HullStats.Timber.Name)
                    };

                    timber += "\nCost: " + GetCost();
                    // timber += ShipStats.HullStats.Timber switch
                    // {
                    //     Pine => "\nCost: TODO" + WoodEnum.Fir.Name,
                    //     Fir => "\nCost: TODO" + WoodEnum.Oak.Name,
                    //     Oak => "\nCost: TODO" + WoodEnum.Teak.Name,
                    //     _ => throw new System.Exception(ShipStats.HullStats.Timber.Name)
                    // };
                    timber += "\n(" + PlayerData.GetLevel(new PatternsAvailable()) + " patterns available)";
                    return timber;
                }

                string Cloth()
                {
                    string cloth = Selection.Item.Description +
                    "\n\nCurrent " + Selection.Item.Name + ": " + ShipStats.RiggingStats.ClothType.Name +
                    ",\n" + ShipStats.RiggingStats.ClothType.Description +
                    "\nModifier: " + ShipStats.RiggingStats.ClothType.Modifier;

                    if (ShipStats.RiggingStats.ClothType is Silk) return cloth;

                    cloth += ShipStats.RiggingStats.ClothType switch
                    {
                        Hemp =>
                            "\n\nNext Upgrade: " +
                            ClothEnum.Cotton.Name +
                            "\n" + ClothEnum.Cotton.Description +
                            "\nModifier: " +
                            ClothEnum.Cotton.Modifier,
                        Cotton =>
                            "\n\nNext Upgrade: " +
                            ClothEnum.Linen.Name +
                            "\n" + ClothEnum.Linen.Description +
                            "\nModifier: " +
                            ClothEnum.Linen.Modifier,
                        Linen =>
                            "\n\nNext Upgrade: " +
                            ClothEnum.Silk.Name +
                            "\n" + ClothEnum.Silk.Description +
                            "\nModifier: " +
                            ClothEnum.Silk.Modifier,
                        _ => throw new System.Exception(ShipStats.RiggingStats.ClothType.Name)
                    };

                    cloth += "\nCost: " + GetCost();
                    // cloth += ShipStats.RiggingStats.ClothType switch
                    // {
                    //     Hemp => "\nCost: TODO" + ClothEnum.Cotton.Name,
                    //     Cotton => "\nCost: TODO" + ClothEnum.Linen.Name,
                    //     Linen => "\nCost: TODO" + ClothEnum.Silk.Name,
                    //     _ => throw new System.Exception(ShipStats.RiggingStats.ClothType.Name)
                    // };
                    cloth += "\n(" + PlayerData.GetLevel(new PatternsAvailable()) + " patterns available)";
                    return cloth;
                }
                string Cannon()
                {
                    string cannon = Selection.Item.Description +
                   "\n\nCurrent " + Selection.Item.Name + ": " + ShipStats.CannonStats.Cannon.Name +
                   ",\n" + ShipStats.CannonStats.Cannon.Description +
                   "\nModifier: " + ShipStats.CannonStats.Cannon.Modifier;

                    if (ShipStats.CannonStats.Cannon is Carronade) return cannon;

                    cannon += ShipStats.CannonStats.Cannon switch
                    {
                        Mynion =>
                            "\n\nNext Upgrade: " +
                            CannonEnum.Saker.Name +
                            "\n" + CannonEnum.Saker.Description +
                            "\nModifier: " +
                            CannonEnum.Saker.Modifier,
                        Saker =>
                            "\n\nNext Upgrade: " +
                            CannonEnum.Culverin.Name +
                            "\n" + CannonEnum.Culverin.Description +
                            "\nModifier: " +
                            CannonEnum.Culverin.Modifier,
                        Culverin =>
                            "\n\nNext Upgrade: " +
                            CannonEnum.DemiCannon.Name +
                            "\n" + CannonEnum.DemiCannon.Description +
                            "\nModifier: " +
                            CannonEnum.DemiCannon.Modifier,
                        DemiCannon =>
                            "\n\nNext Upgrade: " +
                            CannonEnum.Carronade.Name +
                            "\n" + CannonEnum.Carronade.Description +
                            "\nModifier: " +
                            CannonEnum.Carronade.Modifier,
                        _ => throw new System.Exception(ShipStats.CannonStats.Cannon.Name)
                    };
                    cannon += "\nCost: " + GetCost();
                    // cannon += ShipStats.CannonStats.Cannon switch
                    // {
                    //     Mynion => "\nCost: TODO" + CannonEnum.Saker.Name,
                    //     Saker => "\nCost: TODO" + CannonEnum.Culverin.Name,
                    //     Culverin => "\nCost: TODO" + CannonEnum.DemiCannon.Name,
                    //     DemiCannon => "\nCost: TODO" + CannonEnum.Carronade.Name,
                    //     _ => throw new System.Exception(ShipStats.RiggingStats.ClothType.Name)
                    // };
                    cannon += "\n(" + PlayerData.GetLevel(new PatternsAvailable()) + " patterns available)";
                    return cannon;
                }

                // return ((IShipUpgradeStat)Selection.Item).Description +
                //     "\n" + ((IShipUpgradeStat)Selection.Item).Description + "% bonus per level." +
                //     "\nCurrent Level: " + Data.GetLevel(Selection.Item) + " / " + ((IShipUpgradeStat)Selection.Item).MaxLevel +
                //     "\nCurrent Bonus: " + (int)(Data.GetLevel(Selection.Item) * ((IShipUpgradeStat)Selection.Item).Per) + "%" +
                //     "\nCost: " + ((SkillData)Data).GetSkillCost(Selection.Item) + " patterns." +
                //     "\n(" + PlayerData.GetLevel(new PatternsAvailable()) + " patterns available)"
                //      ;
            }
        }

        public IInputHandler Input => new MenuInputHandler()
        {
            North = new ButtonInput(IncreaseItem),
            // West = new ButtonInput(DecreaseItem),
            Up = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Up, this)),
            Down = new ButtonInput(() => Selection = Layout.ScrollMenuItems(Dir.Down, this)),
            South = new ButtonInput(SouthPressed),
        };

        private void SouthPressed()
        {
            ConsequentState = SubsequentState;
        }

        private void IncreaseItem()
        {
            if (GetCost() > PlayerData.GetLevel(new PatternsAvailable())) return;

            HullStats newHull = (HullStats)ShipStats.HullStats;
            CannonStats newCannon = (CannonStats)ShipStats.CannonStats;
            RiggingStats newRigging = (RiggingStats)ShipStats.RiggingStats;

            switch (Selection.Item)
            {
                case TimberType:
                    if (ShipStats.HullStats.Timber is Teak) return;
                    newHull = new HullStats(ShipStats.HullStats.Hull, WoodEnum.ToItem(ShipStats.HullStats.Timber.Enum + 1));
                    break;
                case RiggingType:
                    if (ShipStats.RiggingStats.ClothType is Silk) return;
                    newRigging = new RiggingStats(ClothEnum.ToItem(ShipStats.RiggingStats.ClothType.Enum + 1));
                    break; ;
                case CannonType:
                    if (ShipStats.CannonStats.Cannon is Carronade) return;
                    newCannon = new CannonStats(CannonEnum.ToItem(ShipStats.CannonStats.Cannon.Enum + 1), new Patina());
                    break; ;
            };

            ShipStats.ShipStats newStats = new(newHull, newCannon, newRigging);

            ShipStats = newStats;
            Manager.Io.ShipStats.ActiveShip = newStats;
            Manager.Io.ShipStats.AdjustItem(newStats.HullStats.Hull, newStats);
            PlayerData.AdjustLevel(new PatternsSpent(), GetCost());

            // if (((ShipUpgradeData)Data).GetSkillCost(Selection.Item) >
            //     PlayerData.GetLevel(new PatternsAvailable()) &&
            //     Data.GetLevel(Selection.Item) < ((ISkill)Selection.Item).MaxLevel)
            //     return;

            // PlayerData.AdjustLevel(new PatternsSpent(), +
            //     ((ShipUpgradeData)Data).GetSkillCost(Selection.Item));

            // Data.AdjustLevel(Selection.Item, 1);

            Layout.UpdateText(this);
            // Layout.ScrollMenuItems(Dir.Reset, Selection, MenuItems);
            // Selection.Card.SetTextString(DisplayData(Selection.Item));
        }

        int GetCost()
        {
            int i = Selection.Item switch
            {
                TimberType => (int)(WoodEnum.ToItem(ShipStats.HullStats.Timber.ID + 1).Modifier * ShipStats.HullStats.Hull.Modifier),
                RiggingType => (int)(ClothEnum.ToItem(ShipStats.RiggingStats.ClothType.ID + 1).Modifier * ShipStats.HullStats.Hull.Modifier),
                CannonType => (int)(CannonEnum.ToItem(ShipStats.CannonStats.Cannon.ID + 1).Modifier * ShipStats.HullStats.Hull.Modifier),
                _ => throw new System.Exception(Selection.Item.Name)
            };

            return i;
        }

        public State ConsequentState { get; private set; }
        public IMenuScene Scene { get; set; } = new ShipUpgradeMenuScene();

    }

    public class ShipUpgradeMenuScene : IMenuScene
    {
        public void Initialize()
        {
            South.SetTextString("Back").SetImageColor(Color.white);
            North.SetTextString("Increase").SetImageColor(Color.white);
            ((IMenuScene)this).SetCardPos1(South);
            ((IMenuScene)this).SetCardPos2(North);
        }

        public void SelfDestruct()
        {
            Hud?.SelfDestruct();
            Resources.UnloadUnusedAssets();
        }

        public Transform TF => null;

        public Card Hud { get; set; }
        public Card North { get; set; }
        public Card East { get; set; }
        public Card South { get; set; }
        public Card West { get; set; }
    }


    // public interface IShipUpgradeStat : IItem
    // {
    //     ShipUpgradeStatEnum Enum { get; }
    //     int IItem.ID => Enum.Id;
    //     string IItem.Name => Enum.Name;
    //     string IItem.Description => Enum.Description;
    // }

    // [System.Serializable] public readonly struct TimberType : IShipUpgradeStat { public readonly ShipUpgradeStatEnum Enum => ShipUpgradeStatEnum.TimberType; }
    // [System.Serializable] public readonly struct RiggingType : IShipUpgradeStat { public readonly ShipUpgradeStatEnum Enum => ShipUpgradeStatEnum.RiggingType; }
    // [System.Serializable] public readonly struct CannonType : IShipUpgradeStat { public readonly ShipUpgradeStatEnum Enum => ShipUpgradeStatEnum.CannonType; }

    // [System.Serializable]
    // public class ShipUpgradeStatEnum : Enumeration
    // {
    //     public ShipUpgradeStatEnum() : base(0, null) { }
    //     public ShipUpgradeStatEnum(int id, string name) : base(id, name) { }
    //     public ShipUpgradeStatEnum(int id, string name, string description) : base(id, name)
    //     {
    //         Description = description;
    //     }

    //     public readonly string Description;

    //     public readonly static ShipUpgradeStatEnum TimberType = new(0, "This is the Timber type you're looking for");
    //     public readonly static ShipUpgradeStatEnum RiggingType = new(1, "Rigging");
    //     public readonly static ShipUpgradeStatEnum CannonType = new(2, "Cannons");

    //     internal static IItem ToItem(ShipUpgradeStatEnum @enum)
    //     {
    //         return @enum switch
    //         {
    //             _ when @enum == TimberType => new TimberType(),
    //             _ when @enum == RiggingType => new RiggingType(),
    //             _ when @enum == CannonType => new CannonType(),
    //             _ => throw new System.ArgumentOutOfRangeException(@enum.Name)
    //         };
    //     }
    // }

}
