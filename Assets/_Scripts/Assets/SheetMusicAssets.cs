using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SheetMusicAssets
{
    #region SHEET MUSIC SPRITES
    public static Sprite Staff => Resources.Load<Sprite>("SheetMusic/Staff");
    public static Sprite StaffDoubleLeft => Resources.Load<Sprite>("SheetMusic/Staff");
    public static Sprite StaffDoubleRight => Resources.Load<Sprite>("SheetMusic/Staff");

    public static Sprite Dot => Resources.Load<Sprite>("SheetMusic/Dot");
    public static Sprite Tie => Resources.Load<Sprite>("SheetMusic/tie4");

    public static Sprite Triplet3 => Resources.Load<Sprite>("SheetMusic/Triplet3");
    public static Sprite TripletBracket => Resources.Load<Sprite>("SheetMusic/TripletBracket");

    public static Sprite WhiteNote => Resources.Load<Sprite>("SheetMusic/WhiteNote");
    public static Sprite Stem => Resources.Load<Sprite>("SheetMusic/Stem");

    public static Sprite BlackNote => Resources.Load<Sprite>("SheetMusic/BlackNote");

    public static Sprite EighthFlag => Resources.Load<Sprite>("SheetMusic/EighthFlag");
    public static Sprite SixteenthFlag => Resources.Load<Sprite>("SheetMusic/SixteenthFlag");

    public static Sprite EighthBoth => Resources.Load<Sprite>("SheetMusic/8thBoth");
    public static Sprite SixteenthBoth => Resources.Load<Sprite>("SheetMusic/SixteenthNoteBoth");
    public static Sprite EighthForward => Resources.Load<Sprite>("SheetMusic/8thNoteForward");
    public static Sprite EighthReverse => Resources.Load<Sprite>("SheetMusic/8thNoteReversed");
    public static Sprite SixteenthForward => Resources.Load<Sprite>("SheetMusic/SixteenthForward");
    public static Sprite SixteenthReverse => Resources.Load<Sprite>("SheetMusic/SixteenthReverse");
    public static Sprite SixteenthReverseEighthForward => Resources.Load<Sprite>("SheetMusic/SixteenthReverseEighthForward");
    public static Sprite SixteenthForwardEighthReverse => Resources.Load<Sprite>("SheetMusic/SixteenthForwardEighthReverse");

    public static Sprite EighthBeam => Resources.Load<Sprite>("SheetMusic/EighthBeam");
    public static Sprite EighthBeamReverse => Resources.Load<Sprite>("SheetMusic/EighthBeamReverse");
    public static Sprite SixteenthBeam => Resources.Load<Sprite>("SheetMusic/SixteenthBeam");
    public static Sprite SixteenthBeamReverse => Resources.Load<Sprite>("SheetMusic/SixteenthBeamReverse");

    public static Sprite WholeRest => Resources.Load<Sprite>("SheetMusic/WholeRest");
    public static Sprite HalfRest => Resources.Load<Sprite>("SheetMusic/HalfRest");
    public static Sprite QuarterRest => Resources.Load<Sprite>("SheetMusic/QuarterRest");
    public static Sprite EighthRest => Resources.Load<Sprite>("SheetMusic/EighthRest");
    public static Sprite SixteenthRest => Resources.Load<Sprite>("SheetMusic/SixteenthRest");
    #endregion SHEET MUSIC SPRITES
}
