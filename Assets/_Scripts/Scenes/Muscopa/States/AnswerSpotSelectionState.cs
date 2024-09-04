using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory.Notes.Arithmetic;

namespace Muscopa
{
    public class AnswerSpotSelectionState : State
    {
        readonly MuscopaScene Scene;
        public AnswerSpotSelectionState(MuscopaScene scene)
        {
            Scene = scene;
        }

        protected override void EngageState()
        {
            Scene.CardManager.CurrentAnswerSpot = Scene.ScrollAnswerSpot(Dir.Reset);
            Scene.HighlightAnswerSpot();
        }

        protected override void DisengageState()
        {
            Scene.CardManager.CurrentAnswerSpot = AnswerSpot.Off;
            Scene.HighlightAnswerSpot();
        }

        protected override void EastPressed()
        {
            if (Scene.SelectedCard().HarmonicFunction.Equals(Scene.GetFunction(Scene.SelectedAnswerSpot())))
            {
                Scene.DropCardInAnswerSpot(Scene.SelectedCard(), Scene.CardManager.CurrentAnswerSpot);
                Scene.CardManager.ActiveHandSpots.Remove(Scene.CardManager.CurrentHandSpot);
                Scene.CardManager.AnsweredSpots.Add(Scene.CardManager.CurrentAnswerSpot);
                // AnswerChecker.UpdateAnswerTexts();
                // Scene.ScrollHand(Dir.Left);
                Card answerCard =
                   Scene.CardManager.CurrentAnswerSpot switch
                   {
                       AnswerSpot.Two => Scene.MuscopaHud.Answer2ChordName,
                       AnswerSpot.Three => Scene.MuscopaHud.Answer3ChordName,
                       AnswerSpot.Four => Scene.MuscopaHud.Answer4ChordName,
                       _ => throw new System.Exception(Scene.CardManager.CurrentAnswerSpot.ToString())
                   };

                answerCard.TextString = Scene.GetChordAndRomanNames();

                if (Scene.CardManager.AnsweredSpots.Count == 4)
                {
                    Scene.CardManager.CurrentHandSpot = HandSpot.Off;
                    Scene.HighlightHandSpot();
                    Scene.CardManager.CurrentHandSpot = HandSpot.Off;
                    Scene.HighlightHandSpot();
                    SetState(new EndMuscopaState(Scene, winResult: true));
                    return;
                }
            }
            else if (Scene.AnyWrongAnswers)
            {
                Scene.CardManager.CurrentHandSpot = HandSpot.Off;
                Scene.HighlightHandSpot();

                SetState(new EndMuscopaState(Scene, winResult: false));
                return;
            }
            else
            {
                Scene.Discard();
                Scene.ColorAnswersYellow();
                Scene.AnyWrongAnswers = true;
            }

            Scene.CardManager.CurrentHandSpot = HandSpot.Off;
            Scene.HighlightHandSpot();
            SetState(new CardSelectionState(Scene));
        }

        protected override void DirectionPressed(Dir dir)
        {
            if (dir != Dir.Left && dir != Dir.Right) return;

            Scene.CardManager.CurrentAnswerSpot = Scene.ScrollAnswerSpot(dir);
            Scene.HighlightAnswerSpot();
        }

        protected override void SouthPressed()
        {
            SetState(new CardSelectionState(Scene));
        }

        protected override void NorthPressed()
        {
            Scene.Discard();
            SetState(new CardSelectionState(Scene));
        }

    }
}