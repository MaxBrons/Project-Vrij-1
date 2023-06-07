using UnityEngine;

namespace PV.Core
{
    public abstract class Module : MonoBehaviour
    {
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnDestroy() { }
    }
}