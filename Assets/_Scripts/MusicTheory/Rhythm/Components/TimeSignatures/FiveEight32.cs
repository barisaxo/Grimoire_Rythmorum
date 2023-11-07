using System.Collections.Generic;
using UnityEngine;
using SheetMusic;

namespace Musica.Rhythms
{
    public class FiveEight32 : Time
    {
        public FiveEight32() { Signature = TimeSignatureEnum.FiveEight32; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(TripEighth.SetCount(1));
                        cells.Add(DupEighth.SetCount(4));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(TripEighth.SetCount(1));
                        }
                        else
                        {
                            cells.Add(DupSixteenth.SetCount(1));
                            cells.Add(DupSixteenth.SetCount(2));
                            cells.Add(DupSixteenth.SetCount(3));
                        }

                        cells.Add(Random.value > .5f ? DupEighth.SetCount(4) : QuadSixteenth.SetCount(4));
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(DupSixteenth.SetCount(1));
                        cells.Add(DupSixteenth.SetCount(2));
                        cells.Add(DupSixteenth.SetCount(3));

                        cells.Add(QuadSixteenth.SetCount(4));
                        break;
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
        RhythmCell QuadSixteenth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Sixteenth)
                                    .SetRhythmicShape(this.RandomQuadCell());
        RhythmCell DupSixteenth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Sixteenth)
                                    .SetRhythmicShape(this.RandomDupleCell());
    }
}