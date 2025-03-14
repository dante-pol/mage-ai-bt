using Root.Core.Factories.Tools;
using Root.Core.Tools;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeSpellBallFactory : ABaseFactory
    {
        private const string PATH_TO_PREFAB = "Prefabs/Actors/Agents/Range/SpellBall";

        private readonly SpellBall _prefabBall;

        private readonly Pool _pool;
        
        private Teams _teamID;

        private float _damage;

        private int _attackLevel;

        public RangeSpellBallFactory()
        {
            _prefabBall = AssetsProvider.Load<SpellBall>(PATH_TO_PREFAB);

            _pool = new Pool();

            _damage = 0;

            _attackLevel = 0;
        }

        public void UpdateConfig(Teams teamID, int damage, int attackLevel)
        {
            _teamID = teamID;

            _damage = damage;

            _attackLevel = attackLevel;
        }

        public override Object Create()
        {
            SpellBall ball = _pool.Request() as SpellBall;

            if (ball == null)
            {
                ball = Instantiate(_prefabBall);

                ball.Construct();

                _pool.RegisterObject(ball);

                ball.ReturnToPoolEvent += _pool.Return;
            }

            ball.Initialize(_teamID, _damage, _attackLevel);

            return ball;
        }

        public override Object Create(Vector3 position, Quaternion orientation)
        {
            SpellBall ball = _pool.Request() as SpellBall;

            if (ball == null)
            {
                ball = Instantiate(_prefabBall, position, orientation);

                ball.Construct();

                _pool.RegisterObject(ball);

                ball.ReturnToPoolEvent += _pool.Return;

                ball.Initialize(_teamID, _damage, _attackLevel);
            }

            ball.Initialize(_teamID, _damage, _attackLevel, position);

            return ball;
        }
    }
}
