using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    public interface IInteractable
    {
        public string InteractionPrompt { get; }
        public void Interact(Interactor interactor);
    }
}
