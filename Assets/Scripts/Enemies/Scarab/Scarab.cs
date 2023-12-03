using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class Scarab : Enemy
    {
        [Header("Combat Colliders")] public Collider2D crushableArea;
        public Collider2D hitPointA;
        public Collider2D hitPointB;
        
        private void Awake()
        {
            enemyStateMachine = new EnemyStateMachine(this);
            idleState = new ScarabIdleState(enemyStateMachine);
            passiveState = new ScarabPassiveState(enemyStateMachine);
            aggroState = new ScarabAggroState(enemyStateMachine);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            enemyAnimator = GetComponent<Animator>();
            enemyRB = GetComponent<Rigidbody2D>();
            _bodyCollider2D = GetComponent<CapsuleCollider2D>();
        }

        void Start()
        {
            startingPosition = enemyRB.position;
            enemyStateMachine.ChangeState(enemyStateMachine.IdleState);
        }

        void Update()
        {
            enemyStateMachine.Update();
        }

        private void FixedUpdate()
        {
            enemyStateMachine.PhysicsUpdate();
            AfterJump();
        }

        public override void Move()
        {
            float minPosX = Mathf.Min(pointXA, pointXB);
            float maxPosX = Mathf.Max(pointXA, pointXB);

            if (enemyRB.position.x > maxPosX + 1f|| enemyRB.position.x < minPosX - 1f)
                enemyRB.position = startingPosition;
            if (enemyStateMachine.returnCurrent() == aggroState)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    float playerPosX = player.GetComponent<Rigidbody2D>().position.x;
                    float clampedPlayerX = Mathf.Clamp(playerPosX, minPosX, maxPosX);
                    direction = new Vector2(clampedPlayerX - enemyRB.position.x, 0).normalized;
                    enemyRB.velocity = direction * chasingSpeed;
                }
            }

            if (enemyStateMachine.returnCurrent() == passiveState)
            {
                if (isMovingRight)
                {
                    direction = new Vector2(pointXB - enemyRB.position.x, 0).normalized;
                    if (Mathf.Abs(enemyRB.position.x - pointXB) < 0.1f)
                    {
                        isMovingRight = false;
                    }
                }
                else
                {
                    direction = new Vector2(pointXA - enemyRB.position.x, 0).normalized;
                    if (Mathf.Abs(enemyRB.position.x - pointXA) < 0.1f)
                    {
                        isMovingRight = true;
                    }
                }

                enemyRB.velocity = direction * walkingSpeed;
            }
        }
    }
}