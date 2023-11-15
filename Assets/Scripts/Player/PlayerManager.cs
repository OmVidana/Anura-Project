using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class PlayerManager : MonoBehaviour
    {
        public Player Anura;
        public Player Uri;
        
        private void Awake()
        {
            Anura.isActive = true;
            Anura.startState = Anura.movementStateMachine.IdleState;
            
            Uri.isActive = false;
            Uri.startState = Uri.movementStateMachine.DisableState;
        }
    }
}
