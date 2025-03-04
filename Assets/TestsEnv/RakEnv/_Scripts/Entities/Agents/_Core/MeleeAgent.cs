using UnityEngine;
using UnityEngine.AI;

namespace Root
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class MeleeAgent : MonoBehaviour, IEntityAttacked
    {
        public bool IsLife { get; private set; }

        public bool HasDeadYet { get; set; }

        public float HeatPoint = 100;

        public bool IsAlone => _commandCenter.IsOneAgent;

        public AgentCommandCenter _commandCenter;

        public MeleeMotion Motion;

        public EntitiesBroker EntitiesBroker;

        public AgentEyes Eyes;

        public MeleeAnimator Animator;

        public MeleeEscape Escape;

        public MeleeZombie ZombieMode;

        private MeleeBrain _brain;

        [SerializeField] private AnimatorOverrideController _overrideController;

        private void Awake()
        {
            IsLife = true;

            HasDeadYet = false;

            var agent = GetComponent<NavMeshAgent>();

            var animator = transform.GetChild(0).GetComponent<Animator>();

            Motion = new MeleeMotion(agent);

            Eyes = new AgentEyes(transform);

            Animator = new MeleeAnimator(animator, _overrideController);

            Escape = new MeleeEscape(Motion);

            ZombieMode = new MeleeZombie(Animator);

            Eyes.SetSearchTarget(EntitiesBroker.Player);

            _brain = new MeleeBrain(this);
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