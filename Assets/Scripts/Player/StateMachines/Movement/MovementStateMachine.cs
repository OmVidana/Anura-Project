namespace Anura
{
    public class MovementStateMachine
    {
        public Player Player { get; }
        private MoveState _currentState;
        public IdleState IdleState { get; }
        public WalkingState WalkingState { get; }
        public RunningState RunningState { get; }

        public MovementStateMachine(Player player)
        {
            Player = player;
            IdleState = new IdleState(this);
            WalkingState = new WalkingState(this);
            RunningState = new RunningState(this);
        }

        public void ChangeState(MoveState newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        public void InputHandling()
        {
            _currentState?.OnInput();
        }
        
        public void Update()
        {
            _currentState?.Update();
        }
        
        public void PhysicsUpdate()
        {
            _currentState?.PhysicsUpdate();
        }
    }
}