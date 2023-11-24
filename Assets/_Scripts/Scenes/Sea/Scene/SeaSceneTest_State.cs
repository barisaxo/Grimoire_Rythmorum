using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSceneTest_State : State
{
    protected override void PrepareState(Action callback)
    {
        Data.SeaScene ??= new();
        MonoHelper.OnUpdate += CheckInput;
        MonoHelper.OnUpdate += FPS;
        MonoHelper.OnFixedUpdate += Movement;

        base.PrepareState(callback);
    }

    protected override void DisengageState()
    {
        MonoHelper.OnUpdate -= CheckInput;
        MonoHelper.OnUpdate -= FPS;
        MonoHelper.OnFixedUpdate -= Movement;
    }

    SeaScene SeaScene => Data.SeaScene;
    Vector3 Move = Vector3.zero;

    protected override void DirectionPressed(Dir dir)
    {
        // switch (dir)
        // {
        //     // case Dir.Up: Move += Vector3Int.forward; break;
        //     // case Dir.Down: Move += Vector3Int.back; break;
        //     // case Dir.Left: Move += Vector3Int.left; break;
        //     // case Dir.Right: Move += Vector3Int.right; break;
        //     // case Dir.Reset: Move = Vector3Int.zero; break;
        // }
    }

    protected void CheckInput()
    {
        if (Input.GetKey(KeyCode.W)) Move.z = Mathf.Clamp(Move.z - (Time.deltaTime * .8f), -.8f, .3f);
        else if (Input.GetKey(KeyCode.S)) Move.z = Mathf.Clamp(Move.z + Time.deltaTime, -.8f, .3f);
        else Move.z *= .99f;

        if (Input.GetKey(KeyCode.A)) Move.x = Mathf.Clamp(Move.x - Time.deltaTime * 2, -1f, 1);
        else if (Input.GetKey(KeyCode.D)) Move.x = Mathf.Clamp(Move.x + Time.deltaTime * 2, -1f, 1);
        else Move.x *= .9f;
    }

    void Movement()
    {
        Move = SeaScene.DirectionPressed(Move);
    }


    protected override void InteractPressed()
    {
        if (SeaScene.NearNPCShip == null) return;


        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        SetStateDirectly(new DialogStart_State(new PirateEncounter_Dialogue(null)));

    }

    protected override void LStickInput(Vector2 v2)
    {
        Move.z = Mathf.Clamp(Move.z - (Time.deltaTime * v2.y * .8f), -.8f, .3f);
        Move.x = Mathf.Clamp(Move.x - Time.deltaTime * -v2.x * 3, -1f, 1f);
    }

    protected override void RStickInput(Vector2 v2)
    {
        Move.x = Mathf.Clamp(Move.x - Time.deltaTime * -v2.x * 5f, -1f, 1f);
        SeaScene.CameraFollow.MoveCamera(v2.y);
    }


    int frames = 0;
    float elapsed = 0;
    const float interval = 1;
    float timer = 0;

    void FPS()
    {
        frames++;
        elapsed += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer -= interval;
            SeaScene.DayCounterText.TextString = ((int)(frames / elapsed)).ToString();
            frames = 0;
            elapsed = 0;
        }
    }
}
