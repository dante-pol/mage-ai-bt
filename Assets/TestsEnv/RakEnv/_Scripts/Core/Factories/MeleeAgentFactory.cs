using Root.Core.Tools;

namespace Root.Core.Entities.Agents.Range
{
    public class MeleeAgentFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "";

        private readonly Agent _prefabAgent;

        public MeleeAgentFactory()
        {
            _prefabAgent = AssetsProvider.Load<Agent>(PATH_TO_PREFAB);
        }

        public Agent Create()
        {
            var agent = Instantiate(_prefabAgent);

            return agent;
        }

    }
}
