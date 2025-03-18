using System;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class ExplosiveMechanism
    {
        private readonly ParticleSystem[] _system;

        private ParticleSystem _currentSystem;

        public ExplosiveMechanism(ParticleSystem[] system)
        {
            _system = system;

            _currentSystem = _system[0];

            var main = _currentSystem.main;

            main.loop = false;
            main.playOnAwake = false;

            _currentSystem.gameObject.SetActive(true);
        }

        public void Play() 
            => _currentSystem.Play();

        public void UpdateEffect(int attackLevel)
        {
            _currentSystem = _system[attackLevel];

            var main = _currentSystem.main;

            main.loop = false;
            main.playOnAwake = false;

            _currentSystem.gameObject.SetActive(true);
        }
    }
}
