using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PV
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioModule : MonoBehaviour
    {
        private AudioSource m_Source;

        private void Start()
        {
            m_Source = GetComponent<AudioSource>();   
        }

        public void Play()
        {
            m_Source.Play();
        }

        public void Stop()
        {
            m_Source.Stop();
        }
    }
}
