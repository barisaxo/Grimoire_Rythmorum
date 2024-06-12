// using System.Collections.Generic;
// using MusicTheory.Rhythms;
// namespace Batterie
// {
//     public class RhythmBars
//     {
//         public RhythmBars(int numberOfMeasures)
//         {
//             Measures = new RhythmCell[numberOfMeasures][];
//             // GD = gd;
//         }

//         public bool Ties;
//         public bool Rests;
//         public float Tempo;
//         public List<Note> Notes = new List<Note>();
//         public RhythmCell[][] Measures = null;
//         public List<MappedBeat> BeatMap = new();
//         public SubDivisionTier SubDivision;
//         public List<RhythmOption> Options = new();
//         // readonly GameplayData GD;

//         public RhythmBars AddRhythmOption(RhythmOption option) { Options.Add(option); return this; }
//         public RhythmBars SetTempo(float tempo) { Tempo = tempo; return this; }
//         public RhythmBars SetSubDivision(SubDivisionTier tier) { SubDivision = tier; return this; }
//         public RhythmBars SetNumberOfMeasures(int numberOfMeasures) { Measures = new RhythmCell[numberOfMeasures][]; return this; }
//         public RhythmBars SetSpecificRhythms(RhythmCell[][] rhythms) { Measures = rhythms; return this; }
//         public RhythmBars UseRandomTies() { Ties = true; return this; }
//         public RhythmBars UseRandomRests() { Rests = true; return this; }

//         // public RhythmBars ConstructRhythmBars(bool random)
//         // {
//         //     if (random)
//         //     {
//         //         this.CreateRandomCells();
//         //         this.AssignCellSubDivision();
//         //         this.AssignRandomCellShapes(GD);
//         //     }
//         //     if (Ties) this.AssignRandomTies();
//         //     if (Rests) this.AssignRandomRests();

//         //     this.AddNotes();
//         //     this.MapBeats();

//         //     return this;
//         // }

//     }

//     public enum BatteryMode { Real, Practice, Boss }
// }





// //TODO TODO TODO make time different time signatures
// //public int TimeSignatureTop;
// //public int TimeSignatureBottom;

// // public Rhythm4Bars(float tempo, List<RhythmOption> options, SubDivisionTier subDivision)
// // {
// //     Tempo = tempo;
// //     Options = options;
// //     SubDivision = subDivision;
// //     Measures = new RhythmCell[4][];
// //     Notes = new List<Note>();
// //     BeatMap = new();

// //     CreateCells();
// //     AssignCellSubDivision();
// //     AssignCellShapes();
// //     AssignTies();
// //     AssignRests();
// //     AddNotes();
// //     MapBeats();
// // }
