using UnityEngine;

public static class Assets
{
    private static Sprite _alsHead;
    private static Sprite _eyesFlat;
    public static AudioClip TypingClicks => Resources.Load<AudioClip>("Audio/SFX/Typing Clicks");
    public static AudioClip BGMus1 => Resources.Load<AudioClip>("Audio/BGMusic/Boss a loop");
    public static AudioClip BGMus2 => Resources.Load<AudioClip>("Audio/BGMusic/machete");
    public static AudioClip BGMus3 => Resources.Load<AudioClip>("Audio/BGMusic/Roulette loop");
    public static AudioClip BGMus4 => Resources.Load<AudioClip>("Audio/BGMusic/Finger Stretch");

    public static Material Video_Mat => Resources.Load<Material>("Materials/Video_Mat");

    public static Sprite White => Resources.Load<Sprite>("Sprites/Misc/White");
    public static Material Stars => Resources.Load<Material>("Skyboxes/Stars");

    public static Sprite EastButton => Resources.Load<Sprite>("Sprites/GamePad_Button/East");
    public static Sprite NorthButton => Resources.Load<Sprite>("Sprites/GamePad_Button/North");
    public static Sprite SouthButton => Resources.Load<Sprite>("Sprites/GamePad_Button/South");
    public static Sprite WestButton => Resources.Load<Sprite>("Sprites/GamePad_Button/West");
    public static Sprite GamePad => Resources.Load<Sprite>("Sprites/GamePad_Button/White_Gamepad");

    public static GameObject BigBoat => Resources.Load<GameObject>("Prefabs/BigBoat");
    public static GameObject CatBoat => Resources.Load<GameObject>("Prefabs/CatBoat");

    public static Sprite AlsHead => _alsHead =
        _alsHead != null ? _alsHead : Resources.Load<Sprite>("Sprites/AL/AlsHead_" + Random.Range(1, 6));

    public static Sprite EyesUp => Resources.Load<Sprite>("Sprites/AL/EyesUp");
    public static Sprite EyesDown => Resources.Load<Sprite>("Sprites/AL/EyesDown");

    public static Sprite EyesFlat => _eyesFlat = _eyesFlat != null
        ? _eyesFlat
        : Resources.Load<Sprite>("Sprites/AL/EyesFlat_" + Random.Range(1, 3));

    public static Sprite MouthUp => Resources.Load<Sprite>("Sprites/AL/MouthUp");
    public static Sprite MouthDown => Resources.Load<Sprite>("Sprites/AL/MouthDown");
    public static Sprite MouthFlat => Resources.Load<Sprite>("Sprites/AL/MouthFlat");



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
    //public static Sprite Eighth_Beam => Resources.Load<Sprite>("SheetMusic/Eighth_Beam");
    //public static Sprite Sixteenth_Beam => Resources.Load<Sprite>("SheetMusic/Sixteenth_Beam");
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