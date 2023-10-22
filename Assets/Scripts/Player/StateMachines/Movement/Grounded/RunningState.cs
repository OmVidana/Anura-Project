using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class RunningState : MoveState
    {
        protected Vector2 MovementInput;

        public RunningState(MovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnInput()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void PhysicsUpdate()
        {
            if (MovementInput == Vector2.zero)
                return;
            Vector2 moveDirection = new Vector2(MovementInput.x, 0);
            Vector2 currentHorizontalSpeed = new Vector2(StateMachine.Player.PlayerRB.velocity.x, 0);
            float speed = StateMachine.Player.runningSpeed;
            StateMachine.Player.PlayerRB.AddForce(moveDirection * speed - currentHorizontalSpeed,
                (ForceMode2D)ForceMode.VelocityChange);
        }
    }
}