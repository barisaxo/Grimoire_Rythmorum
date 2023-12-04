using System;
using System.Collections;
using Audio;
using UnityEngine;

public abstract class State
{
    #region REFERENCES

    protected DataManager Data => DataManager.Io;
    protected AudioManager Audio => AudioManager.Io;

    #endregion REFERENCES


    #region STATE SYSTEMS
    // These state systems are organized in order of execution

    /// <summary>
    /// Called by SetStateDirectly() and InitiateFade().
    /// </summary>
    protected void DisableInput()
    {
        InputKey.ButtonEvent -= GPInput;
        InputKey.StickEvent -= GPStickInput;
        InputKey.MouseClickEvent -= Clicked;
        MonoHelper.OnUpdate -= UpdateStickInput;
    }

    /// <summary>
    /// Called by SetStateDirectly() and FadeOutToBlack().
    /// </summary>
    protected virtual void DisengageState() { }

    /// <summary>
    /// Called by SetStateDirectly() and FadeOutToBlack(). Don't set new states here.
    /// </summary>
    protected virtual void PrepareState(Action callback) { callback?.Invoke(); }

    /// <summary>
    /// Called by SetSceneDirectly() and FadeInToScene().
    /// </summary>
    protected void EnableInput()
    {
        InputKey.ButtonEvent += GPInput;
        InputKey.StickEvent += GPStickInput;
        InputKey.MouseClickEvent += Clicked;
        MonoHelper.OnUpdate += UpdateStickInput;
    }

    /// <summary>
    /// Called by FadeToState() after Prepare(), then waits a step before Engage().
    /// Useful for initializing hidden GOs that need a step before disabling them in Engage().
    /// Don't set new states here.
    /// </summary>
    protected virtual void PreEngageState(Action callback) { callback?.Invoke(); }

    /// <summary>
    /// Called by SetStateDirectly() and FadeInToScene(). OK to set new states here.
    /// </summary>
    protected virtual void EngageState() { }

    protected void SetStateDirectly(State newState)
    {
        DisableInput();
        DisengageState();

        newState.PrepareState(Initiate().StartCoroutine);

        IEnumerator Initiate()
        {
            yield return null;
            newState.EnableInput();
            newState.EngageState();
        }
    }

    protected void FadeToState(State newState)
    {
        ScreenFader fader = new();
        InitiateFade().StartCoroutine();
        return;

        IEnumerator InitiateFade()
        {
            DisableInput();
            yield return null;
            FadeOutToBlack().StartCoroutine();
        }

        IEnumerator FadeOutToBlack()
        {
            while (fader.Screen.color.a < .99f)
            {
                yield return null;
                fader.Screen.color += new Color(0, 0, 0, Time.deltaTime * 1.25f);
            }

            fader.Screen.color = Color.black;
            DisengageState();
            yield return null;
            newState.PrepareState(WaitAStep().StartCoroutine);
        }

        IEnumerator WaitAStep()
        {
            yield return null;
            newState.PreEngageState(FadeInToScene().StartCoroutine);
        }

        IEnumerator FadeInToScene()
        {
            while (fader.Screen.color.a > .01f)
            {
                yield return null;
                fader.Screen.color -= new Color(0, 0, 0, Time.deltaTime * 2.0f);
            }

            fader.SelfDestruct();
            newState.EnableInput();
            newState.EngageState();
        }
    }

    #endregion STATE SYSTEMS


    #region INPUT

    protected virtual void DirectionPressed(Dir dir) { }
    protected virtual void WestPressed() { }
    protected virtual void ConfirmPressed() { }
    protected virtual void InteractPressed() { }
    protected virtual void CancelPressed() { }
    protected virtual void StartPressed() { }
    protected virtual void SelectPressed() { }
    protected virtual void R1Pressed() { }
    protected virtual void L1Pressed() { }
    protected virtual void R2Pressed() { }
    protected virtual void L2Pressed() { }
    protected virtual void R3Pressed() { }
    protected virtual void L3Pressed() { }
    protected virtual void LStickInput(Vector2 v2) { }
    protected virtual void RStickInput(Vector2 v2) { }
    protected virtual void ClickedOn(GameObject go) { }
    protected virtual void Clicked(MouseAction action, Vector3 mousePos)
    {
        if (action != MouseAction.LUp) return;

        if (Cam.Io.UICamera.orthographic)
        {
            RaycastHit2D hitUI = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hitUI.collider != null) { ClickedOn(hitUI.collider.gameObject); return; }

            var hit = Physics2D.Raycast(Cam.Io.UICamera.ScreenToWorldPoint(mousePos), Vector2.zero);
            if (hit.collider != null)
            {
                ClickedOn(hit.collider.gameObject);
                return;
            }

            return;
        }

        else
        {
            var hit = Physics2D.GetRayIntersection(Cam.Io.Camera.ScreenPointToRay(mousePos));
            var hitUI = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null) { ClickedOn(hit.collider.gameObject); return; }
            else if (hitUI.collider != null) { ClickedOn(hitUI.collider.gameObject); return; }
            return;
        }
    }

    protected virtual void GPInput(GamePadButton gpb)
    {
        switch (gpb)
        {
            #region BUTTON PRESSED

            case GamePadButton.Up_Press: DirectionPressed(Dir.Up); break;
            case GamePadButton.Down_Press: DirectionPressed(Dir.Down); break;
            case GamePadButton.Left_Press: DirectionPressed(Dir.Left); break;
            case GamePadButton.Right_Press: DirectionPressed(Dir.Right); break;
            case GamePadButton.North_Press: InteractPressed(); break;
            case GamePadButton.East_Press: ConfirmPressed(); break;
            case GamePadButton.South_Press: CancelPressed(); break;
            case GamePadButton.West_Press: WestPressed(); break;
            case GamePadButton.Start_Press: StartPressed(); break;
            case GamePadButton.Select_Press: SelectPressed(); break;
            case GamePadButton.R1_Press: R1Pressed(); break;
            case GamePadButton.R2_Press: R2Pressed(); break;
            case GamePadButton.R3_Press: R3Pressed(); break;
            case GamePadButton.L1_Press: L1Pressed(); break;
            case GamePadButton.L2_Press: L2Pressed(); break;
            case GamePadButton.L3_Press: L3Pressed(); break;

            #endregion BUTTON PRESSED

            #region BUTTON RELEASED

            case GamePadButton.Up_Release: DirectionPressed(Dir.Up_Off); break;
            case GamePadButton.Down_Release: DirectionPressed(Dir.Down_Off); break;
            case GamePadButton.Left_Release: DirectionPressed(Dir.Left_Off); break;
            case GamePadButton.Right_Release: DirectionPressed(Dir.Right_Off); break;
            case GamePadButton.North_Release: break;
            case GamePadButton.East_Release: break;
            case GamePadButton.South_Release: break;
            case GamePadButton.Start_Release: break;
            case GamePadButton.Select_Release: break;
            case GamePadButton.R1_Release: break;
            case GamePadButton.R2_Release: break;
            case GamePadButton.R3_Release: break;
            case GamePadButton.L1_Release: break;
            case GamePadButton.L2_Release: break;
            case GamePadButton.L3_Release: break;

                #endregion BUTTON RELEASED
        }
    }

    protected Vector2 LStick;
    protected Vector2 RStick;

    private void GPStickInput(GamePadButton gpi, Vector2 v2)
    {
        switch (gpi)
        {
            case GamePadButton.LStick: LStick = v2; break;
            case GamePadButton.RStick: RStick = v2; break;
        }
    }

    private void UpdateStickInput()
    {
        LStickInput(LStick);
        RStickInput(RStick);
    }


    #endregion INPUT

}


// protected void PanCameraToState(State newState, Vector3 pan, Vector3 strafe)
// {
//     InitiatePan().StartCoroutine();
//     return;

//     IEnumerator InitiatePan()
//     {
//         DisableInput();
//         DisengageState();
//         yield return null;
//         PanCamera().StartCoroutine();
//     }

//     IEnumerator PanCamera()
//     {
//         pan = new Vector3(pan.x.SignedMod(360), pan.y.SignedMod(360), pan.z.SignedMod(360));

//         while (!Cam.Io.Camera.transform.rotation.eulerAngles.x.IsPOM(1, pan.x) ||
//                !Cam.Io.Camera.transform.rotation.eulerAngles.y.IsPOM(1, pan.y) ||
//                !Cam.Io.Camera.transform.rotation.eulerAngles.z.IsPOM(1, pan.z) ||
//                !Cam.Io.Camera.transform.position.x.IsPOM(1, strafe.x) ||
//                !Cam.Io.Camera.transform.position.y.IsPOM(1, strafe.y) ||
//                !Cam.Io.Camera.transform.position.z.IsPOM(1, strafe.z))
//         {
//             Cam.Io.Camera.transform.rotation = Quaternion.Slerp(Cam.Io.Camera.transform.rotation, Quaternion.Euler(pan), Time.deltaTime * 2.3f);
//             Cam.Io.Camera.transform.position = Vector3.Lerp(Cam.Io.Camera.transform.position, strafe, Time.deltaTime * 2.3f);
//             yield return null;
//         }

//         newState.PrepareState(WaitAStep().StartCoroutine);
//     }

//     IEnumerator WaitAStep()
//     {
//         yield return null;
//         Cam.Io.Camera.transform.position = strafe;
//         Cam.Io.Camera.transform.rotation = Quaternion.Euler(pan);
//         newState.PreEngageState(SetScene().StartCoroutine);
//     }

//     IEnumerator SetScene()
//     {
//         yield return null;
//         newState.EnableInput();
//         newState.EngageState();
//     }
// }

