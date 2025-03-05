using UnityEngine;
using UnityEngine.Events;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAgent : MonoBehaviour, IEntityAttacked
    {
        public event UnityAction DeathEvent;

        public bool IsLife { get; private set; }

        public bool IsDeath { get; set; }

        public int NumberProgress { get; private set; }

        public AgentEyes Eyes;

        public RangeAnimator Animator;

        public RangeAttacker Attacker;


        public SpellBall _prefabSpellBall;

        private RangeBrain _brain;

        private RangeConfig _config;

        private Transform _player;
        
        private float _heatPoint;

        [SerializeField] GameObject _spellBall;

        public void Construct(RangeConfig config)
        {
            _config = config;

            _player = GameObject.FindGameObjectWithTag("Player").transform;

            IsLife = true;

            IsDeath = false;

            Eyes = new AgentEyes(transform);

            Animator = new RangeAnimator(gameObject, _spellBall);

            Eyes.SetSearchTarget(_player);

            Attacker = new RangeAttacker(this, _player, _spellBall.transform);

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

        public void Dead()
            => DeathEvent?.Invoke();
    }
}
