using Root.Core.Factories.Tools;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpellBall : MonoBehaviour, IObjectPool
    {
        [SerializeField] private float _speed;

        [SerializeField] private GameObject[] _meshes;

        private Rigidbody _rigidbody;

        private AudioSource _audioSource;

        private Teams _teamID;

        private float _damage;

        public void Construct(Teams teamId, float damage, int attackLevel)
        {
            _teamID = teamId;

            _damage = damage;

            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;

            _audioSource = GetComponent<AudioSource>();

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

            gameObject.SetActive(false);
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
