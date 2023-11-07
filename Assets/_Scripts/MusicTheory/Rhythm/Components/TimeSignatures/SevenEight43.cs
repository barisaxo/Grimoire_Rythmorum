using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SheetMusic;

namespace Musica.Rhythms
{
    public class SevenEight43 : Time
    {
        public SevenEight43() { Signature = TimeSignatureEnum.SevenEight43; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(QuadEighth.SetCount(1));

                        cells.Add(TripEighth.SetCount(5));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(QuadEighth.SetCount(1));
                        }
                        else
                        {
                            cells.Add(QuadSixteenth.SetCount(1));
                            cells.Add(QuadSixteenth.SetCount(3));
                        }

                        if (Random.value > .5f)
                        {
                            cells.Add(TripEighth.SetCount(5));
                        }
                        else
                        {
                            cells.Add(DupSixteenth.SetCount(5));
                            cells.Add(DupSixteenth.SetCount(6));
                            cells.Add(DupSixteenth.SetCount(7));
                        }

                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(QuadSixteenth.SetCount(1));
                        cells.Add(QuadSixteenth.SetCount(1));

                        cells.Add(DupSixteenth.SetCount(1));
                        cells.Add(DupSixteenth.SetCount(1));
                        cells.Add(DupSixteenth.SetCount(1));
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }
        RhythmCell TripEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomTripCell());
        RhythmCell QuadEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomQuadCell());
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