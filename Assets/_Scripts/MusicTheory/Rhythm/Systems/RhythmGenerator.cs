using UnityEngine;
using System.Collections.Generic;
using SheetMusic;

namespace MusicTheory.Rhythms
{
    public static class RhythmGenerator
    {
        public static CellShape RandomDupleCell(this Time _) => Random.value > .5f ? CellShape.DL : CellShape.DLL;
        public static CellShape RandomQuadCell(this Time _) => (CellShape)Random.Range(0, 8);
        public static CellShape RandomTripCellNoTL(this Time _) => (CellShape)Random.Range(8, 11);
        public static CellShape RandomTripCell(this Time _) => (CellShape)Random.Range(8, 12);

        public static void GetRestsAndTies(this MusicSheet ms)
        {
            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    ms.Measures[m].Cells[c].SetTiedTo(ms.GetTiedTo(m, c));
                }
            }

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    ms.Measures[m].Cells[c].SetTiedFrom(ms.GetTiedFrom(m, c));
                }
            }

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    ms.Measures[m].Cells[c].SetRest(ms.GetRest(m, c));
                }
            }
        }

        public static RhythmCell PreviousCellOrDefault(this MusicSheet ms, int m, int c)
        {
            if (c == 0)
            {
                if (m == 0)
                {
                    return default;
                }
                return ms.Measures[m - 1].Cells[^1];
            }
            return ms.Measures[m].Cells[c - 1];
        }

        public static bool GetTiedTo(this MusicSheet ms, int m, int c)
        {
            if (!ms.RhythmSpecs.HasTies) return false;
            if (m == ms.Measures.Length - 1 && c == ms.Measures[^1].Cells.Length - 1) return false;//don't tie last cell to nothing
            if (c == 0 &&
                ms.Measures[m].Cells?.Length == 2 &&
                ms.Measures[m].Cells?[0].Shape == CellShape.L &&
                ms.Measures[m].Cells?[1].Shape == CellShape.L) return false;//don't tie long to long in same measure.
            return Random.value > .666f;
        }

        public static bool GetTiedFrom(this MusicSheet ms, int m, int c)
        {
            if (c == 0)
            {
                if (m == 0)
                {
                    return false;
                }
                return ms.Measures[m - 1].Cells[^1].TiedTo;
            }
            return ms.Measures[m].Cells[c - 1].TiedTo;
        }

        public static bool GetRest(this MusicSheet ms, int m, int c)
        {
            if (!ms.RhythmSpecs.HasRests) return false;

            if (ms.Measures[m].Cells[c].TiedFrom) return false;//prevents ties to rests

            if (ms.Measures[m].Cells[c].TiedTo &&
               (ms.Measures[m].Cells[c].Shape == CellShape.L ||
                ms.Measures[m].Cells[c].Shape == CellShape.TL ||
                ms.Measures[m].Cells[c].Shape == CellShape.DL)) return false;//prevents ties from rests

            if (ms.Measures[m].Cells[c].MetricLevel == MetricLevel.Beat &&
                (ms.Measures[m].Cells[c].Shape == CellShape.L ||
                ms.Measures[m].Cells[c].Shape == CellShape.TL ||
                ms.Measures[m].Cells[c].Shape == CellShape.DL)) return false;//no full measure rests

            return Random.value > .5f;
        }

        public static void GetNotes(this MusicSheet ms)
        {
            List<Note> notes = new();

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    notes.AddRange(ms.NotesFromCell(ms.Measures[m].Cells[c], (MeasureNumber)m + 1));
                }
            }

            ms.Notes = notes.ToArray();
        }

        public static List<Note> NotesFromCell(this MusicSheet ms, RhythmCell c, MeasureNumber measure)
        {
            List<Note> notes = new List<Note>();

            switch (c.Shape)
            {
                case CellShape.DL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        Rest = c.Rest,
                        TiesTo = c.TiedTo,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.DLL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new E()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new E())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.L:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Whole),
                        Rest = c.Rest,
                        TiesTo = c.TiedTo,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.LL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new N()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new N())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.LSS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new N()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new N())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new A()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new A())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.SSL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new D())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new E()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new E())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new N()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new N())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.SLS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new D())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new E()),

                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new E())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new A()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new A())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.SL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new D())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.DotHalf),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new E()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new E())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.LS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.DotHalf),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new A()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new A())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.SSSS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new E()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new E())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new N()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new N())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new A()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(new A())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.TSSS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new T()),

                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(ms.RhythmSpecs.HasTriplets ? new T() : new E())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new L()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(ms.RhythmSpecs.HasTriplets ? new L() : new N())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.TSL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripHalf : RhythmicValue.Half),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new T()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(ms.RhythmSpecs.HasTriplets ? new T() : new E())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.TLS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripHalf : RhythmicValue.Half),
                        Rest = c.Rest,
                        TiedFrom = c.TiedFrom,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new L()),
                                SubBeatAssignment = c.GetQuantizedSubBeatAssignment(ms.RhythmSpecs.HasTriplets ? new L() : new N())
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
                case CellShape.TL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.DotWhole),//TripWhole???
                        Rest = c.Rest,
                        TiesTo = c.TiedTo,
                        TiedFrom = c.TiedFrom,

                        BeatLocation = new BeatLocation
                        {
                            BeatAssignment = new()
                            {
                                Count = c.GetQuantizedCount(new D()),
                                SubBeatAssignment = new D()
                            },
                            MeasureNumber = measure,
                        }
                    });
                    break;
            }

            return notes;
        }

        public static Count GetQuantizedCount(this RhythmCell cell, SubBeatAssignment s)
        {
            return s switch
            {
                E => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 1,
                    _ => cell.Count
                },

                N => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 2,
                    MetricLevel.D1 => cell.Count + 1,
                    _ => cell.Count
                },

                A => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 3,
                    MetricLevel.D1 => cell.Count + 1,
                    _ => cell.Count
                },

                T => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 1,
                    MetricLevel.BeatT => cell.Count + 1,
                    _ => cell.Count
                },

                L => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 2,
                    MetricLevel.BeatT => cell.Count + 2,
                    _ => cell.Count
                },

                _ => cell.Count,
            };
        }

        public static RhythmicValue GetQuantizedRhythmicValue(this RhythmCell c, RhythmicValue v)
        {
            return v switch
            {
                RhythmicValue.Whole => c.Quantizement switch
                {
                    Quantizement.Sixteenth => RhythmicValue.Quarter,
                    Quantizement.Eighth => RhythmicValue.Half,
                    _ => RhythmicValue.Whole,
                },
                RhythmicValue.DotWhole => c.Quantizement switch
                {
                    Quantizement.Sixteenth => RhythmicValue.DotEighth,
                    Quantizement.Eighth => RhythmicValue.DotQuarter,
                    Quantizement.Quarter => RhythmicValue.DotHalf,
                    _ => RhythmicValue.DotWhole
                },
                RhythmicValue.DotHalf => c.Quantizement switch
                {
                    Quantizement.Sixteenth => RhythmicValue.DotEighth,
                    Quantizement.Eighth => RhythmicValue.DotQuarter,
                    _ => RhythmicValue.DotHalf
                },
                RhythmicValue.Half => c.Quantizement switch
                {
                    Quantizement.Sixteenth => RhythmicValue.Eighth,
                    Quantizement.Eighth => RhythmicValue.Quarter,
                    Quantizement.Half => RhythmicValue.Whole,
                    _ => RhythmicValue.Half
                },
                //RhythmicValue.DotQuarter => c.Quantizement switch
                //{
                //    Quantizement.Eighth => RhythmicValue.DotEighth,
                //    _ => RhythmicValue.DotQuarter,
                //},
                RhythmicValue.Quarter => c.Quantizement switch
                {
                    Quantizement.Sixteenth => RhythmicValue.Sixteenth,
                    Quantizement.Eighth => RhythmicValue.Eighth,
                    Quantizement.Half => RhythmicValue.Half,
                    _ => RhythmicValue.Quarter,
                },
                //RhythmicValue.DotEighth => c.Quantizement switch
                //{
                //    _ => RhythmicValue.DotEighth,
                //},
                RhythmicValue.Eighth => c.Quantizement switch
                {
                    _ => RhythmicValue.Eighth,
                },
                //RhythmicValue.Sixteenth => c.Quantizement switch
                //{
                //    _ => RhythmicValue.Sixteenth,
                //},
                RhythmicValue.TripQuarter => c.Quantizement switch
                {
                    Quantizement.Eighth => RhythmicValue.TripEighth,
                    Quantizement.EighthTrips => RhythmicValue.TripEighth,
                    _ => RhythmicValue.TripQuarter,
                },
                RhythmicValue.TripHalf => c.Quantizement switch
                {
                    Quantizement.Eighth => RhythmicValue.TripQuarter,
                    Quantizement.EighthTrips => RhythmicValue.TripQuarter,
                    _ => RhythmicValue.TripHalf,
                },
                _ => Debugger(),
            };
            RhythmicValue Debugger()
            {
                Debug.Log(nameof(GetQuantizedRhythmicValue) + " " + v);
                return RhythmicValue.Whole;
            }
        }

        public static SubBeatAssignment GetQuantizedSubBeatAssignment(this RhythmCell c, SubBeatAssignment s)
        {
            return s switch
            {
                D => new D(),

                E => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Eighth => new N(),
                    Quantizement.Sixteenth => new E(),
                    _ => new D(),
                },

                N => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Sixteenth => new N(),
                    //Quantizement.Half => new N(),
                    _ => new D(),
                },

                A => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Eighth => new N(),
                    Quantizement.Sixteenth => new A(),
                    _ => new D(),
                },

                T => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Quarter => new L(),
                    _ => new T(),//half and eighth
                },

                L => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Quarter => new T(),
                    _ => new L(),
                },

                _ => Debugger()
            }; ;

            SubBeatAssignment Debugger()
            {
                Debug.Log(nameof(GetQuantizedSubBeatAssignment) + " " + s);
                return new D();
            }
        }

        public static Quantizement QuantizedBeatlevel(this RhythmCell c)
        {
            return c.Quantizement switch
            {
                Quantizement.Eighth => c.MetricLevel switch
                {
                    MetricLevel.Beat => Quantizement.Quarter,
                    _ => Quantizement.Eighth,
                },

                Quantizement.Sixteenth => c.MetricLevel switch
                {
                    //MetricLevel.Beat => Quantizement.Eighth,
                    MetricLevel.D1 => Quantizement.Eighth,
                    _ => Quantizement.Sixteenth,
                },

                Quantizement.Half => c.MetricLevel switch
                {
                    MetricLevel.Beat => Quantizement.Half,
                    _ => Quantizement.Eighth
                },

                _ => c.Quantizement
            };
        }
    }
}
