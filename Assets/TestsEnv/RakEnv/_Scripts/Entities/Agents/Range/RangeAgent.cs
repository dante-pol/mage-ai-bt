using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAgent : MonoBehaviour
    {
        public bool IsLife { get; private set; }

        public bool IsDeath { get; set; }

        public int NumberProgress { get; private set; }

        public AgentEyes Eyes;

        public RangeAnimator _animator;

        public RangeAttacker Attacker;

        public Transform _player;

        public SpellBall _prefabSpellBall;

        public RangeAttackConfig[] _attackConfigs;

        private RangeBrain _brain;

        private void Start()
        {
            IsLife = true;

            IsDeath = false;

            Eyes = new AgentEyes(_player);

            Attacker = new RangeAttacker(this, _player, _prefabSpellBall, _attackConfigs);

            _brain = new RangeBrain(this);
        }

        #region Tests
        private void Update()
        {
            _brain.Update();

            Attacker.Update();
        }
        #endregion
    }
}
