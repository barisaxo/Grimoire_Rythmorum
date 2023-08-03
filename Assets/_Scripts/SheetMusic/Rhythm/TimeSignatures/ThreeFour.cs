using System.Collections.Generic;
using UnityEngine;


using SheetMusic;

namespace MusicTheory.Rhythms
{
    public class ThreeFour : Time
    {
        public ThreeFour() { Signature = TimeSignature.ThreeFour; }

        protected override void GetRhythmCells(MusicSheet ms)
        {
            ms.Measures = new Measure[ms.RhythmSpecs.NumberOfMeasures];

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                List<RhythmCell> cells = new();
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        cells.Add(TripQuarter.SetCount(1));
                        break;

                    case SubDivisionTier.BeatAndD1:
                        if (Random.value > .5f)
                        {
                            cells.Add(DupEighth.SetCount(1));
                            cells.Add(DupEighth.SetCount(2));
                            cells.Add(DupEighth.SetCount(3));
                        }
                        else
                        {
                            cells.Add(TripQuarter);
                        }
                        break;

                    case SubDivisionTier.D1Only:
                        cells.Add(DupEighth.SetCount(1));
                        cells.Add(DupEighth.SetCount(2));
                        cells.Add(DupEighth.SetCount(3));
                        break;

                    case SubDivisionTier.D1AndD2:
                        cells.Add(Random.value > .5f ? DupEighth.SetCount(1) : QuadSixteenth.SetCount(1));
                        cells.Add(Random.value > .5f ? DupEighth.SetCount(2) : QuadSixteenth.SetCount(2));
                        cells.Add(Random.value > .5f ? DupEighth.SetCount(3) : QuadSixteenth.SetCount(3));
                        break;

                    case SubDivisionTier.D2Only:
                        cells.Add(QuadSixteenth.SetCount(1));
                        cells.Add(QuadSixteenth.SetCount(2));
                        cells.Add(QuadSixteenth.SetCount(3));
                        break;
                }

                ms.Measures[m].Cells = cells.ToArray();
            }
        }


        RhythmCell TripQuarter => new RhythmCell()
                            .SetMetricLevel(MetricLevel.Beat)
                            .SetQuantizement(Quantizement.Quarter)
                            .SetRhythmicShape(this.RandomTripCell());
        RhythmCell DupEighth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D1)
                                    .SetQuantizement(Quantizement.Eighth)
                                    .SetRhythmicShape(this.RandomDupleCell());
        RhythmCell QuadSixteenth => new RhythmCell()
                                    .SetMetricLevel(MetricLevel.D2)
                                    .SetQuantizement(Quantizement.Sixteenth)
                                    .SetRhythmicShape(this.RandomQuadCell());
    }
}


/*
 *         protected override void GetNotes(MusicSheet ms)
        {
            List<Note> notes = new();
            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    switch (ms.Measures[m].Cells[c].Shape)
                    {
                        case CellShape.TL:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.DotHalf,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            }); break;
                        case CellShape.TLS:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Half,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Quarter,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count + 2,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            break;
                        case CellShape.TSL:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Quarter,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Half,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count + 1,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            break;

                        case CellShape.TSSS:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Quarter,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Quarter,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count + 1,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Quarter,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count + 2,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            break;

                        case CellShape.L:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Quarter,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            break;

                        case CellShape.LL:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Eighth,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            }); notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Eighth,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.N
                                }
                            });
                            break;

                        case CellShape.LS:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.DotEighth,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            }); notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.A
                                }
                            });
                            break;

                        case CellShape.SL:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.DotEighth,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.E
                                }
                            });
                            break;

                        case CellShape.SLS:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Eighth,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.E
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.A
                                }
                            });
                            break;

                        case CellShape.LSS:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Eighth,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.N
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.A
                                }
                            });
                            break;
                        case CellShape.SSL:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.E
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Eighth,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.N
                                }
                            });
                            break;
                        case CellShape.SSSS:
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                Rest = ms.Measures[m].Cells[c].Rest,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.D
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.E
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.N
                                }
                            });
                            notes.Add(new Note()
                            {
                                ParentCell = ms.Measures[m].Cells[c],
                                QuantizedRhythmicValue = RhythmicValue.Sixteenth,
                                TiesTo = ms.Measures[m].Cells[c].TiedTo,
                                BeatLocation = new BeatLocation
                                {
                                    Count = ms.Measures[m].Cells[c].Count,
                                    MeasureNumber = (MeasureNumber)m + 1,
                                    SubBeatAssignment = SubBeatAssignment.A
                                }
                            });
                            break;
                    }
                }
            }
            ms.Notes = notes;
        }

 */