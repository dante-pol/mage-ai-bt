using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Root
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour
    {
        private AgentContext _context;

        private AgentMotion _motion;

        private Brain _brain;

        private void Awake()
        {
            
        }
    }

    public class AgentContext
    {
        public float Mana { get; set; }

        public float HeatPoint { get; set; }
    }

    public class Brain
    {

    }

    public class AgentMotion
    {
        public event Action ArriveToTargetEvent;

        public void SendToTarget(Transform target)
        {

        }

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

namespace Root.Core.BT
{
    public enum NodeStatus { FAILURE=-1, RUNNING=0, SUCCESS=1}

    public abstract class ABTNode
    {
        public abstract NodeStatus Tick();
    }

    public class ConditionNode : ABTNode
    {
        private Func<bool> _condition;

        public ConditionNode(Func<bool> condition) 
            => _condition = condition;

        public override NodeStatus Tick()
            => _condition.Invoke() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;   
    }

    public class ActionNode : ABTNode
    {
        private Func<NodeStatus> _action;

        public ActionNode(Func<NodeStatus> action) 
            => _action = action;

        public override NodeStatus Tick()
            => _action.Invoke();
    }

    public abstract class ACollectionNode : ABTNode
    {
        protected List<ABTNode> _children;

        public void AddNode()
            => _children.Add(this);

        public void Remove(ABTNode node)
            => _children.Remove(node);
    }

    public class SequenceNode : ACollectionNode
    {
        public SequenceNode() 
            => _children = new List<ABTNode>();

        public SequenceNode(List<ABTNode> nodes) 
            => _children = nodes;

        public override NodeStatus Tick()
        {
            foreach (var node in _children)
            {
                if (node.Tick() == NodeStatus.FAILURE)
                    return NodeStatus.FAILURE;
                else if (node.Tick() == NodeStatus.RUNNING)
                    return NodeStatus.RUNNING;
            }

            return NodeStatus.SUCCESS;
        }
    }

    public class SelectorNode : ACollectionNode
    {
        public SelectorNode()
            => _children = new List<ABTNode>();

        public SelectorNode(List<ABTNode> nodes)
            => _children = nodes;

        public override NodeStatus Tick()
        {
            foreach (var node in _children)
            {
                if (node.Tick() == NodeStatus.SUCCESS)
                    return NodeStatus.SUCCESS;

                else if (node.Tick() == NodeStatus.RUNNING)
                    return NodeStatus.RUNNING;
            }

            return NodeStatus.FAILURE;
        }
    }
}
