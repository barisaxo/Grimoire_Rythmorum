namespace MusicTheory.Rhythms
{
    public struct Measure
    {
        public RhythmSpecs Specs;
        public RhythmCell[] Cells;

        public Measure SetSpecs(RhythmSpecs specs) { Specs = specs; return this; }
        public Measure SetCells(RhythmCell[] cells) { Cells = cells; return this; }
    }
}





//public enum CellShape { w, dhq, hh, qdh, hqq, qqh, qhq, qqqq, thq, tqh, tqqq, tw, }


//
//Whole note             = 64 : 240 / BPM
//Half note              = 32 : 120 / BPM
//Dotted quarter note    = 24 : 90 / BPM
//Quarter note           = 16 : 60 / BPM
//Dotted eighth note     = 12 : 45 / BPM
//Triplet quarter note   = 10 : ??
//Eighth note            =  8 : 30 / BPM
//Triplet eighth note    =  6 : 20 / BPM
//Sixteenth note         =  4 : 15 / BPM
//
