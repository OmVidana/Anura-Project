using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Anura
{
    public class Player : MonoBehaviour
    {
        private MovementStateMachine movementStateMachine;
        public SpriteRenderer spriteRenderer;
        public Animator playerAnimator;
        public Rigidbody2D playerRB;
        public CapsuleCollider2D collider2D;
        public PlayerInput input;
        public PlayerParameters playerData;

        public LayerMask enemyLayer;
        private bool _attackOnCooldown;
        private int _playerDirection = 1;
        private void Awake()
        {
            movementStateMachine = new MovementStateMachine(this);
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerAnimator = GetComponent<Animator>();
            playerRB = GetComponent<Rigidbody2D>();
            collider2D = GetComponent<CapsuleCollider2D>();
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
            Attack();
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
            if (!spriteRenderer.flipX && MovementInput().x < 0f || spriteRenderer.flipX && MovementInput().x > 0f)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                _playerDirection *= -1;
            }
        }

        public bool IsGrounded()
        {
            Vector2 boxOrigin = new Vector2(collider2D.transform.position.x, collider2D.bounds.min.y);
            Vector2 boxSize = new Vector2(collider2D.size.x, playerData.GroundOffset);
            return Physics2D.BoxCast(boxOrigin, boxSize, 0, Vector2.down, playerData.GroundOffset, playerData.Jumpable) ||
                   Physics2D.BoxCast(boxOrigin, boxSize, 0, Vector2.down, playerData.GroundOffset, playerData.Interactable);
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
        
        private void Attack()
        {
            if (input.actions["Attack"].triggered && !_attackOnCooldown)
            {
                Collider2D _attackCollider = Physics2D.OverlapCircle(collider2D.bounds.center + new Vector3(playerData.AttackRange * _playerDirection, 0, 0), playerData.AttackRadius, enemyLayer);
                Debug.Log(_attackCollider);
                StartCoroutine(Attacking());
            }
        }
        
        IEnumerator Attacking()
        {
            yield return new WaitForSeconds(playerData.AttackCooldown);
            _attackOnCooldown = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Recibe 1 de daño
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            //Recibe 1 de daño cada x segundos delimitados
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(collider2D.bounds.center + new Vector3(playerData.AttackRange * _playerDirection, 0, 0), playerData.AttackRadius);
        }
    }
}