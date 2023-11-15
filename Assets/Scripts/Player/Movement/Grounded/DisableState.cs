using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class DisableState : State
    {
        private MovementStateMachine _stateMachine;

        public DisableState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            if (_stateMachine.Player.isActive)
                _stateMachine.Player.isActive = false;
        }

        public override void OnExit()
        {
            _stateMachine.Player.isActive = true;
        }

        public override void OnHandle()
        {
            if (_stateMachine.Player.Input.actions["Switch"].triggered)
            {
                Debug.Log(_stateMachine.Player.transform.name);
                _stateMachine.ChangeState(_stateMachine.IdleState);
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