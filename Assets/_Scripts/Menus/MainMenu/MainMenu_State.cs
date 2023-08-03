using System;
using Menus;
using Menus.MainMenu;
using UnityEngine;

public class MainMenu_State : State
{
    private const float RotSpeed = 25;
    private readonly RockTheBoat RockTheBoat = new();
    private float LightRotY = -40;
    private MainMenu MainMenu;
    private bool SaveDataExists = true;

    protected override void PrepareState(Action callback)
    {
        MainMenu = (MainMenu)new MainMenu().Initialize();
        MainMenu.MenuItems[0].Card.GO.SetActive(SaveDataExists);
        MainMenu.MenuItems[1].Card.GO.SetActive(SaveDataExists);
        RockTheBoat.AddBoat(MainMenu.Scene.CatBoat.transform);
        callback();
    }

    protected override void EngageState()
    {
        MonoHelper.OnUpdate += RotateLightHouse;
        RockTheBoat.Rocking = true;

        if (SaveDataExists || (MainMenu.Selection != MainMenu.MainMenuItem.Continue &&
                               MainMenu.Selection != MainMenu.MainMenuItem.LoadGame)) return;
        MainMenu.Selection = MainMenu.MenuItems[MainMenu.MainMenuItem.NewGame];
        MainMenu.UpdateTextColors();
    }

    protected override void DisengageState()
    {
        RockTheBoat.Rocking = false;
        MonoHelper.OnUpdate -= RotateLightHouse;
        MainMenu.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        //MainMenu.CurrItem = null;
        for (var i = 0; i < MainMenu.MenuItems.Count; i++)
            if (go.transform.IsChildOf(MainMenu.MenuItems[i].Card.GO.transform))
            {
                MainMenu.Selection = MainMenu.MenuItems[i];
                break;
            }

        ConfirmPressed();
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir == Dir.Reset) return;
        MainMenu.ScrollMenuItems(dir);

        if (SaveDataExists || (MainMenu.Selection.Item != MainMenu.MainMenuItem.Continue &&
                               MainMenu.Selection.Item != MainMenu.MainMenuItem.LoadGame)) return;
        MainMenu.Selection = MainMenu.MenuItems[MainMenu.MainMenuItem.NewGame];
        MainMenu.UpdateTextColors();
    }

    protected override void ConfirmPressed()
    {
        MainMenu.UpdateTextColors();

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.Continue) return;

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.LoadGame)
        {
            SetStateDirectly(new LoadGameSelectSlot_State());
            return;
        }

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.NewGame)
        {
            SetStateDirectly(new NewGameSelectSlot_State());
            return;
        }

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.Options)
        {
            SetStateDirectly(new VolumeMenu_State(new MainMenu_State()));
            return;
        }

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.HowToPlay)
        {
            SetStateDirectly(new HowToPlayMenu_State());
            return;
        }

        if (MainMenu.Selection.Item == MainMenu.MainMenuItem.Quit) return;
    }

    protected override void LStickInput(Vector2 v2)
    {
        MainMenu.Scene.CatBoat.transform.Rotate(RotSpeed * Time.deltaTime * new Vector3(0, v2.x, 0), Space.World);
    }

    protected override void RStickInput(Vector2 v2)
    {
        MainMenu.Scene.CatBoat.transform.localScale = Vector3.one * 3 + (Vector3)v2 * 2;
    }

    protected override void StartPressed()
    {
        RockTheBoat.Rocking = !RockTheBoat.Rocking;

        SaveDataExists = !SaveDataExists;
        MainMenu.MenuItems[0].Card.GO.SetActive(SaveDataExists);
        MainMenu.MenuItems[1].Card.GO.SetActive(SaveDataExists);
        MainMenu.UpdateTextColors();
    }

    private void RotateLightHouse()
    {
        LightRotY += Time.deltaTime * 25;
        MainMenu.Scene.LightHouse.transform.rotation = Quaternion.Euler(0, LightRotY, 0);
    }
}