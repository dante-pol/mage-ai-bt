using Root.Core.BT;
using System.Collections.Generic;

namespace Root
{
    public class Brain
    {
        private Agent _agent;

        private SelectorNode _root;

        public Brain(Agent agent)
        {
            _agent = agent;

            var isZombiCondition = new ConditionNode(() => _agent.IsZombi);


            _root = new SelectorNode(new List<ABTNode>
            {
                BuildLifeScenario()
            });

        }

        public SequenceNode BuildLifeScenario()
        {
            var isNotDetectPlayerCondition = new ConditionNode(() => !_agent.Eyes.IsDetect);

            var idleAction = new ActionNode(IdleAction);

            var idleScenario = new SequenceNode(new List<ABTNode>
            {
                isNotDetectPlayerCondition,
                idleAction
            });

            var isDetectPlayerCondition = new ConditionNode(() => _agent.Eyes.IsDetect && !_agent.Motion.HasReachedTarget);

            var goToPlayerAction = new ActionNode(GoToPlayer);

            var goToPlayerScenario = new SequenceNode(new List<ABTNode>
            {
                isDetectPlayerCondition,
                goToPlayerAction
            });

            var hasReachPlayerCondition = new ConditionNode(() => _agent.Motion.HasReachedTarget);

            var attackPlayerAction = new ActionNode(AttackToPlayer);

            var attackToPlayerScenario = new SequenceNode(new List<ABTNode>
            {
                hasReachPlayerCondition,
                attackPlayerAction
            });

            var liveScenario = new SelectorNode(new List<ABTNode>
            {
                idleScenario,
                goToPlayerScenario,
                attackToPlayerScenario
            });

            var isLifeCondition = new ConditionNode(() => _agent.IsLife);

            var lifeScenario = new SequenceNode(new List<ABTNode>
            {
                isLifeCondition,
                liveScenario
            });

            return lifeScenario;
        }

        private NodeStatus AttackToPlayer()
        {
            _agent.Animator.SetBaseAttack();

            return _agent.Animator.IsAttacking ? NodeStatus.RUNNING : NodeStatus.SUCCESS; 
        }

        private NodeStatus GoToPlayer()
        {
            _agent.Motion.SetTarget(_agent.EntitiesBroker.Player);

            return _agent.Motion.HasReachedTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
        }

        private NodeStatus IdleAction()
        {
            _agent.Animator.SetIdle();

            return NodeStatus.SUCCESS;
        }

        public void Update() 
            => _root.Tick();

    }
}