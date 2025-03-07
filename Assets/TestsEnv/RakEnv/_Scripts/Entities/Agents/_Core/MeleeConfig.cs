using UnityEngine;

namespace Root
{
    [CreateAssetMenu(fileName = "NewMeleeConfig", menuName = "Create New Melee Config", order = 21)]
    public class MeleeConfig : ScriptableObject, IMeleeMotionConfig, IMeleeAgentConfig
    {
        [System.Serializable]
        public class MeleeSoundConfig : IMeleeSoundConfig
        {
            public AudioClip Walk => _walk;
            public AudioClip Run => _run;
            public AudioClip Attack => _attack;
            public AudioClip Death => _death;
            public AudioClip ZombieTransformation => _zombieTransformation;

            [SerializeField] private AudioClip _walk;
            [SerializeField] private AudioClip _run;
            [SerializeField] private AudioClip _attack;
            [SerializeField] private AudioClip _death;
            [SerializeField] private AudioClip _zombieTransformation;
        }

        public bool IsLifeDefault => _isLifeDefault;
        public int HeatPoint => _heatPoint;
        public int Damage => _damage;
        public int HeatPointZombie => _heatPointZombie;
        public int DamageZombie => _damageZombie;
        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
        public float WalkSpeedZombie => _walkSpeedZombie;
        public float RunSpeedZombie => _runSpeedZombie;

        public IMeleeSoundConfig SoundConfig => _soundConfig;


        [Header("Melee Agent")]
        [Space]
        [Header("Human Agent")]
        [SerializeField] bool _isLifeDefault;

        [SerializeField][Range(0, 100)] private int _heatPoint;
        [SerializeField][Range(0, 100)] private int _damage;

        [Header("Zombie Agent")]
        [SerializeField][Range(0, 100)] private int _heatPointZombie;
        [SerializeField][Range(0, 100)] private int _damageZombie;

        [Header("Melee Motion")]
        [Space]

        [Header("Human Motion")]
        [SerializeField][Range(0, 100)] private float _walkSpeed;
        [SerializeField][Range(0, 100)] private float _runSpeed;

        [Header("Zombie Motion")]
        [SerializeField][Range(0, 100)] private float _walkSpeedZombie;
        [SerializeField][Range(0, 100)] private float _runSpeedZombie;

        [Header("Sounds Agent")]
        [SerializeField] private MeleeSoundConfig _soundConfig;
    }
}