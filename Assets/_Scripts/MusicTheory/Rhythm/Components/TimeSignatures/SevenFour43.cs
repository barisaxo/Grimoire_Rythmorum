using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class SevenFour43 : Time
    {
        public SevenFour43() { Signature = TimeSignatureEnum.SevenEight43; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(QuadQuarter.SetCount(Count.One));

                        cells.Add(TripQuarter.SetCount(Count.Fiv));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(QuadQuarter.SetCount(Count.One));
                        }
                        else
                        {
                            cells.Add(QuadEighth.SetCount(Count.One));
                            cells.Add(QuadEighth.SetCount(Count.Thr));
                        }

                        if (Random.value > .5f)
                        {
                            cells.Add(TripQuarter.SetCount(Count.Fiv));
                        }
                        else
                        {
                            cells.Add(DupEighth.SetCount(Count.Fiv));
                            cells.Add(DupEighth.SetCount(Count.Six));
                            cells.Add(DupEighth.SetCount(Count.Sev));
                        }

                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(QuadEighth.SetCount(Count.One));
                        cells.Add(QuadEighth.SetCount(Count.Thr));

                        cells.Add(DupEighth.SetCount(Count.Fiv));
                        cells.Add(DupEighth.SetCount(Count.Six));
                        cells.Add(DupEighth.SetCount(Count.Sev));
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }
        RhythmCell TripQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Quarter)
                                    .SetRhythmicShape(this.RandomTripCell());
        RhythmCell QuadQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Quarter)
                                    .SetRhythmicShape(this.RandomQuadCell());
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