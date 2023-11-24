using System.Collections.Generic;
using UnityEngine;


using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class TwelveEight : Time
    {
        public TwelveEight() { Signature = TimeSignatureEnum.TwelveEight; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        for (int i = 0; i < 4; i++)
                        {
                            cells.Add(Eighth.SetCount(1 + (3 * i))); ;
                        }
                        break;

                    case SubDivisionTier.BeatAndD1:
                        for (int i = 0; i < 4; i++)
                        {
                            if (Random.value > .5f)
                            {
                                cells.Add(Sixteenth.SetCount(1 + (3 * i)));
                                cells.Add(Sixteenth.SetCount(2 + (3 * i)));
                                cells.Add(Sixteenth.SetCount(3 + (3 * i)));
                            }
                            else
                            {
                                cells.Add(Eighth.SetCount(1 + (3 * i)));
                            }
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        for (int i = 0; i < 4; i++)
                        {
                            cells.Add(Sixteenth.SetCount(1 + (3 * i)));
                            cells.Add(Sixteenth.SetCount(2 + (3 * i)));
                            cells.Add(Sixteenth.SetCount(3 + (3 * i)));
                        }
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
