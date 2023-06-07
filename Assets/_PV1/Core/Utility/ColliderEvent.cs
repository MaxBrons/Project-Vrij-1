
using System;
using UnityEngine;

namespace PV.Core.Utility
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class ColliderEvent : MonoBehaviour
    {
        public Action<Collider> OnCollisionEnterEvent;
        public Action<Collider> OnCollisionExitEvent;
        public Action<Collider> OnCollisionStayEvent;

        [SerializeField] private Collider m_Collider;
        [SerializeField] private Rigidbody m_RigidBody;

        public Collider Collider => m_Collider;
        public Rigidbody RigidBody => m_RigidBody;

        private void OnTriggerEnter(Collider collision) => OnCollisionEnterEvent?.Invoke(collision);

        private void OnTriggerExit(Collider collision) => OnCollisionExitEvent?.Invoke(collision);

        private void OnTriggerStay(Collider collision) => OnCollisionStayEvent?.Invoke(collision);
    }
}