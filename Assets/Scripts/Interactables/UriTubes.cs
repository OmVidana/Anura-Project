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
            //Display a Text saying go inside Anura
            if (GameObject.Find("Uri") != null)
                _tubesCollider.isTrigger = true;
            else
                _tubesCollider.isTrigger = false;
        }
    }
}
