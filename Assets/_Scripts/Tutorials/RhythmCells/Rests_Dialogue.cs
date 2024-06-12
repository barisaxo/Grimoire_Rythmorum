using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class RestsAndTies_Dialogue : Dialogue
{
    public RestsAndTies_Dialogue(State consequentState) : base(consequentState) { }

    public override Dialogue Initiate()
    {
        MuteTypingSounds();
        FirstLine = StartLine;
        return base.Initiate();
    }

    Line StartLine => new Line("Those were the 8 basic rhythm cells you find in common time.", Line2);
    Line Line2 => new Line("There are still four more '3 count' rhythm cells used in triplets or music that is 'in three' such as waltzes, but we wont be using those for a while.", Line3);
    Line Line3 => new Line("Let's take a look at rests and ties.", new HrH_Dialogue(ConsequentState));

}
