namespace OLDMuscopa
{

    public class CardSelectionState : State
    {
        MuscopaScene Muscopa => MuscopaScene.Io;

        protected override void EngageState()
        {
            if (NotDrawingCards())
            {
                CardHandler.CheckForEmptyHand(Muscopa);
                CardHandler.ScrollHand(Muscopa, Dir.Reset);
            }
        }

        protected override void DisengageState()
        {
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
            if (Muscopa.PuzzleIsSolved())
            {
                SetState(new EndMuscopaState(winResult: true));
                return;
            }

            if (NotDrawingCards() && Muscopa.puzzleActive)
            {
                SetState(new PileSelectionState());
            }
        }

        void InteractPressed()
        {
            if (Muscopa.PuzzleIsSolved())
            {
                SetState(new EndMuscopaState(winResult: true));
                return;
            }

            if (NotDrawingCards() && Muscopa.puzzleActive)
            {
                Muscopa.Discard();
                Muscopa.ScrollHand(Dir.Left);
            }
        }

        override protected void DirectionPressed(Dir dir)
        {
            if (NotDrawingCards() && Muscopa.puzzleActive)
            {
                Muscopa.ScrollHand(dir);
            }
        }

        public void SelectPressed(bool si) { }

        bool NotDrawingCards() => Muscopa.puzzleActive && Muscopa.CardsInAnimation.Count == 0;

    }
}