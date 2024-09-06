using System;
using Datum;
using Menus;

public class MenuState : State
{
    readonly IHeaderMenu Header;
    IMenu Sub;

    public MenuState(IHeaderMenu header)
    {
        Header = header;
        // Sub = Header.CurrentSub;
    }

    public MenuState(IMenu sub)
    {
        Sub = sub;
    }

    protected override void PrepareState(Action callback)
    {
        Header?.SetUpMenuCards();

        if (Header?.CurrentSub is not null) Sub ??= Header.CurrentSub;
        else Sub ??= Header?.SubMenus[0];

        Sub?.Scene?.HideTexts();
        Header?.Scene?.HideTexts();
        Sub?.SetUpDescription();
        Sub?.SetUpMenuCards();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        Header?.Scene?.Initialize();
        Sub?.Scene?.Initialize();
    }

    protected override void DisengageState()
    {
        Sub?.SelfDestruct();
        Header?.SelfDestruct();
    }

    protected override void DirectionPressed(Dir dir)
    {
        switch (dir)
        {
            case Dir.Up: Sub?.Input?.Up?.Action(); break;
            case Dir.Down: Sub?.Input?.Down?.Action(); break;
            case Dir.Left: Sub?.Input?.Left?.Action(); break;
            case Dir.Right: Sub?.Input?.Right?.Action(); break;
        }
    }

    protected override void R1Pressed()
    {
        if (Header is not null)
        {
            Header?.Input?.R1?.Action();
            ChangeSubMenu();
        }
        else
        {
            Sub?.Input?.R1?.Action();
        }
    }

    protected override void L1Pressed()
    {
        if (Header is not null)
        {
            Header?.Input?.L1?.Action();
            ChangeSubMenu();
        }

        else
        {
            Sub?.Input?.L1?.Action();
        }
    }

    void ChangeSubMenu()
    {
        Sub?.SelfDestruct();
        Sub = Header?.CurrentSub;
        Sub.Scene?.HideTexts();
        Sub.SetUpDescription();
        Sub.SetUpMenuCards();
        Sub.Scene?.Initialize();
    }

    // protected override void GPInput(GamePadButton gpb)
    // {
    //     base.GPInput(gpb);
    // }

    protected override void EastPressed()
    {
        if (Sub?.Input?.East is null && Header?.Input?.East is null) return;

        Sub?.Input?.East?.Action();
        State subConState = Sub?.ConsequentState;
        if (subConState is not null) { SetState(subConState); return; }

        Header?.Input?.East?.Action();
        if (Header?.ConsequentState is not null) SetState(Header.ConsequentState);
    }

    protected override void NorthPressed()
    {
        if (Sub?.Input?.North is null && Header?.Input?.North is null) return;

        Sub?.Input?.North?.Action();
        if (Sub?.ConsequentState is not null) { SetState(Sub.ConsequentState); return; }

        Header?.Input?.North?.Action();
        if (Header?.ConsequentState is not null) SetState(Header.ConsequentState);
    }
    protected override void WestPressed()
    {
        if (Sub?.Input?.West is null && Header?.Input?.West is null) return;

        Sub?.Input?.West?.Action();
        if (Sub?.ConsequentState is not null) { SetState(Sub.ConsequentState); return; }

        Header?.Input?.West?.Action();
        if (Header?.ConsequentState is not null) SetState(Header.ConsequentState);
    }

    protected override void SouthPressed()
    {
        if (Sub?.Input?.South is null && Header?.Input?.South is null) return;

        Sub?.Input?.South?.Action();
        if (Sub?.ConsequentState is not null) { SetState(Sub.ConsequentState); return; }

        Header?.Input?.South?.Action();
        if (Header?.ConsequentState is not null) SetState(Header.ConsequentState);
    }

    protected override void StartPressed()
    {
        if (Sub?.Input?.Start is not null) Sub?.Input?.Start?.Action();
        else if (Header?.Input?.Start is not null) Header?.Input?.Start?.Action();
    }

    protected override void SelectPressed()
    {
        if (Sub?.Input?.Select is not null) Sub?.Input?.Select?.Action();
        else if (Header?.Input?.Select is not null) Header?.Input?.Select?.Action();
    }

}
