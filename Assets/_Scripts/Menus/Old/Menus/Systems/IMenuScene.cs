using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IMenuScene
{
    public Transform TF { get; }
    public void Initialize();
    public void SelfDestruct();

    public Card Hud { get; set; }
    protected Card _hud => Hud ??= new(nameof(Hud), null);

    public Card North { get; set; }
    protected Card _north => North ??= _hud.CreateChild(nameof(North), _hud.Canvas)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 3.15f)
        .SetFontScale(.5f, .5f)
        // .AutoSizeTextContainer(true)
        .AllowWordWrap(false)
        .SetImageSprite(Assets.NorthButton)
        .SetImageSize(.5f, .5f)
        // .SetTextString("supercalifragisticexpialadocious")
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)
        ;

    public Card East { get; set; }
    protected Card _east => East ??= _hud.CreateChild(nameof(East), _hud.Canvas)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 4.15f)
        .SetFontScale(.5f, .5f)
        // .AutoSizeTextContainer(true)
        .AllowWordWrap(false)
        .SetImageSprite(Assets.EastButton)
        .SetImageSize(.5f, .5f)
        // .SetTextString("supercalifragisticexpialadocious")
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)
        ;

    public Card South { get; set; }
    protected Card _south => South ??= _hud.CreateChild(nameof(South), _hud.Canvas)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 5.15f)
        .SetFontScale(.5f, .5f)
        // .AutoSizeTextContainer(true)
        .AllowWordWrap(false)
        .SetImageSize(.5f, .5f)
        .SetImageSprite(Assets.SouthButton)
        // .SetTextString("supercalifragisticexpialadocious")
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)
        ;

    Card West { get; set; }
    protected Card _west => West ??= _hud.CreateChild(nameof(West), _hud.Canvas)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 6.15f)
        .SetFontScale(.5f, .5f)
        // .AutoSizeTextContainer(true)
        .AllowWordWrap(false)
        .SetImageSprite(Assets.WestButton)
        .SetImageSize(.5f, .5f)
        // .SetTextString("supercalifragisticexpialadocious")
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)
        ;

    public void HideTexts()
    {
        _north.SetImageColor(Color.clear).SetTextString("");
        _west.SetImageColor(Color.clear).SetTextString("");
        _east.SetImageColor(Color.clear).SetTextString("");
        _south.SetImageColor(Color.clear).SetTextString("");
    }
    public void SetCardPos1(Card card)
    {
        card.SetPositionAll(Cam.UIOrthoX - 1.5f, -Cam.UIOrthoY + 1.15f)
        .OffsetImagePosition(Vector2.right);
    }
    public void SetCardPos2(Card card)
    {
        card.SetPositionAll(Cam.UIOrthoX - 1.5f, -Cam.UIOrthoY + 2.15f)
        .OffsetImagePosition(Vector2.right);
    }
    public void SetCardPos3(Card card)
    {
        card.SetPositionAll(Cam.UIOrthoX - 1.5f, -Cam.UIOrthoY + 3.15f)
        .OffsetImagePosition(Vector2.right);
    }
    public void SetCardPos4(Card card)
    {
        card.SetPositionAll(Cam.UIOrthoX - 1.5f, -Cam.UIOrthoY + 4.15f)
        .OffsetImagePosition(Vector2.right);
    }
}
