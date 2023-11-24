using UnityEngine;

public static class Assets
{
    #region AUDIO

    #region SFX
    public static AudioClip TypingClicks => Resources.Load<AudioClip>("Audio/SFX/Typing Clicks");
    public static AudioClip AlertHalfDim => Resources.Load<AudioClip>("Audio/SFX/AlertHalfDim");
    #endregion SFX


    #region BGMusic
    public static AudioClip BGMus1 => Resources.Load<AudioClip>("Audio/BGMusic/Boss a loop");
    public static AudioClip BGMus2 => Resources.Load<AudioClip>("Audio/BGMusic/machete");
    public static AudioClip BGMus3 => Resources.Load<AudioClip>("Audio/BGMusic/Roulette loop");
    public static AudioClip BGMus4 => Resources.Load<AudioClip>("Audio/BGMusic/Finger Stretch");
    #endregion BGMusic

    #endregion AUDIO


    #region MISC

    public static Material Video_Mat => Resources.Load<Material>("Materials/Video_Mat");
    public static Sprite White => Resources.Load<Sprite>("Sprites/Misc/White");
    public static Material Stars => Resources.Load<Material>("Skyboxes/Stars");

    public static Sprite CircleKeyboard => Resources.Load<Sprite>("Sprites/Misc/Circle_Keyboard");

    #endregion MISC


    #region GAMEPAD 

    public static Sprite EastButton => Resources.Load<Sprite>("Sprites/GamePad_Button/East");
    public static Sprite NorthButton => Resources.Load<Sprite>("Sprites/GamePad_Button/North");
    public static Sprite SouthButton => Resources.Load<Sprite>("Sprites/GamePad_Button/South");
    public static Sprite WestButton => Resources.Load<Sprite>("Sprites/GamePad_Button/West");
    public static Sprite GamePad => Resources.Load<Sprite>("Sprites/GamePad_Button/White_Gamepad");

    #endregion GAMEPAD 


    #region  CHARACTERS

    #region AL

    private static Sprite _alsHead;
    private static Sprite _eyesFlat;

    public static Sprite AlsHead => _alsHead =
        _alsHead != null ? _alsHead : Resources.Load<Sprite>("Sprites/AL/AlsHead_" + Random.Range(1, 6));

    public static Sprite EyesUp => Resources.Load<Sprite>("Sprites/AL/EyesUp");
    public static Sprite EyesDown => Resources.Load<Sprite>("Sprites/AL/EyesDown");

    public static Sprite EyesFlat => _eyesFlat = _eyesFlat != null ? _eyesFlat :
        Resources.Load<Sprite>("Sprites/AL/EyesFlat_" + Random.Range(1, 3));

    public static Sprite MouthUp => Resources.Load<Sprite>("Sprites/Characters/AL/MouthUp");
    public static Sprite MouthDown => Resources.Load<Sprite>("Sprites/Characters/AL/MouthDown");
    public static Sprite MouthFlat => Resources.Load<Sprite>("Sprites/Characters/AL/MouthFlat");

    #endregion AL

    #region PINO

    public static Sprite Pino => Resources.Load<Sprite>("Sprites/Characters/Pino/Pino");

    #endregion PINO

    #endregion  CHARACTERS


    #region SEA

    #region SHIPS
    public static GameObject _schooner => Resources.Load<GameObject>("Prefabs/BigBoat2");
    public static GameObject Schooner => GameObject.Instantiate(_schooner);
    public static GameObject CatBoat => Resources.Load<GameObject>("Prefabs/Catboat2");
    #endregion SHIPS


    private static GameObject _rocks => Resources.Load<GameObject>("Prefabs/RocksParent");
    public static GameObject Rocks => GameObject.Instantiate(_rocks);
    public static GameObject Island => Resources.Load<GameObject>("Models/Island");

    #endregion SEA


    #region MATERIALS

    public static Material Overlay_Mat => Resources.Load<Material>("Materials/Overlay_Mat");

    #endregion MATERIALS
}