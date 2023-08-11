namespace MusicTheory.Rhythms
{
    public class Count : Enumeration
    {
        public Count() : base(0, "") { }
        private Count(int id, string name) : base(id, name) { }
        public static readonly Count One = new(01, nameof(One));
        public static readonly Count Two = new(02, nameof(Two));
        public static readonly Count Thr = new(03, nameof(Thr));
        public static readonly Count For = new(04, nameof(For));
        public static readonly Count Fiv = new(05, nameof(Fiv));
        public static readonly Count Six = new(06, nameof(Six));
        public static readonly Count Sev = new(07, nameof(Sev));
        public static readonly Count Eht = new(08, nameof(Eht));
        public static readonly Count Nin = new(09, nameof(Nin));
        public static readonly Count Ten = new(10, nameof(Ten));
        public static readonly Count Elv = new(11, nameof(Elv));
        public static readonly Count Tlv = new(12, nameof(Tlv));


        public static implicit operator Count(int i)
        {
            foreach (Count c in GetAll<Count>()) if (i == c.Id) return c;
            return One;
        }
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
