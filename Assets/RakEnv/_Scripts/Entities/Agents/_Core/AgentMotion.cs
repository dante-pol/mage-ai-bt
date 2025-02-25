using UnityEngine;
using UnityEngine.AI;

namespace Root
{
    public class AgentMotion
    {
        public bool IsFreeze { get; set; }

        public bool IsArriveToTarget { get; private set; }

        private NavMeshAgent _agent;

        private Transform _target;

        public AgentMotion(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public void Update()
        {
            IsArriveToTarget = false;

            if (IsFreeze || _target == null) return;

            if (_target != null)
                _agent.SetDestination(_target.position);

            if (HasReachedDestination())
                IsArriveToTarget = true;

            Debug.Log($"IsArriveToTarget: {IsArriveToTarget}");
        }

        private bool HasReachedDestination()
            => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;

        public void SendToTarget(Transform target) 
            => _target = target;

        public void Stop()
        {

        }
    }
}