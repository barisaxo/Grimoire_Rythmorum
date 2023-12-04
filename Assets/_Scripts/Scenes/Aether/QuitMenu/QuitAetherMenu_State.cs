using System;
using Menus;
using Menus.QuitMenu;
using UnityEngine;

public class QuitAetherMenu_State : State
{
    private QuitAetherMenu QuitMenu;

    public QuitAetherMenu_State(State subsequentState) { SubsequentState = subsequentState; }
    readonly State SubsequentState;

    protected override void PrepareState(Action callback)
    {
        QuitMenu = (QuitAetherMenu)new QuitAetherMenu().Initialize();
        callback();
    }

    protected override void EngageState()
    {
        QuitMenu.UpdateTextColors();
    }

    protected override void DisengageState()
    {
        QuitMenu.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(QuitMenu.Back.Button.GO.transform))
        {
            SetStateDirectly(SubsequentState);
            return;
        }

        for (var i = 0; i < QuitMenu.MenuItems.Length; i++)
            if (go.transform.IsChildOf(QuitMenu.MenuItems[i].Card.GO.transform))
            {
                QuitMenu.Selection = QuitMenu.MenuItems[i];
                break;
            }
        ConfirmPressed();
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir == Dir.Reset) return;
        QuitMenu.ScrollMenuItems(dir);
        QuitMenu.UpdateTextColors();
    }

    protected override void ConfirmPressed()
    {
        QuitMenu.UpdateTextColors();
        Cam.Io.Camera.transform.SetParent(null);
        AetherScene.Io.SelfDestruct();

        if (QuitMenu.Selection.Item == QuitAetherMenu.QuitMenuItem.QuitToMainMenu)
        {
            FadeToState(new MainMenu_State());
            return;
        }

        if (QuitMenu.Selection.Item == QuitAetherMenu.QuitMenuItem.QuitGame)
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

#if !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }

    protected override void CancelPressed()
    {
        SetStateDirectly(SubsequentState);
    }

    protected override void SelectPressed()
    {
        SetStateDirectly(SubsequentState);
    }

    protected override void LStickInput(Vector2 v2)
    {
        Cam.Io.SetObliqueness(v2);
    }

    protected override void RStickInput(Vector2 v2)
    {

    }


}