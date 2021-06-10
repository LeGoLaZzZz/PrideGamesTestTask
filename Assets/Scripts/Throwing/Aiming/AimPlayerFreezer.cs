using FPSMovement;
using UnityEngine;

namespace Throwing
{
    public class AimPlayerFreezer : MonoBehaviour
    {
        [Header("Aim events")]
        [SerializeField] private AimingEventsChanel aimingEventsChanel;
        [Header("Systems to freeze on Aim")]
        [SerializeField] private PlayerMovement playerMovement;

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
            playerMovement.StopMovement();
        }

        private void OnAimingStopped()
        {
            playerMovement.StartMovement();
        }
    }
}