using System;
using UnityEngine;

namespace Root
{
    public class MeleeAnimator
    {
        public event Action DeathEvent;

        public event Action BeZombieEvent;

        public bool IsAttacking { get; private set; } = false;
        public bool IsTurningZombie { get; private set; } = false;
        public bool IsDeading{ get; private set; } = false;

        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private readonly int DeathHash = Animator.StringToHash("Death");
        private readonly int BaseAttackHash = Animator.StringToHash("BaseAttack");
        private readonly int BeZombieHash = Animator.StringToHash("BeZombie");
        private readonly int EscapeHash = Animator.StringToHash("HasEscape");

        private readonly Animator _stateMachine;
        private readonly AnimatorOverrideController _overrideController;

        public MeleeAnimator(Animator animator,AnimatorOverrideController overrideController)
        {
            _stateMachine = animator;
            _overrideController = overrideController;
        }

        public void SetConfigForZombie()
            => _stateMachine.runtimeAnimatorController = _overrideController;

        public void SetIdle()
            => _stateMachine.SetFloat(SpeedHash, 0.5f);

        public void SetWalk()
            => _stateMachine.SetFloat(SpeedHash, 1.5f);

        public void SetRun()
            => _stateMachine.SetFloat(SpeedHash, 2.5f);

        public void SetEscape() 
            => _stateMachine.SetTrigger(EscapeHash);

        public void SetDeath()
        {
            IsDeading = true;

            _stateMachine.SetTrigger(DeathHash);
        }

        public void SetBaseAttack()
        {
            IsAttacking = true;

            _stateMachine.SetTrigger(BaseAttackHash);
        }

        public void SetTurnIntoZombie()
        {
            IsTurningZombie = true;

            _stateMachine.SetTrigger(BeZombieHash);
        }

        public void EndAttack()
            => IsAttacking = false;

        public void EndTurningZombie()
        {
            BeZombieEvent?.Invoke();

            IsTurningZombie = false;
        }

        public void EndDeath()
        {
            DeathEvent?.Invoke();

            IsDeading = false;
        }
    }
}