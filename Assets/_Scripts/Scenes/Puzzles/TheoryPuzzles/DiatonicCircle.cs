using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiatonicCircle
{

    public DiatonicCircle()
    {
        _ = I;
        _ = II;
        _ = III;
        _ = IV;
        _ = V;
        _ = VI;
        _ = VII;
    }

    public void SelfDestruct()
    {
        Parent.SelfDestruct();
    }

    private Card _parent;
    public Card Parent => _parent ??= new Card(nameof(DiatonicCircle), null);

    public float RotationalOffset = -1.75f;
    public float RotateSpeed = .2f;

    private Card _i;
    private Card _ii;
    private Card _iii;
    private Card _iv;
    private Card _v;
    private Card _vi;
    private Card _vii;

    public Card I => _i ??= Parent.CreateChild(nameof(I), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("I")
        .SetTMPPosition(StarPos(0))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card II => _ii ??= Parent.CreateChild(nameof(II), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("II-")
        .SetTMPPosition(StarPos(1))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
       ;

    public Card III => _iii ??= Parent.CreateChild(nameof(III), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("III-")
        .SetTMPPosition(StarPos(2))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card IV => _iv ??= Parent.CreateChild(nameof(IV), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("IV")
        .SetTMPPosition(StarPos(3))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card V => _v ??= Parent.CreateChild(nameof(V), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("V")
        .SetTMPPosition(StarPos(4))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card VI => _vi ??= Parent.CreateChild(nameof(VI), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("VI-")
        .SetTMPPosition(StarPos(5))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;

    public Card VII => _vii ??= Parent.CreateChild(nameof(VII), Parent.Canvas.transform, Parent.Canvas)
        .SetTextString("VIIø")
        .SetTMPPosition(StarPos(6))
        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
        .TMPClickable()
        ;


    public Vector2 StarPos(float i)
    {

        int numPoints = 7;
        float radius = 1.5f; // Radius of the circle
        float centerX = -Cam.UIOrthoX + 2; // X coordinate of the circle's center
        float centerY = -Cam.UIOrthoY + 2; // Y coordinate of the circle's center

        float angle = -2 * Mathf.PI * (i + RotationalOffset) / numPoints;
        float x = centerX + radius * Mathf.Cos(angle);
        float y = centerY + radius * Mathf.Sin(angle);


        return new Vector2(x, y);
    }


    public void RotateCircle(float delta)
    {

        RotationalOffset += delta * RotateSpeed;
        I.SetTMPPosition(StarPos(0));
        II.SetTMPPosition(StarPos(1));
        III.SetTMPPosition(StarPos(2));
        IV.SetTMPPosition(StarPos(3));
        V.SetTMPPosition(StarPos(4));
        VI.SetTMPPosition(StarPos(5));
        VII.SetTMPPosition(StarPos(6));
    }
}