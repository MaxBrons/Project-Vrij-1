using UnityEngine;

namespace _PV1.Assets.Bryan_Scripts.Scuffed
{
    public class Radiation : MonoBehaviour
    {
        public Transform _target;
        public float _damageTick;
        public float _tickRate = 1;
        public float damage = 10;
        public float maxDistance = 10;
        
        private void OnTriggerEnter(Collider other)
        {
            _target = other.transform;
            Debug.Log("ENTERED");
        }

        private void OnTriggerStay(Collider other)
        {
            if (_target == null)
            {
                return;
            }

            if (Time.time > _damageTick)
            {
                _damageTick = Time.time + _tickRate;
                var distance = Vector3.Distance(transform.position, _target.position);
                var damage = (maxDistance - distance) * this.damage;
              
                Debug.Log($"distance: {distance}");
                Debug.Log($"damage: {damage}");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _target = null;
        }
    }
}
