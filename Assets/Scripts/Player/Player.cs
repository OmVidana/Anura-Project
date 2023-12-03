using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Anura
{
    public class Player : MonoBehaviour
    {
        private MovementStateMachine movementStateMachine;
        private SpriteRenderer _spriteRenderer;
        public Animator playerAnimator;
        public Rigidbody2D playerRB;
        private CapsuleCollider2D _collider2D;
        [NonSerialized] public PlayerInput input;
        public PlayerParameters playerData;
        
        private void Awake()
        {
            movementStateMachine = new MovementStateMachine(this);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            playerAnimator = GetComponent<Animator>();
            playerRB = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<CapsuleCollider2D>();
            input = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdleState);
        }

        private void Update()
        {
            movementStateMachine.Update();
            playerAnimator.SetBool("IsGrounded", IsGrounded());
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();            
            AfterJump();
        }
        
        public Vector2 MovementInput()
        {
            return input.actions["Move"].ReadValue<Vector2>();
        }

        public void Move(float givenSpeed)
        {
            if (MovementInput() == Vector2.zero)
                playerRB.velocity = Vector2.zero;
            Vector2 currentVelocity = new Vector2(playerRB.velocity.x, 0);
            playerRB.AddForce(new Vector2(givenSpeed * MovementInput().x, 0) - currentVelocity, ForceMode2D.Impulse);
        }

        public void PlayerDirection()
        {
            if (!_spriteRenderer.flipX && MovementInput().x < 0f || _spriteRenderer.flipX && MovementInput().x > 0f)
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        public bool IsGrounded()
        {
            Vector2 boxOrigin = new Vector2(_collider2D.transform.position.x, _collider2D.bounds.min.y);
            Vector2 boxSize = new Vector2(_collider2D.size.x, playerData.GroundOffset);
            return Physics2D.BoxCast(boxOrigin, boxSize, 0, Vector2.down, playerData.GroundOffset, playerData.Jumpable);
        }

        public void Jump()
        {
            playerRB.AddForce(new Vector2(0, playerData.JumpForce), ForceMode2D.Impulse);
        }

        private void AfterJump()
        {
            if (!IsGrounded() && playerRB.velocity.y < 0.5f && playerRB.velocity.y > playerData.MaxJumpDeceleration)
            { 
                playerRB.AddForce(new Vector2(0, playerData.JumpDownForce), ForceMode2D.Impulse);
            }
        }
    }
}