using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PV.Utility
{
    public abstract class Module : MonoBehaviour
    {
        protected Transform m_Owner;

        public void Init(Transform owner)
        {
            m_Owner = owner;
        }

        public Transform Owner => m_Owner;

        public abstract void OnStart();
        public abstract void OnUpdate();
        public abstract void OnDestroy();
    }
}