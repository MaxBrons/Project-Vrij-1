using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace PV
{
    public class PreGameVideo : MonoBehaviour
    {
        [SerializeField] private VideoPlayer m_Player;

        private void Start()
        {
            StartCoroutine(WaitForVideoEnd());
        }

        private IEnumerator WaitForVideoEnd()
        {
            yield return new WaitForSecondsRealtime((float)m_Player.clip.length);
            SceneManager.LoadScene(2);
        }
    }
}
