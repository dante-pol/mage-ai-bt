using Root.Core.Tools;
using Root.Tests;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class MeleeAgentFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "Prefabs/Actors/Agents/MeleeAgent";

        private readonly MeleeAgent _prefabAgent;
        private readonly TestCommandCenter commandCenter;

        public MeleeAgentFactory(TestCommandCenter commandCenter)
        {
            _prefabAgent = AssetsProvider.Load<MeleeAgent>(PATH_TO_PREFAB);
            this.commandCenter = commandCenter;
        }

        public override Object Create()
        {
            var agent = Instantiate(_prefabAgent);

            agent.Construct(commandCenter);

            return agent;
        }

        public override Object Create(Vector3 position, Quaternion orientation)
        {
            var agent = Instantiate(_prefabAgent, position, orientation);

            agent.Construct(commandCenter);

            return agent;
        }
    }
}
