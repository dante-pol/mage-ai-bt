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

        public RangeAttacker _attacker;

        public Transform _player;

        public SpellBall _prefabSpellBall;

        public RangeAttackConfig[] _attackConfigs;

        private void Start()
        {
            _attacker = new RangeAttacker(this, _player, _prefabSpellBall, _attackConfigs);
        }

        #region Tests
        private void Update()
        {
            if (_attacker == null)
            {
                Debug.Log("Krara///////");
                return;
            }
            _attacker.Update(); 
        }
        #endregion
    }
}
