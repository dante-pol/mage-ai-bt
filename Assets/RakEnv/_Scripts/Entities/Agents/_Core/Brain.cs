using JetBrains.Annotations;
using Root.Core.BT;
using System.Collections.Generic;
using UnityEngine;

namespace Root
{
    public class Brain
    {
        private Agent _agent;
        private SelectorNode _root;

        public Brain(Agent agent)
        {
            _agent = agent;

            var checkIdle = new ConditionNode(CheckIdle);

            var checkMana = new ConditionNode(CheckMana);

            var heatPoint = new ConditionNode(CheckHeatPoint);

            var checkStartPoint = new ConditionNode(CheckStartPoint);

            var idleAction = new ActionNode(MakeIdle);

            var heatPointAction = new ActionNode(GotToBoxHeatPoint);

            var manaAction = new ActionNode(GoToBoxMana);

            var startPointAction = new ActionNode(GoToStartPoint);

            var manaScenario = new SequenceNode(new List<ABTNode>
            {
                checkMana,
                manaAction
            });

            var heatPointScenario = new SequenceNode(new List<ABTNode>
            {
                heatPoint,
                heatPointAction
            });


            var idleScenario = new SequenceNode(new List<ABTNode>
            {
                checkIdle,
                idleAction
            });

            var startPointScenario = new SequenceNode(new List<ABTNode>
            {
                checkStartPoint,
                startPointAction
            });


            _root = new SelectorNode(new List<ABTNode>
            {
                idleScenario,
                startPointScenario,
                manaScenario,
                heatPointScenario
            });

        }

        public void Update() 
            => _root.Tick();

        public bool CheckIdle()
            => _agent.Mana >= 70 &&  _agent.HeatPoint >=50 && Vector3.Distance(_agent.transform.position, _agent.EntitiesBroker.Player.position) < 1f ? true : false;

        public bool CheckStartPoint()
            => Vector3.Distance(_agent.transform.position, _agent.EntitiesBroker.Player.position) > 1f && _agent.Mana >= 70 &&  _agent.HeatPoint >=50 ? true : false;

        public bool CheckMana()
            => _agent.Mana < 70 ? true : false;

        public bool CheckHeatPoint()
            => _agent.HeatPoint < 20 ? true : false;

        public NodeStatus MakeIdle()
        {
            Debug.Log("Idle Tree");

            _agent.Animator.SetIdle();

            return NodeStatus.SUCCESS;
        }

        public NodeStatus GoToStartPoint()
        {
            Debug.Log("Start Point Tree");

            _agent.Motion.SendToTarget(_agent.EntitiesBroker.Player);

            _agent.Animator.SetWalk();

            return !_agent.Motion.IsArriveToTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
        }

        public NodeStatus GoToBoxMana()
        {
            Debug.Log("Mana Box Tree");

            _agent.Motion.SendToTarget(_agent.EntitiesBroker.ManaBox);

            _agent.Animator.SetWalk();

            return !_agent.Motion.IsArriveToTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
        }

        public NodeStatus GotToBoxHeatPoint()
        {
            Debug.Log("Heat Point Tree");

            _agent.Motion.SendToTarget(_agent.EntitiesBroker.HeatPointBox);

            _agent.Animator.SetRun();

            return !_agent.Motion.IsArriveToTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
         }
    }
}