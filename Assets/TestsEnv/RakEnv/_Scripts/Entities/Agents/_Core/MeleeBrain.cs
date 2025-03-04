using Root.Core.BT;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Root
{
    public class MeleeBrain
    {
        private MeleeAgent _agent;

        private SelectorNode _root;

        public MeleeBrain(MeleeAgent agent)
        {
            _agent = agent;

            _root = new SelectorNode(new List<ABTNode>
            {
                BuildLifeScenario(),
                BuildDeathScenario(),
                BuildZombieProcessScenario()
            });

        }

        public void Update()
            => _root.Tick();


        private SequenceNode BuildZombieProcessScenario()
        {
            var hasTurningZombieCondition = new ConditionNode(() => _agent.ZombieMode.IsStartingProcessZombie);

            var isNotActiveZombieProcessAnimCondition = new ConditionNode(() => !_agent.Animator.IsTurningZombie);

            var zombieProcessAnimActive = new ActionNode(() =>
            {
                _agent.Animator.SetTurnIntoZombie();

                return NodeStatus.SUCCESS;
            });

            var unLockEyeAction = new ActionNode(() =>
            {
                _agent.Eyes.IsFreeze = false;

                return NodeStatus.SUCCESS;
            });

            return new SequenceNode(new List<ABTNode>
            {
                hasTurningZombieCondition,
                isNotActiveZombieProcessAnimCondition,
                zombieProcessAnimActive,
                unLockEyeAction
            });
        }


        private SequenceNode BuildDeathScenario()
        {
            var isDeathCondition = new ConditionNode(() =>  !_agent.IsLife);

            var hasNotDeadYet = new ConditionNode(() => !_agent.HasDeadYet);

            var motionBlockAction = new ActionNode(() =>
            {
                _agent.Motion.SetMotionLock(true);

                return NodeStatus.SUCCESS;
            });

            var visionBlockAction = new ActionNode(() =>
            {
                _agent.Eyes.IsFreeze = true;

                return NodeStatus.SUCCESS;
            });

            var deathAnimationActiveAction = new ActionNode(() =>
            {
                _agent.Animator.SetIdle();

                _agent.Animator.SetDeath();

                return NodeStatus.SUCCESS;
            });

            var tryBeZombieAction = new ActionNode(() =>
            {
                _agent.HasDeadYet = true;

                _agent.ZombieMode.TryBeZombie();

                return NodeStatus.SUCCESS;
            });

            return new SequenceNode(new List<ABTNode>
            {
                isDeathCondition,
                hasNotDeadYet,
                motionBlockAction,
                visionBlockAction,
                deathAnimationActiveAction,
                tryBeZombieAction
            });
        }

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

                    Debug.Log("Choose Escape Point");

                    return NodeStatus.SUCCESS;
                })
            });
        }

        public SequenceNode BuildGoRescuePoint()
        {
            var isSelectEscapePoint = new ConditionNode(() => _agent.Escape.IsSelect);

            var isNotAttackingCondition =  new ConditionNode(() => !_agent.Animator.IsAttacking);

            var hasNotEscapeCondition = new ConditionNode(() => !_agent.Escape.HasEscape);

            var goToRescuePointAction = new ActionNode(() => 
            {
                _agent.Escape.Run();

                _agent.Motion.SetMotionLock(false);

                _agent.Animator.SetRun();

                return _agent.Motion.HasReachedTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
            });

            var hasReachedRescuePointCondition = new ConditionNode(() => _agent.Motion.HasReachedTarget);

            var stopNearRescuePointAction = new ActionNode(() =>
            {
                _agent.Eyes.IsFreeze = true;

                _agent.Escape.HasEscape = true;

                _agent.Motion.SetMotionLock(true);

                _agent.Animator.SetDeath();

                return NodeStatus.SUCCESS;
            });

            return new SequenceNode(new List<ABTNode>
            {
                isNotAttackingCondition,
                isSelectEscapePoint,
                hasNotEscapeCondition,
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
                    new ActionNode(() => { Debug.Log("Life Selector"); return NodeStatus.SUCCESS; }),
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
                idleAction,
                new ActionNode(() => { Debug.Log("Idle Scenario"); return NodeStatus.SUCCESS; })
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
                isPlayerBeatenCondition,
                new ActionNode(() => { Debug.Log("Go To Player Scenario"); return NodeStatus.SUCCESS; })
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
                attackPlayerAction,
                new ActionNode(() => { Debug.Log("Attack To Player Scenario"); return NodeStatus.SUCCESS; })
            });

            return attackToPlayerScenario;
        }

        public SequenceNode BuildLifeScenario()
        {
            var isLifeCondition = new ConditionNode(() => _agent.IsLife || _agent.ZombieMode.IsZombie);

            var lifeScenario = new SequenceNode(new List<ABTNode>
            {
                new ActionNode(() => { Debug.Log("Life Scenario"); return NodeStatus.SUCCESS; }),
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