using System;
using Data;
using Menus;

public class MenuState : State
{
    readonly IHeaderMenu Header;
    IMenu Sub;

    public MenuState(IHeaderMenu header)
    {
        Header = header;
        Sub = Header.CurrentSub;
        Header?.Scene?.HideTexts();
        Sub?.Scene?.HideTexts();
    }

    public MenuState(IMenu sub)
    {
        Sub = sub;
        Sub?.Scene?.HideTexts();
    }

    protected override void PrepareState(Action callback)
    {
        Header?.SetUpMenuCards();
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
        // Data.Save(Sub?.Data);
        Header?.SelfDestruct();
        Sub?.SelfDestruct();
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
        Sub?.SetUpDescription();
        Sub?.SetUpMenuCards();
        Sub?.Scene?.HideTexts();
        Sub?.Scene?.Initialize();
    }

    // protected override void GPInput(GamePadButton gpb)
    // {
    //     base.GPInput(gpb);
    // }

    protected override void EastPressed()
    {
        if (Sub?.Input?.East is null && Header?.Input?.East is null) return;

        Sub?.Input?.East?.Action();
        if (Sub?.ConsequentState is not null) { SetState(Sub.ConsequentState); return; }

        Header?.Input?.East?.Action();
        if (Header?.ConsequentState is not null) SetState(Header.ConsequentState);
    }

    protected override void NorthPressed() => Sub?.Input?.North?.Action();
    protected override void WestPressed() => Sub?.Input?.West?.Action();

    protected override void SouthPressed()
    {
        if (Sub?.Input?.South is null && Header?.Input?.South is null) return;

        Sub?.Input?.South?.Action();
        if (Sub?.ConsequentState is not null) { SetState(Sub.ConsequentState); return; }

        Header?.Input?.South?.Action();
        if (Header?.ConsequentState is not null) SetState(Header.ConsequentState);
    }


}
