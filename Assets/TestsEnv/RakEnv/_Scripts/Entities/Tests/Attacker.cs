using UnityEngine;

namespace Root.Tests
{

    public class Attacker : MonoBehaviour
    {
        [SerializeField] [Range(0, 50)] private float _damage = 100;

        private void OnTriggerEnter(Collider other)
        {
            IEntityAttacked entity = other.GetComponent<IEntityAttacked>();

            if (entity == null) return;

            Debug.Log(other.gameObject.name);

            entity.TakeAttack(new AttackProcess(_damage));

            gameObject.SetActive(false);
        }
    }
}