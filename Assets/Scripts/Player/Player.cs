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
        private SpriteRenderer SpriteRenderer;
        public Animator playerAnimator;
        public Rigidbody2D playerRB;
        private CapsuleCollider2D _collider2D;
        [NonSerialized] public PlayerInput input;
        public PlayerParameters playerData;

        private bool _isGrounded;

        private bool _isFacingRight = true;
        
        private void Awake()
        {
            movementStateMachine = new MovementStateMachine(this);
            SpriteRenderer = GetComponent<SpriteRenderer>();
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
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
        
        public Vector2 MovementInput()
        {
            Debug.Log(input.actions["Move"].ReadValue<Vector2>());
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
            if (_isFacingRight && MovementInput().x < 0f || !_isFacingRight && MovementInput().x > 0f)
            {
                _isFacingRight = !_isFacingRight;
                SpriteRenderer.flipX = !_isFacingRight;
            }
        }

        public bool IsGrounded()
        {
            Vector2 boxOrigin = new Vector2(_collider2D.transform.position.x, _collider2D.bounds.min.y);
            Vector2 boxSize = new Vector2(_collider2D.size.x, playerData.GroundOffset);
            return Physics2D.BoxCast(boxOrigin, boxSize, 0, Vector2.down, playerData.GroundOffset, playerData.Ground);
        }

        public void Jump()
        {
            playerRB.AddForce(new Vector2(0, playerData.JumpForce), ForceMode2D.Impulse);
        }
    }
}