using UnityEngine;
using UnityEngine.AI;

namespace Root
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour, IEntityAttacked
    {
        public bool IsLife { get; private set; }

        public bool IsZombie { get; private set; }

        public float HeatPoint = 100;

        public bool IsAlone => _commandCenter.IsOneAgent;

        public AgentCommandCenter _commandCenter;

        public AgentMotion Motion;

        public EntitiesBroker EntitiesBroker;

        public AgentEyes Eyes;

        public AgentAnimator Animator;

        public AgentEscape Escape;

        private Brain _brain;

        private void Awake()
        {
            IsLife = true;

            IsZombie = false;

            var agent = GetComponent<NavMeshAgent>();

            var animator = transform.GetChild(0).GetComponent<Animator>();

            Motion = new AgentMotion(agent);

            Eyes = new AgentEyes(transform);

            Animator = new AgentAnimator(animator);

            Escape = new AgentEscape(Motion);

            Eyes.SetSearchTarget(EntitiesBroker.Player);

            _brain = new Brain(this);
        }

        private void Update()
        {
            Motion.Update();

            Eyes.Update();

            _brain.Update();

            Debug.Log($"Animator.IsAttacking: {Animator.IsAttacking}");

        }

        public void TakeAttack(IAttackProcess attackProcess)
        {
            if (attackProcess == null) return;

            HeatPoint -= attackProcess.Damage;

            if (HeatPoint <= 0)
                HeatPoint = 0;

            IsLife = false;
        }

        public void StartZombie()
            => IsZombie = true;
    }
}