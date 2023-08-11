using System.Collections.Generic;
using UnityEngine;

using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class FiveFour32 : Time
    {
        public FiveFour32() { Signature = TimeSignature.FiveFour32; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(TripQuarter.SetCount(1));

                        cells.Add(DupQuarter.SetCount(4));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(TripQuarter.SetCount(1));
                        }
                        else
                        {
                            cells.Add(DupEighth.SetCount(1));
                            cells.Add(DupEighth.SetCount(2));
                            cells.Add(DupEighth.SetCount(3));
                        }

                        cells.Add(Random.value > .5f ? DupQuarter.SetCount(4) : QuadEighth.SetCount(4));
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(DupEighth.SetCount(1));
                        cells.Add(DupEighth.SetCount(2));
                        cells.Add(DupEighth.SetCount(3));

                        cells.Add(QuadEighth.SetCount(4));
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }
        RhythmCell TripQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Quarter)
                                    .SetRhythmicShape(this.RandomTripCell());
        RhythmCell DupQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Quarter)
                                    .SetRhythmicShape(this.RandomDupleCell());
        RhythmCell QuadEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomQuadCell());
        RhythmCell DupEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomDupleCell());
    }
}