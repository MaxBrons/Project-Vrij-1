using System.Collections;
using UnityEngine;

namespace PV.Systems.Geiger
{
    public class GasMask : MonoBehaviour, IDamagable, IHealable
    {
        //UI Toevoegen

        public float NormalizedDurability => m_Durability / m_MaxDurability;
        public bool HasDurability => m_Durability > 0f;

        [SerializeField] private float m_Durability;
        [SerializeField] private float m_MaxDurability;
        [SerializeField] private float m_DefaultDamage = 10.0f;

        private void Start()
        {
            StartCoroutine(TakeDamageTimed());
        }

        private IEnumerator TakeDamageTimed()
        {
            while (true) {
                yield return new WaitForSeconds(1);
                TakeDamage(m_DefaultDamage);
            }
        }

        public void TakeDamage(float amount)
        {
            m_Durability -= amount;
        }

        public void Heal(float amount)
        {
            m_Durability = Mathf.Clamp(m_Durability + amount, 0.0f, m_MaxDurability);
        }

        public float GetDurability() => m_Durability;
    }
}