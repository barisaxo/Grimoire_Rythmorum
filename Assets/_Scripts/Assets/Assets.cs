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
    public static Sprite StartButton => Resources.Load<Sprite>("Sprites/GamePad_Button/Start");
    public static Sprite SelectButton => Resources.Load<Sprite>("Sprites/GamePad_Button/Select");
    public static Sprite R3Button => Resources.Load<Sprite>("Sprites/GamePad_Button/R3");
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

    public static GameObject _catBoat => Resources.Load<GameObject>("Prefabs/Catboat2");
    public static GameObject CatBoat => GameObject.Instantiate(_catBoat);

    public static GameObject _cannonFire => Resources.Load<GameObject>("Prefabs/CannonFire");
    public static ParticleSystem CannonFire => GameObject.Instantiate(_cannonFire).GetComponent<ParticleSystem>();
    #endregion SHIPS

    #region FLAGS
    public static Sprite IonianFlag => Resources.Load<Sprite>("Sprites/Flags/IonianFlag");
    public static Sprite DorianFlag => Resources.Load<Sprite>("Sprites/Flags/DorianFlag");
    public static Sprite PhrygianFlag => Resources.Load<Sprite>("Sprites/Flags/PhrygianFlag");
    public static Sprite LydianFlag => Resources.Load<Sprite>("Sprites/Flags/LydianFlag");
    public static Sprite MixoLydianFlagFlag => Resources.Load<Sprite>("Sprites/Flags/MixoLydianFlag");
    public static Sprite AeolianFlag => Resources.Load<Sprite>("Sprites/Flags/AeolianFlag");
    public static Sprite LocrianFlag => Resources.Load<Sprite>("Sprites/Flags/LocrianFlag");

    public static Sprite ChromaticFlag => Resources.Load<Sprite>("Sprites/Flags/ChromaticFlag");

    public static Sprite PirateFlag => Resources.Load<Sprite>("Sprites/Flags/PirateFlag_1");
    #endregion FLAGS


    private static GameObject _rocks => Resources.Load<GameObject>("Prefabs/RocksParent");
    public static GameObject Rocks => GameObject.Instantiate(_rocks);
    public static GameObject Island => Resources.Load<GameObject>("Models/Island");

    public static AudioClip SailAmbience => Resources.Load<AudioClip>("Audio/SFX/SailAmbience");

    #endregion SEA


    #region MATERIALS

    public static Material Overlay_Mat => Resources.Load<Material>("Materials/Overlay_Mat");

    #endregion MATERIALS

    public static AudioClip GetScaleChordClip(MusicTheory.RegionalMode mode) => mode switch
    {
        MusicTheory.RegionalMode.Aeolian => Resources.Load<AudioClip>("Audio/ScaleChords/Aeolian_Scale_Chord_2"),
        MusicTheory.RegionalMode.Dorian => Resources.Load<AudioClip>("Audio/ScaleChords/Dorian_Scale_Chord_2"),
        MusicTheory.RegionalMode.Lydian => Resources.Load<AudioClip>("Audio/ScaleChords/Lydian_Scale_Chord_2"),
        MusicTheory.RegionalMode.Phrygian => Resources.Load<AudioClip>("Audio/ScaleChords/Phrygian_Scale_Chord_2"),
        MusicTheory.RegionalMode.MixoLydian => Resources.Load<AudioClip>("Audio/ScaleChords/Mixolydian_Scale_Chord_2"),
        MusicTheory.RegionalMode.Locrian => Resources.Load<AudioClip>("Audio/ScaleChords/Locrian_Scale_Chord_2"),
        _ => Resources.Load<AudioClip>("Audio/ScaleChords/Ionian_Scale_Chord_2"),
    };

    public static Color RandomColor => Random.Range(0, 12) switch
    {
        1 => R,
        2 => Rg,
        3 => Y,
        4 => Gr,
        5 => G,
        6 => Gb,
        7 => C,
        8 => Bg,
        9 => B,
        10 => Br,
        11 => M,
        _ => Rb,
    };

    public static Color R = new Color(.666f, .1666f, .1666f);
    public static Color Rg = new Color(.666f, .333f, .1666f);
    public static Color Y = new Color(.666f, .666f, .1666f);
    public static Color Gr = new Color(.333f, .666f, .1666f);
    public static Color G = new Color(.1666f, .1666f, .666f);
    public static Color Gb = new Color(.1666f, .666f, .333f);
    public static Color C = new Color(.1666f, .666f, .666f);
    public static Color Bg = new Color(.1666f, .333f, .666f);
    public static Color B = new Color(.1666f, .1666f, .666f);
    public static Color Br = new Color(.333f, .1666f, .666f);
    public static Color M = new Color(.666f, .1666f, .666f);
    public static Color Rb = new Color(.666f, .1666f, .333f);
}