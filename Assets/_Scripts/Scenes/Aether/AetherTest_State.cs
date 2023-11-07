using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherTest_State : State
{
    AetherScene AetherScene;
    Vector2 lStick, rStick;

    protected override void PrepareState(Action callback)
    {
        MonoHelper.ToFixedUpdate += HandleInput;
        AetherScene = new();
        Cam.Io.Camera.transform.SetParent(AetherScene.Player.GO.transform);
        Cam.Io.Camera.transform.position = AetherScene.Player.GO.transform.position;
        Cam.Io.Camera.transform.Translate(new Vector3(0, 8, 5));
        Cam.Io.Camera.transform.Rotate(new Vector3(-35, 0, 0));
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        PanCamera().StartCoroutine();
        IEnumerator PanCamera()
        {
            while (RStick.y == 0)
            {
                rStick.y = -.333f;
                yield return null;
            }
        }
    }

    protected override void DisengageState()
    {
        AetherScene.SelfDestruct();
    }

    protected override void LStickInput(Vector2 v2)
    {
        if (v2.y > .5f) v2.y = 1;
        else if (v2.y > 0) v2.y = .5f;
        lStick = v2;
    }

    protected override void RStickInput(Vector2 v2)
    {
        rStick = v2;
    }

    void HandleInput()
    {
        AetherScene.Player.RotatePlayer(rStick.x + (lStick.x * .7f));
        AetherScene.Player.MovePlayer(new Vector2(-lStick.x * .7f, -lStick.y));
        AetherScene.Player.MoveCamera(rStick.y);

        CheckDistances();
    }

    private void CheckDistances()
    {
        bool bark = false;
        if (NearShip) bark = true;

        if (bark) AetherScene.Player.Bark.SetTextString("...");
        else AetherScene.Player.Bark.SetTextString("");

        foreach (var x in AetherScene.Player.Bark.TMP.material.enabledKeywords)
            Debug.Log(x.name);
    }

    bool NearShip => Dist(AetherScene.Player.GO, AetherScene.Ship) < 2.5f;


    float Dist(GameObject a, GameObject b) => Vector2.Distance(
      new Vector2(a.transform.position.x, a.transform.position.z),
      new Vector2(b.transform.position.x, b.transform.position.z));
}