using PV.Core.Utility;
using PV.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PV
{
    public class Finishline : MonoBehaviour
    {
        [SerializeField] private ColliderEvent m_ColliderEvent;

        private void OnEnable()
        {
            m_ColliderEvent.OnCollisionEnterEvent += OnCollisionEnterEvent;
        }

        private void OnDisable()
        {
            m_ColliderEvent.OnCollisionEnterEvent -= OnCollisionEnterEvent;
        }

        private void OnCollisionEnterEvent(Collider collision)
        {
            if (collision.GetComponent<Player>())
                SceneManager.LoadScene(0);
        }
    }
}
