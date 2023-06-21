using PV.Core.Utility;
using PV.Input;
using System;
using System.Linq;
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
            if (m_InteractableCheckCollider) {
                m_InteractableCheckCollider.OnCollisionEnterEvent += OnCollisionEnterEvent;
                m_InteractableCheckCollider.OnCollisionExitEvent += OnCollisionExitEvent;
            }

            if (m_InteractableUI)
                m_InteractableUI.gameObject.SetActive(false);
        }

        public override void OnDestroy()
        {
            if (m_InteractableCheckCollider) {
                m_InteractableCheckCollider.OnCollisionEnterEvent -= OnCollisionEnterEvent;
                m_InteractableCheckCollider.OnCollisionExitEvent -= OnCollisionExitEvent;
            }

            InputManager.Instance.Unsubscribe(OnInteract, OnPickUp);
        }

        private void OnCollisionExitEvent(Collider collision)
        {
            if (!collision.transform.TryGetComponent<IInteractable>(out var interactable))
                return;
            if (collision.transform != m_CurrentInteractable.Owner)
                return;

            m_CurrentInteractable = new InteractableTransform();
            m_InteractableUI.gameObject.SetActive(false);
        }

        public void OnCollisionEnterEvent(Collider collision)
        {
            var interactables = collision.transform.GetComponents<IInteractable>();
            if (interactables.Length <= 0)
                return;

            if (collision.transform == m_CurrentInteractable.Owner)
                return;

            if (m_CurrentInteractable) {
                float collisionDistance = Vector3.Distance(collision.transform.position, m_InteractableCheckCollider.transform.position);
                float interactableDistance = Vector3.Distance(m_CurrentInteractable.Interactable.GetInteractionTransform().position, m_InteractableCheckCollider.transform.position);

                if (collisionDistance > interactableDistance)
                    return;
            }

            var first = collision.transform.GetComponents<IInteractable>().ToList().Find(i => i.CanInteract());
            if (first == null)
                return;

            m_CurrentInteractable = new InteractableTransform(first, collision.transform);
            m_InteractableUI.transform.position = m_CurrentInteractable.Interactable.GetInteractionTransform().position;
            m_InteractableUI.gameObject.SetActive(true);

            UpdateInteractable(first.GetSettings().Type, false);
        }

        public void UpdateInteractable(InteractionSetting.InteractionType type, bool holding)
        {
            if (!m_CurrentInteractable.Interactable.CanInteract()) {
                m_CurrentInteractable = new InteractableTransform();
                m_InteractableUI.gameObject.SetActive(false);
                return;
            }

            var settings = m_CurrentInteractable.Interactable.GetSettings();
            var interactionSetting = m_Settings.Settings.Find(e => e.Type == type);
            m_InteractableUI.Init(settings.Name, interactionSetting.ButtonSprite, interactionSetting.Content, settings.HoldInteraction, settings.InteractionDuration, holding);
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