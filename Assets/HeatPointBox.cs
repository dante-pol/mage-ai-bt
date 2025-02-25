using UnityEngine;

namespace Root
{
    public class HeatPointBox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Agent>();

            if (enemy == null) return;

            enemy.HeatPoint = 100;
        }
    }
}
