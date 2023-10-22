using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class IdleState : MoveState
    {
        public IdleState(MovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Idle");
        }

        public override void OnExit()
        {
            
        }

        public override void OnInput()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void PhysicsUpdate()
        {
            
        }
    }
}