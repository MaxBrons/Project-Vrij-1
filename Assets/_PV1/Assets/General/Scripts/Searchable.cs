using PV.Systems.Geiger;
using UnityEngine;

namespace PV
{
    [RequireComponent(typeof(AudioSource))]
    public class Searchable : Interaction.Interactable
    {
        [SerializeField] private GasMask m_GasMask;
        [SerializeField] private AudioClip m_Clip;

        private AudioSource m_Source;
        private bool m_Searched = false;

        private void Start()
        {
            m_Source = GetComponent<AudioSource>();
            m_Source.clip = m_Clip;
        }

        protected override void OnInteraction(bool success)
        {
            if (m_Searched)
                return;

            if (!success)
                return;
            m_Searched = true;
            m_GasMask.Heal(1000.0f);
            m_Source.Play();
        }

        protected override bool IsInteractable()
        {
            return !m_Searched;
        }
    }
}
