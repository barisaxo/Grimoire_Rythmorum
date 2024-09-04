
namespace OLDMuscopa
{

    public class EndMuscopaState : State
    {
        public EndMuscopaState(bool winResult)
        {
            WinResult = winResult;
        }

        readonly bool WinResult;

        static MuscopaScene Muscopa => MuscopaScene.Io;

        protected override void EngageState()
        {
            // switch (Character_Data.GetArea())
            // {
            //     case Area.Sea: StateMachine.FadeToState(new Sea.SeaState()); break;
            //     case Area.Practice: StateMachine.FadeToState(new Aether.AetherExploreState()); break;
            // }
        }

        protected override void DisengageState()
        {
            Muscopa.puzzleActive = false;

            // if (Character_Data.GetArea() == Area.Sea)
            // {
            //     Rewards.PushFinishResults(WinResult, Area.Sea, Muscopa.anyWrongAnswers ? 1 : 2);
            // }

            // MusicDirector.Disable();
            MuscopaScene.Io.SelfDestruct();
        }

    }
}