using System;
using UnityEngine;

namespace _PV1.Assets.Bryan_Scripts
{
    //IDamagable
        
    //Player : IDamagable
        //HP
        //Item?
            
    //RadiationSource
        //MaxDistance
        //MaxDamage
        //TickRate
        
    //Item
        //Durability
        
    //moeten dingen gebeuren

    [RequireComponent(typeof(SphereCollider))]
    public class RadiationSource : MonoBehaviour
    {
        [SerializeField] private float m_maxDamage;
        [SerializeField] private float m_tickRate;
        [SerializeField] private SphereCollider m_sphereCollider;
        [SerializeField] private VisualCue m_visualCue;

        private Player m_player;
        private float m_maxDistance;
        private float m_tick;

        private void Start()
        {
            m_maxDistance = m_sphereCollider.radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();
            
            if (player == null)
            {
                return;
            }

            m_player = player;
        }

        private void OnTriggerStay(Collider other)
        {
            if (m_player == null)
            {
                return;
            }
            
            float distance = Vector3.Distance(m_player.transform.position, transform.position);
            float normalized = (1 - (distance / m_maxDistance));
            normalized = Mathf.Clamp(normalized, 0, 1);
            
            m_visualCue.UpdateRadiationCue(normalized);
            
            if (Time.time >= m_tick)
            {
                float damage = normalized * m_maxDamage;

                if (damage <= 0)
                {
                    return;
                }
                
                m_player.TakeDamage(damage);
                m_tick = Time.time + m_tickRate;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            m_player = null;
            m_tick = 0f;
        }
    }
}