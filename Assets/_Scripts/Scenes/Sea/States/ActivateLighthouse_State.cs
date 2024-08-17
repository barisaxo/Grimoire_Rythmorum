using System;
using Datum;
namespace Sea
{
    public class ActivateLighthouse_State : State
    {
        readonly Lighthouse Lighthouse;
        readonly State SubsequentState;

        public ActivateLighthouse_State(Lighthouse lighthouse, State subsequentState)
        {
            Lighthouse = lighthouse;
            SubsequentState = subsequentState;
        }

        protected override void PrepareState(Action callback)
        {
            Manager.Io.Lighthouse.AdjustLevel(Lighthouse.RegionData, 1);
            // if (!DataManager.Io.CharacterData.ActivatedLighthouses.Contains(Lighthouse.Region))
            // {
            // DataManager.Io.CharacterData.ActivatedLighthouses = DataManager.Io.CharacterData.ActivatedLighthouses.Added(Lighthouse.Region);
            Lighthouse.LighthousePrefab.Light.SetActive(true);
            Lighthouse.RemoveInteractable();
            // }

            base.PreEngageState(callback);
        }

        protected override void EngageState()
        {
            if (Manager.Io.Lighthouse.AllActivated())
            {
                DataManager.Player.AdjustLevel(new Datum.PatternsFound(), 10000);
                new DialogStart_State(new WonTheGame_Dialogue());
            }
            else SetState(SubsequentState);
        }
    }
}