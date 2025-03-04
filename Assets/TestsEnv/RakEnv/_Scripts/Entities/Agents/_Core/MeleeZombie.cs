using UnityEngine;

namespace Root
{
    public class MeleeZombie
    {
        public bool IsZombie { get; private set; }

        public bool IsStartingProcessZombie { get; private set; }

        public readonly MeleeAnimator _animator;
        
        private readonly float _maxLimit;

        private readonly float _minLimit;

        private readonly float _thresholdValue;

        public MeleeZombie(MeleeAnimator animator)
        {
            _animator = animator;

            IsZombie = false;

            IsStartingProcessZombie = false;

            _maxLimit = 100;

            _minLimit = 1;

            _thresholdValue = 2;
        }

        public void TryBeZombie()
        {
            float probability = CalculateProbabilityBeZombie();

            if (!CheckChanceBeZombie(probability)) return;
            
            IsStartingProcessZombie = true;

            Debug.Log("I am Zombie!!!!");
        }

        public void HandlerEndTurningIntoZombie()
        {
            _animator.SetConfigForZombie();

            IsStartingProcessZombie = false;

            IsZombie = true;
        }

        private float CalculateProbabilityBeZombie() 
            => Random.Range(_maxLimit, _minLimit);

        private bool CheckChanceBeZombie(float probability)
            => probability >= _thresholdValue;
    }
}