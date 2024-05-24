using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cove
{
    public class HUD
    {
        #region  INSTANCE

        public HUD()
        {
            _ = North;
            _ = East;
            _ = South;
            _ = West;
        }

        public void SelfDestruct()
        {
            _hud?.SelfDestruct();
            Resources.UnloadUnusedAssets();
        }

        #endregion  INSTANCE

        private Card _hud;
        public Card Hud => _hud ??= new(nameof(Hud), null);

        private Card _northButton;
        public Card North => _northButton ??= Hud.CreateChild(nameof(North), Hud.Canvas)
            .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 3.15f)
            .SetFontScale(.5f, .5f)
            .AutoSizeTextContainer(true)
            .SetImageSprite(Assets.NorthButton)
            .SetImageSize(.5f, .5f)
            .SetTextString("supercalifragisticexpialadocious")
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
            .SetTextString("supercalifragisticexpialadocious")
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
            .SetTextString("supercalifragisticexpialadocious")
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
            .SetTextString("supercalifragisticexpialadocious")
            .SetTextAlignment(TextAlignmentOptions.Right)
            .SetTMPRectPivot(new Vector2(1, .5f))
            .SetOutlineWidth(.15f)
            .OffsetImagePosition(Vector2.right)
            ;

        public void HideTexts()
        {
            North.SetImageColor(Color.clear).SetTextString("");
            West.SetImageColor(Color.clear).SetTextString("");
            East.SetImageColor(Color.clear).SetTextString("");
            South.SetImageColor(Color.clear).SetTextString("");
        }
    }
}