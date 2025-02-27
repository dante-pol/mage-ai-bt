using UnityEngine;

namespace Root
{
    public class ChildAnimationHandler : MonoBehaviour
    {
        private Agent _agent;

        private void Start() 
            => _agent = GetComponentInParent<Agent>();

        public void OnBaseAttackAnimationEvent()
            => _agent.Animator.EndAttack();
    }
}