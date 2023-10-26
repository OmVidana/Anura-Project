using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class WalkState : State
    {
        private readonly MovementStateMachine _stateMachine;
        private Vector2 _movementInput;

        public WalkState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnHandle()
        {
            _movementInput = _stateMachine.Player.Input.actions["Move"].ReadValue<Vector2>();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void PhysicsUpdate()
        {
            if (_movementInput == Vector2.zero)
                return;
            Vector2 moveDirection = new Vector2(_movementInput.x, 0);
            Vector2 currentHorizontalSpeed = new Vector2(_stateMachine.Player.PlayerRB.velocity.x, 0);
            float speed = _stateMachine.Player.walkingSpeed;
            _stateMachine.Player.PlayerRB.AddForce(moveDirection * speed - currentHorizontalSpeed, (ForceMode2D)ForceMode.VelocityChange);
        }
    }
}
