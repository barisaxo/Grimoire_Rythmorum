using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialog;

public class PirateTask_Dialogue : Dialogue
{
    public Dialogue ReturnTo;
    bool pending => Data.Two.Manager.Io.Quests.GetLevel(new Data.Two.Bounty()) == 1;
    readonly Data.Two.Standing Standing;

    public PirateTask_Dialogue(Dialogue returnTo, Speaker speaker, Data.Two.Standing standing)
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

    Line _startLine;
    Line StartLine => _startLine ??= new Line("Always having problems with pirates...", GetResponses())
        .SetSpeaker(Speaker)
        ;

    Response[] _responses;
    Response[] Responses => _responses ??= GetResponses();
    Response[] GetResponses()
    {
        List<Response> responses = new();

        if (pending) { responses.Add(BountiesResponse); }

        responses.Add(BountiesResponse);
        // responses.Add(RewardsResponse);
        // responses.Add(AttackResponse);
        responses.Add(BackResponse);

        return responses.ToArray();
    }

    Response _bountiesResponse;
    Response BountiesResponse => _bountiesResponse ??= new Response("Bounties", new Bounty_Dialogue(this, Speaker, Standing));

    // Response _rewardsResponse;
    // Response RewardsResponse => _rewardsResponse ??= new Response("Rewards", new BountyRewards_Dialogue(this, Speaker));

    // Response _attackResponse;
    // Response AttackResponse => _attackResponse ??= new Response("[Attack the ship]", new AttackShip_Dialogue(this, Speaker));

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("Never mind", ReturnTo);
}
