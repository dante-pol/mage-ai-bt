using UnityEngine;

namespace Root.Tests
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControllerTests : MonoBehaviour
    {
        [Range(0, 20)]
        public float SpeedMove = 5;

        private CharacterController _controller;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Vector3 dir = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
                dir += transform.forward;

            if ( Input.GetKey(KeyCode.S))
                dir -= transform.forward;

            if ( Input.GetKey(KeyCode.D))
                dir += transform.right;

            if ( Input.GetKey(KeyCode.A))
                dir -= transform.right;

            var velocity = dir * (SpeedMove * Time.deltaTime);

            _controller.Move(velocity);
        }
    }
}
