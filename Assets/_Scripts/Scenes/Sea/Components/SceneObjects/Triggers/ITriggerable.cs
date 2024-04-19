namespace Sea
{
    public interface ITriggerable
    {
        public State CurrentState { get; }
        public State SubsequentState { get; }
    }

    public class NotTriggerable : ITriggerable
    {
        public State CurrentState => null;
        public State SubsequentState => null;
    }

    public class PirateTrigger : ITriggerable
    {
        public PirateTrigger(State currentState, NPCShip ship)
        {
            _currentState = currentState;
            Ship = ship;
        }
        readonly NPCShip Ship;
        public WorldMapScene Scene;
        private readonly State _currentState;
        public State CurrentState => _currentState;
        public State SubsequentState => new NMESailApproach_State(Ship);
    }

}
