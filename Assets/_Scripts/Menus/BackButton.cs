using TMPro;
using UnityEngine;

public class BackButton
{
    private readonly Transform Parent;

    private Card _button;

    public BackButton(Transform parent)
    {
        Parent = parent;
        _ = Button;
    }

    public Card Button => _button ??= new Card(nameof(BackButton), Parent)
        .TMPClickable()
        .ImageClickable()
        .SetTextString("Back")
        .SetImageSize(Vector2.one * .6f)
        .SetTMPSize(new Vector2(1f, 1f))
        .SetPositionAll(new Vector2(Cam.OrthoX - .5f, -Cam.OrthoY))
        .OffsetTMPPosition(Vector2.right * .85f)
        .SetImageSprite(Assets.SouthButton)
        .SetTextColor(new Color(1, 1, 1, .65f))
        .AutoSizeFont(true)
        .AllowWordWrap(false)
        .SetTextAlignment(TextAlignmentOptions.Center)
        .SetFontScale(.5f, .5f);
}