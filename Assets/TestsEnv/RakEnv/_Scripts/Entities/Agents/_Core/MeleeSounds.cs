using UnityEngine;

namespace Root
{
    public class MeleeSounds
    {
        private readonly AudioSource _agentAudioSource;

        private readonly AudioSource _legsAudioSource;

        private readonly IMeleeSoundConfig _config;

        public MeleeSounds(IMeleeSoundConfig config, AudioSource agentSource, AudioSource legsSource)
        {
            _config = config;

            _agentAudioSource = agentSource;

            _legsAudioSource = legsSource;
        }

        public void PlayAttack()
        {
            _agentAudioSource.clip = _config.Attack;

            _agentAudioSource.Play();
        }

        public void PlayDeath()
        {
            _agentAudioSource.clip = _config.Death;

            _agentAudioSource.Play();
        }

        public void PlayZombieTransformation()
        {
            _agentAudioSource.clip = _config.ZombieTransformation;

            _agentAudioSource.Play();
        }
    }
}