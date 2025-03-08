using System;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    [CreateAssetMenu(fileName = "New Attack Config", menuName = "Create Range Attack Config", order = 22)]
    public class RangeConfig : ScriptableObject, IRangeSoundsConfig
    {
        public int HeatPoint => _heatPoint;
        public Color IdleColor => _idleColor;

        public AttackConfig[] AttackConfigs => _attackConfigs;

        public SoundsConfig SoundConfig => _soundConfig;

        [Header("Range Agent")]
        [SerializeField] private int _heatPoint;
        [SerializeField] private Color _idleColor;

        [Header("Range Attacker")]
        [SerializeField] private AttackConfig[] _attackConfigs;

        [Header("Range Sounds")]
        [SerializeField] private SoundsConfig _soundConfig;

        [Serializable]
        public class AttackConfig
        {
            public int Damage => _damage;
            public float Cooldown => _cooldown;
            public Color ColorAttack => _colorAttack;

            [SerializeField] private int _damage;
            [SerializeField] private float _cooldown;
            [SerializeField] private Color _colorAttack;
        }

        [Serializable]
        public class SoundsConfig
        {
            public AudioClip Attack => _attack;
            public AudioClip TakeDamage => _takeDamage;
            public AudioClip Death => _death;


            [SerializeField] private AudioClip _attack;
            [SerializeField] private AudioClip _takeDamage;
            [SerializeField] private AudioClip _death;
        }
    }
}
