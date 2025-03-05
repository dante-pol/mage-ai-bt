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

        private float _damage;

        private Color _colorAttack;


        public RangeSpellBallFactory()
        {
            _prefabBall = AssetsProvider.Load<SpellBall>(PATH_TO_PREFAB);

            _damage = 0;

            _colorAttack = Color.white;
        }

        public void UpdateConfig(int damage, Color color)
        {
            _damage = damage;

            _colorAttack = color;
        }

        public override Object Create()
        {
            //TODO: Configs...

            SpellBall ball = Instantiate(_prefabBall);

            ball.Construct(_damage, _colorAttack);

            return ball;
        }

        public override Object Create(Vector3 position, Quaternion orientation)
        {
            SpellBall ball = Instantiate(_prefabBall, position, orientation);

            ball.Construct(_damage, _colorAttack);

            return ball;
        }
    }
}
