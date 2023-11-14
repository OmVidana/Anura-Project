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

        [Header("Character Settings")] public float walkingSpeed;
        public float crouchingSpeed;
        public float runningSpeed;
        public float jumpForce;
        public int attackDmg;
        public float health;
        [NonSerialized]
        public bool isGrounded;
        public bool isActive;

        private void Awake()
        {
            PlayerRB = GetComponent<Rigidbody2D>();
            Input = GetComponent<PlayerInput>();
            movementStateMachine = new MovementStateMachine(this);
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdleState);
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