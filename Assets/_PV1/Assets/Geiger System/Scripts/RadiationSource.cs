﻿using PV.Entities;
using UnityEngine;

namespace PV.Systems.Geiger
{
    [RequireComponent(typeof(SphereCollider), typeof(AudioSource))]
    public class RadiationSource : MonoBehaviour
    {
        [SerializeField] private float m_MaxDamage;
        [SerializeField] private float m_TickRate;
        [SerializeField] private SphereCollider m_SphereCollider;
        [SerializeField] private VisualCue m_VisualCue;

        [SerializeField] private AudioSource m_AudioSource;
        [SerializeField] private AudioSource m_HeartbeatSource;
        [SerializeField] private AudioClip[] m_GeigerCounter;
        [SerializeField] private AudioClip[] m_Heartbeats;

        private Player m_Player;
        private float m_MaxDistance;
        private float m_Tick;
        private AudioClip m_CurrentClip;
        private AudioClip m_CurrentHBClip;

        private void Start()
        {
            m_MaxDistance = m_SphereCollider.radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();

            if (player == null) {
                return;
            }

            m_Player = player;

            m_HeartbeatSource.Play();
        }

        private void OnTriggerStay(Collider other)
        {
            if (m_Player == null) {
                return;
            }

            float distance = Vector3.Distance(m_Player.transform.position, transform.position);
            float normalized = (1 - (distance / m_MaxDistance));
            normalized = Mathf.Clamp(normalized, 0, 1);

            //Sound
            if (normalized <= 0.3f) {
                if (m_AudioSource.clip != m_GeigerCounter[0]) {
                    m_CurrentClip = m_GeigerCounter[0];
                    m_CurrentHBClip = m_Heartbeats[0];
                    m_AudioSource.Stop();
                    m_HeartbeatSource.Stop();
                    Debug.Log("Normalized <= 0.3f");
                }
            }
            else if (normalized <= 0.6f) {
                if (m_AudioSource.clip != m_GeigerCounter[1]) {
                    m_CurrentClip = m_GeigerCounter[1];
                    m_CurrentHBClip = m_Heartbeats[1];
                    m_AudioSource.Stop();
                    m_HeartbeatSource.Stop();
                    Debug.Log("normalized <= 0.6f && normalized > 0.3f");
                }
            }
            else if (normalized <= 1f) {
                if (m_AudioSource.clip != m_GeigerCounter[2]) {
                    m_CurrentClip = m_GeigerCounter[2];
                    m_CurrentHBClip = m_Heartbeats[2];
                    m_AudioSource.Stop();
                    m_HeartbeatSource.Stop();
                    Debug.Log("normalized <= 1f && normalized > 0.6f");
                }
            }

            if (!m_AudioSource.isPlaying) {
                m_AudioSource.clip = m_CurrentClip;
                m_AudioSource.Play();
                m_HeartbeatSource.clip = m_CurrentHBClip;
                m_HeartbeatSource.Play();
            }

            Debug.Log($"Normalized: {normalized}");


            m_VisualCue.UpdateRadiationCue(normalized);

            if (Time.time >= m_Tick) {
                float damage = normalized * m_MaxDamage;

                if (damage <= 0) {
                    return;
                }

                m_Player.TakeDamage(damage);
                m_Tick = Time.time + m_TickRate;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            m_AudioSource.Stop();
            m_Player = null;
            m_Tick = 0f;
            m_HeartbeatSource.Stop();
        }
    }
}