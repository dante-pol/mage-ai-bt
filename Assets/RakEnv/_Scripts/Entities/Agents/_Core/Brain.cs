using Root.Core.BT;
using System;
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

            var isZombiCondition = new ConditionNode(() => _agent.IsZombie);


            _root = new SelectorNode(new List<ABTNode>
            {
                BuildLifeScenario()
            });

        }

        public void Update()
            => _root.Tick();


        public SequenceNode BuildRetreatScenario()
        {
            var isAloneAgentCondition = new ConditionNode(() => _agent.IsAlone);

            

            var retreatScenario = new SequenceNode(new List<ABTNode>
            {
                isAloneAgentCondition,
                BuildRetreatingSelector()
            });

            return retreatScenario;
        }


        public SelectorNode BuildRetreatingSelector()
        {
            return new SelectorNode(new List<ABTNode>
            {
                BuildChoosRescuePoint(),
                BuildGoRescuePoint()
            });
        }

        public SequenceNode BuildChoosRescuePoint()
        {
            return new SequenceNode(new List<ABTNode>
            {
                new ConditionNode(() => ! _agent.Escape.IsSelect),
                new ActionNode(() =>
                {
                    _agent.Escape.ChooseEscapePoint();

                    return NodeStatus.SUCCESS;
                })
            });
        }

        public SequenceNode BuildGoRescuePoint()
        {
            var isSelectEscapePoint = new ConditionNode(() => _agent.Escape.IsSelect);

            var goToRescuePointAction = new ActionNode(() => 
            {
                _agent.Escape.Run();

                _agent.Animator.SetRun();

                return _agent.Motion.HasReachedTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
            });

            var hasReachedRescuePointCondition = new ConditionNode(() => _agent.Motion.HasReachedTarget);

            var stopNearRescuePointAction = new ActionNode(() =>
            {
                _agent.Motion.SetMotionLock(true);

                _agent.Animator.SetDeath();

                return NodeStatus.SUCCESS;
            });

            return new SequenceNode(new List<ABTNode>
            {
                isSelectEscapePoint,
                goToRescuePointAction,
                hasReachedRescuePointCondition,
                stopNearRescuePointAction
            });
        }

        public SelectorNode BuildLifeSelector()
        {
            var lifeSelectorScenario = new SelectorNode(new List<ABTNode>
            {
                BuildRetreatScenario(),
                new SequenceNode(new List<ABTNode>
                {
                    new ConditionNode(() => !_agent.IsAlone),
                    BuildActiveLifeScenario(),
                })
            });

            return lifeSelectorScenario;
        }

        private SelectorNode BuildActiveLifeScenario()
        {
            var activeLifeScenario = new SelectorNode(new List<ABTNode>
            {
                BuildIdleScenario(),
                BuildGoToPlayerScenario(),
                BuildAttackToPlayerScenario(),

            });

            return activeLifeScenario;
        }

        public SequenceNode BuildIdleScenario()
        {
            var isNotDetectPlayerCondition = new ConditionNode(() => !_agent.Eyes.IsDetect);

            var hasActieEyesCondition = new ConditionNode(() => !_agent.Eyes.IsFreeze);

            var idleAction = new ActionNode(IdleAction);

            var idleScenario = new SequenceNode(new List<ABTNode>
            {
                isNotDetectPlayerCondition,
                hasActieEyesCondition,
                idleAction
            });

            return idleScenario;
        }

        public SequenceNode BuildGoToPlayerScenario()
        {
            var isDetectPlayerCondition = new ConditionNode(() => _agent.Eyes.IsDetect);

            var hasReachedPlayerCondition = new ConditionNode(() => !_agent.Motion.HasReachedTarget);

            var isNotAttackingCondition = new ConditionNode(() => !_agent.Animator.IsAttacking);

            var goToPlayerAction = new ActionNode(GoToPlayer);

            var isPlayerBeatenCondition = new ConditionNode(() => _agent.HeatPoint <= 50);

            var activeSuperMotionAction = new ActionNode(ActiveSuperMotion);

            var goToPlayerScenario = new SequenceNode(new List<ABTNode>
            {
                isDetectPlayerCondition,
                hasReachedPlayerCondition,
                isNotAttackingCondition,
                goToPlayerAction,
                isPlayerBeatenCondition
            });
            
            return goToPlayerScenario;
        }

        public SequenceNode BuildAttackToPlayerScenario()
        {
            var hasReachPlayerCondition = new ConditionNode(() => _agent.Motion.HasReachedTarget);

            var isNotPreviousAttackingCondition = new ConditionNode(() => !_agent.Animator.IsAttacking);

            var attackPlayerAction = new ActionNode(AttackToPlayer);

            var attackToPlayerScenario = new SequenceNode(new List<ABTNode>
            {
                hasReachPlayerCondition,
                isNotPreviousAttackingCondition,
                attackPlayerAction
            });

            return attackToPlayerScenario;
        }

        public SequenceNode BuildLifeScenario()
        {
            var isLifeCondition = new ConditionNode(() => _agent.IsLife);

            var lifeScenario = new SequenceNode(new List<ABTNode>
            {
                isLifeCondition,
                BuildLifeSelector()
            });

            return lifeScenario;
        }

        private NodeStatus RetreatAction()
        {
            _agent.Escape.Run();

            return _agent.Motion.HasReachedTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
        }

        private NodeStatus AttackToPlayer()
        {
            _agent.Animator.SetBaseAttack();

            _agent.Motion.SetMotionLock(true);

            return _agent.Animator.IsAttacking ? NodeStatus.RUNNING : NodeStatus.SUCCESS; 
        }

        private NodeStatus GoToPlayer()
        {
            _agent.Motion.SetTarget(_agent.EntitiesBroker.Player);

            _agent.Animator.SetWalk();

            _agent.Motion.SetMotionLock(false);

            return _agent.Motion.HasReachedTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
        }

        private NodeStatus IdleAction()
        {
            _agent.Animator.SetIdle();

            _agent.Motion.SetMotionLock(true);

            return NodeStatus.SUCCESS;
        }

        private NodeStatus ActiveSuperMotion()
        {
            throw new NotImplementedException();
        }
    }
}