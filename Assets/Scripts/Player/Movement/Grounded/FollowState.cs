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
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnHandle()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void PhysicsUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
