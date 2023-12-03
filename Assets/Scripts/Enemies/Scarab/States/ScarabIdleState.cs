using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class ScarabIdleState : State
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly Enemy _enemy;
        private bool _timerActive;
        private Coroutine _idleTime;
        public ScarabIdleState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyStateMachine.IdleState = this;
            _enemy = _enemyStateMachine.Enemy;
        }

        public override void OnEnter()
        {
            _timerActive = true;
            _idleTime = _enemy.StartCoroutine(IdlingTime());
        }

        public override void OnExit()
        {
            
        }

        public override void Update()
        {
            SwitchState();
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void SwitchState()
        {
            if (_enemy.InRange())
            {
                if(_idleTime != null)
                    _enemy.StopCoroutine(_idleTime);
                _enemyStateMachine.ChangeState(_enemyStateMachine.AggroState);
            }
            if (!_enemy.InRange() && !_timerActive)
                _enemyStateMachine.ChangeState(_enemyStateMachine.PassiveState);
        }
        
        IEnumerator IdlingTime()
        {
            yield return new WaitForSeconds(2.0f);
            _timerActive = false;
        }
    }
}
