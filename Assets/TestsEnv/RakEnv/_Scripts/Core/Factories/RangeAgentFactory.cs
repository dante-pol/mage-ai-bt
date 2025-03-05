using Root.Core.Tools;
using Root.Tests;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAgentFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "Prefabs/Actors/Agents/Range/RangeAgent";

        private readonly RangeAgent _prefabAgent;
        private readonly TestCommandCenter _testCommandCenter;

        public RangeAgentFactory(TestCommandCenter testCommandCenter)
        {
            _testCommandCenter = testCommandCenter;

            _prefabAgent = AssetsProvider.Load<RangeAgent>(PATH_TO_PREFAB);
        }

        public override Object Create()
        {
            var agent = Instantiate(_prefabAgent);

            agent.Construct();

            return agent;
        }

        public override Object Create(Vector3 position, Quaternion orientation)
        {
            var agent = Instantiate(_prefabAgent, position, orientation);

            agent.Construct();

            return agent;
        }
    }
}
