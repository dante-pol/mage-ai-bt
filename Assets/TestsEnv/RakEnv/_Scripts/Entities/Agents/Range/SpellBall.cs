using Root.Core.Factories.Tools;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpellBall : MonoBehaviour, IObjectPool
    {
        [SerializeField] private float _speed;
        
        private Material _material;

        private Rigidbody _rigidbody;


        private float _damage;

        public void Construct(float damage, Color color)
        {
            _damage = damage;

            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;

            _material = GetComponentInChildren<MeshRenderer>().material;

            _material.color = color;
        }

        public void PushIt(Vector3 direction)
        {
            _rigidbody.velocity = direction * _speed;
        }
    }
}
