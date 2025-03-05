using Root.Core.Tools;
using Root.Tests;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class MeleeAgentFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "Prefabs/Actors/Agents/MeleeAgent";
        private const string PATH_TO_CONFIG = "Prefabs/Actors/Agents/MeleeConfig";

        private readonly MeleeAgent _prefabAgent;
        private readonly MeleeConfig _config;

        private readonly TestCommandCenter _commandCenter;

        public MeleeAgentFactory(TestCommandCenter commandCenter)
        {
            _prefabAgent = AssetsProvider.Load<MeleeAgent>(PATH_TO_PREFAB);
            _config = AssetsProvider.Load<MeleeConfig>(PATH_TO_CONFIG);

            _commandCenter = commandCenter;
        }

        public override Object Create()
        {
            var agent = Instantiate(_prefabAgent);

            agent.Construct(_commandCenter, _config);

            return agent;
        }

        public override Object Create(Vector3 position, Quaternion orientation)
        {
            var agent = Instantiate(_prefabAgent, position, orientation);

            agent.Construct(_commandCenter, _config);

            return agent;
        }
    }
}
