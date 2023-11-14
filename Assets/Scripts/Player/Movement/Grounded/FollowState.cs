using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class FollowState : State
    {
        private readonly MovementStateMachine _stateMachine;
        
        public FollowState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            Debug.Log("Is Following " + _stateMachine.Player.transform.name);

            _stateMachine.Player.isActive = false;
        }

        public override void OnExit()
        {
            _stateMachine.Player.isActive = true;
        }

        public override void OnHandle()
        {
            _stateMachine.ObtainInput();
            if (_stateMachine.Player.Input.actions["Switch"].triggered)
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }

        public override void Update()
        {
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        private void Follow()
        {
            
        }
    }
}
