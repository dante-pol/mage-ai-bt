using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeSounds : IRangeSounds
    {
        private readonly AudioSource _audioSource;

        private readonly IRangeSoundsConfig _config;

        public RangeSounds(AudioSource audioSource, IRangeSoundsConfig config)
        {
            _audioSource = audioSource;
            _config = config;

            //_audioSource.playOnAwake = false;
            //_audioSource.loop = false;
        }

        public void PlayAttack()
        {
            _audioSource.clip = _config.SoundConfig.Attack;

            _audioSource.Play();
        }

        public void PlayTakeDamage()
        {
            _audioSource.clip = _config.SoundConfig.TakeDamage;

            _audioSource.PlayOneShot(_config.SoundConfig.TakeDamage);
        }

        public void PlayDeath()
        {
            _audioSource.clip = _config.SoundConfig.Death;

            _audioSource.Play();
        }
    }
}
