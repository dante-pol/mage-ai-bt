using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Root
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class Agent : MonoBehaviour, IEntityAttacked
    {
        public bool IsLife { get; private set; }

        public bool HasDeadYet { get; set; }

        public float HeatPoint = 100;

        public bool IsAlone => _commandCenter.IsOneAgent;

        public AgentCommandCenter _commandCenter;

        public AgentMotion Motion;

        public EntitiesBroker EntitiesBroker;

        public AgentEyes Eyes;

        public AgentAnimator Animator;

        public AgentEscape Escape;

        public AgentZombie ZombieMode;

        private Brain _brain;

        [SerializeField] private AnimatorOverrideController _overrideController;

        private void Awake()
        {
            IsLife = true;

            HasDeadYet = false;

            var agent = GetComponent<NavMeshAgent>();

            var animator = transform.GetChild(0).GetComponent<Animator>();

            Motion = new AgentMotion(agent);

            Eyes = new AgentEyes(transform);

            Animator = new AgentAnimator(animator, _overrideController);

            Escape = new AgentEscape(Motion);

            ZombieMode = new AgentZombie(Animator);

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
    }
}