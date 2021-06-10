using FPSMovement;
using UnityEngine;

namespace Throwing
{
    public class AimPlayerFreezer : MonoBehaviour
    {
        [Header("Settings")]
        public bool needFreezeMovement = true;
        public bool needFreezeCameraRotation = false;
        [Header("Aim events")]
        [SerializeField] private AimingEventsChanel aimingEventsChanel;
        [Header("Links")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerViewRotation playerViewRotation;

        private void OnEnable()
        {
            aimingEventsChanel.aimingStartedEvent.AddListener(OnAimingStarted); //TODO responsibility?
            aimingEventsChanel.aimingStoppedEvent.AddListener(OnAimingStopped);
        }

        private void OnDisable()
        {
            aimingEventsChanel.aimingStartedEvent.RemoveListener(OnAimingStarted);
            aimingEventsChanel.aimingStoppedEvent.RemoveListener(OnAimingStopped);
        }


        private void OnAimingStarted()
        {
            if (needFreezeMovement) playerMovement.StopMovement();
            if (needFreezeCameraRotation) playerViewRotation.StopRotation();
        }

        private void OnAimingStopped()
        {
            if (needFreezeMovement) playerMovement.StartMovement();
            if (needFreezeCameraRotation) playerViewRotation.StartRotation();
        }
    }
}