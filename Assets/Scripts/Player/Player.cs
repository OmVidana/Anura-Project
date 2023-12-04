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
        public PlayerManager playerManager;
        
        public LayerMask enemyLayer;
        [SerializeField] private Transform _damageIndicator;
        [NonSerialized] public bool attackOnCooldown;
        [NonSerialized] public bool hitOnCooldown;
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
            if (spriteRenderer.flipX)
                _playerDirection = -1;
            else
                _playerDirection = 1;
            DamageIndicator();
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
                spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        public bool IsGrounded()
        {
            Vector2 boxOrigin = new Vector2(collider2D.transform.position.x, collider2D.bounds.min.y);
            Vector2 boxSize = new Vector2(collider2D.size.x, playerData.GroundOffset);
            return Physics2D.BoxCast(boxOrigin, boxSize, 0, Vector2.down, playerData.GroundOffset, playerData.JumpableLayers);
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
            if (input.actions["Attack"].triggered && !attackOnCooldown)
            {
                attackOnCooldown = true;
                StartCoroutine(Attacking());
            }
        }
        
        IEnumerator Attacking()
        {
            playerAnimator.SetTrigger("Attacking");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(collider2D.bounds.center + new Vector3(playerData.AttackRange * _playerDirection, 0, 0), playerData.AttackRadius, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(playerData.AttackDmg);
            }

            yield return new WaitForSeconds(playerData.AttackCooldown);
            attackOnCooldown = false;
        }

        private void DamageIndicator()
        {
            _damageIndicator.position = collider2D.bounds.center + new Vector3((playerData.AttackRange + playerData.AttackRadius) * _playerDirection, 0, 0);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!hitOnCooldown && other.gameObject.CompareTag("Enemy"))
            {
                playerAnimator.SetTrigger("Hitted");
                playerManager.currentHealth -= other.gameObject.GetComponent<Enemy>().attackDmg;
                if (playerManager.currentHealth <= 0)
                    playerManager.Death();
                hitOnCooldown = true;
                StartCoroutine(WaitForHit());
            }
            if  (!hitOnCooldown && other.gameObject.layer.Equals(7))
            {
                playerAnimator.SetTrigger("Hitted");
                playerManager.currentHealth -= 1;
                if (playerManager.currentHealth <= 0)
                    playerManager.Death();
                hitOnCooldown = true;
                StartCoroutine(WaitForHit());
            }  
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if ((!hitOnCooldown && other.gameObject.CompareTag("Enemy")) ||
                (!hitOnCooldown && other.gameObject.layer.Equals(7)))
            {
                playerAnimator.SetTrigger("Hitted");
                playerManager.currentHealth -= other.gameObject.GetComponent<Enemy>().attackDmg;
                if (playerManager.currentHealth <= 0)
                    playerManager.Death();
                hitOnCooldown = true;
                StartCoroutine(WaitForHit());
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((!hitOnCooldown && other.gameObject.CompareTag("Enemy")) ||
                (!hitOnCooldown && other.gameObject.layer.Equals(7)))
            {
                playerAnimator.SetTrigger("Hitted");
                playerManager.currentHealth -= other.gameObject.GetComponent<Enemy>() == null ? 1 : other.gameObject.GetComponent<Enemy>().attackDmg;
                if (playerManager.currentHealth <= 0)
                    playerManager.Death();
                hitOnCooldown = true;
                StartCoroutine(WaitForHit());

            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if ((!hitOnCooldown && other.gameObject.CompareTag("Enemy")) ||
                (!hitOnCooldown && other.gameObject.layer.Equals(7)))
            {
                playerAnimator.SetTrigger("Hitted");
                playerManager.currentHealth -= other.gameObject.GetComponent<Enemy>() == null ? 1 : other.gameObject.GetComponent<Enemy>().attackDmg;
                if (playerManager.currentHealth <= 0)
                    playerManager.Death();
                hitOnCooldown = true;
                StartCoroutine(WaitForHit());
            }
        }

        IEnumerator WaitForHit()
        {
            yield return new WaitForSeconds(playerData.TakingDmgCooldown);
            hitOnCooldown = false;
        }
        
        // private void OnDrawGizmos()
        // {
        //     Gizmos.DrawWireSphere(collider2D.bounds.center + new Vector3(playerData.AttackRange * _playerDirection, 0, 0), playerData.AttackRadius);
        // }
    }
}