using PV.Core;
using PV.Input;
using UnityEngine;
using UnityEngine.InputSystem;


namespace PV
{
    [RequireComponent(typeof(Animator))]
    public class AnimationModule : Module
    {
        [SerializeField] private Animator m_Animator;

        private bool m_Moving = false;
        private bool m_Running = false;
        private bool m_Crouching = false;
        private static readonly int Move = Animator.StringToHash("Walk");
        private static readonly int Running = Animator.StringToHash("Run");
        private static readonly int Crouch = Animator.StringToHash("Crouch");

        public override void OnStart()
        {
            InputManager.Instance?.Subscribe(OnMove, OnCrouch, OnRun);
        }

        public override void OnUpdate()
        {
            m_Animator.SetBool(Move, false);
            m_Animator.SetBool(Running, false);
            m_Animator.SetBool(Crouch, false);
            if (m_Moving) {
                m_Animator.SetBool(Move, true);
            }
            if (m_Running) {
                m_Animator.SetBool(Running, true);
            }
            if (m_Crouching) {
                m_Animator.SetBool(Crouch, true);
            }
        }

        public override void OnDestroy()
        {
            InputManager.Instance?.Unsubscribe(OnMove, OnCrouch, OnRun);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            m_Moving = context.ReadValue<float>() != 0.0f ? true : false;
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            m_Running = context.ReadValueAsButton();

        }

        private void OnCrouch(InputAction.CallbackContext context)
        {
            m_Crouching = context.ReadValueAsButton();
        }
    }
}