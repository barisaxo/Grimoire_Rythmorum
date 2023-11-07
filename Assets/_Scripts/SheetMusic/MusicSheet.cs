using UnityEngine;
using Musica.Rhythms;
using Batterie;

namespace SheetMusic
{
    public class MusicSheet
    {
        public void SelfDestruct()
        {
            Object.Destroy(_parent.gameObject);
        }

        public RhythmSpecs RhythmSpecs;
        public Measure[] Measures;
        public Note[] Notes;
        public MappedBeat[] BeatMap;

        private Transform _parent;
        public Transform Parent => !_parent ? _parent = new GameObject(nameof(MusicSheet)).transform : _parent;

        private Card _card;
        public Card Card => _card ??= new Card(nameof(Card), Parent)
            .SetCanvasSortingOrder(1);

        //public Card TimeSig;
        //public Card[] ScribedStaves;
        //public Card[] ScribedNotes;
        //public Card[] ScribedCounts;

        private Card _bg;
        public Card BackGround => _bg ??= new Card(nameof(BackGround), Parent)
            .SetImageSprite(Assets.White)
            .SetImageColor(new Color(0, 0, 0, .65f))
            .SetImageSize(new Vector3(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2))
            .SetCanvasSortingOrder(0)
            .SetImagePosition(new Vector3(0, 0, 3));
    }
}