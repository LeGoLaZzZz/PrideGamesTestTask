using System;
using UnityEngine;
using UnityEngine.Events;

namespace Throwing
{
    [Serializable]
    public class AimingStartedEvent : UnityEvent
    {
    }

    [Serializable]
    public class AimingStoppedEvent : UnityEvent
    {
    }

    [CreateAssetMenu(fileName = "AimingEventsChanel", menuName = "Chanels/AimingEventsChanel", order = 0)]
    public class AimingEventsChanel : ScriptableObject
    {
        public AimingStartedEvent aimingStartedEvent = new AimingStartedEvent();
        public AimingStoppedEvent aimingStoppedEvent = new AimingStoppedEvent();

        public void AimingStartedInvoke()
        {
            aimingStartedEvent.Invoke();
        }

        public void AimingStoppedInvoke()
        {
            aimingStoppedEvent.Invoke();
        }
    }
}