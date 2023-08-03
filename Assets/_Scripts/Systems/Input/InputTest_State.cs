using UnityEngine;

public class InputTest_State : State
{
    TestObject TestObject;
    Card TestCard => TestObject.TestCard;

    protected override void EngageState()
    {
        TestObject = new();
    }

    protected override void DisengageState()
    {
        TestObject.SelfDestruct();
    }

    protected override void CancelPressed()
    {
        FadeToState(new InputTest_State());
    }

    protected override void ConfirmPressed()
    {
        Debug.Log(nameof(ConfirmPressed));
        TestCard.SetSpriteColor(Random.Range(0, 8) switch
        {
            1 => Color.red,
            2 => Color.green,
            3 => Color.blue,
            4 => Color.cyan,
            5 => Color.yellow,
            6 => Color.magenta,
            7 => Color.grey,
            _ => Color.black
        });
    }

    protected override void InteractPressed()
    {
        SetStateDirectly(new InputTest_State());
    }

    //protected override void SelectPressed()
    //{
    //    Debug.Log(nameof(SelectPressed));
    //}

    protected override void StartPressed()
    {
        Debug.Log(nameof(StartPressed));
        TestCard.SetSpriteColor(Color.white);
        TestCard.SetGOSize(Vector2.one);
    }

    protected override void DirectionPressed(Dir dir)
    {
        Debug.Log(dir);
        switch (dir)
        {
            case Dir.Up: TestCard.SetGOSize(Vector2.one * 2); break;
            case Dir.Down: TestCard.SetGOSize(Vector2.one * .5f); break;
            case Dir.Left: TestCard.SetGOSize(new Vector2(1, 2)); break;
            case Dir.Right: TestCard.SetGOSize(new Vector2(2, 1)); break;
                //case Dir.Reset: TestCard.SetGOSize(Vector2.one); break;
        }
    }

    protected override void LStickInput(Vector2 v2)
    {
        Debug.Log(nameof(LStickInput) + v2);
    }

    protected override void RStickInput(Vector2 v2)
    {
        TestCard.SetGOPosition(v2);
    }

}


public class TestObject
{
    public TestObject()
    {
        _ = TestCard;
        _ = ControlsText;
    }

    public void SelfDestruct() { GameObject.Destroy(Parent.gameObject); }

    private Transform _parent;
    public Transform Parent => _parent != null ? _parent : _parent = new GameObject(nameof(TestObject)).transform;

    private Card _testCard;
    public Card TestCard => _testCard ??= new Card(nameof(TestCard), Parent)
        .SetSprite(Assets.White);

    private Card _controlsText;
    public Card ControlsText => _controlsText ??= new Card(nameof(ControlsText), Parent)
         .SetTextString("Start = Reset Square\n" +
                       "DPad = Resize Square\n" +
                       "Interact (North) = SetStateDirectly(new InputTestState());\n" +
                       "Cancel (South) = FadeToState(new InputTestState());\n" +
                       "Confirm (East) = Randomly Color Square\n")
        .SetFontScale(3, 3)
        .SetTMPSize(new Vector2(4, 8))
        .SetTMPPosition(new Vector2(-7, -6))
        .AllowWordWrap(false);

}