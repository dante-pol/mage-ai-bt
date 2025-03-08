using UnityEngine;
using UnityEngine.Events;

namespace Root
{
    public interface ITriggerHandler
    {
        event UnityAction<Collider> TriggerEnterEvent;
    }
}