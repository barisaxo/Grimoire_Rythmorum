namespace MusicTheory.Rhythms
{
    public class SubBeatAssignment : Enumeration
    {
        private SubBeatAssignment(int id, string name) : base(id, name) { }

        /// <summary>
        /// The Downbeat, conventionally called whatever the count number is.
        /// </summary>
        public static readonly SubBeatAssignment D = new(01, nameof(D));

        /// <summary>
        /// 'e', the first sixteenth note subdivision of the beat.
        /// </summary>
        public static readonly SubBeatAssignment E = new(04, nameof(E));

        /// <summary>
        /// 'trip', first triplet subdivision of the beat. 
        /// </summary>
        public static readonly SubBeatAssignment T = new(05, nameof(T));
        //Sometimes called "and" when counting "1 + a 2 + a ...".
        //Note that triplet names & placement invert at every beat level.

        /// <summary>
        /// '+' : "and", The frist 8th note, or second 16th note subdivision.
        /// </summary>
        public static readonly SubBeatAssignment N = new(07, nameof(N));

        /// <summary>
        /// 'let', second triplet subdivision of the beat. 
        /// </summary>
        public static readonly SubBeatAssignment L = new(09, nameof(L));
        //Sometimes called "and" when counting "1 + a 2 + a ..."
        //Note that triplet names & placement invert at every beat level.

        /// <summary>
        /// 'a' the 3rd and final sixteenth note subdivision.
        /// </summary>
        public static readonly SubBeatAssignment A = new(10, nameof(A));


        /*
                 |pass   the  god  damn  but-ter      |p...
                 |down     e tup    +    let a        |d...
                 |1  .  .  e  T  .  +  .  L  a  .  .  |1... 
                 |1  2  3  4  5  6  7  8  9  10 11 12 |1...
        */
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
