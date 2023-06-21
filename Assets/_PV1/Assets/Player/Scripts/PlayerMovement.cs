using PV.Core;
using PV.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PV
{
    //TO-DO: Pas dit script aan naar een Module Component, zodat deze door de speler geüpdate kan worden.

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] private float m_CrouchSpeed;
        [SerializeField] private float m_VaultSpeed;
        [SerializeField] private CapsuleCollider m_CrouchCollider;
        [SerializeField] private List<Module> m_Modules = new List<Module>();

        private AudioModule m_AudioModule;
        private Rigidbody m_RB;
        private float m_MovementSpeed = 0.0f;
        private int m_Direction = 0;

        private bool m_Running = false;
        private bool m_Crouching = false;
        private bool m_Vaulting = false;

        void Start()
        {
            InputManager.Instance?.Subscribe(OnMove, OnRun, OnCrouch);

            m_Modules.ForEach(module => module.OnStart());

            m_AudioModule = GetComponent<AudioModule>();
            m_RB = GetComponent<Rigidbody>();
        }

        private void OnDestroy()
        {
            m_Modules.ForEach(module => module.OnDestroy());
            InputManager.Instance?.Unsubscribe(OnMove, OnRun, OnCrouch);
        }

        private void LateUpdate()
        {
            m_Modules.ForEach(m => m.OnLateUpdate());
        }

        void FixedUpdate()
        {
            m_MovementSpeed = !m_Running && !m_Crouching && !m_Vaulting ? m_WalkSpeed : m_MovementSpeed;

            if (m_RB) {
                m_RB.MovePosition(transform.position + new Vector3(m_MovementSpeed * Time.deltaTime, 0.0f, 0.0f) * m_Direction);
            }
            if (m_Direction != 0)
                transform.rotation = Quaternion.Euler(transform.rotation.x, m_Direction >= 0 ? 90 : -90, transform.rotation.z);
        }

        public void ScaleMovementSpeed(float multiplier)
        {
            m_MovementSpeed *= multiplier;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            m_Direction = (int)context.ReadValue<float>();

            if (!m_AudioModule)
                return;

            if (m_Direction != 0)
                m_AudioModule.Play();
            else
                m_AudioModule.Stop();
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            if (m_Running = context.ReadValueAsButton())
                m_MovementSpeed = m_RunSpeed;
        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            if (m_Crouching = context.ReadValueAsButton())
                m_MovementSpeed = m_CrouchSpeed;

            if (m_CrouchCollider)
                m_CrouchCollider.enabled = !m_Crouching;
        }

        private void OnVault(InputAction.CallbackContext context)
        {
            m_Vaulting = context.ReadValueAsButton();
            m_MovementSpeed = m_CrouchSpeed;

            m_RB.AddForce(transform.up * m_VaultSpeed);
        }
    }
}
