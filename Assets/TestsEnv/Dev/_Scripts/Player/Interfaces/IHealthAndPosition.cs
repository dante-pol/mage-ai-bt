using UnityEngine;

namespace Root
{
    public interface IHealthAndPosition
    {
        float CurrentHealth { get; }
        Vector3 Position { get; }
    }
}