using UnityEngine;

namespace PV.Interaction
{
    [System.Serializable]
    public struct InteractableSettings
    {
        public InteractionSetting.InteractionType Type;
        public string Name;
        public bool HoldInteraction;
        public float InteractionDuration;

        public InteractableSettings(InteractionSetting.InteractionType type, string name, bool holdInteraction, float interactionDuration)
        {
            Type = type;
            Name = name;
            HoldInteraction = holdInteraction;
            InteractionDuration = interactionDuration;
        }
    }

    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private InteractableSettings m_InteractableSettings;

        private bool m_Holding = false;
        private float m_HeldTime = 0.0f;

        public virtual void Update()
        {
            if (m_Holding) {
                m_HeldTime += Time.deltaTime;
                return;
            }
            if (m_HeldTime > 0.0f) {

                m_HeldTime = 0.0f;
            }
        }

        public bool OnInteract()
        {
            m_Holding = !m_Holding;

            if (m_InteractableSettings.HoldInteraction) {
                OnInteraction(m_HeldTime >= m_InteractableSettings.InteractionDuration);
                return m_HeldTime >= m_InteractableSettings.InteractionDuration;
            }

            OnInteraction(m_Holding);
            return m_Holding;
        }

        public InteractableSettings GetSettings()
        {
            return m_InteractableSettings;
        }

        protected virtual void OnInteraction(bool success) { }
    }
}