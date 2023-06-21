using UnityEngine;

namespace PV
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : Interaction.Interactable
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private AudioClip m_Clip;

        private AudioSource m_Source;
        private bool m_Interacted = false;

        private void Start()
        {
            m_Source = GetComponent<AudioSource>();
            m_Source.clip = m_Clip;
        }
        protected override void OnInteraction(bool success)
        {
            if (success) {
                m_Animator.SetBool("Open", success);
                m_Source.Play();
                m_Interacted = true;
            }
        }

        protected override bool IsInteractable()
        {
            return !m_Interacted;
        }
    }
}
