using Root.Core.Factories.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{

    [RequireComponent(typeof(Rigidbody))]
    public class SpellBall : MonoBehaviour, IObjectPool
    {
        public event Action<IObjectPool> ReturnToPoolEvent;

        [SerializeField] private float _speed;

        [SerializeField] private GameObject[] _meshes;

        [SerializeField] private ParticleSystem[] _explosives;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private ExplosiveMechanism _explosiveSystem;
        private Collider _collision;
        
        private Teams _teamID;
        private float _damage;

        private GameObject _currentMesh;

        public void Construct()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;

            _audioSource = GetComponent<AudioSource>();

            _explosiveSystem = new ExplosiveMechanism(_explosives);

            _collision = GetComponent<Collider>();
        }

        public void Initialize(Teams teamId, float damage, int attackLevel, Vector3 position)
        {
            Initialize(teamId, damage, attackLevel);

            transform.position = position;

            _collision.enabled = true;
        }

        public void Initialize(Teams teamId, float damage, int attackLevel)
        {
            _teamID = teamId;

            _damage = damage;

            gameObject.SetActive(true);

            SelectEffectExplosive(attackLevel);

            SelectMeshByAttackLevel(attackLevel);
        }

        private void SelectEffectExplosive(int attackLevel)
        {
            if (attackLevel >= _explosives.Length) throw new UnityException("Attack level of bounds...");

            _explosiveSystem.UpdateEffect(attackLevel);
        }

        public void PushIt(Vector3 direction)
        {
            _audioSource.Play();

            _rigidbody.velocity = direction * _speed;
        }

        private void SelectMeshByAttackLevel(int attackLevel)
        {
            if (attackLevel >= _meshes.Length) throw new UnityException("Attack level of bounds...");

            _currentMesh = _meshes[attackLevel];
            
            _currentMesh.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            IEntityAttacked entity = other.GetComponent<IEntityAttacked>();

            if (entity != null)
            {
                if (_teamID != entity.TeamID)
                    entity.TakeAttack(new AttackProcess(_damage));
            }

            Debug.LogWarning($"Spell Ball Trigger -------------{other.name}---------------");
            Deactivate();
        }

        private void Deactivate()
        {
            ResetForPool();

            ReturnToPoolEvent?.Invoke(this);
        }

        public void ResetForPool()
        {
            _collision.enabled = false; 

            _currentMesh.SetActive(false);

            _explosiveSystem.Play();

            _rigidbody.velocity = Vector3.zero;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (transform.childCount == 0) return;

            List<GameObject> _objects = new List<GameObject>();

            foreach (Transform child in transform)
            {
                if (child.GetComponent<ParticleSystem>() == null)
                    _objects.Add(child.gameObject);
            }

            _meshes = _objects.ToArray();
        }
#endif
    }
}
