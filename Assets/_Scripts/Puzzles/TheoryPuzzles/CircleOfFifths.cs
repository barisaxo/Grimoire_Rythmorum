using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleOfFifths
{
    public CircleOfFifths()
    {
        _ = I;
        _ = bII;
        _ = II;
        _ = bIII;
        _ = III;
        _ = IV;
        _ = bV;
        _ = bVI;
        _ = V;
        _ = VI;
        _ = bVII;
        _ = VII;
    }

    public void SelfDestruct()
    {
        Parent.SelfDestruct();
    }

    private Card _parent;
    public Card Parent => _parent ??= new Card(nameof(CircleOfFifths), null);

    public float RotationalOffset = -3;
    public float RotateSpeed = .15f;

    private Card _i;
    private Card _bii;
    private Card _ii;
    private Card _biii;
    private Card _iii;
    private Card _iv;
    private Card _bv;
    private Card _v;
    private Card _bvi;
    private Card _vi;
    private Card _bvii;
    private Card _vii;

    public Card I => _i ??= Parent.CreateChild(nameof(I), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("I")
        .SetTMPPosition(StarPos(0))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card bII => _bii ??= Parent.CreateChild(nameof(bII), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("bII")
        .SetTMPPosition(StarPos(1))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
       ;


    public Card II => _ii ??= Parent.CreateChild(nameof(II), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("II")
        .SetTMPPosition(StarPos(2))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
       ;

    public Card bIII => _biii ??= Parent.CreateChild(nameof(bIII), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("bIII")
        .SetTMPPosition(StarPos(3))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;
    public Card III => _iii ??= Parent.CreateChild(nameof(III), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("III")
        .SetTMPPosition(StarPos(4))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card IV => _iv ??= Parent.CreateChild(nameof(IV), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("IV")
        .SetTMPPosition(StarPos(5))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card bV => _bv ??= Parent.CreateChild(nameof(bV), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("bV")
        .SetTMPPosition(StarPos(6))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card V => _v ??= Parent.CreateChild(nameof(V), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("V")
        .SetTMPPosition(StarPos(7))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card bVI => _bvi ??= Parent.CreateChild(nameof(bVI), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("bVI")
        .SetTMPPosition(StarPos(8))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card VI => _vi ??= Parent.CreateChild(nameof(VI), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("VI")
        .SetTMPPosition(StarPos(9))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card bVII => _bvii ??= Parent.CreateChild(nameof(bVII), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("bVII")
        .SetTMPPosition(StarPos(10))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card VII => _vii ??= Parent.CreateChild(nameof(VII), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("VII")
        .SetTMPPosition(StarPos(11))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Vector2 StarPos(float i)
    {
        int points = 12;
        float radius = 1.5f;
        float centerX = -Cam.UIOrthoX + 2;
        float centerY = Cam.UIOrthoY - 2;

        float angle = -2 * Mathf.PI * (i + RotationalOffset) / points;
        float x = centerX + radius * Mathf.Cos(angle);
        float y = centerY + radius * Mathf.Sin(angle);

        return new Vector2(x, y);
    }

    public void RotateStars(float delta)
    {
        RotationalOffset += delta * RotateSpeed;
        I.SetTMPPosition(StarPos(0));
        bII.SetTMPPosition(StarPos(1));
        II.SetTMPPosition(StarPos(2));
        bIII.SetTMPPosition(StarPos(3));
        III.SetTMPPosition(StarPos(4));
        IV.SetTMPPosition(StarPos(5));
        bV.SetTMPPosition(StarPos(6));
        V.SetTMPPosition(StarPos(7));
        bVI.SetTMPPosition(StarPos(8));
        VI.SetTMPPosition(StarPos(9));
        bVII.SetTMPPosition(StarPos(10));
        VII.SetTMPPosition(StarPos(11));
    }
}