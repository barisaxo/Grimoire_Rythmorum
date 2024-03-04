using System;
using Menus;

public class MenuTest_State : State
{
    readonly IHeaderMenu Header;
    IMenu Sub;

    public MenuTest_State(IHeaderMenu header)
    {
        Header = header;
        Sub = Header.CurrentSub;
    }

    public MenuTest_State(IMenu sub)
    {
        Sub = sub;
    }

    protected override void PrepareState(Action callback)
    {
        Header?.SetUpMenuCards();
        Header?.Scene?.Initialize();
        Sub?.SetUpMenuCards();
        Sub?.Scene?.Initialize();
        base.PrepareState(callback);
    }

    protected override void DisengageState()
    {
        Data.Save(Sub?.Data);
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
        if (Header == null) return;
        Header?.Input?.R1?.Action();
        Sub?.Input?.R1?.Action();
        ChangeSubMenu();
    }

    protected override void L1Pressed()
    {
        if (Header == null) return;
        Header?.Input?.L1?.Action();
        Sub?.Input?.L1?.Action();
        ChangeSubMenu();
    }

    void ChangeSubMenu()
    {
        Sub?.SelfDestruct();
        Sub = Header?.CurrentSub;
        Sub?.SetUpMenuCards();
    }

    protected override void GPInput(GamePadButton gpb)
    {
        base.GPInput(gpb);

    }

    protected override void EastPressed()
    {
        Sub?.Input?.East?.Action();
        SetState(Sub?.ConsequentState);
    }
    protected override void NorthPressed() => Sub?.Input?.North?.Action();
    protected override void WestPressed() => Sub?.Input?.West?.Action();
    protected override void SouthPressed()
    {
        Sub?.Input?.South?.Action();
        SetState(Header?.ConsequentState);
    }


}
