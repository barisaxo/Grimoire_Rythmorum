using UnityEngine;
using MusicTheory.Rhythms;
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
    }
}