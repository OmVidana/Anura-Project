using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public abstract class Enemy : MonoBehaviour
    {
        protected EnemyStateMachine enemyStateMachine;
        public State idleState;
        public State passiveState;
        public State aggroState;
        
        protected SpriteRenderer _spriteRenderer;
        public Animator enemyAnimator;
        public Rigidbody2D enemyRB;
        protected CapsuleCollider2D _bodyCollider2D;
        protected Vector2 startingPosition;
        
        [Header("Enemy Movement")]
        protected bool isMovingRight = true;
        protected Vector2 direction = new Vector2(1, 0);
        [field: Range(0f, 16f)] public float walkingSpeed;
        [field: Range(0f, 20f)] public float chasingSpeed;
        [field: Range(0f, 50f)] public float jumpForce;
        [field: Range(-10f, 0f)] public float jumpDownForce;
        [field: Range(-50f, 0f)] public float maxJumpDeceleration;
        [field: Range(0f, 1f)] public float groundOffset;
        public LayerMask jumpable;
        
        [Header("Combat Stats")]
        [field: Range(0, 100)] public int health;
        [field: Range(0, 100)] public int attackDmg;
        [field: Range(0f, 8f)] public float AggroDistance;
        
        [Header("Movement Delimiters")]
        public int pointXA;
        public int pointXB;
        
        public abstract void Move();

        public bool InRange()
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null) return false;
            if (AggroDistance >= Vector2.Distance(transform.position, player.transform.position))
                return true;
            return false;
        }

        public void EnemyDirection()
        {
            if (!_spriteRenderer.flipX && direction.x < 0f)
                _spriteRenderer.flipX = true;
            if (_spriteRenderer.flipX && direction.x > 0f)
                _spriteRenderer.flipX = false;
        }

        private bool IsGrounded()
        {
            Vector2 boxOrigin = new Vector2(_bodyCollider2D.transform.position.x, _bodyCollider2D.bounds.min.y);
            Vector2 boxSize = new Vector2(_bodyCollider2D.size.x, groundOffset);
            return Physics2D.BoxCast(boxOrigin, boxSize, 0, Vector2.down, groundOffset, jumpable);;
        }

        public void Jump()
        {
            enemyRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        public void AfterJump()
        {
            if (!IsGrounded() && enemyRB.velocity.y < 0.5f && enemyRB.velocity.y > maxJumpDeceleration)
            { 
                enemyRB.AddForce(new Vector2(0, jumpDownForce), ForceMode2D.Impulse);
            }
        }
    }
}
