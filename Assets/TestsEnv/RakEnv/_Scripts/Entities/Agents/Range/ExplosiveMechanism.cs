using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class ExplosiveMechanism
    {
        private readonly ParticleSystem _system;

        public ExplosiveMechanism(ParticleSystem system)
        {
            _system = system;

            var main = _system.main;

            main.loop = false;
            main.playOnAwake = false;

            _system.gameObject.SetActive(true);
        }

        public void Play() 
            => _system.Play();
    }
}
