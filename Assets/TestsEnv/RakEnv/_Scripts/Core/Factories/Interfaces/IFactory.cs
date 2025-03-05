using UnityEngine;

namespace Root.Core.Factories
{
    public interface IFactory
    {
        Object Create();

        Object Create(Vector3 position, Quaternion orientation);
    }
}
