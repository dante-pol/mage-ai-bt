using UnityEngine;
using UnityEngine.AI;

namespace Root
{
    public class AgentMotion
    {
        public bool IsFreeze { get; set; }
        public bool HasReachedTarget { get; private set; }

        private readonly NavMeshAgent _agent;

        private Transform _target;

        public AgentMotion(NavMeshAgent agent) 
            => _agent = agent;

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

        private void Move() 
            => _agent.SetDestination(_target.position);

        private bool HasReachedDestination()
            => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }
}