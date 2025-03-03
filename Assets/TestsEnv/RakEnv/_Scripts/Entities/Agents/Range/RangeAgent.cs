using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAgent : MonoBehaviour, IEntityAttacked
    {
        public bool IsLife { get; private set; }

        public bool IsDeath { get; set; }

        public int NumberProgress { get; private set; }

        public AgentEyes Eyes;

        public RangeAnimator Animator;

        public RangeAttacker Attacker;

        public Transform _player;

        public SpellBall _prefabSpellBall;

        public RangeAttackConfig[] _attackConfigs;

        private RangeBrain _brain;

        private float _heatPoint;

        [SerializeField] GameObject _spellBall;

        private void Start()
        {
            IsLife = true;

            IsDeath = false;

            _heatPoint = 2;

            Eyes = new AgentEyes(transform);

            Animator = new RangeAnimator(gameObject, _spellBall);

            Eyes.SetSearchTarget(_player);

            Attacker = new RangeAttacker(this, _player, _prefabSpellBall, _attackConfigs);

            _brain = new RangeBrain(this);
        }

        private void Update()
        {
            _brain.Update();

            Attacker.Update();

            Eyes.Update();
        }

        public void TakeAttack(IAttackProcess attackProcess)
        {
            _heatPoint -= attackProcess.Damage;

            if (_heatPoint <= 0)
            {
                _heatPoint = 0;

                IsLife = false;
            }
        }
    }
}
