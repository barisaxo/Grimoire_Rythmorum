using System.Collections.Generic;
using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class FiveEight23 : Time
    {
        public FiveEight23() { Signature = TimeSignature.FiveEight23; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(DupEighth.SetCount(1));

                        cells.Add(TripEighth.SetCount(3));
                        break;

                        //case SubDivisionTier.BeatAndD1:
                        //    cells.Add(Random.value > .5f ? DupEighth.SetCount(1) : QuadSixteenth.SetCount(1));

                        //    if (Random.value > .5f)
                        //    {
                        //        cells.Add(TripEighth.SetCount(3));
                        //    }
                        //    else
                        //    {
                        //        cells.Add(DupSixteenth.SetCount(3));
                        //        cells.Add(DupSixteenth.SetCount(4));
                        //        cells.Add(DupSixteenth.SetCount(5));
                        //    }
                        //    break;

                        //case SubDivisionTier.D1Only:
                        //    cells.Add(QuadSixteenth.SetCount(1));

                        //    cells.Add(DupSixteenth.SetCount(3));
                        //    cells.Add(DupSixteenth.SetCount(4));
                        //    cells.Add(DupSixteenth.SetCount(5));
                        //    break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }

        RhythmCell TripEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomTripCell());
        RhythmCell DupEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomDupleCell());
        //RhythmCell QuadSixteenth => new RhythmCell()
        //                            .SetMetricLevel(MetricLevel.D1)
        //                            .SetQuantizement(Quantizement.Sixteenth)
        //                            .SetRhythmicShape(this.RandomQuadCell());
        //RhythmCell DupSixteenth => new RhythmCell()
        //                            .SetMetricLevel(MetricLevel.D1)
        //                            .SetQuantizement(Quantizement.Sixteenth)
        //                            .SetRhythmicShape(this.RandomDupleCell());
    }
}