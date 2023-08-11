using System.Collections.Generic;
using UnityEngine;

using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class TwoTwo : Time
    {
        public TwoTwo() { Signature = TimeSignature.TwoTwo; }
        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(DupHalf.SetCount(1));
                        break;
                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(QuadQuarter.SetCount(1));
                        }
                        else
                        {
                            cells.Add(DupHalf.SetCount(1));
                        }
                        break;
                    case SubDivisionTier.D1Only:
                        cells.Add(QuadQuarter.SetCount(1));
                        break;
                    case SubDivisionTier.D1AndD2:
                        if (Random.value > .5f)
                        {
                            cells.Add(QuadQuarter.SetCount(1));
                        }
                        else
                        {
                            cells.Add(QuadEighth.SetCount(1));
                            cells.Add(QuadEighth.SetCount(2));
                        }

                        break;
                    case SubDivisionTier.D2Only:
                        cells.Add(QuadEighth.SetCount(1));
                        cells.Add(QuadEighth.SetCount(2));
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }

        RhythmCell DupHalf => new RhythmCell()
                            .SetMetricLevel(MetricLevel.Beat)
                            .SetQuantizement(Quantizement.Half)
                            .SetRhythmicShape(this.RandomDupleCell());
        RhythmCell QuadQuarter => new RhythmCell()
                                .SetMetricLevel(MetricLevel.D1)
                                .SetQuantizement(Quantizement.Quarter)
                                .SetRhythmicShape(this.RandomQuadCell());
        RhythmCell QuadEighth => new RhythmCell()
                                .SetMetricLevel(MetricLevel.D2)
                                .SetQuantizement(Quantizement.Eighth)
                                .SetRhythmicShape(this.RandomQuadCell());
    }
}