using System;
using System.Collections;
using Audio;
using UnityEngine;

public abstract class State
{
    #region REFERENCES

    protected DataManager DataManager => DataManager.Io;
    protected AudioManager Audio => AudioManager.Io;

    #endregion REFERENCES


    #region STATE SYSTEMS
    // These state systems are organized in order of execution

    public bool Fade;

    /// <summary>
    /// Called by SetStateDirectly() and InitiateFade().
    /// </summary>
    protected void DisableInput()
    {
        Debug.Log(nameof(DisableInput));
        MonoHelper.OnUpdate -= UpdateStickInput;
        InputKey.ButtonEvent -= GPInput;
        InputKey.StickEvent -= GPStickInput;
        InputKey.MouseClickEvent -= Clicked;
    }

    /// <summary>
    /// Called by SetStateDirectly() and FadeOutToBlack().
    /// </summary>
    protected virtual void DisengageState() { }

    /// <summary>
    /// Initialize the state here. Called by SetStateDirectly() and FadeOutToBlack(). Don't set new states here.
    /// </summary>
    protected virtual void PrepareState(Action callback) { callback?.Invoke(); }

    /// <summary>
    /// Called by FadeToState() after Prepare(), then waits a step before Engage().
    /// Useful for initializing hidden GOs that need a step before disabling them in Engage().
    /// Don't set new states here.
    /// </summary>
    protected virtual void PreEngageState(Action callback) { callback?.Invoke(); }

    /// <summary>
    /// Called by SetStateDirectly() and FadeInToScene(), after PrepareState(). OK to set new states here.
    /// </summary>
    protected virtual void EngageState() { }

    /// <summary>
    /// Called by SetSceneDirectly() and FadeInToScene().
    /// </summary>
    protected void EnableInput()
    {
        Debug.Log(nameof(EnableInput));
        MonoHelper.OnUpdate += UpdateStickInput;
        InputKey.ButtonEvent += GPInput;
        InputKey.StickEvent += GPStickInput;
        InputKey.MouseClickEvent += Clicked;
    }

    protected void SetState(State newState)
    {
        if (newState is null) return;

        if (newState.Fade) FadeToState(newState);
        else SetStateDirectly(newState);
    }

    private void SetStateDirectly(State newState)
    {
        if (newState is null) return;

        DisableInput();
        DisengageState();

        newState.PrepareState(Initiate().StartCoroutine);

        IEnumerator Initiate()
        {
            yield return null;
            newState.PreEngageState(Wait().StartCoroutine);
        }

        IEnumerator Wait()
        {
            yield return null;
            newState.EngageState();
            newState.EnableInput();
        }
    }

    private void FadeToState(State newState)
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
            newState.EngageState();

            while (fader.Screen.color.a > .01f)
            {
                yield return null;
                fader.Screen.color -= new Color(0, 0, 0, Time.deltaTime * 2.0f);
            }

            fader.SelfDestruct();
            newState.EnableInput();
        }
    }

    #endregion STATE SYSTEMS


    #region INPUT

    protected virtual void DirectionPressed(Dir dir) { }
    protected virtual void WestPressed() { }
    protected virtual void EastPressed() { }
    protected virtual void NorthPressed() { }
    protected virtual void SouthPressed() { }
    protected virtual void StartPressed() { }
    protected virtual void SelectPressed() { }
    protected virtual void R1Pressed() { }
    protected virtual void L1Pressed() { }
    protected virtual void R2Pressed() { }
    protected virtual void L2Pressed() { }
    protected virtual void R3Pressed() { }
    protected virtual void L3Pressed() { }
    protected virtual void DirectionReleased(Dir dir) { }
    protected virtual void EastReleased() { }
    protected virtual void NorthReleased() { }
    protected virtual void SouthReleased() { }
    protected virtual void WestReleased() { }
    protected virtual void StartReleased() { }
    protected virtual void SelectReleased() { }
    protected virtual void R1Released() { }
    protected virtual void L1Released() { }
    protected virtual void R2Released() { }
    protected virtual void L2Released() { }
    protected virtual void R3Released() { }
    protected virtual void L3Released() { }
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
            case GamePadButton.North_Press: NorthPressed(); break;
            case GamePadButton.East_Press: EastPressed(); break;
            case GamePadButton.South_Press: SouthPressed(); break;
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
            case GamePadButton.Up_Release: DirectionReleased(Dir.Up_Off); break;
            case GamePadButton.Down_Release: DirectionReleased(Dir.Down_Off); break;
            case GamePadButton.Left_Release: DirectionReleased(Dir.Left_Off); break;
            case GamePadButton.Right_Release: DirectionReleased(Dir.Right_Off); break;
            case GamePadButton.North_Release: EastReleased(); break;
            case GamePadButton.East_Release: NorthReleased(); break;
            case GamePadButton.South_Release: SouthReleased(); break;
            case GamePadButton.West_Release: WestReleased(); break;
            case GamePadButton.Start_Release: StartReleased(); break;
            case GamePadButton.Select_Release: SelectReleased(); break;
            case GamePadButton.R1_Release: R1Released(); break;
            case GamePadButton.R2_Release: R2Released(); break;
            case GamePadButton.R3_Release: R3Released(); break;
            case GamePadButton.L1_Release: L1Released(); break;
            case GamePadButton.L2_Release: L2Released(); break;
            case GamePadButton.L3_Release: L3Released(); break;
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
