using UnityEngine;
using UnityEngine.AI;

namespace Root.Entities.Interactive
{
    [RequireComponent(typeof(NavMeshObstacle), typeof(Animator))]
    public class DoorObstacle : MonoBehaviour
    {
        private readonly int IsOpenHash = Animator.StringToHash("IsOpenDoor");

        private NavMeshObstacle _obstacleController;

        private Animator _animator;

        public void Start()
        {
            _obstacleController = GetComponent<NavMeshObstacle>();

            _animator = GetComponent<Animator>();
        }

        public void OpenDoor()
        {
            Debug.Log("Door Opening..");

            _obstacleController.enabled = false;

            _animator.SetBool(IsOpenHash, true);
        }

        public void CloseDoor()
        {
            Debug.Log("Door Closing..");

            _obstacleController.enabled = true;

            _animator.SetBool(IsOpenHash, false);
        }

    }
}