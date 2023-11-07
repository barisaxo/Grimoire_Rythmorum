namespace Musica.Rhythms
{
    public class MeasureNumber : Enumeration
    {
        public MeasureNumber() : base(0, "") { }
        public MeasureNumber(int id, string name) : base(id, name) { }

        public static readonly MeasureNumber Pickup = new(0, nameof(Pickup));
        public static readonly MeasureNumber One = new(1, nameof(One));
        public static readonly MeasureNumber Two = new(2, nameof(Two));
        public static readonly MeasureNumber Thr = new(3, nameof(Thr));
        public static readonly MeasureNumber For = new(4, nameof(For));
        public static readonly MeasureNumber Fiv = new(5, nameof(Fiv));

        public static implicit operator MeasureNumber(int i)
        {
            foreach (MeasureNumber mn in All<MeasureNumber>()) if (i == mn.Id) return mn;
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
