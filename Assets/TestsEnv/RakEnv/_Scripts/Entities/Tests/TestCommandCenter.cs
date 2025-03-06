using Root.Core.Entities.Agents.Range;
using UnityEngine;

namespace Root.Tests
{
    public interface ICommandCenter
    {
        bool IsAloneMelee { get; }
    }

    public class TestCommandCenter : MonoBehaviour, ICommandCenter
    {
        public bool IsAloneMelee => _meleeCommandCenter.IsOneMelee;

        private MeleeCommandCenter _meleeCommandCenter;

        private RangeCommandCenter _rangeCommandCenter;

        private void Awake()
        {
            _meleeCommandCenter = new MeleeCommandCenter(this);

            _rangeCommandCenter = new RangeCommandCenter();

            var rangeFactory = new RangeAgentFactory();

            var meleeFactory = new MeleeAgentFactory(this);

        }
    }

}