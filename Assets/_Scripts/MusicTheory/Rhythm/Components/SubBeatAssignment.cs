namespace Musica.Rhythms
{
    public class SubBeatAssignment : Enumeration
    {
        public SubBeatAssignment(int id, string name) : base(id, name) { }
        //public SubBeatAssignment SubBeat { get; protected set; }

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

        ///// <summary>
        ///// The Downbeat, conventionally called whatever the count number is.
        ///// </summary>
        //public class D : SubBeatAssignment { public D() : base(01, nameof(D)) { } }// SubBeat = D;
        ///// <summary>
        ///// 'e', the first sixteenth note subdivision of the beat.
        ///// </summary>
        //public class E : SubBeatAssignment { public E() : base(04, nameof(E)) { } }// SubBeat = E;
        ///// <summary>
        ///// 'trip', first triplet subdivision of the beat. 
        ///// </summary>
        //public class T : SubBeatAssignment { public T() : base(05, nameof(T)) { } }// SubBeat = T;
        ///// <summary>
        ///// '+' : "and", The frist 8th note, or second 16th note subdivision.
        ///// </summary>
        //public class N : SubBeatAssignment { public N() : base(07, nameof(N)) { } }// SubBeat = N;
        ///// <summary>
        ///// 'let', second triplet subdivision of the beat.
        ///// </summary>
        //public class L : SubBeatAssignment { public L() : base(09, nameof(L)) { } }// SubBeat = L;
        ///// <summary>
        ///// 'a' the 3rd and final sixteenth note subdivision.
        ///// </summary>
        //public class A : SubBeatAssignment { public A() : base(10, nameof(A)) { } }// SubBeat = A;
    }

    /// <summary>
    /// The Downbeat, conventionally called whatever the count number is.
    /// </summary>
    public class D : SubBeatAssignment { public D() : base(01, nameof(D)) { } }// SubBeat = D;
    /// <summary>
    /// 'e', the first sixteenth note subdivision of the beat.
    /// </summary>
    public class E : SubBeatAssignment { public E() : base(04, nameof(E)) { } }// SubBeat = E;
    /// <summary>
    /// 'trip', first triplet subdivision of the beat. 
    /// </summary>
    public class T : SubBeatAssignment { public T() : base(05, nameof(T)) { } }// SubBeat = T;
    /// <summary>
    /// '+' : "and", The frist 8th note, or second 16th note subdivision.
    /// </summary>
    public class N : SubBeatAssignment { public N() : base(07, nameof(N)) { } }// SubBeat = N;
    /// <summary>
    /// 'let', second triplet subdivision of the beat.
    /// </summary>
    public class L : SubBeatAssignment { public L() : base(09, nameof(L)) { } }// SubBeat = L;
    /// <summary>
    /// 'a' the 3rd and final sixteenth note subdivision.
    /// </summary>
    public class A : SubBeatAssignment { public A() : base(10, nameof(A)) { } }// SubBeat = A;
}