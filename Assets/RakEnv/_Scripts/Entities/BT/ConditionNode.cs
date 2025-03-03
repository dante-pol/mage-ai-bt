using System;

namespace Root.Core.BT
{
    public class ConditionNode : ABTNode
    {
        private Func<bool> _condition;

        public ConditionNode(Func<bool> condition) 
            => _condition = condition;

        public override NodeStatus Tick()
            => _condition.Invoke() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;   
    }
}
