using System.Collections;
using System.Collections.Generic;

namespace Sea
{
    public interface IInteractable
    {
        // public IInteraction Interaction();
        public string PopupText { get; }
        public State CurrentState { get; }
        public State SubsequentState { get; }
    }

    public class NoInteraction : IInteractable
    {
        public State CurrentState => null;
        public State SubsequentState => null;
        public string PopupText => null;
    }

    public class HailShipInteraction : IInteractable
    {
        public HailShipInteraction(State currentState)
        {
            _currentState = currentState;
        }

        public WorldMapScene Scene => WorldMapScene.Io;
        private readonly State _currentState;
        public State CurrentState => _currentState;
        public State SubsequentState => new SeaToDialogueTransition_State(new HailShip_Dialogue(
                new Speaker(
                    Scene.NearestNPC.Flag,
                    Scene.NearestNPC.Name,
                    Scene.NearestNPC.FlagColor)
            ));
        public string PopupText => "Hail";
    }

    public class CoveInteraction : IInteractable
    {
        public CoveInteraction()
        {
        }

        public WorldMapScene Scene;
        private readonly State _currentState;
        public State CurrentState => _currentState;
        public State SubsequentState => new SeaToCoveTransition_State();
        public string PopupText => "Null Cove";
    }

    public class BottleInteraction : IInteractable
    {
        public BottleInteraction(State currentState)
        {
            _currentState = currentState;
        }

        public WorldMapScene Scene;
        private readonly State _currentState;
        public State CurrentState => _currentState;
        public State SubsequentState =>
            new DisplayItem_State(
                new DialogStart_State(
                    new FoundBottle_Dialogue(CurrentState)),
                Assets._bottle.gameObject,
                clearCell: true);
        public string PopupText => "Pickup";
    }

    public class LighthouseInteraction : IInteractable
    {
        public LighthouseInteraction(Lighthouse lighthouse, State currentState)
        {
            _currentState = currentState;
            Lighthouse = lighthouse;
        }
        readonly Lighthouse Lighthouse;
        private readonly State _currentState;
        public State CurrentState => _currentState;
        public State SubsequentState =>
            DataManager.Io.CharacterData.ActivatedLighthouses.Contains(Lighthouse.Region) ? null :
            new ActivateLighthouse_State(Lighthouse, CurrentState);
        public string PopupText =>
            DataManager.Io.CharacterData.ActivatedLighthouses.Contains(Lighthouse.Region) ? null :
            "Activate Lighthouse";
    }


    public class FishingInteraction : IInteractable
    {
        public FishingInteraction(State currentState) => _currentState = currentState;
        private readonly State _currentState;
        public State CurrentState => _currentState;
        public State SubsequentState => new SeaToAnglingTransition_State(CurrentState);
        public string PopupText => "Fish";
    }

    public class DockInteraction : IInteractable
    {
        public DockInteraction(State currentState, WorldMapScene scene)
        {
            _currentState = currentState;
            Scene = scene;
        }

        public WorldMapScene Scene;
        private readonly State _currentState;
        public State CurrentState => _currentState;
        public State SubsequentState => throw new System.NotImplementedException();
        public string PopupText => "Dock";
    }

}
