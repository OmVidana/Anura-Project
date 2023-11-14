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
        public FollowState FollowingState { get; }
        public WalkState WalkingState { get; }
        public RunState RunningState { get; }
        public JumpState JumpingState { get; }

        public MovementStateMachine(Player player)
        {
            Player = player;
            IdleState = new IdleState(this);
            FollowingState = new FollowState(this);
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
            Vector2 moveDirection = new Vector2(MovementInput.x, 0);
            Vector2 currentHorizontalSpeed = new Vector2(Player.PlayerRB.velocity.x, 0);
            float speed = givenSpeed;
            Player.PlayerRB.AddForce(moveDirection * speed - currentHorizontalSpeed, (ForceMode2D)ForceMode.VelocityChange);
        }

        public void Rotate()
        {
            // Player.transform.rotation = Quaternion.Euler(0, 180 * Mathf.Clamp(MovementInput.x, -1, 0),0);
        }
    }
}
