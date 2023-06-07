using PV.Core;
using PV.Core.Utility;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PV.Interaction
{
    [System.Serializable]
    public class InteractionManager : Core.Behaviour
    {
        private struct InteractableTransform
        {
            public IInteractable Interactable;
            public Transform Owner;

            public static implicit operator bool(InteractableTransform left)
            {
                return !(left.Owner == null);
            }

            public InteractableTransform(IInteractable interactable, Transform owner)
            {
                Interactable = interactable;
                Owner = owner;
            }
        }

        public Action<InteractionSettings> OnInteractionCompleted;

        [SerializeField] private InteractionSettings m_Settings;
        [SerializeField] private InteractableUI m_InteractableUI;
        [SerializeField] private ColliderEvent m_InteractableCheckCollider;

        private InteractableTransform m_CurrentInteractable;

        public override void OnStart()
        {
            InputManager.Instance.Subscribe(OnInteract, OnPickUp);

            m_InteractableCheckCollider.OnCollisionEnterEvent += OnColliderEnterEvent;
            m_InteractableCheckCollider.OnCollisionExitEvent += OnColliderExitEvent;

            m_InteractableUI.gameObject.SetActive(false);
        }

        public override void OnDestroy()
        {
            m_InteractableCheckCollider.OnCollisionEnterEvent -= OnColliderEnterEvent;
            m_InteractableCheckCollider.OnCollisionExitEvent -= OnColliderExitEvent;

            InputManager.Instance.Unsubscribe(OnInteract, OnPickUp);
        }

        private void OnColliderExitEvent(Collider collision)
        {
            m_CurrentInteractable = new InteractableTransform();
            m_InteractableUI.gameObject.SetActive(false);
        }

        public void OnColliderEnterEvent(Collider collision)
        {
            if (!collision.transform.TryGetComponent<IInteractable>(out var interactable))
                return;

            if (collision.transform == m_CurrentInteractable.Owner)
                return;

            if (m_CurrentInteractable) {
                float collisionDistance = Vector3.Distance(collision.transform.position, m_InteractableCheckCollider.transform.position);
                float interactableDistance = Vector3.Distance(m_CurrentInteractable.Owner.position, m_InteractableCheckCollider.transform.position);

                if (collisionDistance > interactableDistance)
                    return;
            }

            m_CurrentInteractable = new InteractableTransform(interactable, collision.transform);
            m_InteractableUI.transform.position = m_CurrentInteractable.Owner.position;
            m_InteractableUI.gameObject.SetActive(true);

            UpdateInteractable(interactable.GetSettings().Type, false);
        }

        public void UpdateInteractable(InteractionSetting.InteractionType type, bool holding)
        {
            var settings = m_CurrentInteractable.Interactable.GetSettings();
            var interactionSetting = m_Settings.Settings.Find(e => e.Type == type);
            m_InteractableUI.Init(settings.InteractableTitle, interactionSetting.ButtonSprite, interactionSetting.Content, settings.HoldInteraction, settings.InteractionDuration, holding);
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (!m_CurrentInteractable)
                return;

            bool interacting = m_CurrentInteractable.Interactable.OnInteract();
            UpdateInteractable(m_CurrentInteractable.Interactable.GetSettings().Type, context.ReadValueAsButton());

            if (interacting) {
                OnInteractionCompleted?.Invoke(m_Settings);
            }
        }

        private void OnPickUp(InputAction.CallbackContext context)
        {
            if (!m_CurrentInteractable)
                return;

            bool interacting = m_CurrentInteractable.Interactable.OnInteract();
            UpdateInteractable(m_CurrentInteractable.Interactable.GetSettings().Type, context.ReadValueAsButton());

            if (interacting) {
                OnInteractionCompleted?.Invoke(m_Settings);
            }
        }
    }
}