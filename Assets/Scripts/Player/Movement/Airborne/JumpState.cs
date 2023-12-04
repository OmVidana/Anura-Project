using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class JumpState : State
    {
        private readonly MovementStateMachine _stateMachine;
        private Player _player;
        private PlayerParameters _playerData;
        private bool _isJumping;
        public JumpState(MovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _player = stateMachine.Player;
            _playerData = stateMachine.Player.playerData;
        }

        public override void OnEnter()
        {
            _player.playerAnimator.SetBool("IsJumping", true);
            _player.Jump();
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
            if (_player.MovementInput().x == 0f)
                _stateMachine.ChangeState(_stateMachine.IdleState);
            else
                _stateMachine.ChangeState(_stateMachine.WalkingState);
            if (_player.input.actions["Sprint"].IsPressed())
                _stateMachine.ChangeState(_stateMachine.RunningState);
        }
    }
}