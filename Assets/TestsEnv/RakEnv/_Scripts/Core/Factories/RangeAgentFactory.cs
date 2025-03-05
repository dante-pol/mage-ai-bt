using Root.Core.Tools;
using Root.Tests;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAgentFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "Prefabs/Actors/Agents/Range/RangeAgent";
        private const string PATH_TO_CONFIG = "Prefabs/Actors/Agents/Range/RangeConfig";

        private readonly RangeAgent _prefabAgent;

        private readonly RangeConfig _rangeConfig;

        private readonly TestCommandCenter _testCommandCenter;

        public RangeAgentFactory(TestCommandCenter testCommandCenter)
        {
            _testCommandCenter = testCommandCenter;

            _prefabAgent = AssetsProvider.Load<RangeAgent>(PATH_TO_PREFAB);

            _rangeConfig = AssetsProvider.Load<RangeConfig>(PATH_TO_CONFIG);
        }

        public override Object Create()
        {
            var agent = Instantiate(_prefabAgent);

            agent.Construct(_rangeConfig);

            return agent;
        }

        public override Object Create(Vector3 position, Quaternion orientation)
        {
            var agent = Instantiate(_prefabAgent, position, orientation);

            agent.Construct(_rangeConfig);

            return agent;
        }
    }
}
