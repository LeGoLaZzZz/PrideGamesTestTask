using System;
using PlayerInput;
using Throwing.Trajectory;
using UnityEngine;
using UnityEngine.Events;

namespace Throwing
{
    public class Aimer : MonoBehaviour
    {
        [Header("Settings")]
        public float initialSpeed = 5f;
        public TrajectoryFormula trajectoryFormula;
        public bool isAiming;

        [Header("Links")]
        [SerializeField] private AimingEventsChanel aimingEventsChanel;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Transform projectileSpawnPoint;


        public Vector3 GetSpawnPosition()
        {
            return projectileSpawnPoint.position;
        }

        public Vector3 GetCurrentDirection()
        {
            return playerCamera.transform.forward;
        }

        private void OnEnable()
        {
            InputReader.FireButtonPressedEvent.AddListener(OnFireButtonPressed);
            InputReader.FireButtonReleasedEvent.AddListener(OnFireButtonReleased);
        }

        private void OnDisable()
        {
            InputReader.FireButtonPressedEvent.RemoveListener(OnFireButtonPressed);
            InputReader.FireButtonReleasedEvent.RemoveListener(OnFireButtonReleased);
        }

        private void OnDrawGizmosSelected()
        {
            if (projectileSpawnPoint)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(projectileSpawnPoint.transform.position, 0.1f);
            }
        }

        private void OnFireButtonReleased()
        {
            StopAiming();
        }

        private void OnFireButtonPressed()
        {
            StartAiming();
        }

        private void StartAiming()
        {
            isAiming = true;
            aimingEventsChanel.AimingStartedInvoke();
        }

        private void StopAiming()
        {
            isAiming = false;
            aimingEventsChanel.AimingStoppedInvoke();
        }
    }
}