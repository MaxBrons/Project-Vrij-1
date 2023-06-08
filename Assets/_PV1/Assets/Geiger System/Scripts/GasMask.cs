using UnityEngine;

namespace PV.Systems.Geiger
{
    public class GasMask : MonoBehaviour, IDamagable
    {
        //UI Toevoegen

        public float NormalizedDurability => m_Durability / m_MaxDurability;
        public bool HasDurability => m_Durability > 0f;

        [SerializeField] private float m_Durability;
        [SerializeField] private float m_MaxDurability;

        public void TakeDamage(float amount)
        {
            m_Durability -= amount;
        }

        //TO-DO: IHealable interface implementeren, zodat het masker gereset kan worden
    }
}