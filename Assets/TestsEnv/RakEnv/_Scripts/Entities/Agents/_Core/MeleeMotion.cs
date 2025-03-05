using UnityEngine;
using UnityEngine.AI;

namespace Root
{
    public class MeleeMotion
    {
        public bool IsFreeze { get; set; }
        public bool HasReachedTarget { get; private set; }

        private readonly NavMeshAgent _agent;

        private readonly IMeleeMotionConfig _config;
        
        private Transform _target;

        private float _walkSpeed;

        private float _runSpeed;

        public MeleeMotion(NavMeshAgent agent, IMeleeMotionConfig config)
        {
            _agent = agent;
            _config = config;

            InitConfig();
        }

        private void InitConfig()
        {
            _walkSpeed = _config.WalkSpeed;

            _runSpeed = _config.RunSpeed;

            _agent.speed = _walkSpeed;

            _agent.stoppingDistance = 2.0f;

            IsFreeze = false;
        }

        public void Update()
        {
            HasReachedTarget = false;

            if (IsFreeze) return;

            if (_agent.isStopped) return;

            if (_target == null) return;
            
            Move();

            if (HasReachedDestination())
                HasReachedTarget = true;
        }

        public void ClearTarget()
            => _target = null;

        public void SetTarget(Transform target)
            => _target = target;

        public void SetMotionLock(bool value)
            => _agent.isStopped = value;

        public void UpdateConfig(bool isZombie)
        { 
            if (isZombie)
            {
                _walkSpeed = _config.WalkSpeedZombie;

                _runSpeed = _config.RunSpeedZombie;
            }
            else
            {
                _walkSpeed = _config.WalkSpeed;

                _runSpeed = _config.RunSpeed;
            }

            SetActiveRun(false);
        }

        public void SetActiveRun(bool value)
        {
            _agent.speed = _walkSpeed;

            if (value)
            {
                Debug.Log("RUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUn");
                _agent.speed = _runSpeed;
            }
        }

        private void Move() 
            => _agent.SetDestination(_target.position);

        private bool HasReachedDestination()
            => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }
}