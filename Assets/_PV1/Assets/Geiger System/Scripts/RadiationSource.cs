using System;
using UnityEngine;
using UnityEngine.Audio;

namespace _PV1.Assets.Geiger_System.Scripts
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
        
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _geigerCounter;
        private AudioClip _geigerCounterClip;
        
        private Player m_player;
        private float m_maxDistance;
        private float m_tick;
        private bool _isPlaying;

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

            if (!_isPlaying)
            {
                _isPlaying = true;
                _audioSource.Play();
            }

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
            _audioSource.Stop();
            _isPlaying = false;
            m_player = null;
            m_tick = 0f;
        }
    }
}