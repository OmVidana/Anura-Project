using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class WalkingState : MoveState
    {
        protected Vector2 MovementInput;

        public WalkingState(MovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Walking");
        }

        public override void OnExit()
        {
        }

        public override void OnInput()
        {
            MovementInput = StateMachine.Player.Input.actions["Move"].ReadValue<Vector2>();
        }

        public override void Update()
        {
        }

        public override void PhysicsUpdate()
        {
            if (MovementInput == Vector2.zero)
                return;
            Vector2 moveDirection = new Vector2(MovementInput.x, 0);
            Vector2 currentHorizontalSpeed = new Vector2(StateMachine.Player.PlayerRB.velocity.x, 0);
            float speed = StateMachine.Player.walkingSpeed;
            StateMachine.Player.PlayerRB.AddForce(moveDirection * speed - currentHorizontalSpeed, (ForceMode2D)ForceMode.VelocityChange);
        }
    }
}