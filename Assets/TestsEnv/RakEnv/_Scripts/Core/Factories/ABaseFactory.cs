using Root.Core.Factories;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public abstract class ABaseFactory : IFactory
    {
        public abstract Object Create();

        public abstract Object Create(Vector3 position, Quaternion orientation);

        protected T Instantiate<T>(T instance) where T : Object
            => Object.Instantiate(instance);


        protected T Instantiate<T>(T instance, Vector3 position, Quaternion orientation) where T : Object
            => Object.Instantiate(instance, position, orientation);
    }
}
