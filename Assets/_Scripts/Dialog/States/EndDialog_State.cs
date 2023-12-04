
public class EndDialog_State : State
{
    readonly Dialog.Dialog Dialog;
    readonly State SubsequentState;
    readonly bool Fade;
    readonly bool PanCamera;
    readonly UnityEngine.Vector3 Pan;
    readonly UnityEngine.Vector3 Strafe;
    readonly float Speed;

    public EndDialog_State(Dialog.Dialog dialog, State subsequentState, bool fade, bool panCamera, UnityEngine.Vector3 pan, UnityEngine.Vector3 strafe, float speed)
    {
        Dialog = dialog;
        SubsequentState = subsequentState;
        Fade = fade;
        Pan = pan;
        PanCamera = panCamera;
        Strafe = strafe;
        Speed = speed;
    }

    protected override void EngageState()
    {
        //Audio.SoundFX.Stop();

        if (Fade)
        {
            FadeToState(SubsequentState);
        }
        else if (PanCamera)
        {
            SetStateDirectly(
                new CameraPan_State(SubsequentState, Pan, Strafe, Speed));
        }
        else
        {
            SetStateDirectly(SubsequentState);
        }
    }

    protected override void DisengageState()
    {
        Dialog?.SelfDestruct();
    }
}
