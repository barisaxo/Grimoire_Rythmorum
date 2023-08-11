//using UnityEngine;
//namespace 
//{
//    public static class RhythmFactory
//    {
//        public static RhythmCell[][] CreateRandomEmptyCells(this MusicSheet ms)
//        {
//            RhythmCell[][] measures = new RhythmCell[ms.RhythmSpecs.NumberOfMeasures][];

//            for (int m = 0; m < measures.Length; m++)
//            {
//                measures[m] = new RhythmCell[AssignRandomCellCount()];

//            }


//            return measures;
//            int AssignRandomCellCount()
//            {

//                int i = 0;
//                i = ms.RhythmSpecs.Time switch
//                {
//                    _ when ms.RhythmSpecs.Time.Signature == TimeSignature.TwoTwo =>
//                        ms.RhythmSpecs.SubDivisionTier switch
//                        {
//                            SubDivisionTier.BeatAndD1 => ms.RhythmSpecs.HasTriplets ? Random.Range(1, 5) : Random.Range(1, 3),
//                            SubDivisionTier.D1Only => ms.RhythmSpecs.HasTriplets ? Random.Range(1, 5) : 2,
//                            SubDivisionTier.D1AndD2 => Random.Range(2, 5),
//                            SubDivisionTier.D2Only => 4,
//                            _ => 1,
//                        },

//                    _ when ms.RhythmSpecs.Time.Signature == TimeSignature.ThreeTwo =>
//                   ms.RhythmSpecs.SubDivisionTier switch
//                   {
//                       SubDivisionTier.BeatAndD1 => ms.RhythmSpecs.HasTriplets ? Random.Range(1, 5) : Random.Range(1, 3),
//                       SubDivisionTier.D1Only => ms.RhythmSpecs.HasTriplets ? Random.Range(1, 5) : 2,
//                       SubDivisionTier.D1AndD2 => Random.Range(2, 5),
//                       SubDivisionTier.D2Only => 4,
//                       _ => 1,
//                   },

//                    _ => 0
//                };
//                return i;
//                //int x = 0;
//                //x += ms.RhythmSpecs.Meter switch
//                //{
//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleDuple => 1,// 2/*
//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleTriple => 1,// 3/*
//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleQuadruple => 1,// 4/*

//                //    _ when ms.RhythmSpecs.Meter == Meter.CompoundDuple => 2,// 6/*
//                //    _ when ms.RhythmSpecs.Meter == Meter.CompoundTriple => 3,// 9/*
//                //    _ when ms.RhythmSpecs.Meter == Meter.CompoundQuadruple => 4,// 12/*

//                //    _ when ms.RhythmSpecs.Meter == Meter.IrregularDupleTriple => 2,// 2+3/*
//                //    _ when ms.RhythmSpecs.Meter == Meter.IrregularTripleDuple => 2,// 3+2/*
//                //    _ when ms.RhythmSpecs.Meter == Meter.IrregularQuadrupleTriple => 2,// 4+3/*
//                //    _ when ms.RhythmSpecs.Meter == Meter.IrregularTripleQuadruple => 2,// 3+4/*
//                //    _ => 0,
//                //};

//                //x *= ms.RhythmSpecs.Meter.Pulses[0] switch
//                //{
//                //    PulseStress.Duple => ms.RhythmSpecs.SmallestMetricLevel switch
//                //    {
//                //        MetricLevel.Beat => 1,
//                //        MetricLevel.D1 => 2,
//                //    },
//                //};

//                //return x;

//                //(int min, int max) cellCount = ms.RhythmSpecs.Meter switch
//                //{
//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleDuple =>
//                //         ms.RhythmSpecs.SmallestMetricLevel switch
//                //         {
//                //             MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? (2, 4) :
//                //                ms.RhythmSpecs.SubDivisionTier == SubDivisionTier.BeatAndD1 ? (1, 2) : (2, 2),
//                //             MetricLevel.D2 => ms.RhythmSpecs.SubDivisionTier == SubDivisionTier.D2Only ? (4, 4) : (2, 4),
//                //             _ => (1, 1),
//                //         },

//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleTriple =>
//                //    ms.RhythmSpecs.SmallestMetricLevel switch
//                //    {
//                //        MetricLevel.D1 => ms.RhythmSpecs.SubDivisionTier == SubDivisionTier.BeatAndD1 ? (1, 3) : (3, 3),
//                //        MetricLevel.D2 => ms.RhythmSpecs.SubDivisionTier == SubDivisionTier.D2Only ? (4, 4) : (2, 4),
//                //        _ => (1, 1),
//                //    },


//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleQuadruple =>
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? (2, 4) :
//                //               ms.RhythmSpecs.SubDivisionTier == SubDivisionTier.BeatAndD1 ? (1, 2) : (2, 2),
//                //            MetricLevel.D2 => ms.RhythmSpecs.SubDivisionTier == SubDivisionTier.D2Only ? (4, 4) : (2, 4),
//                //            _ => (1, 1),
//                //        },

//                //    _ when ms.RhythmSpecs.Meter == Meter.CompoundDuple =>// 6/*
//                //   ms.RhythmSpecs.SmallestMetricLevel switch
//                //   {
//                //       MetricLevel.Beat => (2, 2),
//                //       MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 6,
//                //       _ => Random.Range(2, 5),
//                //   },
//                //};

//                //int i = ms.RhythmSpecs.Meter switch
//                //{
//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleDuple => // 2/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 1,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(3, 5),
//                //        },
//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleTriple => // 3/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 1,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },
//                //    _ when ms.RhythmSpecs.Meter == Meter.SimpleQuadruple => // 4/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 1,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },

//                //    _ when ms.RhythmSpecs.Meter == Meter.CompoundDuple =>// 6/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 2,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },
//                //    _ when ms.RhythmSpecs.Meter == Meter.CompoundTriple => // 9/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 1,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },
//                //    _ when ms.RhythmSpecs.Meter == Meter.CompoundQuadruple => // 12/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 4,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },


//                //    _ when ms.RhythmSpecs.Meter == Meter.IrregularDupleTriple => // 2+3/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 1,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },
//                //    _ when ms.RhythmSpecs.Meter == Meter.IrregularQuadrupleTriple => // 4+3/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 1,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },
//                //    _ when ms.RhythmSpecs.Meter == Meter.IrregularTripleDuple => // 3+2/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 1,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },
//                //    _ when ms.RhythmSpecs.Meter == Meter.IrregularTripleQuadruple => // 3+4/*
//                //        ms.RhythmSpecs.SmallestMetricLevel switch
//                //        {
//                //            MetricLevel.Beat => 1,
//                //            MetricLevel.D1 => ms.RhythmSpecs.HasTriplets ? Random.Range(2, 5) : 2,
//                //            _ => Random.Range(2, 5),
//                //        },

//                //    _ => 0
//                //};
//                //return i;
//            }
//        }

//        public static void AssignRandomCellSubDivisions(this RhythmCell[][] measures, RhythmSpecs specs)
//        {
//            for (int i = 0; i < measures.Length; i++)
//            {
//                switch (measures[i].Length)
//                {
//                    case 1:
//                        SingleCellMeasure(i);
//                        break;

//                    case 2:
//                        DoubleCellMeasure(i);
//                        break;

//                    case 3:
//                        TripleCellMeasure(i);
//                        break;

//                    case 4:
//                        QuadCellMeasure(i);
//                        break;
//                };
//            }

//            void SingleCellMeasure(int i)
//            {
//                measures[i][0].Quantizement = Quantizement.Quarter;
//            }

//            void DoubleCellMeasure(int i)
//            {
//                if (specs.SubDivisionTier == SubDivisionTier.BeatOnly)//trips only implied
//                {
//                    measures[i][0].Quantizement = Quantizement.QuarterTrips;
//                    measures[i][1].Quantizement = Quantizement.QuarterTrips;
//                }
//                else if (specs.RhythmOptions.Contains(RhythmOption.SomeTrips))
//                {
//                    measures[i][0].Quantizement = UnityEngine.Random.value < .8f ?
//                        Quantizement.Eighth : Quantizement.QuarterTrips;
//                    measures[i][1].Quantizement = UnityEngine.Random.value < .8f ?
//                        Quantizement.Eighth : Quantizement.QuarterTrips;
//                }
//                else
//                {
//                    measures[i][0].Quantizement = Quantizement.Eighth;
//                    measures[i][1].Quantizement = Quantizement.Eighth;
//                }
//            }

//            void TripleCellMeasure(int i)
//            {
//                switch (specs.SubDivisionTier)
//                {
//                    case SubDivisionTier.BeatAndD1:
//                        if (specs.RhythmOptions.Contains(RhythmOption.SomeTrips))
//                        {
//                            switch (UnityEngine.Random.value < .5f)
//                            {
//                                case true://long short short
//                                    measures[i][0].LongCell = true;
//                                    measures[i][0].Quantizement = UnityEngine.Random.value < .8f ?
//                                        Quantizement.Eighth : Quantizement.QuarterTrips;
//                                    measures[i][1].Quantizement = Quantizement.EighthTrips;
//                                    measures[i][2].Quantizement = Quantizement.EighthTrips;
//                                    break;

//                                case false://short short long
//                                    measures[i][0].Quantizement = Quantizement.EighthTrips;
//                                    measures[i][1].Quantizement = Quantizement.EighthTrips;
//                                    measures[i][2].Quantizement = UnityEngine.Random.value < .8f ?
//                                        Quantizement.Eighth : Quantizement.QuarterTrips;
//                                    measures[i][2].LongCell = true;
//                                    break;
//                            }
//                        }
//                        else //trips only is implied
//                        {
//                            switch (UnityEngine.Random.value < .5f)
//                            {
//                                case true://long short short
//                                    measures[i][0].LongCell = true;
//                                    measures[i][0].Quantizement = Quantizement.QuarterTrips;
//                                    measures[i][1].Quantizement = Quantizement.EighthTrips;
//                                    measures[i][2].Quantizement = Quantizement.EighthTrips;
//                                    break;

//                                case false://short short long
//                                    measures[i][0].Quantizement = Quantizement.EighthTrips;
//                                    measures[i][1].Quantizement = Quantizement.EighthTrips;
//                                    measures[i][2].Quantizement = Quantizement.QuarterTrips;
//                                    measures[i][2].LongCell = true;
//                                    break;
//                            }
//                        }
//                        break;

//                    case SubDivisionTier.D1AndD2:
//                        if (specs.RhythmOptions.Contains(RhythmOption.SomeTrips))
//                        {
//                            switch (UnityEngine.Random.value < .5f)
//                            {
//                                case true://long short short
//                                    measures[i][0].LongCell = true;
//                                    measures[i][0].Quantizement = Quantizement.Eighth;
//                                    measures[i][1].Quantizement = UnityEngine.Random.value < .8f ?
//                                           Quantizement.Sixteenth : Quantizement.EighthTrips;
//                                    measures[i][2].Quantizement = UnityEngine.Random.value < .8f ?
//                                        Quantizement.Sixteenth : Quantizement.EighthTrips;
//                                    break;

//                                case false://short short long
//                                    measures[i][0].Quantizement = UnityEngine.Random.value < .8f ?
//                                           Quantizement.Sixteenth : Quantizement.EighthTrips;
//                                    measures[i][1].Quantizement = UnityEngine.Random.value < .8f ?
//                                         Quantizement.Sixteenth : Quantizement.EighthTrips;
//                                    measures[i][2].Quantizement = Quantizement.Eighth;
//                                    measures[i][2].LongCell = true;
//                                    break;
//                            };
//                        }
//                        else//no trips is implied
//                        {
//                            switch (UnityEngine.Random.value < .5f)
//                            {
//                                case true://long short short
//                                    measures[i][0].LongCell = true;
//                                    measures[i][0].Quantizement = Quantizement.Eighth;
//                                    measures[i][1].Quantizement = Quantizement.Sixteenth;
//                                    measures[i][2].Quantizement = Quantizement.Sixteenth;
//                                    break;

//                                case false://short short long
//                                    measures[i][0].Quantizement = Quantizement.Sixteenth;
//                                    measures[i][1].Quantizement = Quantizement.Sixteenth;
//                                    measures[i][2].Quantizement = Quantizement.Eighth;
//                                    measures[i][2].LongCell = true;
//                                    break;
//                            };

//                        }
//                        break;

//                    case SubDivisionTier.D1Only://Some trips is implied
//                        switch (UnityEngine.Random.value < .5f)
//                        {
//                            case true://long short short
//                                measures[i][0].LongCell = true;
//                                measures[i][0].Quantizement = Quantizement.Eighth;
//                                measures[i][1].Quantizement = Quantizement.EighthTrips;
//                                measures[i][2].Quantizement = Quantizement.EighthTrips;
//                                break;

//                            case false://short short long
//                                measures[i][0].Quantizement = Quantizement.EighthTrips;
//                                measures[i][1].Quantizement = Quantizement.EighthTrips;
//                                measures[i][2].Quantizement = Quantizement.Eighth;
//                                measures[i][2].LongCell = true;
//                                break;
//                        }
//                        break;
//                };
//            }

//            void QuadCellMeasure(int i)
//            {
//                if (specs.SubDivisionTier == SubDivisionTier.D1Only ||
//                     specs.SubDivisionTier == SubDivisionTier.BeatAndD1 ||
//                     specs.RhythmOptions.Contains(RhythmOption.TripsOnly))
//                {
//                    for (int ii = 0; ii < 4; ii++)
//                    {
//                        measures[i][ii].Quantizement = Quantizement.EighthTrips;
//                    }
//                }
//                else if (specs.RhythmOptions.Contains(RhythmOption.SomeTrips))
//                {
//                    for (int ii = 0; ii < 4; ii++)
//                    {
//                        measures[i][ii].Quantizement = UnityEngine.Random.value < .5f ?
//                                    Quantizement.Sixteenth : Quantizement.EighthTrips;
//                    }
//                }
//                else
//                {
//                    for (int ii = 0; ii < 4; ii++)
//                    {
//                        measures[i][ii].Quantizement = Quantizement.Sixteenth;
//                    }
//                }
//            }
//        }

//        public static void AssignRandomCellShapes(this RhythmCell[][] measures, GameplayData gd)
//        {
//            for (int i = 0; i < measures.Length; i++)
//            {
//                for (int ii = 0; ii < measures[i].Length; ii++)
//                {
//                    measures[i][ii].Shape = measures[i][ii].Quantizement switch
//                    {
//                        Quantizement.QuarterTrips => RandomTrips(),

//                        Quantizement.EighthTrips => RandomTrips(),

//                        _ => RandomNonTrip()
//                    };
//                }
//            }
//            CellShape RandomTrips()
//            {
//                var c = (CellShape)UnityEngine.Random.Range(8, 11);
//                return gd.RecentCell = c == gd.RecentCell ? (CellShape)11 : c;
//            }
//            CellShape RandomNonTrip()
//            {
//                var c = (CellShape)UnityEngine.Random.Range(1, 8);
//                return gd.RecentCell = c == gd.RecentCell ? 0 : c;
//            }
//        }

//        public static void AssignRandomTies(this RhythmCell[][] measures)
//        {
//            for (int i = 0; i < measures.Length; i++)
//            {
//                switch (measures[i].Length)
//                {
//                    case 1:
//                        if (i == measures.Length - 1) { break; }//Last bar
//                        measures[i][0].TiedTo = RulesCheckAndRandomChance(i);
//                        break;

//                    case 2:
//                        if (measures[i][0].Shape == CellShape.LL) break;
//                        measures[i][0].TiedTo = RulesCheckAndRandomChance(i);
//                        if (i == measures.Length - 1) break; //Last bar
//                        measures[i][1].TiedTo = RulesCheckAndRandomChance(i);
//                        break;

//                    case 3:
//                        measures[i][0].TiedTo = measures[i][0].LongCell && RulesCheckAndRandomChance(i);//if this cell is short, it cannot tie
//                        measures[i][1].TiedTo = !measures[i][0].LongCell && RulesCheckAndRandomChance(i);//if the first cell is long, this cannot tie
//                        if (i == measures.Length - 1) { break; }//Last bar
//                        measures[i][2].TiedTo = RulesCheckAndRandomChance(i);
//                        break;

//                    case 4:
//                        measures[i][0].TiedTo = RulesCheckAndRandomChance(i);
//                        measures[i][1].TiedTo = RulesCheckAndRandomChance(i);
//                        measures[i][2].TiedTo = RulesCheckAndRandomChance(i);
//                        if (i == measures.Length - 1) { break; }//Last bar
//                        measures[i][3].TiedTo = RulesCheckAndRandomChance(i);
//                        break;
//                }
//            }

//            bool RulesCheckAndRandomChance(int i)
//            {
//                //don't tie half note to half note.
//                if (measures[i][0].Quantizement == Quantizement.Eighth &&
//                    measures[i][0].Shape == CellShape.L &&
//                    measures[i][1].Quantizement == Quantizement.Eighth &&
//                    measures[i][1].Shape == CellShape.L)
//                {
//                    return false;
//                }

//                return UnityEngine.Random.value < .333f;
//            }
//        }

//        public static void AssignRandomRests(this RhythmCell[][] measures)
//        {
//            // if (!ms.Options.Contains(RhythmOption.Rests)) return;
//            for (int i = 0; i < measures.Length; i++)
//            {
//                for (int ii = 0; ii < measures[i].Length; ii++)
//                {
//                    if (measures[i].Length == 1 && measures[i][ii].TiedTo) continue;//whole note check
//                    if (ii == 0 && i > 0 && measures[i - 1][^1].TiedTo) continue;//previous measure check
//                    if (ii > 0 && measures[i][ii - 1].TiedTo) continue;//previous cell check
//                    if (measures[i][ii].Shape != CellShape.L)//no long cell rests; having problems with ties to rests, this is an easy fix, hopefully
//                    {
//                        measures[i][ii].Rest = UnityEngine.Random.value < .4f;
//                    }
//                }
//            }
//        }

//        //public static void AssignRhythmicNoteValues(this MusicSheet ms)
//        //{
//        //    for (int m = 0; m < ms.Measures.Length; m++)
//        //    {
//        //        AssignCellRhythmicNoteValues(m);
//        //    }

//        //    void AssignCellRhythmicNoteValues(int m)
//        //    {
//        //        for (int c = 0; c < ms.Measures[m].Length; c++)
//        //        {
//        //            switch (ms.Measures[m][c].Shape)
//        //            {
//        //                case CellShape.L:
//        //                    ms.Notes.Add(new Note(
//        //                         rest: ms.Measures[m][c].Rest,
//        //                         tied: ms.Measures[m][c].TiedTo,
//        //                         trip: false,
//        //                         parentCell: ms.Measures[m][c],
//        //                         beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                         rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Whole, ms.Measures[m][c].Quantizement)
//        //                         ));
//        //                    break;


//        //                case CellShape.LS:
//        //                    ms.Notes.Add(new Note(
//        //                        rest: ms.Measures[m][c].Rest,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.DotHalf, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.For, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.LL:
//        //                    ms.Notes.Add(new Note(
//        //                        rest: ms.Measures[m][c].Rest,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Half, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Thr, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Half, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.SL:
//        //                    ms.Notes.Add(new Note(
//        //                        rest: ms.Measures[m][c].Rest,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Two, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.DotHalf, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.LSS:
//        //                    ms.Notes.Add(new Note(
//        //                        rest: ms.Measures[m][c].Rest,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Half, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Thr, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.For, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.SLS:
//        //                    ms.Notes.Add(new Note(
//        //                        rest: ms.Measures[m][c].Rest,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Two, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Half, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.For, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.SSL:
//        //                    ms.Notes.Add(new Note(
//        //                          rest: ms.Measures[m][c].Rest,
//        //                          tied: false,
//        //                          trip: false,
//        //                          parentCell: ms.Measures[m][c],
//        //                          beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                          rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                          ));
//        //                    ms.Notes.Add(new Note(
//        //                         rest: false,
//        //                         tied: false,
//        //                         trip: false,
//        //                         parentCell: ms.Measures[m][c],
//        //                         beatLocation: GetBeatLocation(m, c, CellPosition.Two, ms.Measures),
//        //                         rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                         ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Thr, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Half, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.SSSS:
//        //                    ms.Notes.Add(new Note(
//        //                         rest: ms.Measures[m][c].Rest,
//        //                         tied: false,
//        //                         trip: false,
//        //                         parentCell: ms.Measures[m][c],
//        //                         beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                         rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                         ));
//        //                    ms.Notes.Add(new Note(
//        //                         rest: false,
//        //                         tied: false,
//        //                         trip: false,
//        //                         parentCell: ms.Measures[m][c],
//        //                         beatLocation: GetBeatLocation(m, c, CellPosition.Two, ms.Measures),
//        //                         rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                         ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Thr, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                         rest: false,
//        //                         tied: ms.Measures[m][c].TiedTo,
//        //                         trip: false,
//        //                         parentCell: ms.Measures[m][c],
//        //                         beatLocation: GetBeatLocation(m, c, CellPosition.For, ms.Measures),
//        //                         rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.Quarter, ms.Measures[m][c].Quantizement)
//        //                         ));
//        //                    break;

//        //                case CellShape.TLS:
//        //                    ms.Notes.Add(new Note(
//        //                        rest: ms.Measures[m][c].Rest,
//        //                        tied: false,
//        //                        trip: false,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.TripHalf, ms.Measures[m][c].Quantizement)
//        //                     ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: true,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Thr, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.TripQuarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.TSL:
//        //                    ms.Notes.Add(new Note(
//        //                        rest: ms.Measures[m][c].Rest,
//        //                        tied: false,
//        //                        trip: true,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.TripQuarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: true,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Two, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.TripHalf, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.TSSS:
//        //                    ms.Notes.Add(new Note(
//        //                         rest: ms.Measures[m][c].Rest,
//        //                         tied: false,
//        //                         trip: true,
//        //                         parentCell: ms.Measures[m][c],
//        //                         beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                         rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.TripQuarter, ms.Measures[m][c].Quantizement)
//        //                         ));
//        //                    ms.Notes.Add(new Note(
//        //                         rest: false,
//        //                         tied: false,
//        //                         trip: true,
//        //                         parentCell: ms.Measures[m][c],
//        //                         beatLocation: GetBeatLocation(m, c, CellPosition.Two, ms.Measures),
//        //                         rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.TripQuarter, ms.Measures[m][c].Quantizement)
//        //                         ));
//        //                    ms.Notes.Add(new Note(
//        //                        rest: false,
//        //                        tied: ms.Measures[m][c].TiedTo,
//        //                        trip: true,
//        //                        parentCell: ms.Measures[m][c],
//        //                        beatLocation: GetBeatLocation(m, c, CellPosition.Thr, ms.Measures),
//        //                        rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.TripQuarter, ms.Measures[m][c].Quantizement)
//        //                        ));
//        //                    break;

//        //                case CellShape.TL:
//        //                    ms.Notes.Add(new Note(
//        //                         rest: ms.Measures[m][c].Rest,
//        //                         tied: ms.Measures[m][c].TiedTo,
//        //                         trip: true,
//        //                         parentCell: ms.Measures[m][c],
//        //                         beatLocation: GetBeatLocation(m, c, CellPosition.One, ms.Measures),
//        //                         rhythmicNoteValue: QuantizedRhythmicNoteValue(RhythmicValue.TripWhole, ms.Measures[m][c].Quantizement)
//        //                         )); break;
//        //            }
//        //        }
//        //    }
//        //}

//        private static RhythmicValue QuantizedRhythmicNoteValue(RhythmicValue nv, Quantizement q) => (nv, q) switch
//        {
//            (RhythmicValue.Whole, Quantizement.Quarter) => RhythmicValue.Whole,
//            (RhythmicValue.DotHalf, Quantizement.Quarter) => RhythmicValue.DotHalf,
//            (RhythmicValue.Half, Quantizement.Quarter) => RhythmicValue.Half,
//            (RhythmicValue.Quarter, Quantizement.Quarter) => RhythmicValue.Quarter,

//            (RhythmicValue.TripHalf, Quantizement.QuarterTrips) => RhythmicValue.TripHalf,
//            (RhythmicValue.TripQuarter, Quantizement.QuarterTrips) => RhythmicValue.TripQuarter,

//            (RhythmicValue.DotHalf, Quantizement.Eighth) => RhythmicValue.DotQuarter,
//            (RhythmicValue.Whole, Quantizement.Eighth) => RhythmicValue.Half,
//            (RhythmicValue.Half, Quantizement.Eighth) => RhythmicValue.Quarter,
//            (RhythmicValue.Quarter, Quantizement.Eighth) => RhythmicValue.Eighth,
//            (RhythmicValue.TripHalf, Quantizement.Eighth) => RhythmicValue.TripQuarter,
//            (RhythmicValue.TripQuarter, Quantizement.Eighth) => RhythmicValue.TripEighth,


//            (RhythmicValue.TripHalf, Quantizement.EighthTrips) => RhythmicValue.TripQuarter,
//            (RhythmicValue.TripQuarter, Quantizement.EighthTrips) => RhythmicValue.TripEighth,

//            (RhythmicValue.Whole, Quantizement.Sixteenth) => RhythmicValue.Quarter,
//            (RhythmicValue.DotHalf, Quantizement.Sixteenth) => RhythmicValue.DotEighth,
//            (RhythmicValue.Half, Quantizement.Sixteenth) => RhythmicValue.Eighth,
//            (RhythmicValue.Quarter, Quantizement.Sixteenth) => RhythmicValue.Sixteenth,

//            _ => RhythmicValue.Whole
//        };

//        private static BeatLocation GetBeatLocation(int measure, int cell, CellPosition pos, RhythmCell[][] measures)
//        {
//            var bl = GetBeatLoc(measure, cell, pos, measures);
//            return new BeatLocation { MeasureNumber = bl.Item1, Count = bl.Item2, SubBeatAssignment = bl.Item3 };
//        }
//        private static (MeasureNumber, Count, SubBeatAssignment) GetBeatLoc(int measure, int cell, CellPosition pos, RhythmCell[][] measures)
//        {
//            if (measures[measure].Length == 3)
//            {
//                if (measures[measure][0].LongCell && (cell == 1 || cell == 2))
//                {
//                    return (measures[measure][cell].Quantizement, cell, pos) switch
//                    {
//                        (Quantizement.Sixteenth, 1, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                        (Quantizement.Sixteenth, 1, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.E),
//                        (Quantizement.Sixteenth, 1, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.N),
//                        (Quantizement.Sixteenth, 1, CellPosition.For) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.A),

//                        (Quantizement.Sixteenth, 2, CellPosition.One) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.D),
//                        (Quantizement.Sixteenth, 2, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.E),
//                        (Quantizement.Sixteenth, 2, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.N),
//                        (Quantizement.Sixteenth, 2, CellPosition.For) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.A),

//                        (Quantizement.EighthTrips, 1, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                        (Quantizement.EighthTrips, 1, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.T),
//                        (Quantizement.EighthTrips, 1, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.L),

//                        (Quantizement.EighthTrips, 2, CellPosition.One) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.D),
//                        (Quantizement.EighthTrips, 2, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.T),
//                        (Quantizement.EighthTrips, 2, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.L),


//                        _ => DebugLogBeat(measure, cell, pos, measures[measure][cell].Quantizement)
//                    };
//                }
//                else if (measures[measure][2].LongCell && cell == 2)
//                {
//                    return (measures[measure][cell].Quantizement, cell, pos) switch
//                    {
//                        (Quantizement.Eighth, 2, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                        (Quantizement.Eighth, 2, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.N),
//                        (Quantizement.Eighth, 2, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.D),
//                        (Quantizement.Eighth, 2, CellPosition.For) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.N),

//                        (Quantizement.QuarterTrips, 2, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                        (Quantizement.QuarterTrips, 2, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.L),
//                        (Quantizement.QuarterTrips, 2, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.T),

//                        _ => DebugLogBeat(measure, cell, pos, measures[measure][cell].Quantizement)
//                    };
//                }
//            }

//            return (measures[measure][cell].Quantizement, cell, pos) switch
//            {
//                (Quantizement.Quarter, 0, CellPosition.One) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.D),
//                (Quantizement.Quarter, 0, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.D),
//                (Quantizement.Quarter, 0, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                (Quantizement.Quarter, 0, CellPosition.For) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.D),

//                (Quantizement.Eighth, 0, CellPosition.One) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.D),
//                (Quantizement.Eighth, 0, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.N),
//                (Quantizement.Eighth, 0, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.D),
//                (Quantizement.Eighth, 0, CellPosition.For) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.N),

//                (Quantizement.Eighth, 1, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                (Quantizement.Eighth, 1, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.N),
//                (Quantizement.Eighth, 1, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.D),
//                (Quantizement.Eighth, 1, CellPosition.For) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.N),

//                (Quantizement.Eighth, 2, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                (Quantizement.Eighth, 2, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.N),
//                (Quantizement.Eighth, 2, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.D),
//                (Quantizement.Eighth, 2, CellPosition.For) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.N),

//                (Quantizement.Sixteenth, 0, CellPosition.One) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.D),
//                (Quantizement.Sixteenth, 0, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.E),
//                (Quantizement.Sixteenth, 0, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.N),
//                (Quantizement.Sixteenth, 0, CellPosition.For) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.A),

//                (Quantizement.Sixteenth, 1, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.D),
//                (Quantizement.Sixteenth, 1, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.E),
//                (Quantizement.Sixteenth, 1, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.N),
//                (Quantizement.Sixteenth, 1, CellPosition.For) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.A),

//                (Quantizement.Sixteenth, 2, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                (Quantizement.Sixteenth, 2, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.E),
//                (Quantizement.Sixteenth, 2, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.N),
//                (Quantizement.Sixteenth, 2, CellPosition.For) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.A),

//                (Quantizement.Sixteenth, 3, CellPosition.One) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.D),
//                (Quantizement.Sixteenth, 3, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.E),
//                (Quantizement.Sixteenth, 3, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.N),
//                (Quantizement.Sixteenth, 3, CellPosition.For) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.A),

//                (Quantizement.QuarterTrips, 0, CellPosition.One) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.D),
//                (Quantizement.QuarterTrips, 0, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.L),
//                (Quantizement.QuarterTrips, 0, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.T),

//                (Quantizement.QuarterTrips, 1, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                (Quantizement.QuarterTrips, 1, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.L),
//                (Quantizement.QuarterTrips, 1, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.T),

//                //(Quantizement.QuarterTrips, 2, CellShapePosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),//should be covered above
//                //(Quantizement.QuarterTrips, 2, CellShapePosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.L),//because LongCell
//                //(Quantizement.QuarterTrips, 2, CellShapePosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.T),

//                (Quantizement.EighthTrips, 0, CellPosition.One) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.D),
//                (Quantizement.EighthTrips, 0, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.T),
//                (Quantizement.EighthTrips, 0, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.One, SubBeatAssignment.L),

//                (Quantizement.EighthTrips, 1, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.D),
//                (Quantizement.EighthTrips, 1, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.T),
//                (Quantizement.EighthTrips, 1, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Two, SubBeatAssignment.L),

//                (Quantizement.EighthTrips, 2, CellPosition.One) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.D),
//                (Quantizement.EighthTrips, 2, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.T),
//                (Quantizement.EighthTrips, 2, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.Thr, SubBeatAssignment.L),

//                (Quantizement.EighthTrips, 3, CellPosition.One) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.D),
//                (Quantizement.EighthTrips, 3, CellPosition.Two) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.T),
//                (Quantizement.EighthTrips, 3, CellPosition.Thr) => ((MeasureNumber)measure + 1, Count.For, SubBeatAssignment.L),

//                _ => DebugLogBeat(measure, cell, pos, measures[measure][cell].Quantizement)
//            };
//        }

//        private static (MeasureNumber, Count, SubBeatAssignment) DebugLogBeat(int m, int c, CellPosition p, Quantizement q)
//        {
//            UnityEngine.Debug.Log("NOTE ERROR: Measure: " + m + ", Cell: " + c + ", ShapePos: " + p + ", Quant: " + q);
//            return (MeasureNumber.One, Count.One, SubBeatAssignment.D);
//        }

//        public static void MapBeats(this MusicSheet ms)
//        {
//            //We'll see how to impliment this, last version needed a note primer to function properly.
//            ms.BeatMap.Add(new MappedBeat(0, NoteFunction.Ignore));

//            for (int i = 0; i < ms.Notes.Count; i++)
//            {
//                double noteValueSub = 0;
//                int noteValueSubCount = 0;

//                switch (ms.Notes[i].QuantizedRhythmicValue)
//                {
//                    case RhythmicValue.Whole:
//                        noteValueSub = (double)(15d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 16;
//                        break;

//                    case RhythmicValue.DotHalf:
//                        noteValueSub = (double)(15d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 12;
//                        break;

//                    case RhythmicValue.Half:
//                        noteValueSub = (double)(15d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 8;
//                        break;

//                    case RhythmicValue.DotQuarter:
//                        noteValueSub = (double)(15d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 6;
//                        break;

//                    case RhythmicValue.Quarter:
//                        noteValueSub = (double)(15d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 4;
//                        break;

//                    case RhythmicValue.DotEighth:
//                        noteValueSub = (double)(15d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 3;
//                        break;

//                    case RhythmicValue.Eighth:
//                        noteValueSub = (double)(15d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 2;
//                        break;

//                    case RhythmicValue.Sixteenth:
//                        noteValueSub = (double)(15d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 1;
//                        break;

//                    case RhythmicValue.TripHalf:
//                        noteValueSub = (double)(20d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 6;
//                        break;

//                    case RhythmicValue.TripQuarter:
//                        noteValueSub = (double)(20d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 3;
//                        break;

//                    case RhythmicValue.TripEighth:
//                        noteValueSub = (double)(20d / ms.RhythmSpecs.Tempo);
//                        noteValueSubCount = 1;
//                        break;
//                }

//                ms.BeatMap.Add(new MappedBeat(noteValueSub, ms.GetNoteFunction(i)));

//                if (ms.GetNoteFunction(i) == NoteFunction.Rest)
//                {
//                    for (int n = 1; n < noteValueSubCount; n++)
//                    {
//                        ms.BeatMap.Add(new MappedBeat(noteValueSub, NoteFunction.Rest));
//                    }
//                }
//                else
//                {
//                    for (int n = 1; n < noteValueSubCount; n++)
//                    {
//                        ms.BeatMap.Add(new MappedBeat(noteValueSub, NoteFunction.Hold));
//                    }
//                }
//            }

//            //Add a 16th note at the end to enforce proper release of last note.(might be currently ignored)//(15f / ms.RhythmSpecs.Tempo)
//            ms.BeatMap.Add(new MappedBeat(0, NoteFunction.Ignore));
//        }

//        private static NoteFunction GetNoteFunction(this MusicSheet ms, int i)
//        {
//            if (ms.Notes[i].Rest) return NoteFunction.Rest;
//            if (i != 0 && ms.Notes[i - 1].TiesTo) return NoteFunction.Hold;
//            return NoteFunction.Attack;
//        }
//    }
//}




///*
// *   private static (Count count, SubBeatAssignment beat) GetBeatAssignment(this MusicSheet ms, int m, int c, BeatAssignment beatLocation)
//        {
//            if (ms.Measures[m].Length == 3)
//            {
//                if (ms.Measures[m][0].LongCell && (c == 1 || c == 2))
//                {
//                    return (beatLocation, c, ms.Measures[m][c].Quantizement) switch
//                    {
//                        (BeatAssignment.One, 1, Quantizement.Sixteenth) => BeatAssignment.Thr,
//                        (BeatAssignment.Two, 1, Quantizement.Sixteenth) => BeatAssignment.ThrE,
//                        (BeatAssignment.Thr, 1, Quantizement.Sixteenth) => BeatAssignment.ThrN,
//                        (BeatAssignment.For, 1, Quantizement.Sixteenth) => BeatAssignment.ThrA,

//                        (BeatAssignment.One, 2, Quantizement.Sixteenth) => BeatAssignment.For,
//                        (BeatAssignment.Two, 2, Quantizement.Sixteenth) => BeatAssignment.ForE,
//                        (BeatAssignment.Thr, 2, Quantizement.Sixteenth) => BeatAssignment.ForN,
//                        (BeatAssignment.For, 2, Quantizement.Sixteenth) => BeatAssignment.ForA,

//                        (BeatAssignment.One, 1, Quantizement.EighthTrips) => BeatAssignment.Thr,
//                        (BeatAssignment.OneT, 1, Quantizement.EighthTrips) => BeatAssignment.ThrT,
//                        (BeatAssignment.OneL, 1, Quantizement.EighthTrips) => BeatAssignment.ThrL,

//                        (BeatAssignment.One, 2, Quantizement.EighthTrips) => BeatAssignment.For,
//                        (BeatAssignment.OneT, 2, Quantizement.EighthTrips) => BeatAssignment.ForT,
//                        (BeatAssignment.OneL, 2, Quantizement.EighthTrips) => BeatAssignment.ForL,

//                        (BeatAssignment.One, 2, Quantizement.QuarterTrips) => BeatAssignment.Thr,
//                        (BeatAssignment.OneT, 2, Quantizement.QuarterTrips) => BeatAssignment.ThrL,
//                        (BeatAssignment.OneL, 2, Quantizement.QuarterTrips) => BeatAssignment.ForT,

//                        _ => DebugLogBeat(beatLocation, c, ms.Measures[m][c].Quantizement)
//                    };
//                }

//                else if (ms.Measures[m][2].LongCell && c == 2)
//                {
//                    return (beatLocation, c, ms.Measures[m][c].Quantizement) switch
//                    {
//                        (BeatAssignment.One, 2, Quantizement.Eighth) => BeatAssignment.Thr,
//                        (BeatAssignment.Two, 2, Quantizement.Eighth) => BeatAssignment.ThrN,
//                        (BeatAssignment.Thr, 2, Quantizement.Eighth) => BeatAssignment.For,
//                        (BeatAssignment.For, 2, Quantizement.Eighth) => BeatAssignment.ForN,

//                        (BeatAssignment.One, 2, Quantizement.QuarterTrips) => BeatAssignment.Thr,
//                        (BeatAssignment.OneT, 2, Quantizement.QuarterTrips) => BeatAssignment.ThrL,
//                        (BeatAssignment.OneL, 2, Quantizement.QuarterTrips) => BeatAssignment.ForT,

//                        _ => DebugLogBeat(beatLocation, c, ms.Measures[m][c].Quantizement)
//                    };
//                }
//            }

//            return (beatLocation, c, ms.Measures[m][c].Quantizement) switch
//            {
//                (BeatAssignment.One, 0, Quantizement.Quarter) => BeatAssignment.One,
//                (BeatAssignment.Two, 0, Quantizement.Quarter) => BeatAssignment.Two,
//                (BeatAssignment.Thr, 0, Quantizement.Quarter) => BeatAssignment.Thr,
//                (BeatAssignment.For, 0, Quantizement.Quarter) => BeatAssignment.For,

//                (BeatAssignment.One, 0, Quantizement.Eighth) => BeatAssignment.One,
//                (BeatAssignment.Two, 0, Quantizement.Eighth) => BeatAssignment.OneN,
//                (BeatAssignment.Thr, 0, Quantizement.Eighth) => BeatAssignment.Two,
//                (BeatAssignment.For, 0, Quantizement.Eighth) => BeatAssignment.TwoN,

//                (BeatAssignment.One, 1, Quantizement.Eighth) => BeatAssignment.Thr,
//                (BeatAssignment.Two, 1, Quantizement.Eighth) => BeatAssignment.ThrN,
//                (BeatAssignment.Thr, 1, Quantizement.Eighth) => BeatAssignment.For,
//                (BeatAssignment.For, 1, Quantizement.Eighth) => BeatAssignment.ForN,

//                (BeatAssignment.One, 2, Quantizement.Eighth) => BeatAssignment.Thr,
//                (BeatAssignment.Two, 2, Quantizement.Eighth) => BeatAssignment.ThrN,
//                (BeatAssignment.Thr, 2, Quantizement.Eighth) => BeatAssignment.For,
//                (BeatAssignment.For, 2, Quantizement.Eighth) => BeatAssignment.ForN,

//                (BeatAssignment.One, 0, Quantizement.Sixteenth) => BeatAssignment.One,
//                (BeatAssignment.Two, 0, Quantizement.Sixteenth) => BeatAssignment.OneE,
//                (BeatAssignment.Thr, 0, Quantizement.Sixteenth) => BeatAssignment.OneN,
//                (BeatAssignment.For, 0, Quantizement.Sixteenth) => BeatAssignment.OneA,

//                (BeatAssignment.One, 1, Quantizement.Sixteenth) => BeatAssignment.Two,
//                (BeatAssignment.Two, 1, Quantizement.Sixteenth) => BeatAssignment.TwoE,
//                (BeatAssignment.Thr, 1, Quantizement.Sixteenth) => BeatAssignment.TwoN,
//                (BeatAssignment.For, 1, Quantizement.Sixteenth) => BeatAssignment.TwoA,

//                (BeatAssignment.One, 2, Quantizement.Sixteenth) => BeatAssignment.Thr,
//                (BeatAssignment.Two, 2, Quantizement.Sixteenth) => BeatAssignment.ThrE,
//                (BeatAssignment.Thr, 2, Quantizement.Sixteenth) => BeatAssignment.ThrN,
//                (BeatAssignment.For, 2, Quantizement.Sixteenth) => BeatAssignment.ThrA,

//                (BeatAssignment.One, 3, Quantizement.Sixteenth) => BeatAssignment.For,
//                (BeatAssignment.Two, 3, Quantizement.Sixteenth) => BeatAssignment.ForE,
//                (BeatAssignment.Thr, 3, Quantizement.Sixteenth) => BeatAssignment.ForN,
//                (BeatAssignment.For, 3, Quantizement.Sixteenth) => BeatAssignment.ForA,

//                (BeatAssignment.One, 0, Quantizement.QuarterTrips) => BeatAssignment.One,
//                (BeatAssignment.OneT, 0, Quantizement.QuarterTrips) => BeatAssignment.OneL,
//                (BeatAssignment.OneL, 0, Quantizement.QuarterTrips) => BeatAssignment.TwoT,

//                (BeatAssignment.One, 1, Quantizement.QuarterTrips) => BeatAssignment.Thr,
//                (BeatAssignment.OneT, 1, Quantizement.QuarterTrips) => BeatAssignment.ThrL,
//                (BeatAssignment.OneL, 1, Quantizement.QuarterTrips) => BeatAssignment.ForT,

//                (BeatAssignment.One, 2, Quantizement.QuarterTrips) => BeatAssignment.Thr,
//                (BeatAssignment.OneT, 2, Quantizement.QuarterTrips) => BeatAssignment.ThrL,
//                (BeatAssignment.OneL, 2, Quantizement.QuarterTrips) => BeatAssignment.ForT,

//                (BeatAssignment.One, 0, Quantizement.EighthTrips) => BeatAssignment.One,
//                (BeatAssignment.OneT, 0, Quantizement.EighthTrips) => BeatAssignment.OneT,
//                (BeatAssignment.OneL, 0, Quantizement.EighthTrips) => BeatAssignment.OneL,

//                (BeatAssignment.One, 1, Quantizement.EighthTrips) => BeatAssignment.Two,
//                (BeatAssignment.OneT, 1, Quantizement.EighthTrips) => BeatAssignment.TwoT,
//                (BeatAssignment.OneL, 1, Quantizement.EighthTrips) => BeatAssignment.TwoL,

//                (BeatAssignment.One, 2, Quantizement.EighthTrips) => BeatAssignment.Thr,
//                (BeatAssignment.OneT, 2, Quantizement.EighthTrips) => BeatAssignment.ThrT,
//                (BeatAssignment.OneL, 2, Quantizement.EighthTrips) => BeatAssignment.ThrL,

//                (BeatAssignment.One, 3, Quantizement.EighthTrips) => BeatAssignment.For,
//                (BeatAssignment.OneT, 3, Quantizement.EighthTrips) => BeatAssignment.ForT,
//                (BeatAssignment.OneL, 3, Quantizement.EighthTrips) => BeatAssignment.ForL,

//                _ => DebugLogBeat(beatLocation, c, ms.Measures[m][c].Quantizement)
//            };
//        }

//        //private static BeatAssignment DebugLogBeat(BeatAssignment bl, int cell, Quantizement q)
//        //{
//        //    UnityEngine.Debug.Log("NOTE ERROR: Beat Location: " + bl + ", Cell: " + cell + ", Quant: " + q);
//        //    return BeatAssignment.D;
//        //}
// * 
// * 
// *   public static RhythmCell[][] CreateRandomEmptyCells(this RhythmSpecs specs)
//        {
//            RhythmCell[][] measures = new RhythmCell[specs.NumberOfMeasures][];

//            for (int i = 0; i < measures.Length; i++)
//            {
//                switch (specs.SubDivisionTier)
//                {//this is assuming 4/4
//                    case SubDivisionTier.QuartersOnly:
//                        if (specs.RhythmOptions.Contains(RhythmOption.SomeTrips))
//                        {
//                            measures[i] = new RhythmCell[UnityEngine.Random.Range(1, 3)];
//                            break;
//                        }
//                        if (specs.RhythmOptions.Contains(RhythmOption.TripsOnly))
//                        {
//                            measures[i] = new RhythmCell[2];
//                            break;
//                        }
//                        measures[i] = new RhythmCell[1];
//                        break;

//                    case SubDivisionTier.EighthsOnly:
//                        if (specs.RhythmOptions.Contains(RhythmOption.SomeTrips))
//                        {
//                            measures[i] = new RhythmCell[UnityEngine.Random.Range(1, 5)];
//                            break;
//                        }
//                        if (specs.RhythmOptions.Contains(RhythmOption.TripsOnly))
//                        {
//                            measures[i] = new RhythmCell[4];
//                            break;
//                        }
//                        measures[i] = new RhythmCell[2];
//                        break;

//                    case SubDivisionTier.QuartersAndEighths:
//                        if (specs.RhythmOptions.Contains(RhythmOption.SomeTrips))
//                        {
//                            measures[i] = new RhythmCell[UnityEngine.Random.Range(1, 5)];
//                            break;
//                        }
//                        if (specs.RhythmOptions.Contains(RhythmOption.TripsOnly))
//                        {
//                            measures[i] = new RhythmCell[UnityEngine.Random.Range(2, 5)];
//                            break;
//                        }
//                        measures[i] = new RhythmCell[UnityEngine.Random.Range(1, 3)];
//                        break;

//                    case SubDivisionTier.EighthsAndSixteenths:
//                        if (specs.RhythmOptions.Contains(RhythmOption.TripsOnly))
//                        {
//                            measures[i] = new RhythmCell[4];
//                            break;
//                        }
//                        measures[i] = new RhythmCell[UnityEngine.Random.Range(2, 5)];
//                        break;

//                    case SubDivisionTier.SixteenthsOnly:
//                        measures[i] = new RhythmCell[4];
//                        break;
//                };

//                for (int ii = 0; ii < measures[i].Length; ii++)
//                {
//                    measures[i][ii] = new RhythmCell();
//                }
//            }

//            return measures;
//        }

// */