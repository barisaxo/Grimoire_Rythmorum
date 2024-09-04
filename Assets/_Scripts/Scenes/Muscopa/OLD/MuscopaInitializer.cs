using System.Collections.Generic;
using UnityEngine;
using MusicTheory;

namespace OLDMuscopa
{
    public static class Initializer
    {
        #region INITIALIZATION

        public static AudioSource[] SetUpMainASs(GameObject parent)
        {
            AudioSource[] ass = new AudioSource[3];
            for (int i = 0; i < ass.Length; i++)
            {
                GameObject child = new GameObject("MainAS" + i);
                child.transform.SetParent(parent.transform);
                ass[i] = child.AddComponent<AudioSource>();
                ass[i].loop = false;
                ass[i].playOnAwake = false;
                // TODO ass[i].volume = InGameData.VolumeSettings_Data.GetVolLVL(VolumeItem.PuzzleChords) * .01f;
            }
            return ass;
        }

        public static AudioSource[] SetUpDrumASs(GameObject parent)
        {
            AudioSource[] ass = new AudioSource[2];
            for (int i = 0; i < ass.Length; i++)
            {
                GameObject child = new GameObject("DrumAS" + i);
                child.transform.SetParent(parent.transform);
                ass[i] = child.AddComponent<AudioSource>();
                ass[i].loop = false;
                ass[i].playOnAwake = false;
                ass[i].volume = .7f;
                //TODO ass[i].volume = InGameData.VolumeSettings_Data.GetVolLVL(VolumeItem.PuzzleDrums) * .01f;
            }
            return ass;
        }

        public static Camera SetUpCam(GameObject parent)
        {
            var c = new GameObject(nameof(Camera)).AddComponent<Camera>();
            c.transform.SetParent(parent.transform, false);
            c.orthographic = false;
            c.clearFlags = CameraClearFlags.Skybox;
            c.transform.position = new Vector3(0, 0, -5);

            var l = c.gameObject.AddComponent<Light>();
            l.type = LightType.Directional;
            l.color = new Color(.9f, .8f, .65f);
            l.shadows = LightShadows.Soft;
            return c;
        }

        public static SpriteRenderer SetUpCardPile(string name, GameObject parent, Vector3 pos, Quaternion rot, Vector3 size, Sprite sprite)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent.transform);
            go.transform.SetPositionAndRotation(pos, rot);
            go.transform.localScale = size;
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;

            return sr;
        }

        public static GameObject SetupHandSlot(string name, GameObject parent, Vector3 pos)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent.transform);
            go.transform.position = pos;

            return go;
        }

        public static SpriteRenderer SetUpHL(string name, GameObject parent, Quaternion rot)
        {
            GameObject hl = UnityEngine.Object.Instantiate(Assets.BackgroundCard);
            hl.name = name;
            hl.transform.SetParent(parent.transform);
            hl.transform.SetPositionAndRotation(
                parent.transform.position + (Vector3.forward * .05f),
                rot);
            Debug.Log(name + hl.transform.position);
            //TODO hl.AddComponent<Pulsate>();
            return hl.GetComponentInChildren<SpriteRenderer>();
        }

        public static SpriteRenderer SetUpAnswerHL(string name, GameObject parent, Vector3 pos, Quaternion rot, Vector3 size)
        {
            GameObject hl = UnityEngine.Object.Instantiate(Assets.BackgroundCard);
            hl.name = name;
            hl.transform.SetParent(parent.transform);
            hl.transform.SetPositionAndRotation(pos, rot);
            hl.transform.localScale = size;
            //TODO hl.AddComponent<Pulsate>().SetScale(size);
            return hl.GetComponentInChildren<SpriteRenderer>();
        }

        public static List<MuscopaCard> SetUpDeck(GameObject parent)
        {
            List<MuscopaCard> cards = new();
            for (int i = 0; i < 3; i++)
            {
                cards.Add(new MuscopaCard(HarmonicFunction.Tonic, parent.transform));
                cards.Add(new MuscopaCard(HarmonicFunction.Dominant, parent.transform));
                cards.Add(new MuscopaCard(HarmonicFunction.Subdominant, parent.transform));
            }
            return cards;
        }

        internal static AudioPuzzleSettings GenerateNewSettings(List<HarmonicFunction> puzzle)
        //TODO not harmonic function, needs chords
        {
            List<AudioClip> mainACs = new() { Assets.BGMus1 };//TODOAssets.Gb90Rock;
            AudioClip[] main = new AudioClip[1];//puzzle.Count
            for (int i = 0; i < main.Length; i++) { main[i] = mainACs[i]; }///\(int)puzzle[i]

            List<AudioClip> drumACs = new() { Assets.BGMus2 };//TODOAssets.Drums;
            AudioClip[] drums = new AudioClip[1];//2
            for (int i = 0; i < drums.Length; i++) { drums[i] = drumACs[i]; }

            return new AudioPuzzleSettings(90, 4, 16, main, drums) { };
        }

        #endregion INITIALIZATION
    }

    public struct AudioPuzzleSettings
    {
        public int BPM;
        public int CountsPerClipMain;
        public int CountsPerClipDrums;
        public AudioClip[] MainClips;
        public AudioClip[] DrumClips;

        public AudioPuzzleSettings(
            int bpm,
            int countsPerClipMain,
            int countsPerClipDrums,
            AudioClip[] mainClips,
            AudioClip[] drumClips)
        {
            BPM = bpm;
            CountsPerClipDrums = countsPerClipDrums;
            CountsPerClipMain = countsPerClipMain;
            MainClips = mainClips;
            DrumClips = drumClips;
        }
    }

}
