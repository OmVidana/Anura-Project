using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class TrapDoor : MonoBehaviour
    {
        [SerializeField] private GameObject _activator;
        [SerializeField] private Animator _trapdoorAnimator;
        [SerializeField] private Collider2D _trapdoorCollider;
        private bool _isActive;

        private void Update()
        {
            if ((_activator.GetComponent<Button>() != null && _activator.GetComponent<Button>().isTriggered) ||
                (_activator.GetComponent<Lever>() != null && _activator.GetComponent<Lever>().isTriggered))
            {
                _isActive = true;
                _trapdoorCollider.enabled = !_isActive;
                _trapdoorAnimator.SetBool("IsActive", _isActive);
            }
            else
            {
                _isActive = false;
                _trapdoorCollider.enabled = !_isActive;
                _trapdoorAnimator.SetBool("IsActive", _isActive);
            }
        }
    }
}
