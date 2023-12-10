using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//TODO HUD, Inventory, 

public class SeaHUD
{
    #region  INSTANCE

    public SeaHUD(CharacterData characterData)
    {
        Data = characterData;
        _ = Icon;
        _ = HealthBar;
        _ = North;
        _ = East;
        _ = South;
        _ = West;
    }

    public void SelfDestruct()
    {
        _hud.SelfDestruct();
        Resources.UnloadUnusedAssets();
    }

    #endregion  INSTANCE

    private Card _hud;
    public Card HUD => _hud ??= new(nameof(SeaHUD), null);
    private IEnumerator currentCoroutine;
    public IEnumerator CurrentCoroutine
    {
        get => currentCoroutine;
        set
        {
            currentCoroutine = value;
            value?.StartCoroutine();
        }
    }
    readonly CharacterData Data;

    private Card _icon;
    public Card Icon => _icon ??= HUD.CreateChild(nameof(Icon), HUD.Canvas)
        .SetImageSprite(Assets.Pino)
        .SetImageSize(1, 1f)
        .SetImagePosition(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 1.5f)
        ;

    private Card _healthBar;
    public Card HealthBar => _healthBar ??= HUD.CreateChild(nameof(HealthBar), HUD.Canvas)
        .SetImageSprite(Assets.White)
        .SetImageColor(Color.green)
        .SetImageSize(1f, .15f)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 2.15f)
        .SetTextString("4/4")
        .SetOutlineWidth(.15f)
        .TMPClickable()
        .ImageClickable()
        // .SetImageSize(Vector2.one * .6f)
        // .SetTMPSize(new Vector2(1f, 1f))
        // .SetPositionAll(new Vector2(Cam.UIOrthoX - 1.5f, -Cam.UIOrthoY + 1.5f))
        .OffsetTMPPosition(Vector2.down * .85f)
        // .SetImageSprite(Assets.SouthButton)
        // .SetTextColor(new Color(1, 1, 1, .65f))
        // .AutoSizeFont(true)
        .AllowWordWrap(false)
        .SetTextAlignment(TextAlignmentOptions.Center)
        // .SetFontScale(.5f, .5f);
        ;

    private Card _northButton;
    public Card North => _northButton ??= HUD.CreateChild(nameof(North), HUD.Canvas)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 3.15f)
        .SetFontScale(.5f, .5f)
        .AutoSizeTextContainer(true)
        .SetImageSprite(Assets.NorthButton)
        .SetImageSize(.5f, .5f)
        .SetTextString("Inventory")
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)
        ;

    private Card _EastButton;
    public Card East => _EastButton ??= HUD.CreateChild(nameof(East), HUD.Canvas)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 4.15f)
        .SetFontScale(.5f, .5f)
        .AutoSizeTextContainer(true)
        .SetImageSprite(Assets.EastButton)
        .SetImageSize(.5f, .5f)
        .SetTextString("Interact")
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)
        ;

    private Card _southButton;
    public Card South => _southButton ??= HUD.CreateChild(nameof(South), HUD.Canvas)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 5.15f)
        .SetFontScale(.5f, .5f)
        .AutoSizeTextContainer(true)
        .SetImageSize(.5f, .5f)
        .SetImageSprite(Assets.SouthButton)
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)
        ;

    private Card _westButton;
    public Card West => _westButton ??= HUD.CreateChild(nameof(West), HUD.Canvas)
        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 6.15f)
        .SetFontScale(.5f, .5f)
        .AutoSizeTextContainer(true)
        .SetImageSprite(Assets.WestButton)
        .SetImageSize(.5f, .5f)
        .SetTextString("Crows Nest")
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)
        ;
}
