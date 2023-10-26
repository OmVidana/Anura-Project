using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class MovementStateMachine : StateMachine
    {
        public Player Player { get; }
        public IdleState IdleState { get; }
        public WalkState WalkingState { get; }
        public RunState RunningState { get; }

        public MovementStateMachine(Player player)
        {
            Player = player;
            IdleState = new IdleState(this);
            WalkingState = new WalkState(this);
            RunningState = new RunState(this);
        }
    }
}
