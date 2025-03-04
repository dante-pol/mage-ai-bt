using System;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    [CreateAssetMenu(fileName = "New Attack Config", menuName = "Create Range Attack Config", order = 22)]
    public class RangeProgressConfig : ScriptableObject
    {
        public int HeatPoint => _heatPoint;

        public int StartLevel => _startLevel;
        public int EndLevel => _endLevel;
        public Color IdleColor => _idleColor;

        public AttackConfig[] AttackConfigs => _attackConfigs;

        [Header("Range Agent")]
        [SerializeField] private int _heatPoint;

        [Header("Range Progressing")]
        [SerializeField] private int _startLevel;
        [SerializeField] private int _endLevel;
        [SerializeField] private Color _idleColor;

        [Header("Range Attacker")]
        [SerializeField] private AttackConfig[] _attackConfigs;

        [Serializable]
        public class AttackConfig
        {
            public int Damage => _damage;
            public float Cooldown => _cooldown;

            [SerializeField] private int _damage;
            [SerializeField] private float _cooldown;
        }

    }
}
