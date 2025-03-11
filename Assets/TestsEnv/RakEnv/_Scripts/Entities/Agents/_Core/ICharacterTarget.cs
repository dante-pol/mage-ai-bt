using UnityEngine;

namespace Root
{
    public interface ICharacterTarget
    {
        public Vector3 Position { get; }
     
        public int HeatPoints { get; }

        public bool IsActiveUlt { get; }

    }
}