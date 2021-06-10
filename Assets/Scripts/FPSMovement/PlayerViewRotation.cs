using System;
using PlayerInput;
using UnityEngine;

namespace FPSMovement
{
    public class PlayerViewRotation : MonoBehaviour
    {
        [Header("Settings")]
        public float sensitivity = 1;
        [SerializeField] private bool canRotation = true;
        [SerializeField] private Vector2 upDownRotationLimit = new Vector2(-90, 90);

        [Header("Links")]
        [SerializeField] private Transform sideRotationTransform;
        [SerializeField] private Transform upDownRotationTransform;

        private float _cameraXRotation;

        public void StopRotation()
        {
            canRotation = false;
        }
        
        public void StartRotation()
        {
            canRotation = true;
        }
        private void Start()
        {
            _cameraXRotation = upDownRotationTransform.transform.localRotation.x;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (canRotation)
                RotationTick();
        }

        private void RotationTick()
        {
            SideRotation(InputReader.Instance.ViewRotation.x * sensitivity);
            UpDownRotation(InputReader.Instance.ViewRotation.y * sensitivity);
        }

        private void SideRotation(float delta)
        {
            sideRotationTransform.transform.Rotate(Vector3.up, delta);
        }

        private void UpDownRotation(float delta)
        {
            _cameraXRotation -= delta;
            _cameraXRotation = Mathf.Clamp(_cameraXRotation, upDownRotationLimit.x, upDownRotationLimit.y);
            upDownRotationTransform.transform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);
        }
    }
}