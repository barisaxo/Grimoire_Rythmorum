using System;
using Menus;
using UnityEngine;

public class LoadGameSelectSlot_State : State
{
    private SaveSlotMenu SaveSlotMenu;

    protected override void PrepareState(Action callback)
    {
        SaveSlotMenu = (SaveSlotMenu)new SaveSlotMenu(Data.GamePlay).Initialize();
        base.PrepareState(callback);
    }

    protected override void DisengageState()
    {
        SaveSlotMenu.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(SaveSlotMenu.Back.Button.GO.transform))
        {
            SetStateDirectly(new MainMenu_State());
            return;
        }

        for (var i = 0; i < SaveSlotMenu.MenuItems.Count; i++)
        {
            if (!go.transform.IsChildOf(SaveSlotMenu.MenuItems[i].Card.GO.transform)) continue;
            SaveSlotMenu.Selection = SaveSlotMenu.MenuItems[i];
            SaveSlotMenu.UpdateTextColors();
            ConfirmPressed();
            return;
        }
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir == Dir.Reset) return;
        SaveSlotMenu.ScrollMenuItems(dir);

        SaveSlotMenu.UpdateTextColors();
    }

    protected override void CancelPressed()
    {
        SetStateDirectly(new MainMenu_State());
    }
}