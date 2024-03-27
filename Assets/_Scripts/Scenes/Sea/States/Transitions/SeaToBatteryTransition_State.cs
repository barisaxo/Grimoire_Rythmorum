using System;

public class SeaToBatteryTransition_State : State
{
    public SeaToBatteryTransition_State() { Fade = false; }//TODO this might be wrong
    protected override void PrepareState(Action callback)
    {
        Sea.WorldMapScene.Io.Ship.SeaPos = Sea.WorldMapScene.Io.Ship.GO.transform.position;
        Sea.WorldMapScene.Io.Ship.SeaRot = Sea.WorldMapScene.Io.Ship.GO.transform.rotation;
        Sea.WorldMapScene.Io.Board.Swells.DisableSwells();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        SetState(new CameraPan_State(
            subsequentState: new BatterieAndCadence_State(),
            pan: new UnityEngine.Vector3(-50, Cam.Io.Camera.transform.rotation.eulerAngles.y, Cam.Io.Camera.transform.rotation.eulerAngles.z),
            strafe: Cam.Io.Camera.transform.position,
            speed: 3));
    }
}