using UnityEngine;
using Data.Two;
namespace Audio
{
    public class Batterie_AudioSystem : AudioSystem
    {
        public Batterie_AudioSystem(VolumeData volumeData) : base(3, nameof(Batterie_AudioSystem))
        {
            VolumeData = volumeData;

            AudioSources[0].volume = VolumeData.GetLevel(new Click()) * .01f;
            AudioSources[0].clip = BatterieAssets.RimShot;
            AudioSources[0].clip.LoadAudioData();

            AudioSources[1].volume = VolumeData.GetLevel(new BatterieVolume()) * .01f;
            AudioSources[1].clip = BatterieAssets.SnareRoll;
            AudioSources[1].clip.LoadAudioData();

            AudioSources[2].volume = VolumeData.GetLevel(new BatterieVolume()) * .01f;
            AudioSources[2].clip = BatterieAssets.MissStick;
            AudioSources[2].clip.LoadAudioData();
        }

        readonly VolumeData VolumeData;
        private double click;

        public override void ResetCues()
        {
            click = AudioSettings.dspTime + .5D;
            base.ResetCues();
        }

        public void PlayClick()
        {
            AudioSources[0].Stop();
            AudioSources[0].Play();
        }

        public void PlaySnareRoll()
        {
            AudioSources[1].Stop();
            AudioSources[1].Play();
        }

        public void RestSnareRoll()
        {
            AudioSources[1].Stop();
        }

        public void MissStick()
        {
            AudioSources[1].volume = 0;
            //AudioSources[1].Stop();
            AudioSources[2].Play();
        }

        public void Miss()
        {
            AudioSources[1].volume = 0;
        }

        public void Hit()
        {
            AudioSources[1].volume = VolumeData.GetLevel(new BatterieVolume()) * .01f;
        }

        public override void Stop()
        {
            foreach (var a in AudioSources) a.Stop();
        }

    }
}