using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class IdleState : State
    {
        private MovementStateMachine _stateMachine;

        public IdleState(MovementStateMachine stateMachine)
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
            if (!_stateMachine.Player.Input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.WalkingState);
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
            
        }
    }
}
