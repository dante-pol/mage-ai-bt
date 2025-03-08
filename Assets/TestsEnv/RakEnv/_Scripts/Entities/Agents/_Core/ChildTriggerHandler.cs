using UnityEngine;
using UnityEngine.Events;

namespace Root
{
    public class ChildTriggerHandler : MonoBehaviour, ITriggerHandler
    {
        public event UnityAction<Collider> TriggerEnterEvent;

        private void OnTriggerEnter(Collider other)
            => TriggerEnterEvent?.Invoke(other);
    }
}