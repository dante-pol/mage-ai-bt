using Root.Core.Factories.Tools;
using Root.Core.Tools;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeSpellBallFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "";

        private readonly SpellBall _prefabBall;

        private readonly Pool<SpellBall> _pool;


        public RangeSpellBallFactory()
        {
            _prefabBall = AssetsProvider.Load<SpellBall>(PATH_TO_PREFAB);
        }

        public SpellBall Create()
        {
            //TODO: Configs...

            SpellBall ball = _pool.Request();

            if (ball == null)
            {
                ball = Instantiate(_prefabBall);

                _pool.Register(ball);
            }

            return ball;
        }
    }
}
