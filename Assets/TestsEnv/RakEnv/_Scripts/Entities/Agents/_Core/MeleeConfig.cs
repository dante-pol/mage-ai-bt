using UnityEngine;

namespace Root
{
    [CreateAssetMenu(fileName = "NewMeleeConfig", menuName = "Create New Melee Config", order = 21)]
    public class MeleeConfig : ScriptableObject, IMeleeMotionConfig, IMeleeAgentConfig
    {
        public int HeatPoint => _heatPoint;
        public int Damage => _damage;
        public int HeatPointZombie => _heatPointZombie;
        public int DamageZombie => _damageZombie;
        public float WalkSpeed => _walkSpeed;
        public float RunSpeed => _runSpeed;
        public float WalkSpeedZombie => _walkSpeedZombie;
        public float RunSpeedZombie => _runSpeedZombie;

        [Header("Melee Agent")]
        [Space]
        [Header("Human Agent")]
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
    }
}