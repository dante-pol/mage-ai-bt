using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class SpawnPointVisualizer : MonoBehaviour
    {
        [SerializeField] Color _color;

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;

            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}
