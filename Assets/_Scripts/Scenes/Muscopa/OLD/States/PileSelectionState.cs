namespace OLDMuscopa
{

    public class PileSelectionState : State
    {
        static MuscopaScene Muscopa => MuscopaScene.Io;

        protected override void EngageState()
        {
            Muscopa.ScrollAnswerPile(Dir.Reset);
            InputKey.ButtonEvent += GPInput;
        }

        protected override void DisengageState()
        {
            Muscopa.ScrollAnswerPile(Dir.Down_Off);
            InputKey.ButtonEvent -= GPInput;
        }

        protected override void GPInput(GamePadButton gpb)
        {
            switch (gpb)
            {
                case GamePadButton.Left_Press:
                    DirectionPressed(Dir.Left); break;
                case GamePadButton.Right_Press:
                    DirectionPressed(Dir.Right); break;
                case GamePadButton.East_Press:
                    ConfirmPressed(); break;
                case GamePadButton.North_Press:
                    InteractPressed(); break;
            }
        }
        void ConfirmPressed()
        {

            // if (AnswerChecker.CheckAnswer(CardHandler.SelectedCard()))
            // {
            //     Muscopa.ActiveHandPiles.Remove(Muscopa.CurrentHandPile);
            //     Muscopa.AnsweredPiles.Add(Muscopa.CurrentAnswerPile);
            //     AnswerChecker.UpdateAnswerTexts();
            //     CardHandler.DropCardInAnswerPile(CardHandler.SelectedCard(), Muscopa.CurrentAnswerPile);
            //     CardHandler.ScrollHand(Dir.Down_Off);
            // }
            // else if (Muscopa.anyWrongAnswers)
            // {
            //     SetState(new EndMuscopaState(winResult: false));
            //     return;
            // }
            // else
            // {
            //     CardHandler.Discard();
            //     CardHandler.ColorAnswersRed();
            //     Muscopa.anyWrongAnswers = true;
            // }

            SetState(new CardSelectionState());
        }

        protected override void DirectionPressed(Dir dir)
        {
            Muscopa.ScrollAnswerPile(dir);
        }

        void CancelPressed()
        {
            SetState(new CardSelectionState());
        }

        void InteractPressed()
        {
            Muscopa.Discard();
            SetState(new CardSelectionState());
        }

    }
}