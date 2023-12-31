using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public static class WaveGenerator
    {
        public static AudioClip CreateSineWave(float freq, float length)
        {
            int sampleFreq = 21000 * 2;
            float amplitude = .35f;

            float[] samples = new float[21000 * 2];
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = (amplitude * Mathf.Sin(Mathf.PI * 2 * i * freq / sampleFreq + 0))
                // +  (.28f * (Mathf.Sin(Mathf.PI * 2 * i * (frequency * 1.25f) / sampleFreq + Mathf.PI)))
                ;
            }

            AudioClip _ac = AudioClip.Create(nameof(AudioClip), samples.Length, 1, sampleFreq, false);
            _ac.SetData(samples, 0);

            return _ac;
        }

    }
}