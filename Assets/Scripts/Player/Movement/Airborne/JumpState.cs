using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class JumpState : State
    {
        private MovementStateMachine _stateMachine;

        public JumpState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            Debug.Log("Jump Start");
        }

        public override void OnExit()
        {
            Debug.Log("Jump Exit");
        }

        public override void OnHandle()
        {
            HandleJump();
            _stateMachine.ObtainInput();
            
            if (_stateMachine.MovementInput.x == 0f)
                _stateMachine.ChangeState(_stateMachine.IdleState);
            if (!_stateMachine.Player.Input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.WalkingState);
            if (_stateMachine.Player.Input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.RunningState);
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
            Jump();
        }
        
        private void Jump()
        {
            if (_stateMachine.Player.isGrounded)
                _stateMachine.Player.PlayerRB.AddForce(new Vector2(0f, _stateMachine.Player.jumpForce), ForceMode2D.Impulse);
        }

        private void HandleJump()
        {
            _stateMachine.Player.isGrounded = Physics2D.Raycast(_stateMachine.Player.transform.position, Vector2.down, _stateMachine.Player.groundOffset);
            Debug.Log(_stateMachine.Player.isGrounded);
        }
    }
}