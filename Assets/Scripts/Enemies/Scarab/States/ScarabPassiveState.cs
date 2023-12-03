using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class ScarabPassiveState : State
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly Enemy _enemy;
        private bool _timerActive;
        private Coroutine _passiveTime;
        public ScarabPassiveState(EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyStateMachine.PassiveState = this;
            _enemy = _enemyStateMachine.Enemy;
        }

        public override void OnEnter()
        {
            _timerActive = true;
            _passiveTime = _enemy.StartCoroutine(PassiveTime());
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
            _enemy.Move();
            _enemy.EnemyDirection();
        }

        public override void SwitchState()
        {
            if (_enemy.InRange())
            {
                if(_passiveTime != null)
                    _enemy.StopCoroutine(_passiveTime);
                _enemyStateMachine.ChangeState(_enemyStateMachine.AggroState);
            }
            if (!_enemy.InRange() && !_timerActive)
                _enemyStateMachine.ChangeState(_enemyStateMachine.IdleState);
        }

        IEnumerator PassiveTime()
        {
            yield return new WaitForSeconds(8.0f);
            _timerActive = false;
        }
    }
}