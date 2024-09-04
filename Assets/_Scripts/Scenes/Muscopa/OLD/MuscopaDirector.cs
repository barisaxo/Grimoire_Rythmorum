using System.Collections.Generic;
using UnityEngine;

namespace OLDMuscopa
{
    public static class MuscopaDirector
    {
        static MuscopaScene Muscopa => MuscopaScene.Io;

        internal static void Enable()
        {
            //TODO VolumeSettings_Data.VolumeChangedEvent += UpdateVolumeLevel;
            MonoHelper.OnUpdate += OnUpdate;

            Muscopa.NextEventTimeMain = AudioSettings.dspTime + .25f;
            Muscopa.NextEventTimeDrums = AudioSettings.dspTime + .25f;
        }

        internal static void Disable()
        {
            //TODO VolumeSettings_Data.VolumeChangedEvent -= UpdateVolumeLevel;
            MonoHelper.OnUpdate -= OnUpdate;

            foreach (AudioSource a in Muscopa.MainASs) { a.Stop(); }
            foreach (AudioSource a in Muscopa.DrumASs) { a.Stop(); }
        }

        private static void UpdateVolumeLevel(Datum.IVolume volItem, float scaledLevel)
        {
            switch (volItem)
            {
                case Datum.Chords:
                    foreach (AudioSource a in Muscopa.MainASs) { a.volume = scaledLevel; }
                    break;

                case Datum.Drums:
                    foreach (AudioSource a in Muscopa.DrumASs) { a.volume = scaledLevel; }
                    break;


                //TODO case Datum.Bass:
                //     foreach (AudioSource a in Muscopa.BassASs) { a.volume = scaledLevel; }
                //     break;

                default: break;
            }
        }

        private static void OnUpdate()
        {
            double time = AudioSettings.dspTime;

            if (time + 1.0f > Muscopa.NextEventTimeMain)
            {
                Muscopa.MainASs[Muscopa.CuedMainAS].clip = Muscopa.AudioPuzzleSettings.MainClips[Muscopa.CuedMainClip];
                Muscopa.MainASs[Muscopa.CuedMainAS].PlayScheduled(Muscopa.NextEventTimeMain);

                Muscopa.NextEventTimeMain += 60.0f / Muscopa.AudioPuzzleSettings.BPM * Muscopa.AudioPuzzleSettings.CountsPerClipMain;

                if (++Muscopa.CuedMainAS == Muscopa.MainASs.Length) { Muscopa.CuedMainAS = 0; }
                if (++Muscopa.CuedMainClip == Muscopa.AudioPuzzleSettings.MainClips.Length) { Muscopa.CuedMainClip = 0; }
            }

            if (time + 1.0f > Muscopa.NextEventTimeDrums)
            {
                Muscopa.DrumASs[Muscopa.CuedDrumAS].clip = Muscopa.AudioPuzzleSettings.DrumClips[Muscopa.CuedDrumClip];
                Muscopa.DrumASs[Muscopa.CuedDrumAS].PlayScheduled(Muscopa.NextEventTimeDrums);

                Muscopa.NextEventTimeDrums += 60.0f / Muscopa.AudioPuzzleSettings.BPM * Muscopa.AudioPuzzleSettings.CountsPerClipDrums;

                if (++Muscopa.CuedDrumAS == Muscopa.DrumASs.Length) { Muscopa.CuedDrumAS = 0; }
                if (++Muscopa.CuedDrumClip == Muscopa.AudioPuzzleSettings.DrumClips.Length) { Muscopa.CuedDrumClip = 0; }
            }
        }



    }



}


