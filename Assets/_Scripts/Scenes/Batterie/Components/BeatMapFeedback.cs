using System.Collections;
using UnityEngine;

namespace Batterie
{
    public class BeatMapFeedBack
    {
        public BeatMapFeedBack(float tempo)
        {
            Tempo = tempo;
            _ = BeatScroller;
        }

        public void SelfDestruct()
        {
            StayinAlive = false;
            Object.Destroy(_beatScroller);
        }

        private readonly float Tempo;
        private bool StayinAlive = true;

        private GameObject _beatScroller;
        public GameObject BeatScroller =>
            _beatScroller != null ? _beatScroller : _beatScroller = SetUpBeatScroller();
        private GameObject SetUpBeatScroller()
        {
            var go = new GameObject(nameof(_beatScroller));
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = Assets.White;
            sr.color = new Color(.8f, .8f, 0, .4f);
            go.transform.localScale = new Vector3(.3f, .6f, 1);
            //go.transform.position = SheetMusicScribingSystems.NotePosition(MeasureNumber.One, BeatLocation.One) + new Vector3(0, .8f, 0);
            return go;
        }

        public void StartScrolling() => Scroll(0).StartCoroutine();

        private IEnumerator Scroll(int i)
        {
            if (!StayinAlive) yield break;
            //BeatScroller.transform.position = new Vector3(0, .8f, 0) +
            //RhythmScriberSystems.NotePosition(RhythmScriberSystems.BeatIndexToNoteLocation(i));
            yield return new WaitForSecondsRealtime(60 / Tempo / 4);
            Scroll(++i).StartCoroutine();
        }
    }



}