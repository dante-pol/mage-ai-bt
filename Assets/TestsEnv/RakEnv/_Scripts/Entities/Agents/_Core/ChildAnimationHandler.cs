using UnityEngine;

namespace Root
{
    public class ChildAnimationHandler : MonoBehaviour
    {
        private MeleeAgent _agent;

        private void Start()
            => _agent = GetComponentInParent<MeleeAgent>();

        public void OnBaseAttackAnimationEvent()
        {
            Debug.Log("i am nooob2!");

            _agent.Animator.EndAttack();
        }

        public void OnRisingFromDead()
        {
            Debug.Log("i am nooob!");

            _agent.Animator.EndTurningZombie();

            _agent.ZombieMode.HandlerEndTurningIntoZombie();
        }

        public void OnDeath()
        {
            _agent.Animator.EndAttack();
            _agent.Animator.EndDeath();
        }
    }
}