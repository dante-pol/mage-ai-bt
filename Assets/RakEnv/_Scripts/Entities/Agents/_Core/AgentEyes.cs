using UnityEngine;

namespace Root
{
    public class AgentEyes
    {
        public bool IsFreeze { get; set; }

        public bool IsDetect { get; private set; }

        private Transform _agent;

        private Transform _targetDetect;

        public AgentEyes(Transform agent)
        {
            _agent = agent;
        }

        public void Update()
        {
            if (IsFreeze) return;

            Detecting();
        }

        public void SetTarget(Transform target)
            => _agent = target;

        private void Detecting()
        {
            IsDetect = false;

            if (_targetDetect == null) return; 

            if (Vector3.Distance(_agent.position, _targetDetect.position) < 0.05f) 
                IsDetect = true;
        }
    }
}