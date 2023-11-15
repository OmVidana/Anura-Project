using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Anura
{
    public class Player : MonoBehaviour
    {
        public MovementStateMachine movementStateMachine;
        public Rigidbody2D PlayerRB { get; private set; }
        public PlayerInput Input { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        [Header("Character Settings")] 
        public float walkingSpeed;
        public float runningSpeed;
        public float crouchingSpeed;
        
        public float jumpForce;
        public float groundOffset;
        public bool isGrounded;
        
        public int attackDmg;
        public float health;
        
        [NonSerialized] public bool isActive;
        
        [NonSerialized] public bool isFacingRight = true;

        public State startState;

        private void Awake()
        {
            PlayerRB = GetComponent<Rigidbody2D>();
            Input = GetComponent<PlayerInput>();
            movementStateMachine = new MovementStateMachine(this);
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            movementStateMachine.ChangeState(startState);
        }

        private void Update()
        {
            movementStateMachine.OnHandle();
            movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
    }
}