namespace Root
{
    public class AttackProcess : IAttackProcess
    {
        public float Damage => _damage;

        private float _damage;

        public AttackProcess(float damage) 
            => _damage = damage;
    }
}