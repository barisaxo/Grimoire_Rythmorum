
namespace Musica.Rhythms
{
    public class RhythmSpecs
    {
        public float Tempo = 80;
        public int NumberOfMeasures = 4;
        public SubDivisionTier SubDivisionTier = SubDivisionTier.BeatOnly;
        public Meter Meter = Meter.SimpleQuadruple;
        public MetricLevel SmallestMetricLevel = MetricLevel.Beat;
        public Time Time;
        public bool HasTies;
        public bool HasRests;
        public bool HasTriplets;

        public RhythmSpecs SetTempo(float tempo) { Tempo = tempo; return this; }
        public RhythmSpecs SetNumberOfMeasures(int numberOfMeasures) { NumberOfMeasures = numberOfMeasures; return this; }
        public RhythmSpecs SetSubDivision(SubDivisionTier tier) { SubDivisionTier = tier; return this; }
        public RhythmSpecs SetMeter(Meter meter) { Meter = meter; return this; }
        public RhythmSpecs SetMetricLevel(MetricLevel level) { SmallestMetricLevel = level; return this; }
        public RhythmSpecs SetTime(Time time) { Time = time; return this; }
        public RhythmSpecs SetTies(bool tf) { HasTies = tf; return this; }
        public RhythmSpecs SetRests(bool tf) { HasRests = tf; return this; }
        public RhythmSpecs SetTrips(bool tf) { HasTriplets = tf; return this; }
    }
}