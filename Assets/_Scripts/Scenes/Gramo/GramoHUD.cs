using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gramophones
{
    public class GramoHUD : IHud
    {
        public IHud Initialize()
        {
            _ = QuitButton;
            return this;
        }

        private Card _parent;
        public Card Parent => _parent ??= new Card(nameof(Parent), null);

        private Card _keyOf;
        internal Card KeyOf => _keyOf ??= Parent.CreateChild(nameof(KeyOf), Parent.Canvas)
            .SetTextString("KEY OF: Gb")
            .SetTMPPosition(-2f, 1.5f - Cam.UIOrthoY)
            .SetFontScale(.5f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _allChords;
        internal Card AllChords => _allChords ??= Parent.CreateChild(nameof(AllChords), Parent.Canvas)
            .SetTextString("I: Gb,   II-: Ab-,   III-: Bb-,   IV: Cb,   V: Db,   VI-: Eb-,   VIIø: Fø")
            .SetTMPPosition(0, .75f - Cam.UIOrthoY)
            .SetFontScale(.5f)
            .SetTextAlignment(TextAlignmentOptions.Center)
            .AllowWordWrap(false);

        private Card _confirmButton;
        public Card ConfirmButton => _confirmButton ??= Parent.CreateChild(nameof(ConfirmButton), Parent.Canvas)
            .SetTextString("Try Open")
            .SetImageSprite(Assets.EastButton)
            .SetPositionAll(Cam.UIOrthoX - 2, -Cam.UIOrthoY + 2)
            .SetFontScale(.5f, .5f)
            .AllowWordWrap(false)
            .SetImageSize(.5f, .5f)
            .SetTextAlignment(TextAlignmentOptions.Right)
            .SetTMPRectPivot(new Vector2(1, .5f))
            .SetOutlineWidth(.15f)
            .OffsetImagePosition(Vector2.right)
            ;

        private Card _quitButton;
        public Card QuitButton => _quitButton ??= Parent.CreateChild(nameof(QuitButton), Parent.Canvas)
            .SetTextString("Quit")
            .SetPositionAll(Cam.UIOrthoX - 2, -Cam.UIOrthoY + 1)
            .SetFontScale(.5f, .5f)
            .AllowWordWrap(false)
            .SetImageSize(.5f, .5f)
            .SetImageSprite(Assets.SelectButton)
            .SetTextAlignment(TextAlignmentOptions.Right)
            .SetTMPRectPivot(new Vector2(1, .5f))
            .SetOutlineWidth(.15f)
            .OffsetImagePosition(Vector2.right)
            ;
    }
}