using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class JumpState : State
    {
        private readonly MovementStateMachine _stateMachine;

        public JumpState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            Debug.Log("Jump");
        }

        public override void OnExit()
        {
            
        }

        public override void OnHandle()
        {
            _stateMachine.ObtainInput();
            if (_stateMachine.MovementInput.x == 0f)
                _stateMachine.ChangeState(_stateMachine.IdleState);
            if (!_stateMachine.Player.Input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.WalkingState);
            if (_stateMachine.Player.Input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.RunningState);
            if (_stateMachine.Player.Input.actions["Switch"].triggered)
            {
                _stateMachine.ChangeState(_stateMachine.FollowingState);
            }
        }

        public override void Update()
        {
        }

        public override void PhysicsUpdate()
        {
            Jump();
        }
        
        private void Jump()
        {
            Debug.Log("Jump");
            // if(Player.isGrounded)
            //     Player.PlayerRB.AddForce(new Vector2(1,Player.jumpForce), (ForceMode2D)ForceMode.VelocityChange);
        }
    }
}