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

            IsFreeze = false;

            IsDetect = false;
        }

        public void Update()
        {
            if (IsFreeze) return;

            Detecting();
        }

        public void SetTarget(Transform target)
            => _targetDetect = target;

        private void Detecting()
        {
            Debug.Log("Kara");

            IsDetect = false;

            if (_targetDetect == null) return;

            Debug.Log("Kara2");

            Debug.DrawLine(_agent.position, (_targetDetect.position - _agent.position).normalized * 20, Color.red);

            if (Vector3.Distance(_agent.position, _targetDetect.position) < 20)
            {
                IsDetect = true;
            }
        }
    }
}