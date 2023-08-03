using UnityEngine;
using System.Collections.Generic;
using SheetMusic;

namespace MusicTheory.Rhythms
{
    public static class RhythmGeneratorSystems
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
            return Random.value > .5f;
        }

        public static void GetNotes(this MusicSheet ms)
        {
            List<Note> notes = new();

            for (int m = 0; m < ms.Measures.Length; m++)
            {
                for (int c = 0; c < ms.Measures[m].Cells.Length; c++)
                {
                    notes.AddRange(ms.NotesFromCell(ms.Measures[m].Cells[c], m + 1));
                }
            }

            ms.Notes = notes;
        }

        public static List<Note> NotesFromCell(this MusicSheet ms, RhythmCell c, int measure)
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
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    break;
                case CellShape.DLL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.E),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.E)
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
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    break;
                case CellShape.LL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.N),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.N)
                        }
                    });
                    break;
                case CellShape.LSS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.N),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.N)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.A),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.A)
                        }
                    });
                    break;
                case CellShape.SSL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.D)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.E),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.E)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.N),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.N)
                        }
                    });
                    break;
                case CellShape.SLS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.D)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Half),
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.E),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.E)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.A),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.A)
                        }
                    });
                    break;
                case CellShape.SL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.D)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.DotHalf),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.E),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.E)
                        }
                    });
                    break;
                case CellShape.LS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.DotHalf),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.A),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.A)
                        }
                    });
                    break;
                case CellShape.SSSS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.E),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.E)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.N),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.N)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.A),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(SubBeatAssignment.A)
                        }
                    });
                    break;
                case CellShape.TSSS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.T),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(ms.RhythmSpecs.HasTriplets ? SubBeatAssignment.T : SubBeatAssignment.E)
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.L),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(ms.RhythmSpecs.HasTriplets ? SubBeatAssignment.L : SubBeatAssignment.N)
                        }
                    });
                    break;
                case CellShape.TSL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripHalf : RhythmicValue.Half),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.T),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(ms.RhythmSpecs.HasTriplets ? SubBeatAssignment.T : SubBeatAssignment.E)
                        }
                    });
                    break;
                case CellShape.TLS:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripHalf : RhythmicValue.Half),
                        Rest = c.Rest,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
                        }
                    });
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(ms.RhythmSpecs.HasTriplets ? RhythmicValue.TripQuarter : RhythmicValue.Quarter),
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.L),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = c.GetQuantizedSubBeatAssignment(ms.RhythmSpecs.HasTriplets ? SubBeatAssignment.L : SubBeatAssignment.N)
                        }
                    });
                    break;
                case CellShape.TL:
                    notes.Add(new Note()
                    {
                        ParentCell = c,
                        QuantizedRhythmicValue = c.GetQuantizedRhythmicValue(RhythmicValue.TripWhole),
                        Rest = c.Rest,
                        TiesTo = c.TiedTo,
                        BeatLocation = new BeatLocation
                        {
                            Count = c.GetQuantizedCount(SubBeatAssignment.D),
                            MeasureNumber = (MeasureNumber)measure,
                            SubBeatAssignment = SubBeatAssignment.D
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
                SubBeatAssignment.E => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 1,
                    _ => cell.Count
                },

                SubBeatAssignment.N => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 2,
                    MetricLevel.D1 => cell.Count + 1,
                    _ => cell.Count
                },

                SubBeatAssignment.A => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 3,
                    MetricLevel.D1 => cell.Count + 1,
                    _ => cell.Count
                },

                SubBeatAssignment.T => cell.MetricLevel switch
                {
                    MetricLevel.Beat => cell.Count + 1,
                    MetricLevel.BeatT => cell.Count + 1,
                    _ => cell.Count
                },

                SubBeatAssignment.L => cell.MetricLevel switch
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
                RhythmicValue.TripWhole => c.Quantizement switch
                {
                    Quantizement.Sixteenth => RhythmicValue.DotEighth,
                    Quantizement.Eighth => RhythmicValue.DotQuarter,
                    _ => RhythmicValue.DotHalf
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
                SubBeatAssignment.E => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Eighth => SubBeatAssignment.N,
                    Quantizement.Sixteenth => SubBeatAssignment.E,
                    _ => SubBeatAssignment.D,
                },
                SubBeatAssignment.N => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Sixteenth => SubBeatAssignment.N,
                    _ => SubBeatAssignment.D,
                },
                SubBeatAssignment.A => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Eighth => SubBeatAssignment.N,
                    Quantizement.Sixteenth => SubBeatAssignment.A,
                    _ => SubBeatAssignment.D,
                },
                SubBeatAssignment.T => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Quarter => SubBeatAssignment.L,
                    _ => SubBeatAssignment.T,//half and eighth
                },
                SubBeatAssignment.L => c.QuantizedBeatlevel() switch
                {
                    Quantizement.Quarter => SubBeatAssignment.T,
                    _ => SubBeatAssignment.L,
                },
                _ => Debugger()
            };

            SubBeatAssignment Debugger()
            {
                Debug.Log(nameof(GetQuantizedSubBeatAssignment) + " " + s);
                return SubBeatAssignment.D;
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
                _ => c.Quantizement
            };
        }
    }
}





//public enum CellShape { w, dhq, hh, qdh, hqq, qqh, qhq, qqqq, thq, tqh, tqqq, tw, }


//
//Whole note             = 64 : 240 / BPM
//Half note              = 32 : 120 / BPM
//Dotted quarter note    = 24 : 90 / BPM
//Quarter note           = 16 : 60 / BPM
//Dotted eighth note     = 12 : 45 / BPM
//Triplet quarter note   = 10 : ??
//Eighth note            =  8 : 30 / BPM
//Triplet eighth note    =  6 : 20 / BPM
//Sixteenth note         =  4 : 15 / BPM
//


/*
 *           return s switch
            {
                SubBeatAssignment.E => cell.Quantizement switch
                {
                    Quantizement.Sixteenth => cell.Count,
                    Quantizement.Eighth => cell.MetricLevel == MetricLevel.Beat ? cell.Count + 1 : cell.Count,
                    _ => cell.Count + 1,
                },

                SubBeatAssignment.N => cell.Quantizement switch
                {
                    Quantizement.Sixteenth => cell.Count,
                    Quantizement.Eighth => cell.MetricLevel == MetricLevel.Beat ? cell.Count + 2 : cell.Count + 1,
                    _ => cell.Count + 2,
                },

                SubBeatAssignment.A => cell.Quantizement switch
                {
                    Quantizement.Sixteenth => cell.Count,
                    Quantizement.Eighth => cell.MetricLevel == MetricLevel.Beat ? cell.Count + 2 : cell.Count + 1,
                    _ => cell.Count + 3,
                },

                SubBeatAssignment.T => cell.Quantizement switch
                {
                    Quantizement.EighthTrips => cell.Count,
                    Quantizement.Eighth => cell.MetricLevel == MetricLevel.Beat ? cell.Count + 1 : cell.Count,
                    Quantizement.Quarter => cell.Count + 1,
                    Quantizement.QuarterTrips => cell.Count + 1,
                    _ => cell.Count + 2,
                },

                SubBeatAssignment.L => cell.Quantizement switch
                {
                    Quantizement.EighthTrips => cell.Count,
                    Quantizement.Eighth => cell.MetricLevel == MetricLevel.Beat ? cell.Count + 2 : cell.Count + 1,
                    Quantizement.Quarter => cell.Count + 2,
                    Quantizement.QuarterTrips => cell.Count + 2,
                    _ => cell.Count + 2,
                },
                _ => cell.Count,
            };
 * 
 */