using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class ScarabAggroState : State
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly Enemy _enemy;
        private bool _timerActive;
        private Coroutine _idleTime;
        public ScarabAggroState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyStateMachine.AggroState = this;
            _enemy = _enemyStateMachine.Enemy;
        }
        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }

        public override void Update()
        {
            SwitchState();
            _enemy.EnemyDirection();
        }

        public override void PhysicsUpdate()
        {
            _enemy.Move();
        }

        public override void SwitchState()
        {
            if (!_enemy.InRange())
                _enemyStateMachine.ChangeState(_enemyStateMachine.IdleState);
        }
    }
}
