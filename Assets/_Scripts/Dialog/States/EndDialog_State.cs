
public class EndDialog_State : State
{
    readonly Dialog.Dialog Dialog;
    readonly State SubsequentState;
    // readonly bool FadeToNext;
    readonly bool PanCamera;
    readonly UnityEngine.Vector3 Pan;
    readonly UnityEngine.Vector3 Strafe;
    readonly float Speed;

    public EndDialog_State(Dialog.Dialog dialog, State subsequentState, bool panCamera, UnityEngine.Vector3 pan, UnityEngine.Vector3 strafe, float speed)
    {
        Dialog = dialog;
        SubsequentState = subsequentState;
        // FadeToNext = fade;
        Pan = pan;
        PanCamera = panCamera;
        Strafe = strafe;
        Speed = speed;
    }

    protected override void EngageState()
    {
        //Audio.SoundFX.Stop();

        // if (Fade)
        // {
        //     FadeToState(SubsequentState);
        // }
        // SubsequentState.Fade = FadeToNext;

        if (PanCamera)
        {
            SetState(new CameraPan_State(SubsequentState, Pan, Strafe, Speed));
        }
        else
        {
            SetState(SubsequentState);
        }
    }

    protected override void DisengageState()
    {
        Dialog?.SelfDestruct();
    }
}
