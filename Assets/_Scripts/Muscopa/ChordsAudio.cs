using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Muscopa
{
    public class ChordsAudio : AudioSystem
    {
        public ChordsAudio() : base(numOfAudioSources: 2, name: nameof(ChordsAudio))
        {

        }
    }

}