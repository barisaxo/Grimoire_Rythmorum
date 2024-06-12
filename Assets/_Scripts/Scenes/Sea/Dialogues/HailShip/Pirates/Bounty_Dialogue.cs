using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;
using Data;

public class Bounty_Dialogue : Dialogue
{
    public Bounty_Dialogue(Dialogue returnTo, Speaker speaker, Standing standing)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
        Standing = standing;
    }

    public override Dialogue Initiate()
    {
        FirstLine = StartLine;
        return base.Initiate();
    }

    readonly Dialogue ReturnTo;
    readonly Standing Standing;
    readonly bool activeQuest = Manager.Io.Quests.GetLevel(new Bounty()) == 1;
    bool ContractWithRegion => activeQuest && (Manager.Io.Quests.GetQuest(new Bounty()) as Quests.BountyQuest).Standing == Standing;
    string RegionalName => (Manager.Io.Quests.GetQuest(new Bounty()) as Quests.BountyQuest).Standing.ToRegionalName();

    Line _startLine;
    Line StartLine => _startLine ??= new Line(GetStartLine, GetResponses())
        .SetSpeaker(Speaker)
        ;

    string GetStartLine => activeQuest ?
        ContractWithRegion ? "Any luck hunting the pirate?" :
            "You currently have an open contract with the " + RegionalName :
            "We are always having problems with pirates..."
        ;

    Response[] GetResponses()
    {
        List<Response> responses = new();

        if (!activeQuest)
        {
            responses.Add(NewQuestResponse);
            responses.Add(BackResponse);
            return responses.ToArray();
        }

        if (ContractWithRegion)
            if (Manager.Io.Quests.GetQuest(new Bounty()).Complete)
            {
                responses.Add(RewardsResponse);
            }
            else
            {
                responses.Add(AbandonQuestResponse);
            }
        else
        {
            responses.Add(AbandonQuestResponse);
        }

        responses.Add(BackResponse);

        return responses.ToArray();
    }

    Line _abandonedQuestLine;
    Line AbandonedQuest_Line => _abandonedQuestLine ??=
        new Line("That's too bad...",
            new NPCSailAway_State(new SeaScene_State()))
            .SetSpeaker(Speaker);

    Response _abandonQuestResponse;
    Response AbandonQuestResponse => _abandonQuestResponse ??=
        new Response("Abandon Quest", AbandonedQuest_Line)
            .SetPlayerAction(AbandonQuestPlayerAction);


    // Line _overrideQuestLine;
    // Line OverrideQuest_Line => _overrideQuestLine ??=
    //     new Line("That's too bad...",
    //         new NPCSailAway_State(new SeaScene_State()))
    //         .SetSpeaker(Speaker);

    Response _overrideQuestResponse;
    Response OverrideQuestResponse => _overrideQuestResponse ??=
        new Response("Get new Quest", TradeComplete_Line)
            .SetPlayerAction(OverrideQuestPlayerAction);


    void AbandonQuestPlayerAction()
    {
        Manager.Io.Quests.SetLevel(new Bounty(), 0);
    }

    void OverrideQuestPlayerAction()
    {
        Manager.Io.StandingData.AdjustLevel(Standing, -1);
        AcceptQuestPlayerAction();
    }

    void AcceptQuestPlayerAction()
    {
        Manager.Io.Quests.SetQuest(new Bounty(),
            new Quests.BountyQuest(
                new Sea.Inventoriable(Rewards()),
                Standing,
                Loc,
                LatLong
                ));

        Sea.WorldMapScene.Io.Map.AddToMap(Loc, Sea.CellType.Bounty);

        Debug.Log(nameof(AcceptQuestPlayerAction) + " " + Manager.Io.Quests.GetQuest(new Bounty()).Description);
    }

    string LatLong => Loc.GlobalCoordsToLatLongs(Sea.WorldMapScene.Io.Map.GlobalSize);
    Vector2Int Loc = Sea.WorldMapScene.Io.Ship.GlobalCoord + RandomLoc();

    static private Vector2Int RandomLoc()
    {
        return new Vector2Int(
            UnityEngine.Random.Range(30, 60) * (UnityEngine.Random.value < .5f ? 1 : -1),
            UnityEngine.Random.Range(30, 60) * (UnityEngine.Random.value < .5f ? 1 : -1)
        );
    }

    (IData Data, IItem DataItem, int Amount)[] Rewards()
    {
        return new (IData Data, IItem DataItem, int Amount)[]
        {
            (Manager.Io.Inventory, new Gold(), 1000),
            (Manager.Io.StandingData, Standing, 1),
        };
    }

    void RewardsPlayerAction()
    {
        Debug.Log("Rewards!!");
        Manager.Io.StandingData.AdjustLevel(Standing, 1);
        var q = Manager.Io.Quests.GetQuest(new Bounty());
        q.Reward.AddRewards();
    }

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);

    Response _newQuestResponse;
    Response NewQuestResponse => _newQuestResponse ??= new Response("New Bounty", TradeComplete_Line)
        .SetPlayerAction(AcceptQuestPlayerAction);

    string TradeComplete_LineText => "Here is the contract..."
        + "\nHunt the pirate ship at " + LatLong +
        "\nCome back when the task is complete.";

    Line _tradeCompleteLine;
    Line TradeComplete_Line => _tradeCompleteLine ??= new Line(TradeComplete_LineText, new NPCSailAway_State(new SeaScene_State()))
        .SetSpeaker(Speaker)
        ;


    Response _rewardsResponse;
    Response RewardsResponse => _rewardsResponse ??= new Response("Rewards", QuestComplete_Line)
        .SetPlayerAction(RewardsPlayerAction);


    readonly string QuestComplete_LineText = "Good work!";
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
