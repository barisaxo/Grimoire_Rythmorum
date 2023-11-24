using System.Collections.Generic;
using UnityEngine;


using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class ThreeEight : Time
    {
        public ThreeEight() { Signature = TimeSignatureEnum.ThreeEight; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(Eighth.SetCount(1));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(Sixteenth.SetCount(1));
                            cells.Add(Sixteenth.SetCount(2));
                            cells.Add(Sixteenth.SetCount(3));
                        }
                        else
                        {
                            cells.Add(Eighth.SetCount(1));
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(Sixteenth.SetCount(1));
                        cells.Add(Sixteenth.SetCount(2));
                        cells.Add(Sixteenth.SetCount(3));
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }

        RhythmCell Eighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomTripCell());
        RhythmCell Sixteenth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Sixteenth)
                                    .SetRhythmicShape(this.RandomDupleCell());
    }
}
