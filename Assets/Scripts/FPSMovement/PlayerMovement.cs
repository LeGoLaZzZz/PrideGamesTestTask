using System;
using PlayerInput;
using Throwing;
using UnityEngine;

namespace FPSMovement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        public float movementSpeed = 1;
        public float gravity = 9.8f;
        [SerializeField] private bool canMove = true;


        private CharacterController _characterController;
        private Transform _transform;

        private bool _isGrounded;
        private Vector3 _verticalMove;
        private Vector3 _horizontalMove;

        public bool CanMove => canMove;

        public void StopMovement()
        {
            canMove = false;
        }

        public void StartMovement()
        {
            canMove = true;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _transform = transform;
        }


        private void Update()
        {
            VerticalMove(Time.deltaTime); //Falling
            if (!CanMove) return;
            HorizontalMove(Time.deltaTime);//Walking
        }

        private void HorizontalMove(float deltaTime)
        {
            _horizontalMove = _transform.right * InputReader.Instance.MoveVector.x +
                              _transform.forward * InputReader.Instance.MoveVector.y;

            _characterController.Move(_horizontalMove * (movementSpeed * deltaTime));
        }

        private void VerticalMove(float deltaTime)
        {
            _isGrounded = _characterController.isGrounded;
            if (_isGrounded && _verticalMove.y < 0)
            {
                _verticalMove.y = 0f;
            }

            _verticalMove.y += -gravity * deltaTime;
            _characterController.Move(_verticalMove * deltaTime);
        }
    }
}