using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public abstract class ABaseFactory
    {
        protected T Instantiate<T>(T instance) where T : Object
            => Object.Instantiate(instance);


        protected T Instantiate<T>(T instance, Vector3 position, Quaternion orientation) where T : Object
            => Object.Instantiate(instance, position, orientation);
    }
}
