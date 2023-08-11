using System.Collections.Generic;
using UnityEngine;


using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class TwoFour : Time
    {
        public TwoFour() { Signature = TimeSignature.TwoFour; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripQuarter.SetCount(1) : Quarter.SetCount(1));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            if (ms.RhythmSpecs.HasTriplets && Random.value > .5f)
                            {
                                cells.Add(TripEighth.SetCount(1));
                                cells.Add(TripEighth.SetCount(2));
                            }
                            else
                            {
                                cells.Add(Eighth.SetCount(1));
                            }
                        }
                        else
                        {
                            cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripQuarter.SetCount(1) : Quarter.SetCount(1));
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        if (ms.RhythmSpecs.HasTriplets && Random.value > .5f)
                        {
                            cells.Add(TripEighth.SetCount(1));
                            cells.Add(TripEighth.SetCount(2));
                        }
                        else
                        {
                            cells.Add(Eighth.SetCount(1));
                        }
                        break;

                    case SubDivisionTier.D1AndD2:
                        if (Random.value > .5f)
                        {
                            cells.Add(Eighth.SetCount(1));
                        }
                        else
                        {
                            cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(1) : Sixteenth.SetCount(1));
                            cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(2) : Sixteenth.SetCount(2));
                        }
                        break;

                    case SubDivisionTier.D2Only:
                        cells.Add(Sixteenth.SetCount(1));
                        cells.Add(Sixteenth.SetCount(2));
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }
        RhythmCell Quarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Quarter)
                                    .SetRhythmicShape(this.RandomDupleCell());
        RhythmCell TripQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.BeatT)
                                    .SetQuantizement(Quantizement.QuarterTrips)
                                    .SetRhythmicShape(this.RandomTripCellNoTL());
        RhythmCell Eighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomQuadCell());
        RhythmCell TripEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1T)
                                    .SetQuantizement(Quantizement.EighthTrips)
                                    .SetRhythmicShape(this.RandomTripCellNoTL());
        RhythmCell Sixteenth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D2)
                                    .SetQuantizement(Quantizement.Sixteenth)
                                    .SetRhythmicShape(this.RandomQuadCell());
    }
}