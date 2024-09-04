using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeatFishing
{
    public static class BeatFishingSystems
    {


        public static void ReelingAnimations(this BeatFishingScene scene)
        {
            scene.Spool.SetImageSize((float)(((float)scene.Score + scene.Difficulty) / (float)(scene.Difficulty * 2)) * 5f * Vector3.one);
            if (scene.Reeling)
            {
                scene.Reel.Image.rectTransform.Rotate(-30 * Time.deltaTime * Vector3.forward);
                scene.Spool.Image.rectTransform.Rotate(-30 * Time.deltaTime * Vector3.forward);
                scene.Handle.Image.rectTransform.Rotate(-50 * Time.deltaTime * Vector3.forward);
            }
            else if (scene.Spooling)
            {
                scene.Reel.Image.rectTransform.Rotate(110 * Time.deltaTime * Vector3.forward);
                scene.Spool.Image.rectTransform.Rotate(110 * Time.deltaTime * Vector3.forward);
                scene.Handle.Image.rectTransform.Rotate(15 * Time.deltaTime * Vector3.forward);
            }
        }
    }
}