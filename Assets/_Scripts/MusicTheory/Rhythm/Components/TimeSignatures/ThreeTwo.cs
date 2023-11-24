using System.Collections.Generic;
using UnityEngine;


using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class ThreeTwo : Time
    {
        public ThreeTwo() { Signature = TimeSignatureEnum.ThreeTwo; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(TripHalf.SetCount(1));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(DupQuarter.SetCount(1));
                            cells.Add(DupQuarter.SetCount(2));
                            cells.Add(DupQuarter.SetCount(3));
                        }
                        else
                        {
                            cells.Add(TripHalf.SetCount(1));
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(DupQuarter.SetCount(1));
                        cells.Add(DupQuarter.SetCount(2));
                        cells.Add(DupQuarter.SetCount(3));
                        break;

                        //case SubDivisionTier.D1AndD2:
                        //    cells.Add(Random.value > .5f ? DupQuarter.SetCount(1) : Eighth.SetCount(1));
                        //    cells.Add(Random.value > .5f ? DupQuarter.SetCount(2) : Eighth.SetCount(2));
                        //    cells.Add(Random.value > .5f ? DupQuarter.SetCount(3) : Eighth.SetCount(3));
                        //    break;

                        //case SubDivisionTier.D2Only:
                        //    cells.Add(Eighth.SetCount(1));
                        //    cells.Add(Eighth.SetCount(2));
                        //    cells.Add(Eighth.SetCount(3));
                        //    break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }

        RhythmCell TripHalf => new RhythmCell()
                        .SetMetricLevel(MetricLevel.Beat)
                        .SetQuantizement(Quantizement.Half)
                        .SetRhythmicShape(this.RandomTripCell());
        RhythmCell DupQuarter => new RhythmCell()
                        .SetMetricLevel(MetricLevel.D1)
                        .SetQuantizement(Quantizement.Quarter)
                        .SetRhythmicShape(this.RandomDupleCell());
        //RhythmCell Eighth => new RhythmCell()
        //                        .SetMetricLevel(MetricLevel.Beat)
        //                        .SetQuantizement(Quantizement.Eighth)
        //                        .SetRhythmicShape(this.RandomQuadCell());
    }
}
