namespace Anura
{
    public abstract class MoveState
    {
        protected MovementStateMachine StateMachine;

        protected MoveState(MovementStateMachine movementStateMachine)
        {
            StateMachine = movementStateMachine;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnInput();
        public abstract void Update();
        public abstract void PhysicsUpdate();
    }
}