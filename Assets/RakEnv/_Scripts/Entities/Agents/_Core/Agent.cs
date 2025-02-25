using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Root
{


    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour
    {
        public float Mana = 100;

        public float HeatPoint = 100;

        public AgentMotion Motion;

        public EntitiesBroker EntitiesBroker;

        private Brain _brain;

        private void Awake()
        {
            var agent = GetComponent<NavMeshAgent>();

            Motion = new AgentMotion(agent);

            _brain = new Brain(this);

            StartCoroutine(DecreaseMana());

            StartCoroutine(DecreaseHeatPoint());
        }

        private void Update()
        {
            Motion.Update();

            _brain.Update();
        }

        private IEnumerator DecreaseMana()
        {
            while (Mana > 0)
            {
                Mana -= 0.1f;
                yield return new WaitForSeconds(0.100f);
            }

            Mana = 0;
        }

        private IEnumerator DecreaseHeatPoint()
        {
            while (HeatPoint > 0)
            {
                HeatPoint -= 0.1f;
                yield return new WaitForSeconds(0.250f);
            }

            HeatPoint = 0;
        }
    }

    public class AgentEyes
    {
        public bool IsFreeze { get; set; }

        public bool IsDetect { get; private set; }

        private Transform _agent;

        private Transform _targetDetect;

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

            if (IsFreeze) return;

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

    public class AgentAttacker
    {

    }
    public class AgentAnimator
    {

    }
}