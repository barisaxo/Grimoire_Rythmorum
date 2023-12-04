using System;
using Menus;
using Menus.MainMenu;
using UnityEngine;

public class MainMenu_State : State
{
    public MainMenuScene MainMenuScene { get; private set; }
    private MainMenu MainMenu;
    private bool SaveDataExists = true;

    public MainMenu_State() { }
    public MainMenu_State(MainMenuScene scene) { MainMenuScene = scene; }

    protected override void PrepareState(Action callback)
    {
        Cam.Io.Camera.transform.SetPositionAndRotation(Vector3.back * 10, Quaternion.identity);
        MainMenu = (MainMenu)new MainMenu().Initialize();
        MainMenu.MenuItems[0].Card.GO.SetActive(SaveDataExists);
        MainMenu.MenuItems[1].Card.GO.SetActive(SaveDataExists);
        MainMenuScene ??= new();
        Audio.BGMusic.PlayClip(Assets.BGMus1);
        callback();
    }

    protected override void EngageState()
    {
        if (SaveDataExists || (MainMenu.Selection != MainMenu.MainMenuItem.Continue &&
                               MainMenu.Selection != MainMenu.MainMenuItem.LoadGame)) return;
        MainMenu.Selection = MainMenu.MenuItems[MainMenu.MainMenuItem.NewGame];
        MainMenu.UpdateTextColors();
    }

    protected override void DisengageState()
    {
        Audio.BGMusic.Pause();
        MainMenu.SelfDestruct();
        MainMenuScene.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        //MainMenu.CurrItem = null;
        for (var i = 0; i < MainMenu.MenuItems.Length; i++)
            if (go.transform.IsChildOf(MainMenu.MenuItems[i].Card.GO.transform))
            {
                MainMenu.Selection = MainMenu.MenuItems[i];
                break;
            }
        ConfirmPressed();
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir != Dir.Up && dir != Dir.Down) return;
        MainMenu.ScrollMenuItems(dir);

        if (SaveDataExists || (MainMenu.Selection.Item != MainMenu.MainMenuItem.Continue &&
                               MainMenu.Selection.Item != MainMenu.MainMenuItem.LoadGame)) return;
        MainMenu.Selection = MainMenu.MenuItems[MainMenu.MainMenuItem.NewGame];
        MainMenu.UpdateTextColors();
    }

    protected override void ConfirmPressed()
    {
        MainMenu.UpdateTextColors();

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.Continue)
        {
            FadeToState(new NewAetherScene_State());
            return;
        }
        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.LoadGame)
        {
            SetStateDirectly(new LoadGameSelectSlot_State(MainMenuScene));
            return;
        }

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.NewGame)
        {
            SetStateDirectly(new NewGameSelectSlot_State(MainMenuScene));
            return;
        }

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.Options)
        {
            SetStateDirectly(new VolumeMenu_State(new MainMenu_State(MainMenuScene), MainMenuScene));
            return;
        }

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.HowToPlay)
        {
            SetStateDirectly(new HowToPlayMenu_State(MainMenuScene));
            return;
        }

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.Quit)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

#if !UNITY_EDITOR
            Application.Quit();
#endif
        }
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

    protected override void StartPressed()
    {
        MainMenuScene.RockTheBoat.Rocking = !MainMenuScene.RockTheBoat.Rocking;

        SaveDataExists = !SaveDataExists;
        MainMenu.MenuItems[0].Card.GO.SetActive(SaveDataExists);
        MainMenu.MenuItems[1].Card.GO.SetActive(SaveDataExists);
        MainMenu.UpdateTextColors();
    }


}