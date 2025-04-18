﻿using UnityEngine;

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
            Debug.Log($"Is Freeze {IsFreeze}");

            Reset();

            if (IsFreeze) return;

            Detecting();
        }

        public void SetSearchTarget(Transform target)
            => _targetDetect = target;

        public void ClearSearchTarget()
            => _targetDetect = null;

        private void Reset()
            => IsDetect = false;

        private void Detecting()
        {
            if (_targetDetect == null) return;

            Debug.DrawRay(_agent.position, ((_targetDetect.position - _agent.position)).normalized * 20, Color.red);

            if (Vector3.Distance(_agent.position, _targetDetect.position) < 60)
            {
                IsDetect = true;
            }
        }
    }
}