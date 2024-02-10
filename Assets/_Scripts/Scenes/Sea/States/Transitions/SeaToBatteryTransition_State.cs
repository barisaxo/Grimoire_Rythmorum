using System;

public class SeaToBatteryTransition_State : State
{
    protected override void PrepareState(Action callback)
    {
        Sea.WorldMapScene.Io.Ship.SeaPos = Sea.WorldMapScene.Io.Ship.GO.transform.position;
        Sea.WorldMapScene.Io.Ship.SeaRot = Sea.WorldMapScene.Io.Ship.GO.transform.rotation;
        Sea.WorldMapScene.Io.Board.Swells.DisableSwells();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        FadeToState(new BatterieAndCadence_State(new BatteriePack()));
    }
}