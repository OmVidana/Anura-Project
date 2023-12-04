using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class EnemyStateMachine : StateMachine
    {
        public Enemy Enemy { get; }
        public State IdleState { get; set; }
        public State PassiveState { get; set; }
        public State AggroState { get; set; }

        public EnemyStateMachine(Enemy enemy)
        {
            Enemy = enemy;
        }
    }
}