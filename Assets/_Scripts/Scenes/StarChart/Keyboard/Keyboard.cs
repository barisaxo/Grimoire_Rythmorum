using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Audio;

public class Keyboard
{
    public Keyboard(int numOfNotes, KeyboardNoteName bottomNote)
    {
        MakeTwoOctaveKeyboard();
        SelectedKeys = new KeyboardKey[numOfNotes];
        SelectedKeys[0] = Keys[(int)bottomNote];
        LockBottomKey = true;
        this.SetKeyColors(KeyboardInteractionType.Puzzle);
    }

    public Keyboard(int numOfKeys)
    {
        MakeTwoOctaveKeyboard();
        SelectedKeys = new KeyboardKey[numOfKeys];
        this.SetKeyColors(KeyboardInteractionType.Puzzle);
    }

    public void SelfDestruct()
    {
        UnityEngine.Object.Destroy(Parent);
    }

    private GameObject _parent;
    public GameObject Parent => _parent ? _parent : _parent = new(nameof(Keyboard)) { layer = 5 };

    public AudioParserB AudioParser => AudioManager.Io.AudioParser;
    public KeyboardAudioSystem KBAudio => AudioManager.Io.KBAudio;

    public List<SpriteRenderer> WhiteKeySRs = new();
    public List<SpriteRenderer> BlackKeySRs = new();
    public List<SpriteRenderer> twoOctave = new();
    public List<KeyboardKey> Keys = new();

    public Color KeyboardBlack = new(0, 0, 0, .6f);
    public Color KeyboardWhite = new(.6f, .6f, .6f, .6f);
    public Color WhiteKeyHLYellow = new(.75f, .75f, .15f, .55f);
    public Color BlackKeyHLYellow = new(.55f, .55f, .00f, .6f);
    public Color WhiteKeyHLBlue = new(.25f, .25f, .65f, .55f);
    public Color BlackKeyHLBlue = new(.15f, .15f, .55f, .6f);
    public Color WhiteKeyHLRed = new(.65f, .15f, .1f, .55f);
    public Color BlackKeyHLRed = new(.50f, .00f, .00f, .6f);

    public bool LockBottomKey;
    public KeyboardKey[] SelectedKeys;
    public KeyboardKey[] CurrentKeys;

    #region TwoOctaveKeyboard
    void MakeTwoOctaveKeyboard()
    {
        for (KeyboardNoteName key = 0; key < (KeyboardNoteName)Enum.GetNames(typeof(KeyboardNoteName)).Length; key++)
        {
            Keys.Add(new KeyboardKey(key, GetKeyColor(key), KeyPos(key) * Cam.Io.UICamera.aspect, Parent.transform));
        }

        Vector3 KeyPos(KeyboardNoteName key) => key switch
        {
            KeyboardNoteName.C3 => new Vector3(-3.5f, 0, 1F),
            KeyboardNoteName.Db3 => new Vector3(-3.3f, 0, 0),
            KeyboardNoteName.D3 => new Vector3(-3f, 0, 1F),
            KeyboardNoteName.Eb3 => new Vector3(-2.7f, 0, 0),
            KeyboardNoteName.E3 => new Vector3(-2.5f, 0, 1F),
            KeyboardNoteName.F3 => new Vector3(-2f, 0, 1F),
            KeyboardNoteName.Gb3 => new Vector3(-1.8f, 0, 0),
            KeyboardNoteName.G3 => new Vector3(-1.5f, 0, 1F),
            KeyboardNoteName.Ab3 => new Vector3(-1.25f, 0, 0),
            KeyboardNoteName.A3 => new Vector3(-1f, 0, 1F),
            KeyboardNoteName.Bb3 => new Vector3(-.7f, 0, 0),
            KeyboardNoteName.B3 => new Vector3(-.5f, 0, 1F),

            KeyboardNoteName.C4 => new Vector3(0, 0, 1F),
            KeyboardNoteName.Db4 => new Vector3(.2f, 0, 0),
            KeyboardNoteName.D4 => new Vector3(.5f, 0, 1F),
            KeyboardNoteName.Eb4 => new Vector3(.8f, 0, 0),
            KeyboardNoteName.E4 => new Vector3(1f, 0, 1F),
            KeyboardNoteName.F4 => new Vector3(1.5f, 0, 1F),
            KeyboardNoteName.Gb4 => new Vector3(1.7f, 0, 0),
            KeyboardNoteName.G4 => new Vector3(2f, 0, 1F),
            KeyboardNoteName.Ab4 => new Vector3(2.25f, 0, 0),
            KeyboardNoteName.A4 => new Vector3(2.5f, 0, 1F),
            KeyboardNoteName.Bb4 => new Vector3(2.8f, 0, 0),
            KeyboardNoteName.B4 => new Vector3(3f, 0, 1F),

            KeyboardNoteName.C5 => new Vector3(3.5f, 0, 1F),
            _ => throw new ArgumentOutOfRangeException(key.ToString())
        };

        KeyColor GetKeyColor(KeyboardNoteName key) => key switch
        {
            KeyboardNoteName.Db3 or KeyboardNoteName.Eb3 or KeyboardNoteName.Gb3 or KeyboardNoteName.Ab3 or KeyboardNoteName.Bb3 or
            KeyboardNoteName.Db4 or KeyboardNoteName.Eb4 or KeyboardNoteName.Gb4 or KeyboardNoteName.Ab4 or KeyboardNoteName.Bb4 =>
                KeyColor.Black,
            _ => KeyColor.White,
        };
    }
    #endregion

}

public enum KeyboardNoteName
{
    C3, Db3, D3, Eb3, E3, F3, Gb3, G3, Ab3, A3, Bb3, B3,
    C4, Db4, D4, Eb4, E4, F4, Gb4, G4, Ab4, A4, Bb4, B4, C5,
}

public enum KeyboardInteractionType { User, Keyboard, Puzzle }
public enum PlaybackMode { Horizontal, Vertical, HorAndVert }