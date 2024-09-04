using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Musica
{
    public class BassAudio : AudioSystem
    {
        public BassAudio() : base(numOfAudioSources: 2, name: nameof(BassAudio))
        {

        }
    }
}