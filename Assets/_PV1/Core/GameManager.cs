using PV.Input;
using PV.Interaction;
using UnityEngine;

namespace PV.Core
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private InteractionManager m_InteractionManager;
        [SerializeField] private InputManager m_InputManager;

        private void Awake()
        {
            m_InputManager.OnAwake();
        }

        void Start()
        {
            m_InteractionManager.OnStart();
        }

        void Update()
        {

        }

        private void OnDestroy()
        {
            m_InteractionManager.OnDestroy();
            m_InputManager.OnDestroy();
        }
    }
}
