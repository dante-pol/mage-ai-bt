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

            var checkMana = new ConditionNode(CheckMana);
            
            var heatPoint = new ConditionNode(CheckHeatPoint);
            
            var manaAction = new ActionNode(GoToBoxMana);

            var heatPointAction = new ActionNode(GotToBoxHeatPoint);

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

            _root = new SelectorNode(new List<ABTNode>
            {
                manaScenario,
                heatPointScenario
            });
        }

        public void Update() 
            => _root.Tick();

        public bool CheckMana()
            => _agent.Mana < 70 ? true : false;

        public bool CheckHeatPoint()
            => _agent.HeatPoint < 20 ? true : false;

        public NodeStatus GoToBoxMana()
        {
            _agent.Motion.SendToTarget(_agent.EntitiesBroker.ManaBox);

            return _agent.Motion.IsArriveToTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
        }

        public NodeStatus GotToBoxHeatPoint()
        {
            _agent.Motion.SendToTarget(_agent.EntitiesBroker.HeatPointBox);

            return _agent.Motion.IsArriveToTarget ? NodeStatus.SUCCESS : NodeStatus.RUNNING;
        }
    }
}