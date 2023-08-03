using System;
using Menus;
using Menus.OptionsMenu;
using UnityEngine;

public class GamePlayMenu_State : State
{
    private readonly State ConsequentState;
    private GamePlayMenu GamePlayMenu;
    private OptionsMenu Options;

    public GamePlayMenu_State(State consequentState)
    {
        ConsequentState = consequentState;
    }

    protected override void PrepareState(Action callback)
    {
        Options = (OptionsMenu)new OptionsMenu().Initialize(OptionsMenu.OptionsItem.GamePlay);
        GamePlayMenu = (GamePlayMenu)new GamePlayMenu().Initialize(Data.GamePlay);
        callback();
    }

    protected override void DisengageState()
    {
        //LoadSaveSystems.SaveCurrentGame();
        Options.SelfDestruct();
        GamePlayMenu.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Options.Back.Button.GO.transform))
        {
            SetStateDirectly(ConsequentState);
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

        for (var i = 0; i < GamePlayMenu.MenuItems.Count; i++)
            if (go.transform.IsChildOf(GamePlayMenu.MenuItems[i].Card.GO.transform))
            {
                if (GamePlayMenu.Selection == GamePlayMenu.MenuItems[i])
                {
                    IncreaseItem(GamePlayMenu.Selection);
                    return;
                }

                GamePlayMenu.Selection = GamePlayMenu.MenuItems[i];
                GamePlayMenu.UpdateTextColors();
                return;
            }
    }

    protected override void DirectionPressed(Dir dir)
    {
        GamePlayMenu.ScrollMenuItems(dir);
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
            SetStateDirectly(new VolumeMenu_State(ConsequentState));

        else if (Options.Selection == Options.MenuItems[OptionsMenu.OptionsItem.Controls])
            SetStateDirectly(new ShowControls_State(ConsequentState));
    }

    protected override void ConfirmPressed()
    {
        IncreaseItem(GamePlayMenu.Selection);
    }

    private void IncreaseItem(MenuItem<GameplayData.DataItem> item)
    {
        Data.GamePlay.IncreaseItem(item.Item);
        item.Card.SetTextString(item.Item.DisplayData(Data.GamePlay));
    }

    protected override void CancelPressed()
    {
        SetStateDirectly(ConsequentState);
    }

    protected override void StartPressed()
    {
        SetStateDirectly(ConsequentState);
    }
}