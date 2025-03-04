using UnityEngine;

namespace Root.Tests
{
    public class HeatPointBox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<MeleeAgent>();

            if (enemy == null) return;

            enemy.HeatPoint = 100;
        }
    }
}
