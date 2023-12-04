using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Anura
{
    public class UriTubes : MonoBehaviour, IInteractable
    {
        [SerializeField] private Collider2D _tubesCollider;
        public PlayerInput _playerInput;
        [SerializeField] private string _prompt;
        public string InteractionPrompt => _prompt;
        public void Interact(Interactor interactor)
        {
            // if (_playerInput.actions["Interact"].triggered && GameObject.Find("Uri") != null)
            //     Debug.Log(_tubesCollider.isTrigger);
            // if(GameObject.Find("Uri") == null)
            //     _tubesCollider.isTrigger = false;
        }
    }
}
