using PV.Systems.Geiger;
using UnityEngine;

namespace PV.Entities
{
    public class Player : MonoBehaviour, IDamagable
    {
        [SerializeField] private GasMask m_GasMask;

        private float m_HealthPoints = 100f;

        public void TakeDamage(float amount)
        {
            Debug.Log($"TakeDMG: {amount}");

            if (m_GasMask.HasDurability) {
                m_GasMask.TakeDamage(amount);
                return;
            }

            m_HealthPoints -= amount;
        }
        //TO-DO: Maak een lijst met Modules aan en update deze in Start, Update en Destroy
        //TO-DO: Verplaats GasMask Damaming naar een Module component
    }
}