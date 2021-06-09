using System;
using PlayerInput;
using UnityEngine;

namespace FPSMovement
{
    public class PlayerViewRotation : MonoBehaviour
    {
        public float sensitivity = 1;

        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Vector2 upDownRotationLimit = new Vector2(-90, 90);

        private float _cameraXRotation;

        private void Start()
        {
            _cameraXRotation = playerCamera.transform.localRotation.x;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            RotationTick();
        }

        private void RotationTick()
        {
            SideRotation(InputReader.Instance.ViewRotation.x * sensitivity);
            UpDownRotation(InputReader.Instance.ViewRotation.y * sensitivity);
        }

        private void SideRotation(float delta)
        {
            characterController.transform.Rotate(Vector3.up, delta);
        }

        private void UpDownRotation(float delta)
        {
            _cameraXRotation -= delta;
            _cameraXRotation = Mathf.Clamp(_cameraXRotation, upDownRotationLimit.x, upDownRotationLimit.y);
            playerCamera.transform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);
        }
    }
}