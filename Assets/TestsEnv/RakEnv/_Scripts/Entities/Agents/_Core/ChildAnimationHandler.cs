using UnityEngine;

namespace Root
{
    public class ChildAnimationHandler : MonoBehaviour
    {
        private Agent _agent;

        private void Start()
            => _agent = GetComponentInParent<Agent>();

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
    }
}