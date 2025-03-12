using UnityEngine;

namespace Root
{
    public interface IEntityInfo
    {
        float CurrentHealth { get; }
        bool IsUltUse { get; }
        Vector3 Position { get; }
    }
}