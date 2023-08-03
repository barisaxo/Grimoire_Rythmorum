using System;
using Menus;
using Menus.OptionsMenu;
using UnityEngine;

public class ShowControls_State : State
{
    private readonly State RestoreState;
    private OptionsMenu Options;
    private ShowControls Controls;

    public ShowControls_State(State restoreState)
    {
        RestoreState = restoreState;
    }
    
    protected override void PrepareState(Action callback)
    {
        Options = (OptionsMenu)new OptionsMenu().Initialize(OptionsMenu.OptionsItem.Controls);
        Controls = new ShowControls();
        callback();
    }

    protected override void DisengageState()
    {
        Options.SelfDestruct();
        Controls.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Options.Back.Button.GO.transform))
        {
            SetStateDirectly(RestoreState);
            return;
        }

        for (var i = 0; i < Options.MenuItems.Count; i++)
            if (go.transform.IsChildOf(Options.MenuItems[i].Card.GO.transform))
            {
                if (Options.MenuItems[i] == Options.Selection) return;
                Options.Selection = Options.MenuItems[i];
                UpdateMenu();
                return;
            }
    }

    protected override void L1Pressed()
    {
        Options.ScrollMenuItems(Dir.Left);
        UpdateMenu();
    }

    protected override void R1Pressed()
    {
        Options.ScrollMenuItems(Dir.Right);
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        if (Options.Selection == Options.MenuItems[OptionsMenu.OptionsItem.Volume])
            SetStateDirectly(new VolumeMenu_State(RestoreState));
        else if (Options.Selection == Options.MenuItems[OptionsMenu.OptionsItem.GamePlay])
            SetStateDirectly(new GamePlayMenu_State(RestoreState));
    }

    protected override void CancelPressed()
    {
        SetStateDirectly(RestoreState);
    }

    protected override void StartPressed()
    {
        SetStateDirectly(RestoreState);
    }
}