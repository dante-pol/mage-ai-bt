using UnityEngine;

namespace Root.Core.Factories
{
    public interface IFactory
    {
        T Create<T>() where T : Object;

        T Create<T>(Vector3 position, Quaternion orientation) where T : Object;
    }
}
