using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Muscopa
{
    public class DrumsAudio : AudioSystem
    {
        public DrumsAudio() : base(numOfAudioSources: 2, name: nameof(DrumsAudio))
        {
        }

        public override void Stop()
        {
            base.Stop();
        }

        public void Mute()
        {
            FadeOutAndPause().StartCoroutine();

            IEnumerator FadeOutAndPause()
            {
                yield return null;
                if (CurrentVolumeLevel > .15f)
                {
                    CurrentVolumeLevel -= Time.deltaTime * .75f;
                    FadeOutAndPause();
                }
                else
                {
                    CurrentVolumeLevel = 0;
                }
            }
        }

        public void UnMute()
        {
            FadeInAndResume().StartCoroutine();
            IEnumerator FadeInAndResume()
            {
                yield return null;

                if (CurrentVolumeLevel < VolumeLevelSetting)
                {
                    CurrentVolumeLevel += Time.deltaTime * 1.75f;
                    FadeInAndResume();
                }
                else
                {
                    CurrentVolumeLevel = VolumeLevelSetting;
                }
            }
        }
    }
}