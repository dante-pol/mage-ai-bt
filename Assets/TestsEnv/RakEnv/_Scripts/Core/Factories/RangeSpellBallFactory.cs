using Root.Core.Factories.Tools;
using Root.Core.Tools;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeSpellBallFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "Prefabs/Actors/Agents/Range/SpellBall";

        private readonly SpellBall _prefabBall;

        private readonly Pool<SpellBall> _pool;


        public RangeSpellBallFactory()
        {
            _prefabBall = AssetsProvider.Load<SpellBall>(PATH_TO_PREFAB);
        }

        public override Object Create()
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

        public override Object Create(Vector3 position, Quaternion orientation)
        {
            throw new System.NotImplementedException();
        }
    }
}
