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
            foreach (Count c in All<Count>()) if (i == c.Id) return c;
            return One;
        }
    }
}