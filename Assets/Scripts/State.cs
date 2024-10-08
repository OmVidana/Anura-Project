using UnityEngine.InputSystem;

namespace Anura
{
    public abstract class State
    {
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Update();
        public abstract void PhysicsUpdate();
        public abstract void SwitchState();
    }
}
