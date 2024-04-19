using System;
using UnityEngine;

public class DisplayItem_State : State
{
    readonly State SubsequentState;
    readonly GameObject ToInstantiate;
    Card Display3DCard;
    float timer;
    readonly bool ClearCell;
    // readonly Action Action;

    public DisplayItem_State(GameObject toInstantiate, State subsequentState, bool clearCell)
    {
        SubsequentState = subsequentState;
        ToInstantiate = toInstantiate;
        ClearCell = clearCell;
        // Action = action;
    }

    protected override void PrepareState(Action callback)
    {
        Audio.Ambience.Pause();
        Audio.BGMusic.Pause();
        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.AttackPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);
        // Action?.Invoke();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        if (ClearCell) Sea.WorldMapScene.Io.ClearCell();

        Display3DCard = new Card(nameof(Display3DCard), null).SetUIGO(GameObject.Instantiate(ToInstantiate));


        // DisplayItem = GameObject.Instantiate(ToInstantiate);
        // DisplayItem.transform.SetPositionAndRotation(Cam.Io.Camera.transform.position + Cam.Io.Camera.transform.forward, Quaternion.identity);
        MonoHelper.OnUpdate += Tick;
    }
    protected override void DisengageState()
    {
        MonoHelper.OnUpdate -= Tick;
        Display3DCard.SelfDestruct();
    }

    void Tick()
    {
        Display3DCard.UIGO.transform.Rotate(36 * Time.deltaTime * Vector3.up);
        timer += Time.deltaTime;
    }

    protected override void GPInput(GamePadButton gpb)
    {
        if (timer > 1) SetState(SubsequentState);
    }

    protected override void LStickInput(Vector2 v2)
    {
        Display3DCard.UIGO.transform.Rotate(new Vector3(v2.y, v2.x, 0));
    }

    protected override void RStickInput(Vector2 v2)
    {
        Display3DCard.UIGO.transform.Rotate(new Vector3(0, 0, v2.x));
    }
}