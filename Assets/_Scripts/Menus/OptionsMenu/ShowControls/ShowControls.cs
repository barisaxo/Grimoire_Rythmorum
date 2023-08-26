using TMPro;
using UnityEngine;

public class ShowControls
{
    private Card _East;


    private Card _gamepad;

    private Card _North;

    private GameObject _parent;

    private Card _south;

    private Card _West;

    public ShowControls()
    {
        _ = Gamepad;
        _ = North;
        _ = East;
        _ = South;
        _ = West;
    }

    public GameObject Parent => _parent != null ? _parent : _parent = new GameObject(nameof(ShowControls));

    public Card Gamepad => _gamepad ??= new Card(nameof(Gamepad), Parent.transform)
        .SetImageSprite(Assets.GamePad)
        .SetImageSize(4, 3)
        .SetImagePosition(Vector3.left);

    public Card North => _North ??= new Card(nameof(North), Parent.transform)
        .SetImageSprite(Assets.NorthButton)
        .SetImageSize(Vector2.one * .6f)
        .SetPositionAll(Cam.OrthoX - 2, 2)
        .SetTextAlignment(TextAlignmentOptions.Left)
        .SetTextString("Interact")
        .SetFontScale(.6f, .6f)
        .OffsetTMPPosition(new Vector2(1, 0))
        .AutoSizeFont(true)
        .AllowWordWrap(false);

    public Card West => _West ??= new Card(nameof(West), Parent.transform)
        .SetImageSprite(Assets.WestButton)
        .SetImageSize(Vector2.one * .6f)
        .SetPositionAll(Cam.OrthoX - 2, -1)
        .SetTextAlignment(TextAlignmentOptions.Left)
        .SetTextString("")
        .SetFontScale(.6f, .6f)
        .OffsetTMPPosition(new Vector2(1, 0))
        .AutoSizeFont(true)
        .AllowWordWrap(false);

    public Card East => _East ??= new Card(nameof(East), Parent.transform)
        .SetImageSprite(Assets.EastButton)
        .SetImageSize(Vector2.one * .6f)
        .SetPositionAll(Cam.OrthoX - 2, 1)
        .SetTextAlignment(TextAlignmentOptions.Left)
        .SetTextString("Confirm")
        .SetFontScale(.6f, .6f)
        .OffsetTMPPosition(new Vector2(1, 0))
        .AutoSizeFont(true)
        .AllowWordWrap(false);

    public Card South => _south ??= new Card(nameof(South), Parent.transform)
        .SetImageSprite(Assets.SouthButton)
        .SetImageSize(Vector2.one * .6f)
        .SetPositionAll(Cam.OrthoX - 2, 0)
        .SetTextAlignment(TextAlignmentOptions.Left)
        .SetTextString("Cancel /\nBack")
        .SetFontScale(.6f, .6f)
        .OffsetTMPPosition(new Vector2(1, 0))
        .AutoSizeFont(true)
        .AllowWordWrap(false);

    public void SelfDestruct()
    {
        Object.Destroy(_parent);
    }
}