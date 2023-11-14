namespace Anura
{
    public class StateMachine
    {
        private State _currentState;
        public void ChangeState(State newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        public void OnHandle()
        {
            _currentState?.OnHandle();
        }
        
        public void Update()
        {
            _currentState?.Update();
        }
        
        public void PhysicsUpdate()
        {
            _currentState?.PhysicsUpdate();
        }

        public State returnCurrent()
        {
            return _currentState;
        }
    }
}
