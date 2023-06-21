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
        private bool m_Success = false;

        public virtual void Update()
        {
            if (m_Holding) {
                m_HeldTime += Time.deltaTime;

                if(m_HeldTime >= m_InteractableSettings.InteractionDuration && !m_Success) {
                    OnInteraction(true);
                    m_Success = true;
                }
                return;
            }
            m_Success = false;
            m_HeldTime = 0.0f;
        }

        public bool OnInteract()
        {
            m_Holding = !m_Holding;

            if (m_InteractableSettings.HoldInteraction) {
                if(m_HeldTime < m_InteractableSettings.InteractionDuration)
                    OnInteraction(false);
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