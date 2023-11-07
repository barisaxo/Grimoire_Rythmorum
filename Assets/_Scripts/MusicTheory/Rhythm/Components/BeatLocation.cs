namespace Musica.Rhythms
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