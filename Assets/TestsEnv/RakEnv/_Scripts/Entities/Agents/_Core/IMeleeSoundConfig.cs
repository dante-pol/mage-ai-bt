using UnityEngine;

namespace Root
{
    public interface IMeleeSoundConfig
    {
        AudioClip Attack { get; }
        AudioClip Death { get; }
        AudioClip Run { get; }
        AudioClip Walk { get; }
        AudioClip ZombieTransformation { get; }
    }
}