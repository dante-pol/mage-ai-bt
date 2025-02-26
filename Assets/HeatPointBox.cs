using UnityEngine;

namespace Root.Tests
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
