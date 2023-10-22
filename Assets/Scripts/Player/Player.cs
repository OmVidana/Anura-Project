using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Anura
{
    public class Player : MonoBehaviour
    {
        private MovementStateMachine _movementMovementStateMachine;
        public Rigidbody2D PlayerRB { get; private set; }
        public PlayerInput Input { get; private set; }

        [Header("Character Settings")]
        public float walkingSpeed;
        public float crouchingSpeed;
        public float runningSpeed;
        public float jumpHeight;
        public int attackDmg;
        public float health;
        
        private void Awake()
        {
            PlayerRB = GetComponent<Rigidbody2D>();
            Input = GetComponent<PlayerInput>();
            _movementMovementStateMachine = new MovementStateMachine(this);
        }

        private void Start()
        {
            _movementMovementStateMachine.ChangeState(_movementMovementStateMachine.IdleState); 
        }

        private void Update()
        {
            _movementMovementStateMachine.InputHandling();
            _movementMovementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _movementMovementStateMachine.PhysicsUpdate();
        }
    }
}