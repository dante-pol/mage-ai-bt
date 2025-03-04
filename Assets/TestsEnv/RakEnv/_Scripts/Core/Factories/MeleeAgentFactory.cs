using Root.Core.Tools;

namespace Root.Core.Entities.Agents.Range
{
    public class MeleeAgentFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "";

        private readonly MeleeAgent _prefabAgent;

        public MeleeAgentFactory()
        {
            _prefabAgent = AssetsProvider.Load<MeleeAgent>(PATH_TO_PREFAB);
        }

        public MeleeAgent Create()
        {
            var agent = Instantiate(_prefabAgent);

            return agent;
        }

    }
}
