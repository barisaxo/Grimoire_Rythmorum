using System;
using Menus;
using Menus.HowToPlayMenu;
using UnityEngine;

public class HowToPlayMenu_State : State
{
    private HowToPlayMenu HowToPlay;

    protected override void PrepareState(Action callback)
    {
        HowToPlay = (HowToPlayMenu)new HowToPlayMenu().Initialize();
        callback();
    }

    protected override void DisengageState()
    {
        HowToPlay.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(HowToPlay.Back.Button.GO.transform))
        {
            SetStateDirectly(new MainMenu_State());
            return;
        }

        for (var i = 0; i < HowToPlay.MenuItems.Count; i++)
        {
            if (!go.transform.IsChildOf(HowToPlay.MenuItems[i].Card.GO.transform)) continue;
            HowToPlay.Selection = HowToPlay.MenuItems[i];
            ConfirmPressed();
            return;
        }
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir == Dir.Reset) return;
        HowToPlay.ScrollMenuItems(dir);
    }

    protected override void ConfirmPressed()
    {
        if (HowToPlay.Selection == HowToPlayMenu.HowToPlayItem.Muscopa)
        {
            SetStateDirectly(new DialogStart_State(new MuscopaTutorial_Dialogue()));
            return;
        }

        if (HowToPlay.Selection == HowToPlayMenu.HowToPlayItem.Battery)
            SetStateDirectly(new DialogStart_State(new BatteryTutorial_Dialogue()));
    }

    protected override void CancelPressed()
    {
        SetStateDirectly(new MainMenu_State());
    }
}