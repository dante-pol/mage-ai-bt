using Root.Core.BT;
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
                BuildZombieScenario(),
                BuildDeathScenario()
            });

        }

        public void Update()
            => _root.Tick();

        private SequenceNode BuildZombieScenario()
        {
            var hasZombieMode = new ConditionNode(() => _agent.ZombieMode.HasActiveZombieMode);

            var isNotLife = new ConditionNode(() => !_agent.IsLife);

            return new SequenceNode(new List<ABTNode>
            {
                hasZombieMode,
                isNotLife,
                BuildZombieProcess()
            });

        }

        private SequenceNode BuildZombieProcess()
        {
            var isNotActiveAnimation = new ConditionNode(() => !_agent.Animator.IsTurningZombie);

            var activeAnimation = new ActionNode(() =>
            {
                _agent.Animator.SetTurnIntoZombie();

                return NodeStatus.SUCCESS;
            });


            var addListenerToBeZombieEvent = new ActionNode(() =>
            {
                _agent.Animator.BeZombieEvent += HandlerBeZombie;

                Debug.Log("Конец Запуска Становления Зомби!");

                return NodeStatus.SUCCESS;
            });

            return new SequenceNode(new List<ABTNode>
            {
                isNotActiveAnimation,
                activeAnimation,
                addListenerToBeZombieEvent
            });
        }

        private void HandlerBeZombie()
        {
            Debug.Log("--------------------MELEE BE ZOMBIE--------------------");

            _agent.Motion.SetMotionLock(false);

            _agent.Eyes.IsFreeze = false;

            _agent.Attacker.IsFreeze = false;

            _agent.Motion.UpdateConfig(true);

            _agent.Resurrection();

            _agent.Animator.BeZombieEvent -= HandlerBeZombie;
        }

        private SequenceNode BuildDeathScenario()
        {
            var isNotLife = new ConditionNode(() => !_agent.IsLife);
            var isNotDeath = new ConditionNode(() => !_agent.IsDeath);

            return new SequenceNode(new List<ABTNode>
            {
                isNotLife,
                isNotDeath,
                BuildDeathAction()
            });
        }

        private SequenceNode BuildDeathAction()
        {
            var hasNotDeading = new ConditionNode(() => !_agent.Animator.IsDeading);

            var resetAnimation = new ActionNode(() =>
            {
                _agent.Animator.SetIdle();

                return NodeStatus.SUCCESS;
            });

            var lockMotion = new ActionNode(() =>
            {
                _agent.Motion.SetMotionLock(true);

                return NodeStatus.SUCCESS;
            });

            var lockEyes = new ActionNode(() =>
            {
                _agent.Eyes.IsFreeze = true;

                return NodeStatus.SUCCESS;
            });

            var lockAttacker = new ActionNode(() =>
            {
                _agent.Attacker.IsFreeze = true;

                return NodeStatus.SUCCESS;
            });

            var deathAnimationActiveAction = new ActionNode(() =>
            {
                _agent.Animator.SetDeath();

                return NodeStatus.SUCCESS;
            });

            var addListenerToDeathEvent = new ActionNode(() =>
            {
                _agent.Animator.DeathEvent += HandlerDeathEvent;

                Debug.Log("Конец Запуска смерти!");

                return NodeStatus.SUCCESS;
            });


            return new SequenceNode(new List<ABTNode>
            {
                hasNotDeading,
                resetAnimation,
                lockMotion,
                lockEyes,
                lockAttacker,
                deathAnimationActiveAction,
                addListenerToDeathEvent
            });
        }

        private void HandlerDeathEvent()
        {
            Debug.Log("--------------------MELEE DEATH---------------------");

            _agent.IsDeath = true;

            if (_agent.ZombieMode.IsZombie) return;

            Debug.Log("--------------------ZOBIE ????---------------------");

            _agent.ZombieMode.TryBeZombie();
        }

        public SequenceNode BuildRetreatScenario()
        {
            var isAloneAgentCondition = new ConditionNode(() => (_agent.IsAlone || _agent.Player.IsActiveUlt) && !_agent.ZombieMode.IsZombie);

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
            var goToRescuePointAction = new ActionNode(() =>
            {
                _agent.Motion.SetTarget(_agent.Escape.GetEscapePoint());

                _agent.Motion.SetActiveRun(true);

                _agent.Motion.SetMotionLock(false);

                _agent.Animator.SetRun();

                return _agent.Motion.HasReachedTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
            });

            return new SequenceNode(new List<ABTNode>
            {
                new ConditionNode(() => ! _agent.Escape.IsSelect),

                new ActionNode(() =>
                {
                    _agent.Escape.ChooseEscapePoint();
                    return NodeStatus.SUCCESS;
                }),

                goToRescuePointAction,

                new ConditionNode(() => _agent.IsAlone),

                new ActionNode(() =>
                {
                    //TODO: Нельзя стать зомби

                    return NodeStatus.SUCCESS;
                })
            });
        }

        public SequenceNode BuildGoRescuePoint()
        {
            var isSelectEscapePoint = new ConditionNode(() => _agent.Escape.IsSelect);

            var isNotAttackingCondition =  new ConditionNode(() => !_agent.Animator.IsAttacking);

            var hasNotEscapeCondition = new ConditionNode(() => !_agent.Escape.HasEscape);

            var hasReachedRescuePointCondition = new ConditionNode(() => _agent.Motion.HasReachedTarget);

            var stopNearRescuePointAction = new ActionNode(() =>
            {
                _agent.Eyes.IsFreeze = true;

                _agent.Escape.HasEscape = true;

                _agent.Motion.SetMotionLock(true);

                _agent.Animator.SetEscape();

                return NodeStatus.SUCCESS;
            });

            var escapeTypes = new SelectorNode(new List<ABTNode>
            {
                new SequenceNode(new List<ABTNode>
                {
                    new ConditionNode(() => _agent.IsAlone),
                    stopNearRescuePointAction
                }),

                new SequenceNode(new List<ABTNode>
                {
                    new ConditionNode(() => !_agent.IsAlone),
                    new ActionNode(() =>
                    {
                        _agent.Animator.SetIdle();

                        _agent.Motion.ClearTarget();

                        return NodeStatus.SUCCESS;
                    })
                })
            });

            return new SequenceNode(new List<ABTNode>
            {
                isNotAttackingCondition,
                isSelectEscapePoint,
                hasNotEscapeCondition,
                hasReachedRescuePointCondition,
                escapeTypes,
            });
        }

        public SelectorNode BuildLifeSelector()
        {
            var lifeSelectorScenario = new SelectorNode(new List<ABTNode>
            {
                BuildRetreatScenario(),
                new SequenceNode(new List<ABTNode>
                {
                    new ConditionNode(() => (!_agent.IsAlone && !_agent.Player.IsActiveUlt) || _agent.ZombieMode.IsZombie),
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

            var isPlayerBeatenCondition = new ConditionNode(() => _agent.Player.HeatPoints <= 50);

            var isNotZombieCondition = new ConditionNode(() => !_agent.ZombieMode.IsZombie);

            var activeSuperMotionAction = new ActionNode(() =>
            {
                _agent.Motion.SetActiveRun(true);

                _agent.Animator.SetRun();

                return NodeStatus.SUCCESS;
            });

            var goToPlayerScenario = new SequenceNode(new List<ABTNode>
            {
                isDetectPlayerCondition,
                hasReachedPlayerCondition,
                isNotAttackingCondition,
                goToPlayerAction,
                isPlayerBeatenCondition,
                isNotZombieCondition,
                activeSuperMotionAction,
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

        private NodeStatus AttackToPlayer()
        {
            _agent.Animator.SetBaseAttack();

            _agent.Motion.SetMotionLock(true);

            return _agent.Animator.IsAttacking ? NodeStatus.RUNNING : NodeStatus.SUCCESS; 
        }

        private NodeStatus GoToPlayer()
        {
            _agent.Motion.SetTarget(_agent.PlayerTarget);

            _agent.Animator.SetWalk();

            _agent.Motion.SetActiveRun(false);

            _agent.Motion.SetMotionLock(false);

            return NodeStatus.SUCCESS;
        }

        private NodeStatus IdleAction()
        {
            _agent.Animator.SetIdle();

            _agent.Motion.SetMotionLock(true);

            return NodeStatus.SUCCESS;
        }
    }
}