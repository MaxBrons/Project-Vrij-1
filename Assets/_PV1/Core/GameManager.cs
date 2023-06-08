using PV.Input;
using PV.Interaction;
using PV.InventorySystem;
using UnityEngine;

namespace PV.Core
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private InteractionManager m_InteractionManager;
        [SerializeField] private InputManager m_InputManager;

        private Inventory m_Inventory;

        private void Awake()
        {
            m_InputManager.OnAwake();
            m_Inventory = new Inventory();
        }

        void Start()
        {
            m_InteractionManager.OnStart();
            m_Inventory.OnStart();
        }

        void Update()
        {

        }

        private void OnDestroy()
        {
            m_InteractionManager.OnDestroy();
            m_InputManager.OnDestroy();
            m_Inventory.OnDestroy();
        }
    }
}
