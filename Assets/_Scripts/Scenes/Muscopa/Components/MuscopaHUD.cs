using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Muscopa
{
    public class MuscopaHUD : IHud
    {
        public IHud Initialize()
        {
            _ = KeyOf;
            _ = AllChords;
            // _ = CurrentLevel;
            _ = Answer1ChordName;
            _ = Answer2ChordName;
            _ = Answer3ChordName;
            _ = Answer4ChordName;
            return this;
        }

        private Card _parent;
        public Card Parent => _parent ??= new(nameof(Parent), null);

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

        // private Card _currentLevel;
        // internal Card CurrentLevel => _currentLevel ??= Parent.CreateChild(nameof(CurrentLevel), Parent.Canvas)
        //     .SetTextString("Difficulty: I, II-, III-, IV, V, VI-, VIIø")
        //     .SetTMPPosition(2, 1.5f - Cam.UIOrthoY)
        //     .SetFontScale(.5f)
        //     .SetTextAlignment(TextAlignmentOptions.Center)
        //     .AllowWordWrap(false);

        private Card _answer1ChordName;
        internal Card Answer1ChordName => _answer1ChordName ??= Parent.CreateChild(nameof(Answer1ChordName), Parent.Canvas)
            .SetTextString("Gb")
            .SetTMPPosition(-4.25f, 1f)
            .SetFontScale(1f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _answer2ChordName;
        internal Card Answer2ChordName => _answer2ChordName ??= Parent.CreateChild(nameof(Answer2ChordName), Parent.Canvas)
            .SetTextString("?")
            .SetTMPPosition(-1.5f, 1f)
            .SetFontScale(1f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _answer3ChordName;
        internal Card Answer3ChordName => _answer3ChordName ??= Parent.CreateChild(nameof(Answer3ChordName), Parent.Canvas)
            .SetTextString("?")
            .SetTMPPosition(1.5f, 1f)
            .SetFontScale(1f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _answer4ChordName;
        internal Card Answer4ChordName => _answer4ChordName ??= Parent.CreateChild(nameof(Answer4ChordName), Parent.Canvas)
            .SetTextString("?")
            .SetTMPPosition(4.25f, 1f)
            .SetFontScale(1f)
            .SetTextAlignment(TextAlignmentOptions.Center);

        private Card _continueButton;
        public Card ContinueButton => _continueButton ??= Parent.CreateChild(nameof(ContinueButton), Parent.Canvas)
            .SetTextString("Continue")
            .SetImageSprite(Assets.SouthButton)
            .SetPositionAll(Cam.UIOrthoX - 2, -Cam.UIOrthoY + 2)
            .SetFontScale(.5f, .5f)
            .AllowWordWrap(false)
            .SetImageSize(.5f, .5f)
            .SetTextAlignment(TextAlignmentOptions.Right)
            .SetTMPRectPivot(new Vector2(1, .5f))
            .SetOutlineWidth(.15f)
            .OffsetImagePosition(Vector2.right)
            ;
    }
}
