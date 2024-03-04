using System;
using OldMenus.Generic;
using OldMenus.Generic.QuitMenu;
using UnityEngine;
using Sea;

public class QuitSeaMenu_State : State
{
    private QuitSeaMenu QuitMenu;

    public QuitSeaMenu_State(State subsequentState) { SubsequentState = subsequentState; }
    readonly State SubsequentState;

    protected override void PrepareState(Action callback)
    {
        QuitMenu = (QuitSeaMenu)new QuitSeaMenu().Initialize();
        callback();
    }

    protected override void EngageState()
    {
        //TODO QuitMenu.UpdateTextColors();
    }

    protected override void DisengageState()
    {
        QuitMenu.SelfDestruct();
    }

    protected override void ClickedOn(GameObject go)
    {
        if (go.transform.IsChildOf(QuitMenu.Back.Button.GO.transform))
        {
            SetState(SubsequentState);
            return;
        }

        for (var i = 0; i < QuitMenu.MenuItems.Length; i++)
            if (go.transform.IsChildOf(QuitMenu.MenuItems[i].Card.GO.transform))
            {
                QuitMenu.Selection = QuitMenu.MenuItems[i];
                break;
            }
        EastPressed();
    }

    protected override void DirectionPressed(Dir dir)
    {
        if (dir == Dir.Reset) return;
        QuitMenu.Selection = QuitMenu.Layout.ScrollMenuItems(dir, QuitMenu.Selection, QuitMenu.MenuItems);
        //TODO QuitMenu.UpdateTextColors();
    }

    protected override void EastPressed()
    {
        //TODO  QuitMenu.UpdateTextColors();
        WorldMapScene.Io.SelfDestruct();
        if (QuitMenu.Selection.Item == QuitSeaMenu.QuitMenuItem.AbandonRun)
        {
            SetState(new NewCoveScene_State());
            return;
        }
        if (QuitMenu.Selection.Item == QuitSeaMenu.QuitMenuItem.QuitToMainMenu)
        {
            // FadeToState(new MainMenu_State());
            throw new System.NotImplementedException();
            return;
        }

        if (QuitMenu.Selection.Item == QuitSeaMenu.QuitMenuItem.QuitGame)
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

#if !UNITY_EDITOR
            Application.Quit();
#endif
        }
    }

    protected override void SouthPressed()
    {
        SetState(SubsequentState);
    }

    protected override void SelectPressed()
    {
        SetState(SubsequentState);
    }

    protected override void LStickInput(Vector2 v2)
    {
        Cam.Io.SetObliqueness(v2);
    }

    protected override void RStickInput(Vector2 v2)
    {

    }


}