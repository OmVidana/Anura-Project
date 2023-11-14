using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Anura
{
    public class PlayerManager : MonoBehaviour
    {
        public Player Anura;
        public Player Uri;
        
        private void Awake()
        {
        
        }

        private void Start()
        {
            Anura.isActive = true;
            Anura.movementStateMachine.ChangeState(Anura.movementStateMachine.IdleState);
            Uri.isActive = true;
            Anura.movementStateMachine.ChangeState(Anura.movementStateMachine.IdleState);
        }
    }
}
