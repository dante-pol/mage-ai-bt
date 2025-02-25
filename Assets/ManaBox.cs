using System.Xml.Schema;
using UnityEngine;

namespace Root
{
    public class ManaBox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Agent>();

            if (enemy == null) return;

            enemy.Mana = 100;
        }
    }
}
