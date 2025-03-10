using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAttacker
    {
        public bool HasCooldownPassed { get; private set; }

        private readonly RangeAgent _range;

        private readonly Transform _me;

        private readonly RangeSpellBallFactory _ballFactory;

        private readonly Transform _spawnPoint;

        private Transform _target;

        private float _cooldownTime;

        private float _currentCooldown;

        public RangeAttacker(RangeAgent range, Transform target, Transform spawnPoint)
        {
            _spawnPoint = spawnPoint;

            HasCooldownPassed = true;

            _ballFactory = new RangeSpellBallFactory();

            _range = range;

            _me = range.transform;
            _target = target;

            _cooldownTime = 0;
        }

        public void UpdateConfigAttacker(Teams teamID, int damage, float cooldown, Color color)
        {
            _ballFactory.UpdateConfig(teamID, damage, color);

            _cooldownTime = cooldown;
        }

        public void Attack()
        {
            Debug.Log("Attack;");

            SpellBall ball = _ballFactory.Create(_spawnPoint.position, _spawnPoint.rotation) as SpellBall;

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
