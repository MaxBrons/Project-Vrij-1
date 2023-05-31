using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PV.Utility
{
    public abstract class Module
    {
        protected Transform m_Owner;

        public Module(Transform owner)
        {
            m_Owner = owner;
        }

        public Transform Owner => m_Owner;

        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnDestroy() { }
    }
}