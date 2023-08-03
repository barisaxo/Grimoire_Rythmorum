using System.Collections.Generic;
using UnityEngine;

public static class FacialExpressions
{
    public static Sprite[] EyesSprites(this Eyes eyes)
    {
        return eyes switch
        {
            Eyes.Up => new Sprite[1] { Assets.EyesUp },
            Eyes.Flat => new Sprite[1] { Assets.EyesFlat },
            Eyes.Down => new Sprite[1] { Assets.EyesDown },
            Eyes.UpFlat => new Sprite[2] { Assets.EyesUp, Assets.EyesFlat },
            Eyes.UpDown => new Sprite[2] { Assets.EyesUp, Assets.EyesDown },
            Eyes.DownFlat => new Sprite[2] { Assets.EyesDown, Assets.EyesFlat },
            Eyes.UpDownFlat => new Sprite[3] { Assets.EyesUp, Assets.EyesDown, Assets.EyesFlat },
            _ => new Sprite[1] { Assets.EyesUp }
        };
    }

    public static Sprite[] MouthSprites(this Mouth mouth)
    {
        return mouth switch
        {
            Mouth.Up => new Sprite[1] { Assets.MouthUp },
            Mouth.Flat => new Sprite[1] { Assets.MouthFlat },
            Mouth.Down => new Sprite[1] { Assets.MouthDown },
            Mouth.UpFlat => new Sprite[2] { Assets.MouthUp, Assets.MouthFlat },
            Mouth.UpDown => new Sprite[2] { Assets.MouthUp, Assets.MouthDown },
            Mouth.DownFlat => new Sprite[2] { Assets.MouthDown, Assets.MouthFlat },
            Mouth.UpDownFlat => new Sprite[3] { Assets.MouthUp, Assets.MouthDown, Assets.MouthFlat },
            _ => new Sprite[1] { Assets.MouthUp }
        };
    }

    public static Mouth TalkingMouth(this Mouth mouth)
    {
        return mouth switch
        {
            Mouth.Up => Mouth.UpFlat,
            Mouth.Down => Mouth.DownFlat,
            Mouth.Flat => Mouth.UpDown,
            Mouth.UpDown => Mouth.UpDownFlat,
            _ => Mouth.UpFlat
        };
    }


    public static ALsFace
        Expression(this FacialExpression alsFace)
    {
        return alsFace switch
        {
            FacialExpression.Bliss => new ALsFace(Eyes.Up, Mouth.Up),
            FacialExpression.Worry => new ALsFace(Eyes.Up, Mouth.Flat),
            FacialExpression.Sad => new ALsFace(Eyes.Up, Mouth.Down),

            FacialExpression.Cool => new ALsFace(Eyes.Flat, Mouth.Up),
            FacialExpression.Stoic => new ALsFace(Eyes.Flat, Mouth.Flat),
            FacialExpression.Disappointed => new ALsFace(Eyes.Flat, Mouth.Down),

            FacialExpression.Mischievous => new ALsFace(Eyes.Down, Mouth.Up),
            FacialExpression.Unwelcome => new ALsFace(Eyes.Down, Mouth.Flat),
            FacialExpression.Anger => new ALsFace(Eyes.Down, Mouth.Down),
            
            _ => FacialExpression.Stoic.Expression()
        };
    }

    public static Sprite[] Sprites(this FacialExpression expression)
    {
        return expression.Expression().Sprites();
    }

    public static Sprite[] Sprites(this ALsFace alsFace)
    {
        List<Sprite> sprites = new();
        sprites.Add(Assets.AlsHead);
        foreach (var s in alsFace.Eyes.EyesSprites()) sprites.Add(s);
        foreach (var s in alsFace.ClosedMouth.MouthSprites()) sprites.Add(s);
        return sprites.ToArray();
    }
}

public enum Eyes
{
    Up,
    Down,
    Flat,
    UpDown,
    UpFlat,
    DownFlat,
    UpDownFlat
}

public enum Mouth
{
    Up,
    Down,
    Flat,
    UpDown,
    UpFlat,
    DownFlat,
    UpDownFlat
}

public enum FacialExpression
{
    Bliss,
    Mischievous,
    Cool,
    Stoic,
    Worry,
    Disappointed,
    Sad,
    Unwelcome,
    Anger
}

public struct ALsFace
{
    public readonly Eyes Eyes;
    public readonly Mouth ClosedMouth;
    public readonly Mouth OpenMouth;

    public ALsFace(Eyes eyes, Mouth mouth)
    {
        Eyes = eyes;
        ClosedMouth = mouth;
        OpenMouth = ClosedMouth.TalkingMouth();
    }
}