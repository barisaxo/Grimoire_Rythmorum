using System;
using Menus;
using Menus.OptionsMenu;
using Menus.MainMenu;
using UnityEngine;

public class GamePlayMenu_State : State
{
    private readonly State ConsequentState;
    private GamePlayMenu GamePlayMenu;
    private OptionsMenu Options;
    private readonly MainMenuScene MainMenuScene;
    private bool Tuning;
    private TuningNote TuningNote;

    public GamePlayMenu_State(State consequentState, MainMenuScene scene)
    {
        ConsequentState = consequentState;
        MainMenuScene = scene;
    }

    public GamePlayMenu_State(State consequentState)
    {
        ConsequentState = consequentState;
    }

    protected override void PrepareState(Action callback)
    {
        Options = (OptionsMenu)new OptionsMenu().Initialize(OptionsMenu.OptionsItem.GamePlay);
        GamePlayMenu = (GamePlayMenu)new GamePlayMenu().Initialize(Data.GamePlay);
        TuningNote = new(Data.GamePlay.CurrentKey);
        callback();
    }

    protected override void DisengageState()
    {
        //LoadSaveSystems.SaveCurrentGame();
        TuningNote.SelfDestruct();
        Tuning = false;
        Options.SelfDestruct();
        GamePlayMenu.SelfDestruct();
    }

    protected override void Clicked(MouseAction action, Vector3 mousePos)
    {
        base.Clicked(action, mousePos);
        if (action == MouseAction.LUp) ToggleTuningNote();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(Options.Back.Button.GO.transform))
        {
            SetStateDirectly(ConsequentState);
            return;
        }

        for (var i = 0; i < Options.MenuItems.Length; i++)
            if (go.transform.IsChildOf(Options.MenuItems[i].Card.GO.transform))
            {
                if (Options.MenuItems[i] == Options.Selection) return;
                Options.Selection = Options.MenuItems[i];
                UpdateMenu();
                return;
            }

        for (var i = 0; i < GamePlayMenu.MenuItems.Length; i++)
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

        ToggleTuningNote();
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir == Dir.Reset) return;
        GamePlayMenu.ScrollMenuItems(dir);
        ToggleTuningNote();
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
        TuningNote.Stop();
        Tuning = false;
        if (Options.Selection == Options.MenuItems[OptionsMenu.OptionsItem.Volume])
            SetStateDirectly(new VolumeMenu_State(ConsequentState, MainMenuScene));

        else if (Options.Selection == Options.MenuItems[OptionsMenu.OptionsItem.Controls])
            SetStateDirectly(new ShowControls_State(ConsequentState, MainMenuScene));
    }

    protected override void ConfirmPressed()
    {
        IncreaseItem(GamePlayMenu.Selection);
    }

    private void ToggleTuningNote()
    {
        if (GamePlayMenu.Selection == GameplayData.DataItem.Tuning && !Tuning && Options.Selection == OptionsMenu.OptionsItem.GamePlay)
        {
            TuningNote = new(Data.GamePlay.CurrentKey);
            TuningNote.Play();
            Tuning = true;
        }
        else if (GamePlayMenu.Selection == GameplayData.DataItem.Tuning && Tuning)
        {
            TuningNote.Stop();
            Tuning = false;
        }
        else if (GamePlayMenu.Selection != GameplayData.DataItem.Tuning && Tuning)
        {
            TuningNote.Stop();
            Tuning = false;
        }
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

    protected override void LStickInput(Vector2 v2)
    {
        MainMenuScene?.CatBoat.transform.Rotate(25 * Time.deltaTime * new Vector3(0, v2.x, 0), Space.World);
        Cam.Io.SetObliqueness(v2);
    }

    protected override void RStickInput(Vector2 v2)
    {
        if (MainMenuScene == null) return;
        MainMenuScene.CatBoat.transform.localScale = Vector3.one * 3 + (Vector3)v2 * 2;
    }
}