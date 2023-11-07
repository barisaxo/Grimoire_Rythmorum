namespace Musica.Rhythms
{
    public class BeatAssignment : Enumeration
    {
        public Count Count;
        public SubBeatAssignment SubBeatAssignment;

        public BeatAssignment() : base(0, "") { }
        private BeatAssignment(int id, string name) : base(id, name) { }

        public static readonly BeatAssignment OneD = new(01, nameof(OneD)) { Count = Count.One, SubBeatAssignment = new D() };
        public static readonly BeatAssignment OneE = new(02, nameof(OneE)) { Count = Count.One, SubBeatAssignment = new E() };
        public static readonly BeatAssignment OneT = new(03, nameof(OneT)) { Count = Count.One, SubBeatAssignment = new T() };
        public static readonly BeatAssignment OneN = new(04, nameof(OneN)) { Count = Count.One, SubBeatAssignment = new N() };
        public static readonly BeatAssignment OneL = new(05, nameof(OneL)) { Count = Count.One, SubBeatAssignment = new L() };
        public static readonly BeatAssignment OneA = new(06, nameof(TwoA)) { Count = Count.One, SubBeatAssignment = new A() };
        public static readonly BeatAssignment TwoD = new(07, nameof(TwoD)) { Count = Count.Two, SubBeatAssignment = new D() };
        public static readonly BeatAssignment TwoE = new(08, nameof(TwoE)) { Count = Count.Two, SubBeatAssignment = new E() };
        public static readonly BeatAssignment TwoT = new(09, nameof(TwoT)) { Count = Count.Two, SubBeatAssignment = new T() };
        public static readonly BeatAssignment TwoN = new(10, nameof(TwoN)) { Count = Count.Two, SubBeatAssignment = new N() };
        public static readonly BeatAssignment TwoL = new(11, nameof(TwoL)) { Count = Count.Two, SubBeatAssignment = new L() };
        public static readonly BeatAssignment TwoA = new(12, nameof(TwoA)) { Count = Count.Two, SubBeatAssignment = new A() };
        public static readonly BeatAssignment ThrD = new(13, nameof(ThrD)) { Count = Count.Thr, SubBeatAssignment = new D() };
        public static readonly BeatAssignment ThrE = new(14, nameof(ThrE)) { Count = Count.Thr, SubBeatAssignment = new E() };
        public static readonly BeatAssignment ThrT = new(15, nameof(ThrT)) { Count = Count.Thr, SubBeatAssignment = new T() };
        public static readonly BeatAssignment ThrN = new(16, nameof(ThrN)) { Count = Count.Thr, SubBeatAssignment = new N() };
        public static readonly BeatAssignment ThrL = new(17, nameof(ThrL)) { Count = Count.Thr, SubBeatAssignment = new L() };
        public static readonly BeatAssignment ThrA = new(18, nameof(ThrA)) { Count = Count.Thr, SubBeatAssignment = new A() };
        public static readonly BeatAssignment ForD = new(19, nameof(ForD)) { Count = Count.For, SubBeatAssignment = new D() };
        public static readonly BeatAssignment ForE = new(20, nameof(ForE)) { Count = Count.For, SubBeatAssignment = new E() };
        public static readonly BeatAssignment ForT = new(21, nameof(ForT)) { Count = Count.For, SubBeatAssignment = new T() };
        public static readonly BeatAssignment ForN = new(22, nameof(ForN)) { Count = Count.For, SubBeatAssignment = new N() };
        public static readonly BeatAssignment ForL = new(23, nameof(ForL)) { Count = Count.For, SubBeatAssignment = new L() };
        public static readonly BeatAssignment ForA = new(24, nameof(ForA)) { Count = Count.For, SubBeatAssignment = new A() };
        public static readonly BeatAssignment FivD = new(25, nameof(FivD)) { Count = Count.Fiv, SubBeatAssignment = new D() };
        public static readonly BeatAssignment FivE = new(25, nameof(FivE)) { Count = Count.Fiv, SubBeatAssignment = new E() };
        public static readonly BeatAssignment FivT = new(27, nameof(FivT)) { Count = Count.Fiv, SubBeatAssignment = new T() };
        public static readonly BeatAssignment FivN = new(28, nameof(FivN)) { Count = Count.Fiv, SubBeatAssignment = new N() };
        public static readonly BeatAssignment FivL = new(29, nameof(FivL)) { Count = Count.Fiv, SubBeatAssignment = new L() };
        public static readonly BeatAssignment FivA = new(30, nameof(SixA)) { Count = Count.Fiv, SubBeatAssignment = new A() };
        public static readonly BeatAssignment SixD = new(31, nameof(SixD)) { Count = Count.Six, SubBeatAssignment = new D() };
        public static readonly BeatAssignment SixE = new(32, nameof(SixE)) { Count = Count.Six, SubBeatAssignment = new E() };
        public static readonly BeatAssignment SixT = new(33, nameof(SixT)) { Count = Count.Six, SubBeatAssignment = new T() };
        public static readonly BeatAssignment SixN = new(34, nameof(SixN)) { Count = Count.Six, SubBeatAssignment = new N() };
        public static readonly BeatAssignment SixL = new(35, nameof(SixL)) { Count = Count.Six, SubBeatAssignment = new L() };
        public static readonly BeatAssignment SixA = new(36, nameof(SixA)) { Count = Count.Six, SubBeatAssignment = new A() };
        public static readonly BeatAssignment SevD = new(37, nameof(SevD)) { Count = Count.Sev, SubBeatAssignment = new D() };
        public static readonly BeatAssignment SevE = new(38, nameof(SevE)) { Count = Count.Sev, SubBeatAssignment = new E() };
        public static readonly BeatAssignment SevT = new(39, nameof(SevT)) { Count = Count.Sev, SubBeatAssignment = new T() };
        public static readonly BeatAssignment SevN = new(40, nameof(SevN)) { Count = Count.Sev, SubBeatAssignment = new N() };
        public static readonly BeatAssignment SevL = new(41, nameof(SevL)) { Count = Count.Sev, SubBeatAssignment = new L() };
        public static readonly BeatAssignment SevA = new(42, nameof(SevA)) { Count = Count.Sev, SubBeatAssignment = new A() };
        public static readonly BeatAssignment EhtD = new(43, nameof(EhtD)) { Count = Count.Eht, SubBeatAssignment = new D() };
        public static readonly BeatAssignment EhtE = new(44, nameof(EhtE)) { Count = Count.Eht, SubBeatAssignment = new E() };
        public static readonly BeatAssignment EhtT = new(45, nameof(EhtT)) { Count = Count.Eht, SubBeatAssignment = new T() };
        public static readonly BeatAssignment EhtN = new(46, nameof(EhtN)) { Count = Count.Eht, SubBeatAssignment = new N() };
        public static readonly BeatAssignment EhtL = new(47, nameof(EhtL)) { Count = Count.Eht, SubBeatAssignment = new L() };
        public static readonly BeatAssignment EhtA = new(48, nameof(EhtA)) { Count = Count.Eht, SubBeatAssignment = new A() };
        public static readonly BeatAssignment NinD = new(49, nameof(NinD)) { Count = Count.Nin, SubBeatAssignment = new D() };
        public static readonly BeatAssignment NinE = new(50, nameof(NinE)) { Count = Count.Nin, SubBeatAssignment = new E() };
        public static readonly BeatAssignment NinT = new(51, nameof(NinT)) { Count = Count.Nin, SubBeatAssignment = new T() };
        public static readonly BeatAssignment NinN = new(52, nameof(NinN)) { Count = Count.Nin, SubBeatAssignment = new N() };
        public static readonly BeatAssignment NinL = new(53, nameof(NinL)) { Count = Count.Nin, SubBeatAssignment = new L() };
        public static readonly BeatAssignment NinA = new(54, nameof(TenA)) { Count = Count.Nin, SubBeatAssignment = new A() };
        public static readonly BeatAssignment TenD = new(55, nameof(TenD)) { Count = Count.Ten, SubBeatAssignment = new D() };
        public static readonly BeatAssignment TenE = new(56, nameof(TenE)) { Count = Count.Ten, SubBeatAssignment = new E() };
        public static readonly BeatAssignment TenT = new(57, nameof(TenT)) { Count = Count.Ten, SubBeatAssignment = new T() };
        public static readonly BeatAssignment TenN = new(58, nameof(TenN)) { Count = Count.Ten, SubBeatAssignment = new N() };
        public static readonly BeatAssignment TenL = new(59, nameof(TenL)) { Count = Count.Ten, SubBeatAssignment = new L() };
        public static readonly BeatAssignment TenA = new(60, nameof(TenA)) { Count = Count.Ten, SubBeatAssignment = new A() };
        public static readonly BeatAssignment ElvD = new(61, nameof(ThrD)) { Count = Count.Elv, SubBeatAssignment = new D() };
        public static readonly BeatAssignment ElvE = new(62, nameof(ThrE)) { Count = Count.Elv, SubBeatAssignment = new E() };
        public static readonly BeatAssignment ElvT = new(63, nameof(ThrT)) { Count = Count.Elv, SubBeatAssignment = new T() };
        public static readonly BeatAssignment ElvN = new(64, nameof(ThrN)) { Count = Count.Elv, SubBeatAssignment = new N() };
        public static readonly BeatAssignment ElvL = new(65, nameof(ThrL)) { Count = Count.Elv, SubBeatAssignment = new L() };
        public static readonly BeatAssignment ElvA = new(66, nameof(ThrA)) { Count = Count.Elv, SubBeatAssignment = new A() };
        public static readonly BeatAssignment TlvD = new(67, nameof(TlvD)) { Count = Count.Tlv, SubBeatAssignment = new D() };
        public static readonly BeatAssignment TlvE = new(68, nameof(TlvE)) { Count = Count.Tlv, SubBeatAssignment = new E() };
        public static readonly BeatAssignment TlvT = new(69, nameof(TlvT)) { Count = Count.Tlv, SubBeatAssignment = new T() };
        public static readonly BeatAssignment TlvN = new(70, nameof(TlvN)) { Count = Count.Tlv, SubBeatAssignment = new N() };
        public static readonly BeatAssignment TlvL = new(71, nameof(TlvL)) { Count = Count.Tlv, SubBeatAssignment = new L() };
        public static readonly BeatAssignment TlvA = new(72, nameof(TlvA)) { Count = Count.Tlv, SubBeatAssignment = new A() };

        public static implicit operator BeatAssignment(int i)
        {
            foreach (BeatAssignment c in All<BeatAssignment>()) if (i == c.Id) return c;
            UnityEngine.Debug.Log("Beat Assignment Not Found! " + i);
            return OneD;
        }
    }
}

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
//    D = 1, E = 4, T = 5, N = 7, L = 9, A = 10
//    //Down   e      tup    +      let    a

//    //pass    the  god  damn  but-ter
//    //|1  .  .  e  T  .  +  .  L  a  .  . 
//    //|1  2  3  4  5  6  7  8  9  10 11 12 
//
