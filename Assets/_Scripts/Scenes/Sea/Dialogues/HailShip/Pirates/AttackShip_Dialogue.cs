using Dialog;

public class AttackShip_Dialogue : Dialogue
{
    readonly Dialogue ReturnTo;
    public AttackShip_Dialogue(Dialogue returnTo, Speaker speaker)
    {
        ReturnTo = returnTo;
        Speaker = speaker;
    }

    public override Dialogue Initiate()
    {
        FirstLine = ConfirmLine;
        return base.Initiate();
    }

    string Attack_LineText => UnityEngine.Random.Range(0, 3) switch
    {
        0 => "What are you doing!?\nAll hands prepare for battle!",
        2 => "We wont go down without a fight!",
        _ => "Despicable..."
    };

    string ConfirmLineText => "[Are you sure you wish to attack?]";// This action will lower your standing with this region.
    Line _confirmLine;
    Line ConfirmLine => _confirmLine ??= new Line(ConfirmLineText, new Response[] { ConfirmResponse, BackResponse });

    Line _attackLine;//TODO pan camera
    Line Attack_Line => _attackLine ??= new Line(Attack_LineText, new SeaToBatteryTransition_State())
        .SetSpeaker(Speaker)
        // .FadeToNextState()
        ;

    Response _confirmResponse;
    Response ConfirmResponse => _confirmResponse ??= new("[Yes, attack!]", Attack_Line);

    Response _backResponse;
    Response BackResponse => _backResponse ??= new Response("[No, don't attack]", ReturnTo);



}
