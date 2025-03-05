using Root.Core.BT;
using System.Collections.Generic;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeBrain
    {
        private readonly RangeAgent _agent;

        private ABTNode _root;

        public RangeBrain(RangeAgent agent)
        {
            _agent = agent;

            _root = new SelectorNode(new List<ABTNode>
            {
                BuildLifeScenario(),
                BuildDeathScenario()
            });

        }

        public void Update()
            => _root.Tick();

        private SequenceNode BuildLifeScenario()
        {
            var isLifeAgentCondition = new ConditionNode(() => _agent.IsLife);

            return new SequenceNode(new List<ABTNode>
            {
                isLifeAgentCondition,
                BuildLifeActions(),
            });
        }

        private SelectorNode BuildLifeActions()
        {
            return new SelectorNode(new List<ABTNode>
            {
                BuildIdleAction(),
                BuildAttackAction()
            });
        }
        private SequenceNode BuildIdleAction()
        {
            var isNotDetectPlayerCondition = new ConditionNode(() => !_agent.Eyes.IsDetect);

            var effectAnimAction = new ActionNode(() =>
            {
                _agent.Animator.SetIdle();

                return NodeStatus.SUCCESS;
            });

            return new SequenceNode(new List<ABTNode>
            {
                isNotDetectPlayerCondition,
                effectAnimAction
            });
        }

        private SequenceNode BuildAttackAction()
        {
            var isDetectPlayerCondition = new ConditionNode(() => _agent.Eyes.IsDetect);

            var isCooldownPassedCondition = new ConditionNode(() => _agent.Attacker.HasCooldownPassed);

            var attackAction = new ActionNode(() =>
            {
                _agent.Attacker.Attack();

                Debug.Log("Range Agent has Attack!");

                return NodeStatus.SUCCESS;
            });

            return new SequenceNode(new List<ABTNode>
            {
                isDetectPlayerCondition,
                isCooldownPassedCondition,
                attackAction
            });
        }

        private SequenceNode BuildDeathScenario()
        {
            var isNotLifeAgentCondition = new ConditionNode(() => !_agent.IsLife);

            return new SequenceNode(new List<ABTNode>
            {
                isNotLifeAgentCondition,
                BuildDeathActions()
            });
        }

        private SelectorNode BuildDeathActions()
        {
            return new SelectorNode(new List<ABTNode>
            {
                BuildDeathProcessAction(),
                BuildDeathAction()
            });
        }

        private SequenceNode BuildDeathProcessAction()
        {
            var isNotDeathAgentCondition = new ConditionNode(() => !_agent.IsDeath);

            var deathAction = new ActionNode(() =>
            {
                _agent.IsDeath = true;

                _agent.Dead();

                //TODO: Activate Animator

                return NodeStatus.SUCCESS;
            });

            return new SequenceNode(new List<ABTNode>
            {
                isNotDeathAgentCondition,
                deathAction
            });
        }

        private SequenceNode BuildDeathAction()
        {
            var isDeathAgentCondition = new ConditionNode(() => _agent.IsDeath);

            return new SequenceNode(new List<ABTNode>   
            {
                isDeathAgentCondition
            });
        }
    }
}
