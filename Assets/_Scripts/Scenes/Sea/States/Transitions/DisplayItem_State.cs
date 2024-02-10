using System;
using UnityEngine;

public class DisplayItem_State : State
{
    readonly State SubsequentState;
    readonly GameObject ToInstantiate;
    GameObject DisplayItem;
    float timer;
    readonly bool ClearCell;

    public DisplayItem_State(State subsequentState, GameObject toInstantiate, bool clearCell)
    {
        SubsequentState = subsequentState;
        ToInstantiate = toInstantiate;
        ClearCell = clearCell;
    }

    protected override void PrepareState(Action callback)
    {
        Audio.Ambience.Pause();
        Audio.BGMusic.Pause();
        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        if (ClearCell) Sea.WorldMapScene.Io.ClearCell();
        DisplayItem = GameObject.Instantiate(ToInstantiate);
        DisplayItem.transform.SetPositionAndRotation(Cam.Io.Camera.transform.position + Cam.Io.Camera.transform.forward, Quaternion.identity);
        MonoHelper.OnUpdate += Tick;
    }
    protected override void DisengageState()
    {
        MonoHelper.OnUpdate -= Tick;
        GameObject.Destroy(DisplayItem);
    }

    void Tick()
    {
        DisplayItem.transform.Rotate(36 * Time.deltaTime * Vector3.up);
        timer += Time.deltaTime;
    }

    protected override void GPInput(GamePadButton gpb)
    {
        if (timer > 1) SetStateDirectly(SubsequentState);
    }

    protected override void LStickInput(Vector2 v2)
    {
        DisplayItem.transform.Rotate(new Vector3(v2.y, v2.x, 0));
    }

    protected override void RStickInput(Vector2 v2)
    {
        DisplayItem.transform.Rotate(new Vector3(0, 0, v2.x));
    }
}