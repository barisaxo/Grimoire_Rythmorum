namespace MusicTheory.Rhythms
{
    public struct Meter
    {
        public readonly BeatDivisor Divisor => Divisors[0];
        public readonly PulseStress Pulse => Pulses[0];

        public BeatDivisor[] Divisors;
        public PulseStress[] Pulses;

        public static Meter SimpleDuple => new() { Divisors = new[] { BeatDivisor.Simple }, Pulses = new[] { PulseStress.Duple } };// 2/2, 2/4, 2/8...
        public static Meter SimpleTriple => new() { Divisors = new[] { BeatDivisor.Simple, }, Pulses = new[] { PulseStress.Triple } };// 3/2, 3/4, 3/8...
        public static Meter SimpleQuadruple => new() { Divisors = new[] { BeatDivisor.Simple, }, Pulses = new[] { PulseStress.Quadruple } };// 4/2, 4/4, 4/8...

        public static Meter CompoundDuple => new() { Divisors = new[] { BeatDivisor.Compound, }, Pulses = new[] { PulseStress.Duple } };// 6/2, 6/4, 6/8
        public static Meter CompoundTriple => new() { Divisors = new[] { BeatDivisor.Compound, }, Pulses = new[] { PulseStress.Triple } };// 9/2, 9/4, 9/8
        public static Meter CompoundQuadruple => new() { Divisors = new[] { BeatDivisor.Compound, }, Pulses = new[] { PulseStress.Quadruple } };// 12/2, 12/4, 12/8

        public static Meter IrregularDupleTriple => new() { Divisors = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulses = new[] { PulseStress.Duple, PulseStress.Triple } };// 2 + 3
        public static Meter IrregularTripleDuple => new() { Divisors = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulses = new[] { PulseStress.Triple, PulseStress.Duple } };// 3 + 2
        public static Meter IrregularQuadrupleTriple => new() { Divisors = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulses = new[] { PulseStress.Quadruple, PulseStress.Triple } };// 4 + 3
        public static Meter IrregularTripleQuadruple => new() { Divisors = new[] { BeatDivisor.Simple, BeatDivisor.Simple }, Pulses = new[] { PulseStress.Triple, PulseStress.Quadruple } };// 3 + 4

        public static bool operator ==(Meter a, Meter b) => a.Divisors == b.Divisors && a.Pulses == b.Pulses;
        public static bool operator !=(Meter a, Meter b) => a.Divisors != b.Divisors || a.Pulses != b.Pulses;
        public override readonly bool Equals(object obj) => obj is Meter m && Divisors == m.Divisors && Pulses == m.Pulses;
        public override readonly int GetHashCode() => System.HashCode.Combine(Divisors, Pulses);
    }
}
