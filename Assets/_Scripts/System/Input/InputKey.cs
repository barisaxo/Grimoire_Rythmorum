using UnityEngine;
using System;

public static class InputKey
{
    private static InputActions _inputActions;
    public static InputActions InputActions => _inputActions ??= new InputActions();

    private static event Action<GamePadButton> _buttonEvent;
    public static event Action<GamePadButton> ButtonEvent
    {
        add
        {
            _buttonEvent += value;
            // Debug.Log("Adding " + value + " " + _buttonEvent?.GetInvocationList().Length);
            // foreach (var action in _buttonEvent?.GetInvocationList())
            // {
            //     Debug.Log("button event: " + action.GetType().FullName);
            // }
        }
        remove
        {
            _buttonEvent = null;
            // Debug.Log("Removing " + value + " " + _buttonEvent?.GetInvocationList().Length);
        }
    }

    private static event Action<GamePadButton, Vector2> _stickEvent;
    public static event Action<GamePadButton, Vector2> StickEvent
    {
        add
        {
            _stickEvent = value;
            // Debug.Log("Adding " + value + " " + _buttonEvent?.GetInvocationList().Length);
        }
        remove
        {
            _stickEvent = null;
            // Debug.Log("Removing " + value + " " + _buttonEvent?.GetInvocationList().Length);
        }
    }

    //public static event Action<GamePadButton, Vector2> RStickAltEvent;
    public static event Action<MouseAction, Vector3> MouseClickEvent;


    // public static event Action<float> RStickAltXEvent;
    //public static event Action<float> RStickAltYEvent;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AutoInit()
    {
        //button press
        InputActions.Map.DUp.performed += _ => _buttonEvent?.Invoke(GamePadButton.Up_Press);
        InputActions.Map.DDown.performed += _ => _buttonEvent?.Invoke(GamePadButton.Down_Press);
        InputActions.Map.DLeft.performed += _ => _buttonEvent?.Invoke(GamePadButton.Left_Press);
        InputActions.Map.DRight.performed += _ => _buttonEvent?.Invoke(GamePadButton.Right_Press);

        InputActions.Map.East.performed += _ => _buttonEvent?.Invoke(GamePadButton.East_Press);
        InputActions.Map.South.performed += _ => _buttonEvent?.Invoke(GamePadButton.South_Press);
        InputActions.Map.North.performed += _ => _buttonEvent?.Invoke(GamePadButton.North_Press);
        InputActions.Map.West.performed += _ => _buttonEvent?.Invoke(GamePadButton.West_Press);

        InputActions.Map.R1.performed += _ => _buttonEvent?.Invoke(GamePadButton.R1_Press);
        InputActions.Map.R2.performed += _ => _buttonEvent?.Invoke(GamePadButton.R2_Press);
        InputActions.Map.R3.performed += _ => _buttonEvent?.Invoke(GamePadButton.R3_Press);
        InputActions.Map.L1.performed += _ => _buttonEvent?.Invoke(GamePadButton.L1_Press);
        InputActions.Map.L2.performed += _ => _buttonEvent?.Invoke(GamePadButton.L2_Press);
        InputActions.Map.L3.performed += _ => _buttonEvent?.Invoke(GamePadButton.L3_Press);

        InputActions.Map.Start.performed += _ => _buttonEvent?.Invoke(GamePadButton.Start_Press);
        InputActions.Map.Select.performed += _ => _buttonEvent?.Invoke(GamePadButton.Select_Press);

        //button release
        InputActions.Map.DUp.canceled += _ => _buttonEvent?.Invoke(GamePadButton.Up_Release);
        InputActions.Map.DDown.canceled += _ => _buttonEvent?.Invoke(GamePadButton.Down_Release);
        InputActions.Map.DLeft.canceled += _ => _buttonEvent?.Invoke(GamePadButton.Left_Release);
        InputActions.Map.DRight.canceled += _ => _buttonEvent?.Invoke(GamePadButton.Right_Release);

        InputActions.Map.East.canceled += _ => _buttonEvent?.Invoke(GamePadButton.East_Release);
        InputActions.Map.South.canceled += _ => _buttonEvent?.Invoke(GamePadButton.South_Release);
        InputActions.Map.North.canceled += _ => _buttonEvent?.Invoke(GamePadButton.North_Release);
        InputActions.Map.West.canceled += _ => _buttonEvent?.Invoke(GamePadButton.West_Release);

        InputActions.Map.R1.canceled += _ => _buttonEvent?.Invoke(GamePadButton.R1_Release);
        InputActions.Map.R2.canceled += _ => _buttonEvent?.Invoke(GamePadButton.R2_Release);
        InputActions.Map.R3.canceled += _ => _buttonEvent?.Invoke(GamePadButton.R3_Release);
        InputActions.Map.L1.canceled += _ => _buttonEvent?.Invoke(GamePadButton.L1_Release);
        InputActions.Map.L2.canceled += _ => _buttonEvent?.Invoke(GamePadButton.L2_Release);
        InputActions.Map.L3.canceled += _ => _buttonEvent?.Invoke(GamePadButton.L3_Release);

        InputActions.Map.Start.canceled += _ => _buttonEvent?.Invoke(GamePadButton.Start_Release);
        InputActions.Map.Select.canceled += _ => _buttonEvent?.Invoke(GamePadButton.Select_Release);

        //stick input
        InputActions.Map.LStick.performed += _ => _stickEvent?.Invoke(GamePadButton.LStick, _.ReadValue<Vector2>());
        InputActions.Map.LStick.canceled += _ => _stickEvent?.Invoke(GamePadButton.LStick, Vector2.zero);
        InputActions.Map.RStick.performed += _ => _stickEvent?.Invoke(GamePadButton.RStick, _.ReadValue<Vector2>());
        InputActions.Map.RStick.canceled += _ => _stickEvent?.Invoke(GamePadButton.RStick, Vector2.zero);

        //Nintendo Switch RSticks are weird, and the Y is inverted (up is negative, so inverting the sign with minus read value).
        InputActions.Map.RStickAltX.performed += _ => RAltXInput(_.ReadValue<float>());
        InputActions.Map.RStickAltX.canceled += _ => RAltXInput(0);
        InputActions.Map.RStickAltY.performed += _ => RAltYInput(-_.ReadValue<float>());
        InputActions.Map.RStickAltY.canceled += _ => RAltYInput(0);

        MonoHelper.OnUpdate += AltInput;
        MonoHelper.OnUpdate += RStickAltReadLoop;
        // StickEvent += DebugStick;
        InputActions.Map.Enable();
    }

    // static void DebugStick(GamePadButton gpi, Vector2 v3) { Debug.Log(gpi + " " + v3); }

    static void AltInput()
    {
        if (Input.GetMouseButtonDown(0)) { MouseClickEvent?.Invoke(MouseAction.LDown, Input.mousePosition); }
        else if (Input.GetMouseButtonUp(0)) { MouseClickEvent?.Invoke(MouseAction.LUp, Input.mousePosition); }
        else if (Input.GetMouseButton(0)) { MouseClickEvent?.Invoke(MouseAction.LHold, Input.mousePosition); }

        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.R3_Press);
        if (UnityEngine.InputSystem.Keyboard.current.aKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.Left_Press);
        if (UnityEngine.InputSystem.Keyboard.current.sKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.Down_Press);
        if (UnityEngine.InputSystem.Keyboard.current.dKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.Right_Press);
        if (UnityEngine.InputSystem.Keyboard.current.wKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.Up_Press);
        if (UnityEngine.InputSystem.Keyboard.current.iKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.North_Press);
        if (UnityEngine.InputSystem.Keyboard.current.kKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.South_Press);
        if (UnityEngine.InputSystem.Keyboard.current.lKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.East_Press);
        if (UnityEngine.InputSystem.Keyboard.current.jKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.West_Press);
        if (UnityEngine.InputSystem.Keyboard.current.gKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.Select_Press);
        if (UnityEngine.InputSystem.Keyboard.current.hKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.Start_Press);
        if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.L1_Press);
        if (UnityEngine.InputSystem.Keyboard.current.uKey.wasPressedThisFrame) _buttonEvent?.Invoke(GamePadButton.R1_Press);
    }

    ///nintendo switch R sticks are weird
    private static bool NewRStickAltThisFrame;

    private static Vector2 RStickAlt => new(RStickAltX, RStickAltY);
    private static float _rStickAltX;

    private static float RStickAltX
    {
        get => _rStickAltX;
        set
        {
            NewRStickAltThisFrame = true;
            _rStickAltX = value;
        }
    }

    private static float _rStickAltY;

    private static float RStickAltY
    {
        get => _rStickAltY;
        set
        {
            NewRStickAltThisFrame = true;
            _rStickAltY = value;
        }
    }

    private static void RAltXInput(float f)
    {
        RStickAltX = f;
    }

    private static void RAltYInput(float f)
    {
        RStickAltY = f;
    }

    private static void RStickAltReadLoop()
    {
        if (!NewRStickAltThisFrame) return;
        _stickEvent?.Invoke(GamePadButton.RStick, RStickAlt);
        NewRStickAltThisFrame = false;
    }

}

public enum MouseAction { LDown, LUp, LHold }

/// <summary>
/// GamePad Buttons
/// </summary>
public enum GamePadButton
{
    None = -1,

    Up_Press, Down_Press, Left_Press, Right_Press,
    North_Press, East_Press, South_Press, West_Press,
    R1_Press, R2_Press, R3_Press,
    L1_Press, L2_Press, L3_Press,
    Select_Press, Start_Press,

    Up_Release, Down_Release, Left_Release, Right_Release,
    North_Release, East_Release, South_Release, West_Release,
    R1_Release, R2_Release, R3_Release,
    L1_Release, L2_Release, L3_Release,
    Select_Release, Start_Release,

    LStick, RStick,
}

/// <summary>
/// Directional input.
/// </summary>
public enum Dir { Reset = -1, Up, Up_Off, Down, Down_Off, Left, Left_Off, Right, Right_Off }