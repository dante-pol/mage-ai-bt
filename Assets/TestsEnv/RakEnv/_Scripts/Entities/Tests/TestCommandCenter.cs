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
            var rangeFactory = new RangeAgentFactory();
            
            var meleeFactory = new MeleeAgentFactory(this);

            _meleeCommandCenter = new MeleeCommandCenter(this, meleeFactory);

            _rangeCommandCenter = new RangeCommandCenter(rangeFactory);

            _meleeCommandCenter.RunSingleSpawn();

            _meleeCommandCenter.RunPeriodsSpawn();

            _rangeCommandCenter.RunSpawn();

        }
    }

}