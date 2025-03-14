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

        private Rigidbody _rigidbody;

        private AudioSource _audioSource;

        private Teams _teamID;

        private float _damage;

        public void Construct()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;

            _audioSource = GetComponent<AudioSource>();        
        }

        public void Initialize(Teams teamId, float damage, int attackLevel, Vector3 position)
        {
            Initialize(teamId, damage, attackLevel);

            transform.position = position;
        }

        public void Initialize(Teams teamId, float damage, int attackLevel)
        {
            _teamID = teamId;

            _damage = damage;

            gameObject.SetActive(true);

            SelectMeshByAttackLevel(attackLevel);
        }

        public void PushIt(Vector3 direction)
        {
            _audioSource.Play();

            _rigidbody.velocity = direction * _speed;
        }

        private void SelectMeshByAttackLevel(int attackLevel)
        {
            if (attackLevel >= _meshes.Length) throw new UnityException("Attack level of bounds...");

            _meshes[attackLevel].gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            IEntityAttacked entity = other.GetComponent<IEntityAttacked>();

            if (entity == null) return;

            if (_teamID == entity.TeamID) return;

            entity.TakeAttack(new AttackProcess(_damage));

            Deactivate();
        }

        private void Deactivate()
        {
            ResetForPool();

            ReturnToPoolEvent?.Invoke(this);
        }

        public void ResetForPool()
        {
            gameObject.SetActive(false);

            _rigidbody.velocity = Vector3.zero;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (transform.childCount == 0) return;

            List<GameObject> _objects = new List<GameObject>();

            foreach (Transform child in transform)
                _objects.Add(child.gameObject);

            _meshes = _objects.ToArray();
        }
#endif
    }
}
