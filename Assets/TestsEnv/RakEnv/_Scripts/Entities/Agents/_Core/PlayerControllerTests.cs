using UnityEngine;

namespace Root.Tests
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControllerTests : MonoBehaviour, ICharacterTarget
    {
        [SerializeField] [Range(0, 100)] private int _heatPoint = 100;
        
        [Range(0, 20)]
        public float SpeedMove = 5;

        private CharacterController _controller;
        
        [SerializeField] private bool _isActiveUlt = false;

        public Vector3 Position => transform.position;

        public int HeatPoints => _heatPoint;

        public bool IsActiveUlt => _isActiveUlt;

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
