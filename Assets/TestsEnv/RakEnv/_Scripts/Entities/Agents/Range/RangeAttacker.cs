using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{

    public class RangeAttacker
    {
        public bool HasCooldownPassed { get; private set; }

        private readonly RangeAgent _range;

        private readonly Transform _me;

        private readonly RangeSpellBallFactory _ballFactory;

        private RangeProgressConfig _currentAttackConfig;

        private Transform _target;

        private readonly float _cooldownTime;

        private float _currentCooldown;

        public RangeAttacker(RangeAgent range, Transform target)
        {
            HasCooldownPassed = true;

            _range = range;

            _me = range.transform;
            _target = target;

            _cooldownTime = 3;
        }

        public void Attack()
        {
            Debug.Log("Attack;");

            SpellBall ball = _ballFactory.Create();

            Vector3 toTarget = CalculateDirectionToTarget();

            ball.PushIt(toTarget);

            HasCooldownPassed = false;

            _currentCooldown = _cooldownTime;
        }

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
