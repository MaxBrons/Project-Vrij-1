using _PV1.Assets.Geiger_System.Scripts.Interfaces;
using UnityEngine;

namespace _PV1.Assets.Geiger_System.Scripts
{
    public class GasMask : MonoBehaviour, IDamagable
    {
        //UI Toevoegen
        
        public float NormalizedDurability => m_durability / m_maxDurability;
        public bool HasDurability => m_durability > 0f;
        
        [SerializeField] private float m_durability;
        [SerializeField] private float m_maxDurability;

        public void TakeDamage(float amount)
        {
            m_durability -= amount;
        }
    }
}