using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BatterieAssets
{
    public static AudioClip RimShot => Resources.Load<AudioClip>("Audio/Ritmo/RimShot");
    public static AudioClip SnareRoll => Resources.Load("Audio/Ritmo/SnareRoll60bpm16beats") as AudioClip;
    public static AudioClip MissStick => Resources.Load("Audio/Ritmo/MissStick") as AudioClip;
}