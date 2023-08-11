namespace MusicTheory.Rhythms
{
    public struct BeatLocation
    {
        public MeasureNumber MeasureNumber;
        public BeatAssignment BeatAssignment;

        public readonly int BeatValue => BeatSpacingValue(this);

        public BeatLocation SetMeasure(MeasureNumber measure) { MeasureNumber = measure; return this; }
        public BeatLocation SetBeat(BeatAssignment beat) { BeatAssignment = beat; return this; }

        public BeatLocation(MeasureNumber measure, BeatAssignment beat) { MeasureNumber = measure; BeatAssignment = beat; }

        /// <summary>
        /// Measure one, Count one, on the downbeat.
        /// </summary>
        public static BeatLocation DownBeat => new() { MeasureNumber = MeasureNumber.One, BeatAssignment = BeatAssignment.OneD };
        /// <summary>
        /// Default cut off point for a one bar clip;
        /// </summary>
        public static BeatLocation OneBarThenOff => new() { MeasureNumber = MeasureNumber.Two, BeatAssignment = BeatAssignment.ForD };
        /// <summary>
        /// Default cut off point for a four bar clip;
        /// </summary>
        public static BeatLocation FourBarsThenOff => new() { MeasureNumber = MeasureNumber.Fiv, BeatAssignment = BeatAssignment.ForD };

        public static int operator +(BeatLocation a, BeatLocation b) => BeatSpacingValue(a) + BeatSpacingValue(b);
        public static int operator -(BeatLocation a, BeatLocation b) => BeatSpacingValue(a) - BeatSpacingValue(b);

        public static int BeatSpacingValue(BeatLocation bl) => (bl.MeasureNumber - 1 * 48) +
                                                               (bl.BeatAssignment.Count - 1 * 12) +
                                                               (bl.BeatAssignment.SubBeatAssignment - 1);
    }
}


//public enum MeasureNumber { One = 1, Two = 2, Thr = 3, For = 4 }


//public struct BeatLocation
//{
//    public MeasureNumber MeasureNumber;
//    public Count Count;
//    public SubBeatAssignment SubBeatAssignment;
//}
//public enum Count { One = 1, Two = 2, Thr = 3, For = 4, Fiv = 5, Six = 6, Sev = 7, Eht = 8, Nin = 9, Ten = 10, Elv = 11, Tlv = 12 }

//public enum CellShape { w, dhq, hh, qdh, hqq, qqh, qhq, qqqq, thq, tqh, tqqq, tw, }
//
//public enum CellPosition { One = 1, Two = 2, Thr = 3, For = 4 }

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









//public enum SubBeatAssignment
//{
//    D = 1, E = 4, T = 5, N = 7, L = 9, A = 10
//    //Down   e      tup    +      let    a

//    //pass    the  god  damn  but-ter
//    //|1  .  .  e  T  .  +  .  L  a  .  . 
//    //|1  2  3  4  5  6  7  8  9  10 11 12 
//}
