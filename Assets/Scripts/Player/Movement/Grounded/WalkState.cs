using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class WalkState : State
    {
        private MovementStateMachine _stateMachine;

        public WalkState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
        }

        public override void OnHandle()
        {
            _stateMachine.ObtainInput();
            if (_stateMachine.MovementInput.x == 0f)
                _stateMachine.ChangeState(_stateMachine.IdleState);
            if (_stateMachine.Player.Input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.RunningState);
            if (_stateMachine.Player.Input.actions["Jump"].triggered)
                _stateMachine.ChangeState(_stateMachine.JumpingState);
            if (_stateMachine.Player.Input.actions["Switch"].triggered)
            {
                _stateMachine.ChangeState(_stateMachine.DisableState);
            }
        }

        public override void Update()
        {
            
        }

        public override void PhysicsUpdate()
        {
            _stateMachine.Move(_stateMachine.Player.walkingSpeed);
            _stateMachine.PlayerDirection();
        }
    }
}
