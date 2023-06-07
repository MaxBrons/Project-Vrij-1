using UnityEngine;

namespace PV.Core
{
    public abstract class Module : Core.Behaviour
    {
        protected Transform m_Owner;

        public Module(Transform owner)
        {
            m_Owner = owner;
        }

        public Transform Owner => m_Owner;

        public override void OnStart() { }
        public override void OnUpdate() { }
        public override void OnDestroy() { }
    }
}