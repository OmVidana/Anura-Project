using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Collider2D _interactionPoint;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactableMask;

        private readonly Collider2D[] _colliders = new Collider2D[3];
        [SerializeField] private int _found;
        
        private void FixedUpdate()
        {
            _found = Physics2D.OverlapCircleNonAlloc(_interactionPoint.bounds.center, _interactionRadius, _colliders,
                _interactableMask);

            if (_found > 0)
            {
                var interactable = _colliders[0].GetComponent<IInteractable>();
                if (interactable != null)
                    interactable.Interact(this);
            }
        }
    }
}
