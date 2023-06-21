using UnityEngine;

namespace PV.Interaction
{

    public interface IInteractable
    {
        public bool OnInteract();
        public bool CanInteract();
        public Transform GetInteractionTransform();
        public InteractableSettings GetSettings();
    }
}