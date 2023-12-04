using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class IdleState : State
    {
        private readonly MovementStateMachine _stateMachine;
        private Player _player;
        private PlayerParameters _playerData;

        public IdleState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _player = stateMachine.Player;
            _playerData = _player.playerData;
        }

        public override void OnEnter()
        {
            _player.playerAnimator.SetBool("IsIdle", true);
        }

        public override void OnExit()
        {
            _player.playerAnimator.SetBool("IsIdle", false);
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
            if (_player.MovementInput().x != 0)
                _stateMachine.ChangeState(_stateMachine.WalkingState);
            if (_player.input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.RunningState);
            if (_player.input.actions["Jump"].triggered && (_player.IsGrounded() || _player.IsInsideTube()))
                _stateMachine.ChangeState(_stateMachine.JumpingState);
        }
    }
}