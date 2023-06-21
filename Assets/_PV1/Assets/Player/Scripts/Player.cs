using PV.Systems.Geiger;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PV.Entities
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour, IDamagable
    {
        [SerializeField] private GasMask m_GasMask;

        private float m_HealthPoints = 100f;
        private bool m_ShouldTakeDamage = true;

        public void TakeDamage(float amount)
        {
            if (!m_ShouldTakeDamage)
                return;

            Debug.Log($"TakeDMG: {amount}");

            if (m_GasMask.HasDurability) {
                m_GasMask.TakeDamage(amount);
                return;
            }

            m_HealthPoints -= amount;

            if (m_HealthPoints <= 0.0f) {
                if (TryGetComponent(out PlayerMovement pm)) {
                    pm.enabled = false;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public float GetHealth() => m_HealthPoints;
        //TO-DO: Maak een lijst met Modules aan en update deze in Start, Update en Destroy
        //TO-DO: Verplaats GasMask Damaming naar een Module component
    }
}