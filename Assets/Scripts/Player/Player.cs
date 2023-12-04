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
        public CapsuleCollider2D _collider2D;
        public PlayerInput input;
        public PlayerParameters playerData;
        public LayerMask enemyLayer;
        
        private bool _attackOnCooldown;
        
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
            Attack();
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
            //Create a collider infront of the player and play attackAnimation
            if (input.actions["Attack"].triggered && !_attackOnCooldown)
            {
                _attackOnCooldown = true;
                StartCoroutine(AttackCoroutine(playerData.AttackCooldown));
            }
        }

        IEnumerator AttackCoroutine(float cooldown)
        {
            playerAnimator.SetTrigger("Attacking");
            Collider2D _attackCollider = Physics2D.OverlapCircle(_collider2D.bounds.center + new Vector3(playerData.AttackRange * MovementInput().x, 0, 0), playerData.AttackRadius, enemyLayer);
            Debug.Log(Physics2D.OverlapCircle(_collider2D.bounds.center + new Vector3(playerData.AttackRange, 0, 0), playerData.AttackRadius, enemyLayer));
            yield return new WaitForSeconds(cooldown);
            _attackOnCooldown = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(_collider2D.bounds.center + new Vector3(playerData.AttackRange * MovementInput().x, 0, 0), playerData.AttackRadius);
        }
    }
}