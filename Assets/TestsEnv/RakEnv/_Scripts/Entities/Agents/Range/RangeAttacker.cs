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

        public RangeAttacker(RangeAgent range, Transform target, SpellBall prefabSpellBall, RangeAttackConfig[] attackConfigs)
        {
            HasCooldownPassed = false;

            _range = range;

            _me = range.transform;
            _target = target;

            _prefabSpellBall = prefabSpellBall;

            _attackConfigs = attackConfigs;

            _currentAttackConfig = _attackConfigs[0];
        }

        public void Attack()
        {
            Debug.Log("Attack;");

            SpellBall ball = CreateSpellBall();

            Vector3 toTarget = CalculateDirectionToTarget();

            ball.Construct(_currentAttackConfig.Damage);

            ball.PushIt(toTarget);
        }

        private SpellBall CreateSpellBall()
            => Object.Instantiate<SpellBall>(_prefabSpellBall);

        private Vector3 CalculateDirectionToTarget()
            => (_target.position - _me.position).normalized;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Attack();
        }
    }
}
