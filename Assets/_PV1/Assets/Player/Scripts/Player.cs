using PV.Systems.Geiger;
using UnityEngine;

namespace PV.Entities
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour, IDamagable
    {
        [SerializeField] private GasMask m_GasMask;
        [SerializeField] private AudioClip m_DieSound;

        private float m_HealthPoints = 100f;
        private AudioSource m_Source;

        private void Start()
        {
            m_Source = GetComponent<AudioSource>();
            m_Source.clip = m_DieSound;
        }

        public void TakeDamage(float amount)
        {
            Debug.Log($"TakeDMG: {amount}");

            if (m_GasMask.HasDurability) {
                m_GasMask.TakeDamage(amount);
                return;
            }

            m_HealthPoints -= amount;

            if (m_HealthPoints <= 0.0f) {
                m_Source.Play();
            }
        }
        //TO-DO: Maak een lijst met Modules aan en update deze in Start, Update en Destroy
        //TO-DO: Verplaats GasMask Damaming naar een Module component
    }
}