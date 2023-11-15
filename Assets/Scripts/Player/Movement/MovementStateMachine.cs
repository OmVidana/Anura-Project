using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class MovementStateMachine : StateMachine
    {
        public Vector2 MovementInput;
        public Player Player { get; }
        public IdleState IdleState { get; }
        public DisableState DisableState { get; }
        public WalkState WalkingState { get; }
        public RunState RunningState { get; }
        public JumpState JumpingState { get; }

        public MovementStateMachine(Player player)
        {
            Player = player;
            IdleState = new IdleState(this);
            DisableState = new DisableState(this);
            WalkingState = new WalkState(this);
            RunningState = new RunState(this);
            JumpingState = new JumpState(this);
        }
        
        
        public void ObtainInput()
        {
            MovementInput = Player.Input.actions["Move"].ReadValue<Vector2>();
        }
        
        public void Move(float givenSpeed)
        {
            if (MovementInput == Vector2.zero)
                return;
            
            Player.PlayerRB.velocity = givenSpeed * new Vector2(MovementInput.x, 0);
        }

        public void PlayerDirection()
        {
            if (Player.isFacingRight && MovementInput.x < 0f || !Player.isFacingRight && MovementInput.x > 0f)
            {
                Player.isFacingRight = !Player.isFacingRight;
                Player.SpriteRenderer.flipX = !Player.isFacingRight;
            }
        }
    }
}
