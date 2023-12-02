using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class RunState : State
    {
        private readonly MovementStateMachine _stateMachine;
        private Player _player;
        private PlayerParameters _playerData;

        public RunState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _player = stateMachine.Player;
            _playerData = stateMachine.Player.playerData;
        }

        public override void OnEnter()
        {
            _player.playerAnimator.SetBool("IsRunning", true);
        }

        public override void OnExit()
        {
            _player.playerAnimator.SetBool("IsRunning", false);

        }

        public override void Update()
        {
            _player.PlayerDirection();
            SwitchState();
        }

        public override void PhysicsUpdate()
        {
            _player.Move(_playerData.RunningSpeed);
        }
        
        public override void SwitchState()
        {
            if (_player.MovementInput().x == 0f)
                _stateMachine.ChangeState(_stateMachine.IdleState);
            if (!_player.input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.WalkingState);
            if (_player.input.actions["Jump"].triggered && _player.IsGrounded())
                _stateMachine.ChangeState(_stateMachine.JumpingState);
        }
    }
}
