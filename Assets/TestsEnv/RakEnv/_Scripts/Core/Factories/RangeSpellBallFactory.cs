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
        
        private Teams _teamID;

        private float _damage;

        private int _attackLevel;


        public RangeSpellBallFactory()
        {
            _prefabBall = AssetsProvider.Load<SpellBall>(PATH_TO_PREFAB);

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
            //TODO: Configs...

            SpellBall ball = Instantiate(_prefabBall);

            ball.Construct(_teamID, _damage, _attackLevel);

            return ball;
        }

        public override Object Create(Vector3 position, Quaternion orientation)
        {
            SpellBall ball = Instantiate(_prefabBall, position, orientation);

            ball.Construct(_teamID, _damage, _attackLevel);

            return ball;
        }
    }
}
