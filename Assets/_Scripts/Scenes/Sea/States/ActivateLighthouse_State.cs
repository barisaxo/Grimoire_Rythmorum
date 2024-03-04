using System;
using Data.Inventory;
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
            Data.LighthousesData.IncreaseLevel(Lighthouse.RegionData);
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
            SetState(SubsequentState);
        }
    }
}