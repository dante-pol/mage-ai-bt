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

        private RangeBrain _brain;

        private float _heatPoint;

        [SerializeField] GameObject _spellBall;

        public void Construct()
        {
            IsLife = true;

            IsDeath = false;

            Eyes = new AgentEyes(transform);

            Animator = new RangeAnimator(gameObject, _spellBall);

            Eyes.SetSearchTarget(_player);

            Attacker = new RangeAttacker(this, _player);

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
