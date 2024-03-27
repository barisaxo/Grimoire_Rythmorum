using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Data.Inventory;

namespace Sea
{
    public interface IInteractable
    {
        public string PopupText { get; }
        public State CurrentState { get; }
        public State SubsequentState { get; }

        public static IInteractable Null { get; } = new NoInteraction();

    }

    public class NoInteraction : IInteractable
    {
        public State CurrentState => null;
        public State SubsequentState => null;
        public string PopupText => null;
    }
    // public abstract class Interaction : IInteractable
    // {
    // public State CurrentState => null;
    // public State SubsequentState => null;
    // public string PopupText => null;
    // }



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
        public BottleInteraction(State currentState, StarChartsData starChartsData, ShipData shipData, ISceneObject obj)
        {
            CurrentState = currentState;
            Obj = obj;
            StarChartsData = starChartsData;
            ShipData = shipData;
        }

        readonly StarChartsData StarChartsData;
        readonly ShipData ShipData; private string _popupText;
        public string PopupText => _popupText ??= StarChartsData.InventoryIsFull(ShipData.GetLevel(ShipData.DataItem.Bottle)) ?
            "(inventory full)" :
            "Pickup";
        readonly ISceneObject Obj;
        public State CurrentState { get; }

        private State _subsequentState;
        public State SubsequentState => _subsequentState ??=
            StarChartsData.InventoryIsFull(ShipData.GetLevel(ShipData.DataItem.Bottle)) ?
                new DialogStart_State(new InventoryIsFull_Dialogue(CurrentState)) :
                new SeaToItemPickUp_State(CurrentState, StarChartsData, ShipData.DataItem.Bottle, Obj);

    }

    public class GramoInteraction : IInteractable
    {
        public GramoInteraction(State currentState, GramophoneData gramoData, ShipData shipData, ISceneObject obj)
        {
            CurrentState = currentState;
            Obj = obj;
            GramoData = gramoData;
            ShipData = shipData;
        }

        readonly GramophoneData GramoData;
        readonly ShipData ShipData; private string _popupText;
        public string PopupText => _popupText ??= GramoData.InventoryIsFull(ShipData.GetLevel(ShipData.DataItem.Gramos)) ?
            "(inventory full)" :
            "Pickup";
        readonly ISceneObject Obj;
        public State CurrentState { get; }

        private State _subsequentState;
        public State SubsequentState => _subsequentState ??=
            GramoData.InventoryIsFull(ShipData.GetLevel(ShipData.DataItem.Gramos)) ?
                new DialogStart_State(new InventoryIsFull_Dialogue(CurrentState)) :
                new SeaToItemPickUp_State(CurrentState, GramoData, ShipData.DataItem.Gramos, Obj);

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
            // DataManager.Io.CharacterData.ActivatedLighthouses.Contains(Lighthouse.Region) ? null :
            new ActivateLighthouse_State(Lighthouse, CurrentState);
        public string PopupText =>
            // DataManager.Io.CharacterData.ActivatedLighthouses.Contains(Lighthouse.Region) ? null :
            "Activate Lighthouse";
    }

    public class FishingInteraction : IInteractable
    {
        public FishingInteraction(State currentState, FishData fishData, ShipData shipData, ISceneObject obj)
        {
            CurrentState = currentState;
            Obj = obj;
            FishData = fishData;
            ShipData = shipData;
        }

        readonly FishData FishData;
        readonly ShipData ShipData;
        public State CurrentState { get; }
        readonly ISceneObject Obj;
        private State _subsequentState;
        public State SubsequentState => _subsequentState ??= FishData.InventoryIsFull(ShipData.GetLevel(ShipData.DataItem.Fish)) ?
            new DialogStart_State(new InventoryIsFull_Dialogue(CurrentState)) :
            new SeaToAnglingTransition_State(CurrentState);
        private string _popupText;
        public string PopupText => _popupText ??= FishData.InventoryIsFull(ShipData.GetLevel(ShipData.DataItem.Fish)) ?
            "(inventory full)" :
            "Fish";
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
