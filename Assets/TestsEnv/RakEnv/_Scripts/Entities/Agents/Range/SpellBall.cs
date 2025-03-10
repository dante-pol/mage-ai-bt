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

        private AudioSource _audioSource;

        private Teams _teamID;

        private float _damage;

        public void Construct(Teams teamId, float damage, Color color)
        {
            _teamID = teamId;

            _damage = damage;

            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;

            _material = GetComponentInChildren<MeshRenderer>().material;

            _material.color = color;

            _audioSource = GetComponent<AudioSource>();
        }

        public void PushIt(Vector3 direction)
        {
            _audioSource.Play();
            _rigidbody.velocity = direction * _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            IEntityAttacked entity = other.GetComponent<IEntityAttacked>();

            if (entity == null) return;

            if (_teamID == entity.TeamID) return;

            entity.TakeAttack(new AttackProcess(_damage));

            gameObject.SetActive(false);
        }
    }
}
