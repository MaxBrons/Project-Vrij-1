using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PV
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject m_ObjectToFollow;
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_FollowTime;
        [SerializeField] private Vector3 m_Offset;

        private Vector3 m_Velocity;
        private Rigidbody m_RB;

        private void Start()
        {
            m_RB = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 damp = Vector3.SmoothDamp(transform.position, m_ObjectToFollow.transform.position + m_Offset, ref m_Velocity, m_FollowTime, m_Speed);
            m_RB.MovePosition(new Vector3(damp.x, damp.y, damp.z));
        }
    }
}
