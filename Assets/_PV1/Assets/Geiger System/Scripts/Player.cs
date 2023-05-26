using _PV1.Assets.Geiger_System.Scripts;
using _PV1.Assets.Geiger_System.Scripts.Interfaces;
using UnityEngine;

namespace _PV1.Assets.Geiger_System.Scripts
{
    public class Player : MonoBehaviour, IDamagable
    {
        [SerializeField] private GasMask m_gasMask;

        private float m_healthPoints = 100f;
        
        public void TakeDamage(float amount)
        {
            Debug.Log($"TakeDMG: {amount}");
            
            if (m_gasMask.HasDurability)
            {
                m_gasMask.TakeDamage(amount);
                return;
            }
            
            m_healthPoints -= amount;
        }
    }
}