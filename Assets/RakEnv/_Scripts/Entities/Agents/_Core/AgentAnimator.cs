using UnityEngine;

namespace Root
{

    public class ZombieAnimatorStrategy : IAgentAnimatorStrategy
    {
        public void SetBaseAttack()
        {
            throw new System.NotImplementedException();
        }

        public void SetDeath()
        {
            throw new System.NotImplementedException();
        }

        public void SetIdle()
        {
            throw new System.NotImplementedException();
        }

        public void SetRun()
        {
            throw new System.NotImplementedException();
        }

        public void SetWalk()
        {
            throw new System.NotImplementedException();
        }
    }

    public class BaseAnimatorStrategy : IAgentAnimatorStrategy
    {
        public void SetBaseAttack()
        {
            throw new System.NotImplementedException();
        }

        public void SetDeath()
        {
            throw new System.NotImplementedException();
        }

        public void SetIdle()
        {
            throw new System.NotImplementedException();
        }

        public void SetRun()
        {
            throw new System.NotImplementedException();
        }

        public void SetWalk()
        {
            throw new System.NotImplementedException();
        }
    }

    public class AgentAnimator
    {
        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private readonly int DeathHash = Animator.StringToHash("Death");
        private readonly int BaseAttackHash = Animator.StringToHash("BaseAttack");

        private readonly Animator _stateMachine;

        public bool IsAttacking { get; private set; } = false;

        public AgentAnimator(Animator animator)
            => _stateMachine = animator;

        public void SetIdle()
            => _stateMachine.SetFloat(SpeedHash, 0.5f);

        public void SetWalk()
            => _stateMachine.SetFloat(SpeedHash, 1.5f);

        public void SetRun()
            => _stateMachine.SetFloat(SpeedHash, 2.5f);

        public void SetDeath()
            => _stateMachine.SetTrigger(DeathHash);

        public void SetBaseAttack()
        {
            IsAttacking = true;

            _stateMachine.SetTrigger(BaseAttackHash);
        }

        public void EndAttack()
            => IsAttacking = false;
    }
}