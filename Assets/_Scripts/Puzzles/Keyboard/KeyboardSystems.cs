
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class KeyboardSystems
{
    public static KeyboardNoteName GetKeyboardNoteName(this MusicTheory.Keys.Key key) => key.Id switch
    {
        0 => KeyboardNoteName.C3,
        1 => KeyboardNoteName.Db3,
        2 => KeyboardNoteName.D3,
        3 => KeyboardNoteName.Eb3,
        4 => KeyboardNoteName.E3,
        5 => KeyboardNoteName.F3,
        6 => KeyboardNoteName.Gb3,
        7 => KeyboardNoteName.G3,
        8 => KeyboardNoteName.Ab3,
        9 => KeyboardNoteName.A3,
        10 => KeyboardNoteName.Bb3,
        11 => KeyboardNoteName.B3,
        _ => throw new System.ArgumentOutOfRangeException()
    };

    public static MusicTheory.Keys.Key NoteNameToKey(this KeyboardNoteName key) => key switch
    {
        KeyboardNoteName.C3 => new MusicTheory.Keys.C(),
        KeyboardNoteName.Db3 => new MusicTheory.Keys.Db(),
        KeyboardNoteName.D3 => new MusicTheory.Keys.D(),
        KeyboardNoteName.Eb3 => new MusicTheory.Keys.Eb(),
        KeyboardNoteName.E3 => new MusicTheory.Keys.E(),
        KeyboardNoteName.F3 => new MusicTheory.Keys.F(),
        KeyboardNoteName.Gb3 => new MusicTheory.Keys.Gb(),
        KeyboardNoteName.G3 => new MusicTheory.Keys.G(),
        KeyboardNoteName.Ab3 => new MusicTheory.Keys.Ab(),
        KeyboardNoteName.A3 => new MusicTheory.Keys.A(),
        KeyboardNoteName.Bb3 => new MusicTheory.Keys.Bb(),
        KeyboardNoteName.B3 => new MusicTheory.Keys.B(),

        KeyboardNoteName.C4 => new MusicTheory.Keys.C(),
        KeyboardNoteName.Db4 => new MusicTheory.Keys.Db(),
        KeyboardNoteName.D4 => new MusicTheory.Keys.D(),
        KeyboardNoteName.Eb4 => new MusicTheory.Keys.Eb(),
        KeyboardNoteName.E4 => new MusicTheory.Keys.E(),
        KeyboardNoteName.F4 => new MusicTheory.Keys.F(),
        KeyboardNoteName.Gb4 => new MusicTheory.Keys.Gb(),
        KeyboardNoteName.G4 => new MusicTheory.Keys.G(),
        KeyboardNoteName.Ab4 => new MusicTheory.Keys.Ab(),
        KeyboardNoteName.A4 => new MusicTheory.Keys.A(),
        KeyboardNoteName.Bb4 => new MusicTheory.Keys.Bb(),
        KeyboardNoteName.B4 => new MusicTheory.Keys.B(),

        KeyboardNoteName.C5 => new MusicTheory.Keys.C(),
        _ => throw new System.ArgumentOutOfRangeException()
    };

    public static KeyboardKey[] SortKeys(this Keyboard keyboard)
    {
        if (keyboard.SelectedKeys == null || keyboard.SelectedKeys.Length == 0) return keyboard.SelectedKeys;

        List<KeyboardKey> unsortedKeys = new();
        List<KeyboardKey> sortedKeys = new();

        for (int i = 0; i < keyboard.SelectedKeys.Length; i++)
            unsortedKeys.Add(keyboard.SelectedKeys[i]);

        while (unsortedKeys.Count > 0)
        {
            KeyboardKey lowKey = unsortedKeys[0];
            foreach (KeyboardKey key in unsortedKeys)
            {
                if (key == null || lowKey == null) continue;
                lowKey = key.KeyboardNoteName < lowKey.KeyboardNoteName ? key : lowKey;
            }
            sortedKeys.Add(lowKey);
            unsortedKeys.Remove(lowKey);
        }

        return sortedKeys.ToArray();
    }


    public static KeyboardKey IdentifyKey(this Keyboard keyboard, GameObject go)
    {
        foreach (KeyboardKey key in keyboard.Keys) if (key.Go == go) return key;
        throw new System.ArgumentOutOfRangeException(go.name + " is not a key");
    }

    public static void InteractWithKey(
        this Keyboard keyboard,
        KeyboardKey key,
        KeyboardInteractionType interactionType)
    {
        keyboard.KBAudio.Play(keyboard.AudioParser.GetAudioClipFromKey(key.KeyboardNoteName));

        if (interactionType == KeyboardInteractionType.User)
        {
            if (keyboard.LockBottomKey) LockedBottomKey(key);
            else UnlockedBottomKey(key);
        }

        keyboard.SelectedKeys = keyboard.SortKeys();
        return;

        void UnlockedBottomKey(KeyboardKey key)
        {
            // keyboard.CurrentKeys = new KeyboardKey[] { key };

            if (keyboard.SelectedKeys.Length == 1)
            {
                keyboard.SelectedKeys[0] = keyboard.SelectedKeys[0] == key ? null : key;
                keyboard.SetKeyColors(interactionType);
                return;
            }

            for (int i = 0; i < keyboard.SelectedKeys.Length; i++)
            {
                if (keyboard.SelectedKeys[i] == key)
                {
                    keyboard.SelectedKeys[i] = null;
                    keyboard.SetKeyColors(interactionType);
                    return;
                }
            }

            for (int i = 0; i < keyboard.SelectedKeys.Length; i++)
            {
                if (keyboard.SelectedKeys[i] == null)
                {
                    keyboard.SelectedKeys[i] = key;
                    keyboard.SetKeyColors(interactionType);
                    return;
                }
            }
        }

        void LockedBottomKey(KeyboardKey key)
        {
            // keyboard.CurrentKeys = new KeyboardKey[] { key };
            if (keyboard.SelectedKeys.Length <= 1)
            {
                return;
            }

            if (key.KeyboardNoteName <= keyboard.SelectedKeys[0].KeyboardNoteName)
            {
                return;
            }

            if (keyboard.SelectedKeys.Length == 2)
            {
                keyboard.SelectedKeys[1] = keyboard.SelectedKeys[1] == key ? null : key;
                keyboard.SetKeyColors(interactionType);
                return;
            }

            for (int i = 1; i < keyboard.SelectedKeys.Length; i++)
            {
                if (keyboard.SelectedKeys[i] == key)
                {
                    keyboard.SelectedKeys[i] = null;
                    keyboard.SetKeyColors(interactionType);
                    return;
                }
            }

            for (int i = 1; i < keyboard.SelectedKeys.Length; i++)
            {
                if (keyboard.SelectedKeys[i] == null)
                {
                    keyboard.SelectedKeys[i] = key;
                    keyboard.SetKeyColors(interactionType);
                    return;
                }
            }
        }
    }

    public static void PlayVertical(this Keyboard keyboard, KeyboardNoteName[] keys, Action callback, KeyboardInteractionType interactionType)
    {
        Timer().StartCoroutine();

        IEnumerator Timer()
        {
            keyboard.CurrentKeys = new KeyboardKey[] { };
            for (int i = 0; i < keys.Length; i++)
            {
                keyboard.CurrentKeys = keyboard.CurrentKeys.Add(keyboard.FindKey(keys[i]));
                keyboard.KBAudio.Play(keyboard.AudioParser.GetAudioClipFromKey(keys[i]));
            }
            keyboard.SetKeyColors(interactionType);
            yield return new WaitForSeconds(2);
            keyboard.CurrentKeys = null;
            keyboard.SetKeyColors(interactionType);
            yield return null;
            callback?.Invoke();
        }
    }

    public static KeyboardKey FindKey(this Keyboard keyboard, KeyboardNoteName note)
    {
        foreach (KeyboardKey kk in keyboard.Keys) if (kk.KeyboardNoteName == note) return kk;
        throw new System.ArgumentOutOfRangeException();
    }

    public static void PlayHorizontal(this Keyboard keyboard, KeyboardNoteName[] keys, Action callback, KeyboardInteractionType interactionType)
    {
        Timer().StartCoroutine();

        IEnumerator Timer()
        {
            keyboard.CurrentKeys = null;
            for (int i = 0; i < keys.Length; i++)
            {
                keyboard.CurrentKeys = new KeyboardKey[] { keyboard.FindKey(keys[i]) };
                keyboard.SetKeyColors(interactionType);
                keyboard.KBAudio.Play(keyboard.AudioParser.GetAudioClipFromKey(keys[i]));
                float timer = 0;
                while (timer < .35f)
                {
                    yield return null;
                    timer += Time.deltaTime;
                }
            }
            keyboard.CurrentKeys = null;
            keyboard.SetKeyColors(interactionType);
            yield return new WaitForSeconds(2);
            callback?.Invoke();
        }
    }

    public static void PlayHorAndVert(this Keyboard keyboard, KeyboardNoteName[] keys, Action callback, KeyboardInteractionType interactionType)
    {
        Delay().StartCoroutine();

        IEnumerator Delay()
        {
            keyboard.CurrentKeys = null;
            for (int i = 0; i < keys.Length; i++)
            {
                keyboard.CurrentKeys = new KeyboardKey[] { keyboard.FindKey(keys[i]) };
                keyboard.KBAudio.Play(keyboard.AudioParser.GetAudioClipFromKey(keys[i]));
                keyboard.SetKeyColors(interactionType);
                float timer = 0;
                while (timer < .35f)
                {
                    yield return null;
                    timer += Time.deltaTime;
                }
            }
            keyboard.CurrentKeys = null;
            keyboard.SetKeyColors(interactionType);
            yield return new WaitForSeconds(.15f);
            keyboard.PlayVertical(keys, callback, interactionType);
        }
    }

    public static void PlayNotes(
        this Keyboard keyboard,
        KeyboardKey[] keys,
        Action callback,
        PlaybackMode mode,
        KeyboardInteractionType keyboardInteractionType)
    {
        KeyboardNoteName[] keyboardNoteNames = new KeyboardNoteName[keys.Length];
        for (int i = 0; i < keys.Length; i++) keyboardNoteNames[i] = keys[i].KeyboardNoteName;
        keyboard.PlayNotes(keyboardNoteNames, callback, mode, keyboardInteractionType);
    }

    public static void PlayNotes(this Keyboard keyboard,
        KeyboardNoteName[] keys,
        Action callback,
        PlaybackMode mode,
        KeyboardInteractionType keyboardInteractionType)
    {
        switch (mode)
        {
            case PlaybackMode.Horizontal: keyboard.PlayHorizontal(keys, callback, keyboardInteractionType); break;
            case PlaybackMode.Vertical: keyboard.PlayVertical(keys, callback, keyboardInteractionType); break;
            case PlaybackMode.HorAndVert: keyboard.PlayHorAndVert(keys, callback, keyboardInteractionType); break;
        }
    }

    public static void SetKeyColors(this Keyboard keyboard, KeyboardInteractionType keyboardInteractionType)
    {
        foreach (KeyboardKey key in keyboard.Keys)
        {
            bool isSelected = false;
            foreach (KeyboardKey selectedKey in keyboard.SelectedKeys) if (key == selectedKey) isSelected = true;
            if (!isSelected) key.SR.color = key.KeyColor == KeyColor.White ? keyboard.KeyboardWhite : keyboard.KeyboardBlack;
            else key.SR.color = key.KeyColor == KeyColor.White ? keyboard.WhiteKeyHLRed : keyboard.BlackKeyHLRed;
        }

        if (keyboard.SelectedKeys[0] != null)
            keyboard.SelectedKeys[0].SR.color = keyboard.SelectedKeys[0].KeyColor == KeyColor.White ? keyboard.WhiteKeyHLBlue : keyboard.BlackKeyHLBlue;

        if (keyboardInteractionType == KeyboardInteractionType.Keyboard && keyboard.CurrentKeys != null)
            foreach (KeyboardKey kk in keyboard.CurrentKeys)
                kk.SR.color = kk.KeyColor == KeyColor.White ? keyboard.WhiteKeyHLYellow : keyboard.BlackKeyHLYellow;
    }

}
