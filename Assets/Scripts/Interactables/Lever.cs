using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Anura
{
    public class Lever : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _leverAnimator;
        [SerializeField] private Animator _playerAnimator;
        public PlayerInput _playerInput;
        [SerializeField] private string _prompt;
        public string InteractionPrompt => _prompt;
        
        [NonSerialized] public bool isTriggered;
        public void Interact(Interactor interactor)
        {
            //Display a Text Saying "Activate Me"
            if (_playerInput.actions["Interact"].triggered && GameObject.Find("Anura") != null)
            {
                isTriggered = !isTriggered;
                _playerAnimator.SetTrigger("Interacting");
                _leverAnimator.SetBool("IsActive", isTriggered);
            }
        }
    }
}
