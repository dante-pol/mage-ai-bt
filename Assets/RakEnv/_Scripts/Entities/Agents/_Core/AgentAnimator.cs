using Root.Core.BT;
using UnityEngine;

namespace Root
{
    public class AgentAnimator
    {
        private readonly int SpeedHash = Animator.StringToHash("Speed");
        private readonly int DeathHash = Animator.StringToHash("Death");
        private readonly int BaseAttackHash = Animator.StringToHash("BaseAttack");

        private readonly Animator _stateMachine;
        private Animator animator;

        public bool IsAttacking => _stateMachine.GetBool(BaseAttackHash);

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
            => _stateMachine.SetTrigger(BaseAttackHash);
    }
}