using UnityEngine;

namespace PV.Core
{
    public abstract class Module : MonoBehaviour
    {
        public virtual void OnAwake() { }
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnDestroy() { }
    }
}