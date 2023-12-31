using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//TODO HUD, Inventory, 
namespace Sea
{
    public class HUD
    {
        #region  INSTANCE

        public HUD(CharacterData characterData)
        {
            Data = characterData;
            _ = Icon;
            _ = HealthBar;
            _ = North;
            _ = East;
            _ = South;
            _ = West;
            _ = DayCounterText;

        }

        public void SelfDestruct()
        {
            _hud.SelfDestruct();
            Resources.UnloadUnusedAssets();
        }

        #endregion  INSTANCE

        public readonly string _shipString = "SHIP ";
        public readonly string _islandString = "ISLAND ";
        public readonly string _commaSpaceString = ", ";

        private Card _hud;
        public Card Hud => _hud ??= new(nameof(Hud), null);
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
        public Card Icon => _icon ??= Hud.CreateChild(nameof(Icon), Hud.Canvas)
            .SetImageSprite(Assets.Pino)
            .SetImageSize(1, 1f)
            .SetImagePosition(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 1.5f)
            ;

        private Card _healthBar;
        public Card HealthBar => _healthBar ??= Hud.CreateChild(nameof(HealthBar), Hud.Canvas)
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
        public Card North => _northButton ??= Hud.CreateChild(nameof(North), Hud.Canvas)
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
        public Card East => _EastButton ??= Hud.CreateChild(nameof(East), Hud.Canvas)
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
        public Card South => _southButton ??= Hud.CreateChild(nameof(South), Hud.Canvas)
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
        public Card West => _westButton ??= Hud.CreateChild(nameof(West), Hud.Canvas)
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

        private Card _dayCounterText;
        public Card DayCounterText => _dayCounterText ??= Hud.CreateChild(nameof(DayCounterText), Hud.Canvas)
            .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, Cam.UIOrthoY - 1))
            .AutoSizeTextContainer(true)
            .SetTextAlignment(TextAlignmentOptions.Right)
            .SetFontScale(.45f, .45f)
            .AllowWordWrap(false)
            ;

        private Card _sextantText;
        public Card SextantText => _sextantText ??= Hud.CreateChild(nameof(SextantText), Hud.Canvas)
            .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1))
            .SetTMPSize(new Vector2(3, 1))
            .SetTextAlignment(TextAlignmentOptions.Center)
            .SetFontScale(.55f, .55f)
            .AutoSizeFont(true);

        private Card _islandCoordText;
        public Card IslandCoordText => _islandCoordText ??= Hud.CreateChild(nameof(IslandCoordText), Hud.Canvas)
            .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, Cam.UIOrthoY - 1))
            .SetTMPSize(new Vector2(3, 1))
            .SetTextAlignment(TextAlignmentOptions.Left)
            .SetFontScale(.55f, .55f)
            .AutoSizeFont(true);

    }
}