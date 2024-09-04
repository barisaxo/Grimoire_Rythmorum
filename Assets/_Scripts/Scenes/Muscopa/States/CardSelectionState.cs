using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Muscopa
{
    public class CardSelectionState : State
    {
        public CardSelectionState(MuscopaScene scene)
        {
            Scene = scene;
        }

        readonly MuscopaScene Scene;

        protected override void EngageState()
        {
            if (Scene.CardManager.HandIsEmpty())
            {
                SetState(new CardDrawState(Scene));
                return;
            }

            Scene.ScrollHand(Dir.Reset);
            Scene.HighlightHandSpot();
        }

        protected override void DisengageState()
        {
        }

        // protected override void GPInput(GamePadButton gpb)
        // {
        //     switch (gpb)
        //     {


        //         case GamePadButton.East_Press:
        //             ConfirmPressed();
        //             break;

        //         case GamePadButton.North_Press:
        //             InteractPressed();
        //             break;
        //     }
        // }

        protected override void EastPressed()
        {

            SetState(new AnswerSpotSelectionState(Scene));

        }

        protected override void NorthPressed()
        {
            Scene.Discard();
            Scene.ScrollHand(Dir.Left);
            Scene.HighlightHandSpot();
            SetState(this);
        }

        override protected void DirectionPressed(Dir dir)
        {
            if (dir != Dir.Left && dir != Dir.Right) return;
            Scene.ScrollHand(dir);
            Scene.HighlightHandSpot();
        }

    }

    public class CardDrawState : State
    {
        readonly MuscopaScene Scene;
        public CardDrawState(MuscopaScene scene)
        {
            Scene = scene;
        }

        protected override void PrepareState(Action callback)
        {
            Scene.CardManager.CurrentHandSpot = HandSpot.Off;
            Scene.CardManager.CurrentAnswerSpot = AnswerSpot.Off;
            Scene.HighlightHandSpot();
            Scene.HighlightAnswerSpot();
            Scene.DrawANewHand(callback);
        }

        protected override void EngageState()
        {
            SetState(new CardSelectionState(Scene));
        }
    }
}