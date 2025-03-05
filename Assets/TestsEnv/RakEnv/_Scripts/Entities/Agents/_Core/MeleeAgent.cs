using Root.Tests;
using UnityEngine;
using UnityEngine.AI;

namespace Root
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class MeleeAgent : MonoBehaviour, IEntityAttacked
    {
        public bool IsLife { get; private set; }

        public bool HasDeadYet { get; set; }

        public float HeatPoint { get; private set; }

        public float Damage { get; private set; }

        public bool IsAlone => _commandCenter.IsOneMelee;

        public MeleeMotion Motion;

        //public EntitiesBroker EntitiesBroker;

        public AgentEyes Eyes;

        public MeleeAnimator Animator;

        public MeleeEscape Escape;

        public MeleeZombie ZombieMode;

        public TestCommandCenter _commandCenter;

        private MeleeBrain _brain;

        private MeleeConfig _config;

        [SerializeField] 
        private AnimatorOverrideController _overrideController;
        public Transform Player;

        public void Construct(TestCommandCenter commandCenter, MeleeConfig config)
        {
            _commandCenter = commandCenter;

            _config = config;

            InitConfig();

            InitComponents();

            Player = GameObject.FindGameObjectWithTag("Player").transform;

            Eyes.SetSearchTarget(Player);

        }

        private void Update()
        {
            Motion.Update();

            Eyes.Update();

            _brain.Update();

        }

        private void InitConfig()
        {
            IsLife = _config.IsLifeDefault;

            HasDeadYet = false;

            HeatPoint = _config.HeatPoint;

            Damage = _config.Damage;
        }

        private void InitComponents()
        {
            var agent = GetComponent<NavMeshAgent>();

            var animator = GetComponentInChildren<Animator>();

            Motion = new MeleeMotion(agent, _config);

            Eyes = new AgentEyes(transform);

            Animator = new MeleeAnimator(animator, _overrideController);

            Escape = new MeleeEscape(Motion);

            ZombieMode = new MeleeZombie(this, Animator);

            _brain = new MeleeBrain(this);
        }

        public void TakeAttack(IAttackProcess attackProcess)
        {
            if (!IsLife) return;

            if (attackProcess == null) return;

            HeatPoint -= attackProcess.Damage;

            if (HeatPoint <= 0)
            {
                HeatPoint = 0;

                IsLife = false;
                
                return;
            }
        }

        public void Resurrection()
        {
            IsLife = true;
            HasDeadYet = false;
        }
    }
}