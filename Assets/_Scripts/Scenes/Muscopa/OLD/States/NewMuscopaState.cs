
using System;
using UnityEngine;

namespace OLDMuscopa
{
    public class NewMuscopaState : State
    {

        protected override void PrepareState(Action callback)
        {
            Cam.Io.Camera.transform.SetPositionAndRotation(new Vector3(0, 0, -5),
            Quaternion.identity); _ = MuscopaScene.Io;

            base.PrepareState(callback);
        }

        protected override void EngageState()
        {
            MuscopaDirector.Enable();
            MuscopaScene.Io.DisablePileHighlights();
            MuscopaScene.Io.SetUpDeck();
            MuscopaScene.Io.UpdateAnswerTexts();

            SetState(new CardSelectionState());
        }
    }

}

