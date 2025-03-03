using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    [CreateAssetMenu(fileName = "New Attack Config", menuName = "Create Range Attack Config", order = 22)]
    public class RangeAttackConfig : ScriptableObject
    {
        public int Damage => _damage;
        public float Cooldown => _cooldown;
        public Color Color => _color;

        [SerializeField] private int _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private Color _color;
    }
}
