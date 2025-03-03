using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAttacker
    {
        public bool HasCooldownPassed { get; private set; }

        private readonly RangeAgent _range;

        private readonly Transform _me;

        private readonly SpellBall _prefabSpellBall;

        private readonly RangeAttackConfig[] _attackConfigs;

        private RangeAttackConfig _currentAttackConfig;

        private Transform _target;

        private readonly float _cooldownTime;

        private float _currentCooldown;

        public RangeAttacker(RangeAgent range, Transform target, SpellBall prefabSpellBall, RangeAttackConfig[] attackConfigs)
        {
            HasCooldownPassed = true;

            _range = range;

            _me = range.transform;
            _target = target;

            _prefabSpellBall = prefabSpellBall;

            _attackConfigs = attackConfigs;

            _currentAttackConfig = _attackConfigs[0];

            _cooldownTime = 3;
        }

        public void Attack()
        {
            Debug.Log("Attack;");

            SpellBall ball = CreateSpellBall();

            Vector3 toTarget = CalculateDirectionToTarget();

            ball.Construct(_currentAttackConfig.Damage);

            ball.PushIt(toTarget);

            HasCooldownPassed = false;

            _currentCooldown = _cooldownTime;
        }

        private SpellBall CreateSpellBall()
            => Object.Instantiate<SpellBall>(_prefabSpellBall);

        private Vector3 CalculateDirectionToTarget()
            => (_target.position - _me.position).normalized;

        public void Update()
        {
            if (HasCooldownPassed) return;

            _currentCooldown -= Time.deltaTime;

            if (_currentCooldown <= 0)
            {
                HasCooldownPassed = true;

                _currentCooldown = 0;
            }
        }
    }
}
