using PlayerInput;
using Throwing.Trajectory;
using UnityEngine;

namespace Throwing.Aiming
{
    public class Aimer : MonoBehaviour
    {
        [Header("Settings")]
        public TrajectoryFormula trajectoryFormula;
        public bool isAiming;

        [Header("Links")]
        [SerializeField] private AimingEventsChanel aimingEventsChanel;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] public Transform directionPoint;


        public Vector3 GetSpawnPosition()
        {
            return projectileSpawnPoint.position;
        }

        public Vector3 GetCurrentDirection()
        {
            return directionPoint.forward;
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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.ArrowHandleCap(GUIUtility.GetControlID(FocusType.Keyboard),
                projectileSpawnPoint.position,
                Quaternion.LookRotation(directionPoint.forward), 1, EventType.Repaint);
        }
#endif

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