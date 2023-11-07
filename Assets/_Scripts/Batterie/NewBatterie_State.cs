using System;
using SheetMusic;
using Musica.Rhythms;
using Batterie;

public class NewBatterie_State : State
{
    public NewBatterie_State()
    {
    }



    protected override void PrepareState(Action callback)
    {

        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        //SetStateDirectly(new Batterie_State(specs));
    }


}
