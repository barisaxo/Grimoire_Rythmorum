using System.Collections.Generic;
using UnityEngine;


using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class FiveFour23 : Time
    {
        public FiveFour23() { Signature = TimeSignatureEnum.FiveFour23; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(DupQuarter.SetCount(1));

                        cells.Add(TripQuarter.SetCount(3));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        cells.Add(Random.value > .5f ? DupQuarter.SetCount(1) : QuadEighth.SetCount(1));

                        if (Random.value > .5f)
                        {
                            cells.Add(TripQuarter.SetCount(3));
                        }
                        else
                        {
                            cells.Add(DupEighth.SetCount(3));
                            cells.Add(DupEighth.SetCount(4));
                            cells.Add(DupEighth.SetCount(5));
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(QuadEighth.SetCount(1));

                        cells.Add(DupEighth.SetCount(3));
                        cells.Add(DupEighth.SetCount(4));
                        cells.Add(DupEighth.SetCount(5));
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