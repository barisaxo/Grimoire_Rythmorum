//using Dialogs;
//using BBatteries;

//public class W_Dialogue : Dialogue
//{
//    public W_Dialogue(State consequentState) : base(consequentState) { }

//    public override Dialogue Initiate()
//    {
//        MuteTypingSounds();
//        FirstLine = StartLine;
//        FirstLine.Responses[0].SetGoToLine(StartLine);


//        return this;
//    }

//    Line _startLine;
//    Line StartLine => _startLine ??= new Line(s, Replies())
//        .SetVideoClip(Assets.RhythmCellW);

//    Response[] Replies() => new Response[4]{
//        new Response("Play again"),
//        new Response("Try", CountOff),
//        new Response("Next", new HH_Dialogue(ConsequentState)),
//        new Response("Quit", new FinishedRhythmCellTutorial_Dialogue(ConsequentState))
//    };

//    string s => "This is a Whole Note, and is worth four beats. To perform it, tap on beat 1 and hold for four counts. I highly recommend you count out loud\n     'One, Two, Three, Four'...";


//    Line _countOff;
//    Line CountOff => _countOff ??= new Line(count, TrainingBattery());

//    string count => "Before you perform, there will be a 'Count Off'. You will hear a series of clicks to prepare you to start. These clicks will be spaced like this:" +
//    "\n\n1   .   .   .   2   .   .   .   3   .   +   .   4   .   +   .";



//    State TrainingBattery() => new BatteryTutorial_State(this, new HH_Dialogue(ConsequentState), GetRhythmBar());

//    RhythmBars GetRhythmBar()
//    {
//        UnityEngine.Debug.Log("GettingRhythmBars");
//        RhythmBars whole = new RhythmBars(1, DataManager.Io.GameplayData)
//            .SetSpecificRhythms(Rhythm())
//            .SetTempo(80)
//            .SetSubDivision(SubDivisionTier.QuartersOnly)
//            .ConstructRhythmBars(random: false);
//        ;

//        return whole;


//        RhythmCell[][] Rhythm()
//        {
//            RhythmCell[][] rhythm = new RhythmCell[1][]{
//                new RhythmCell[1]{
//                    new RhythmCell()
//                        .SetQuantizement(Quantizement.Quarter)
//                        .SetRhythmicShape(CellShape.w)}
//            };

//            return rhythm;
//        }

//        // RhythmCell[][] Rhythm()
//        // {
//        //     RhythmCell[][] rhythm = new RhythmCell[1][];
//        //     rhythm[0] = new RhythmCell[1];
//        //     rhythm[0][0] = new RhythmCell();
//        //     rhythm[0][0].Quantizement = Quantizement.Quarter;
//        //     rhythm[0][0].LongCell = true;
//        //     rhythm[0][0].Tied = false;
//        //     rhythm[0][0].Rest = false;
//        //     rhythm[0][0].RhythmicShape = CellShape.w;
//        //     return rhythm;
//        // }
//    }
//}
