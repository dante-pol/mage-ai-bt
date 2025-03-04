using Root.Core.Factories.Tools;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpellBall : MonoBehaviour, IObjectPool
    {
        [SerializeField] private float _speed;

        private Rigidbody _rigidbody;

        private int _damage;

        public void Construct(int damage)
        {
            _damage = damage;

            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;
        }

        public void PushIt(Vector3 direction)
        {
            _rigidbody.velocity = direction * _speed;
        }
    }
}
