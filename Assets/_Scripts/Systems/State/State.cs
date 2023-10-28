using System;
using System.Collections;
using UnityEngine;
using Audio;

public abstract class State
{
    #region REFERENCES
    protected static DataManager Data = new DataManager();
    protected static AudioManager Audio = new AudioManager(Data.Volume);
    #endregion REFERENCES


    #region STATE SYSTEMS

    /// These state systems are organized in order of execution
    /// <summary>
    ///     Called by SetStateDirectly() and InitiateFade().
    /// </summary>
    protected void DisableInput()
    {
        InputKey.ButtonEvent -= GPInput;
        InputKey.StickEvent -= GPStickInput;
        InputKey.RStickAltXEvent -= RAltXInput;
        InputKey.RStickAltYEvent -= RAltYInput;
        InputKey.MouseClickEvent -= Clicked;
        MonoHelper.OnUpdate -= RStickAltReadLoop;
        MonoHelper.OnUpdate -= UpdateStickInput;
    }

    /// <summary>
    ///     Called by SetStateDirectly() and FadeOutToBlack().
    /// </summary>
    protected virtual void DisengageState() { }

    /// <summary>
    ///     Called by SetStateDirectly() and FadeOutToBlack(). DONT SET NEW STATES HERE.
    /// </summary>
    protected virtual void PrepareState(Action callback) => callback();

    /// <summary>
    ///     Called by SetSceneDirectly() and FadeInToScene().
    /// </summary>
    protected void EnableInput()
    {
        InputKey.ButtonEvent += GPInput;
        InputKey.StickEvent += GPStickInput;
        InputKey.RStickAltXEvent += RAltXInput;
        InputKey.RStickAltYEvent += RAltYInput;
        InputKey.MouseClickEvent += Clicked;
        MonoHelper.OnUpdate += RStickAltReadLoop;
        MonoHelper.OnUpdate += UpdateStickInput;
    }

    /// <summary>
    ///     Called by SetStateDirectly() and FadeInToScene(). OK to set new states here.
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

            yield return null;
            newState.PrepareState(FadeInToScene().StartCoroutine);
        }

        IEnumerator FadeInToScene()
        {
            DisengageState();

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
    /// <summary>
    /// Deal with the gameobject you clicked on here. Base is empty.
    /// </summary>
    protected virtual void ClickedOn(GameObject go) { }
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

    #endregion INPUT


    #region INPUT HANDLING

    protected virtual void Clicked(MouseAction action, Vector3 mousePos)
    {
        if (action != MouseAction.LUp) return;

        //if (Cam.Io.Camera.orthographic)
        //{
        //    var hit = Physics2D.Raycast(Cam.Io.UICamera.ScreenToWorldPoint(mousePos), Vector2.zero);
        //    if (hit.collider != null) ClickedOn(hit.collider.gameObject);
        //}
        //else
        //{
        var hitUI = Physics2D.GetRayIntersection(Cam.Io.UICamera.ScreenPointToRay(mousePos));
        var hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null) ClickedOn(hit.collider.gameObject);
        else if (hitUI.collider != null) ClickedOn(hitUI.collider.gameObject);
        //}
    }

    private void GPInput(GamePadButton gpb)
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

            case GamePadButton.Up_Release: DirectionPressed(Dir.Reset); break;
            case GamePadButton.Down_Release: DirectionPressed(Dir.Reset); break;
            case GamePadButton.Left_Release: DirectionPressed(Dir.Reset); break;
            case GamePadButton.Right_Release: DirectionPressed(Dir.Reset); break;
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

    public Vector2 LStick { get; private set; }
    public Vector2 RStick { get; private set; }
    public bool LStickZeroed { get; private set; }
    public bool RStickZeroed { get; private set; }

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
        if (!(LStickZeroed = LStickZeroed && LStick == Vector2.zero))
        {
            LStickInput(LStick);
            LStickZeroed = LStick == Vector2.zero;
        }

        if (!(RStickZeroed = RStickZeroed && RStick == Vector2.zero))
        {
            RStickInput(RStick);
            RStickZeroed = RStick == Vector2.zero;
        }
    }

    //nintendo switch R sticks are weird
    public bool NewRStickAltThisFrame { get; private set; }

    public Vector2 RStickAlt => new(RStickAltX, RStickAltY);
    private float _rStickAltX;

    public float RStickAltX
    {
        get => _rStickAltX;
        private set
        {
            NewRStickAltThisFrame = true;
            _rStickAltX = value;
        }
    }

    private float _rStickAltY;
    public float RStickAltY
    {
        get => _rStickAltY;
        private set
        {
            NewRStickAltThisFrame = true;
            _rStickAltY = value;
        }
    }

    private void RAltXInput(float f) => RStickAltX = f;

    private void RAltYInput(float f) => RStickAltY = f;

    private void RStickAltReadLoop()
    {
        if (!NewRStickAltThisFrame) return;
        RStickInput(RStickAlt);
        if (RStickAlt == Vector2.zero)
            NewRStickAltThisFrame = false;
    }

    #endregion INPUT HANDLING
}