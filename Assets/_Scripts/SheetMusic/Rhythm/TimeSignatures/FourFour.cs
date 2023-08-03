using System.Collections.Generic;
using UnityEngine;


using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class FourFour : Time
    {
        public FourFour() { Signature = TimeSignature.FourFour; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(QuadQuarter.SetCount(1));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        switch (Random.Range(1, ms.RhythmSpecs.HasTriplets ? 4 : 3))
                        {
                            case 1:
                                cells.Add(QuadQuarter.SetCount(1));
                                break;

                            case 2:
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripQuarter.SetCount(1) : QuadEighth.SetCount(1));
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripQuarter.SetCount(3) : QuadEighth.SetCount(3));
                                break;

                            case 3:
                                switch (Random.Range(0, 2))
                                {
                                    case 0:
                                        cells.Add(TripEighth.SetCount(1));
                                        cells.Add(TripEighth.SetCount(2));
                                        cells.Add(Random.value > .5f ? TripQuarter.SetCount(3) : QuadEighth.SetCount(3));
                                        break;

                                    case 1:
                                        cells.Add(Random.value > .5f ? TripQuarter.SetCount(1) : QuadEighth.SetCount(1));
                                        cells.Add(TripEighth.SetCount(3));
                                        cells.Add(TripEighth.SetCount(4));
                                        break;

                                        //case 2:
                                        //    cells.Add(TripEighth.SetCount(1));
                                        //    cells.Add(Random.value > .5f ? TripQuarter.SetCount(2) : QuadEighth.SetCount(2));
                                        //    cells.Add(TripEighth.SetCount(4));
                                        //    break;
                                };
                                break;
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        if (ms.RhythmSpecs.HasTriplets)
                        {
                            switch (Random.Range(0, 3))
                            {
                                case 0:
                                    cells.Add(TripEighth.SetCount(1));
                                    cells.Add(TripEighth.SetCount(2));
                                    cells.Add(QuadEighth.SetCount(3));
                                    break;

                                case 1:
                                    cells.Add(QuadEighth.SetCount(1));
                                    cells.Add(TripEighth.SetCount(3));
                                    cells.Add(TripEighth.SetCount(4));
                                    break;

                                case 2:
                                    cells.Add(TripEighth.SetCount(1));
                                    cells.Add(QuadEighth.SetCount(2));
                                    cells.Add(TripEighth.SetCount(4));
                                    break;
                            };
                            break;
                        }
                        else
                        {
                            cells.Add(QuadEighth.SetCount(1));
                            cells.Add(QuadEighth.SetCount(3));
                        }
                        break;

                    case SubDivisionTier.D1AndD2:
                        switch (Random.Range(3, 5))
                        {
                            case 3:
                                switch (Random.Range(0, 2))
                                {
                                    case 0:
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(1) : QuadSixteenth.SetCount(1));
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(2) : QuadSixteenth.SetCount(2));
                                        cells.Add(QuadEighth.SetCount(3));
                                        break;
                                    case 1:
                                        cells.Add(QuadEighth.SetCount(1));
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(3) : QuadSixteenth.SetCount(3));
                                        cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(4) : QuadSixteenth.SetCount(4));
                                        break;
                                        //case 2:
                                        //    cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(1) : QuadSixteenth.SetCount(1));
                                        //    cells.Add(QuadEighth.SetCount(2));
                                        //    cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(4) : QuadSixteenth.SetCount(4));
                                        //    break;
                                };
                                break;

                            case 4:
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(1) : QuadSixteenth.SetCount(1));
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(2) : QuadSixteenth.SetCount(2));
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(3) : QuadSixteenth.SetCount(3));
                                cells.Add(ms.RhythmSpecs.HasTriplets && Random.value > .5f ? TripEighth.SetCount(4) : QuadSixteenth.SetCount(4));
                                break;
                        };
                        break;

                    case SubDivisionTier.D2Only:
                        cells.Add(QuadSixteenth.SetCount(1));
                        cells.Add(QuadSixteenth.SetCount(2));
                        cells.Add(QuadSixteenth.SetCount(3));
                        cells.Add(QuadSixteenth.SetCount(4));
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }

        RhythmCell QuadQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.Beat)
                                    .SetQuantizement(Quantizement.Quarter)
                                    .SetRhythmicShape(this.RandomQuadCell());
        RhythmCell TripQuarter => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.BeatT)
                                    .SetQuantizement(Quantizement.QuarterTrips)
                                    .SetRhythmicShape(this.RandomTripCellNoTL());
        RhythmCell QuadEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomQuadCell());
        RhythmCell TripEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1T)
                                    .SetQuantizement(Quantizement.EighthTrips)
                                    .SetRhythmicShape(this.RandomTripCellNoTL());
        RhythmCell QuadSixteenth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D2)
                                    .SetQuantizement(Quantizement.Sixteenth)
                                    .SetRhythmicShape(this.RandomQuadCell());
    }

}