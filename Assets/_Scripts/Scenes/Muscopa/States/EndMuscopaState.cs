using UnityEngine;
namespace Muscopa
{
    public class EndMuscopaState : State
    {
        public EndMuscopaState(MuscopaScene scene, bool winResult)
        {
            Scene = scene;
            WinResult = winResult;
            SubsequentState = Scene.SubsequentState;
        }
        readonly State SubsequentState;
        readonly MuscopaScene Scene;
        readonly bool WinResult;

        protected override void EngageState()
        {
            Scene.MuscopaHud.Answer2ChordName.TextString = Scene.GetChordAndRomanNames(AnswerSpot.Two);
            Scene.MuscopaHud.Answer3ChordName.TextString = Scene.GetChordAndRomanNames(AnswerSpot.Three);
            Scene.MuscopaHud.Answer4ChordName.TextString = Scene.GetChordAndRomanNames(AnswerSpot.Four);
            Scene.Tableau.AnswerSpot1.color = WinResult ? Color.green : Color.red;
            Scene.Tableau.AnswerSpot2.color = WinResult ? Color.green : Color.red;
            Scene.Tableau.AnswerSpot3.color = WinResult ? Color.green : Color.red;
            Scene.Tableau.AnswerSpot4.color = WinResult ? Color.green : Color.red;
            //todo spin? all cards if won
            if (WinResult) MonoHelper.OnUpdate += Spin;
            else MonoHelper.OnUpdate += Fall;
            // switch (Character_Data.GetArea())
            // {
            //     case Area.Sea: StateMachine.FadeToState(new Sea.SeaState()); break;
            //     case Area.Practice: StateMachine.FadeToState(new Aether.AetherExploreState()); break;
            // }
            _ = Scene.MuscopaHud.ContinueButton;

        }

        void Spin()
        {
            Scene.Tableau.AnswerSpot1.transform.Rotate(0, 0, 150 * Time.deltaTime);
            Scene.Tableau.AnswerSpot2.transform.Rotate(0, 0, 150 * Time.deltaTime);
            Scene.Tableau.AnswerSpot3.transform.Rotate(0, 0, -150 * Time.deltaTime);
            Scene.Tableau.AnswerSpot4.transform.Rotate(0, 0, -150 * Time.deltaTime);
        }

        void Fall()
        {
            Scene.Tableau.AnswerSpot1.transform.Rotate(0, 10 * Mathf.Sin(Time.time), 0);
            Scene.Tableau.AnswerSpot2.transform.Rotate(0, 12 * Mathf.Sin(Time.time), 0);
            Scene.Tableau.AnswerSpot3.transform.Rotate(0, 15 * Mathf.Sin(Time.time), 0);
            Scene.Tableau.AnswerSpot4.transform.Rotate(0, 11 * Mathf.Sin(Time.time), 0);
            Scene.Tableau.AnswerSpot1.transform.Translate(2.5f * Time.deltaTime * Vector3.down);
            Scene.Tableau.AnswerSpot2.transform.Translate(2.2f * Time.deltaTime * Vector3.down);
            Scene.Tableau.AnswerSpot3.transform.Translate(2f * Time.deltaTime * Vector3.down);
            Scene.Tableau.AnswerSpot4.transform.Translate(2.23f * Time.deltaTime * Vector3.down);
        }

        protected override void DisengageState()
        {
            // if (Character_Data.GetArea() == Area.Sea)
            // {
            //     Rewards.PushFinishResults(WinResult, Area.Sea, Muscopa.anyWrongAnswers ? 1 : 2);
            // }

            if (WinResult) MonoHelper.OnUpdate -= Spin;
            else MonoHelper.OnUpdate -= Fall;

            Scene.MusicaAudio.StopTheCadence();

            (Scene as IScene).SelfDestruct();
        }

        protected override void SouthPressed()
        {
            if (Scene.IsPractice)
            {
                SetState(SubsequentState);
            }
            else if (WinResult)
            {
                DataManager.Player.AdjustLevel(new Datum.MuscopaSolved(), 1);

                DataManager.Player.AdjustLevel(new Datum.PatternsFound(), (int)(1000f *
                                DataManager.Skill.GetBonusRatio(new Datum.Apophenia()) *
                                (1.5f) *
                                (UnityEngine.Random.value + 1f)
                                ));
                //todo sail away state after end muscopa dialog
                SetState(
                    new CameraPan_State(
                            new DialogStart_State(
                                new EndMuscopa_Dialogue(
                                    won: true,
                                    subsequentState: new NPCSailAway_State(SubsequentState),
                                    patternsFound: (int)(1000f *
                                        DataManager.Skill.GetBonusRatio(new Datum.Apophenia()) *
                                        (1.5f) *
                                        (UnityEngine.Random.value + 1f)
                                        ))),
                    pan: Cam.StoredCamRot,
                    strafe: Cam.StoredCamPos,
                    speed: 5));
            }
            else
            {
                DataManager.Player.AdjustLevel(new Datum.MuscopaFailed(), 1);

                //todo sail away state after end muscopa dialog
                SetState(
                    new CameraPan_State(
                            new DialogStart_State(
                                new EndMuscopa_Dialogue(
                                    won: false,
                                    subsequentState: new NPCSailAway_State(SubsequentState),
                                    patternsFound: 0
                        )),
                        pan: Cam.StoredCamRot,
                        strafe: Cam.StoredCamPos,
                        speed: 5));
            }
        }
    }
}