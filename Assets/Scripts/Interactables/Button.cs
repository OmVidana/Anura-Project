using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class Button : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _buttonAnimator;
        [SerializeField] private string _prompt;
        public string InteractionPrompt => _prompt;

        [SerializeField] private float _timePressed;
        [NonSerialized] public bool isTriggered;
        public void Interact(Interactor interactor)
        {
            //Display a Canvas Saying "Press Me"
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!isTriggered && other.gameObject.tag.Equals("Player"))
            {
                isTriggered = true;
                _buttonAnimator.SetBool("IsPressed", isTriggered);
                StartCoroutine(PressedTime());
            }
        }

        IEnumerator PressedTime()
        {
            yield return new WaitForSeconds(_timePressed);
            isTriggered = false;
            _buttonAnimator.SetBool("IsPressed", isTriggered);
        }
    }
}
