using Root.Core.Tools;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAgentFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "";

        private readonly RangeAgent _prefabAgent;

        public RangeAgentFactory()
        {
            _prefabAgent = AssetsProvider.Load<RangeAgent>(PATH_TO_PREFAB);
        }

        public RangeAgent Create()
        {
            var agent = Instantiate(_prefabAgent);

            agent.Construct();

            return agent;
        }
    }
}
