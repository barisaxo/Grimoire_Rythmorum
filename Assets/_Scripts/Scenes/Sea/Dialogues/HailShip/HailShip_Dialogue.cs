using UnityEngine;
using Dialog;
using Datum;

public class HailShip_Dialogue : Dialogue
{
    readonly Datum.Standing Standing;
    public HailShip_Dialogue(Speaker speaker, Datum.Standing standing)
    {
        Speaker = speaker;
        Standing = standing;
    }

    public override Dialogue Initiate()
    {
        if (activeQuest)
            Debug.Log("Quest is complete: " + QuestComplete +
                ", and contract with this region: " + ContractWithRegion +
                " hailed ship:" + Standing.Enum.Name +
                " quest standing:" + (Manager.Io.Quests.GetQuest(new Bounty()) as Quests.BountyQuest).Standing.Enum.Name);

        FirstLine = StartLine;
        return base.Initiate();
    }

    int StandingLevel => Datum.Manager.Io.Standings.GetLevel(Standing);

    readonly bool activeQuest = Manager.Io.Quests.GetLevel(new Bounty()) == 1;
    bool ContractWithRegion => activeQuest && (Manager.Io.Quests.GetQuest(new Bounty()) as Quests.BountyQuest).Standing.Enum == Standing.Enum;
    bool QuestComplete => activeQuest && Manager.Io.Quests.GetQuest(new Bounty()).Complete;

    string Hail_LineText => StandingLevel switch
    {
        10 or 9 => "The weather favors us this day.",
        8 or 7 => "Ahoy! We're on a tight schedule.",
        6 or 5 => "You seem trust worthy enough...",
        4 or 3 => "We'll work with you this once, just don't try anything...",
        _ => "You've got some nerve hailing us."
    };



    Line _startLine;
    Line StartLine => _startLine ??=
    (QuestComplete && ContractWithRegion) ?
            new Line("Good job dealing with that bounty!", RewardsResponse).SetSpeaker(Speaker) :
                StandingLevel < 2 ?
                    new Line(Hail_LineText, new SeaToBatteryTransition_State()).SetSpeaker(Speaker) :
                    new Line(Hail_LineText, new ShipTask_Dialogue(Speaker, Standing)).SetSpeaker(Speaker)
            ;


    void RewardsPlayerAction()
    {
        Debug.Log("Rewards!!");
        Manager.Io.Standings.AdjustLevel(Standing, 1);
        var q = Manager.Io.Quests.GetQuest(new Bounty());
        q.Reward.AddRewards();
    }


    Response[] _rewardsResponse;
    Response[] RewardsResponse => _rewardsResponse ??= new Response[]
    {
        new Response("Claim rewards", QuestComplete_Line)
            .SetPlayerAction(RewardsPlayerAction)
    };


    readonly string QuestComplete_LineText = "This is for you...";
    Line _questCompleteLine;
    Line QuestComplete_Line => _questCompleteLine ??=
        new Line(QuestComplete_LineText)
        .SetSpeaker(Speaker)
        .SetNextDialogue(
            new FoundItem_Dialogue(
                Manager.Io.Quests.GetQuest(new Bounty()).Reward,
                new NPCSailAway_State(new SeaScene_State())
                ));

}

