using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerInput
{
    [Serializable]
    public class FireButtonPressedEvent : UnityEvent
    {
    }

    [Serializable]
    public class FireButtonReleasedEvent : UnityEvent
    {
    }

    [Serializable]
    public class InteractButtonPressedEvent : UnityEvent
    {
    }

    public abstract class InputReader : MonoBehaviour
    {
        public static FireButtonPressedEvent FireButtonPressedEvent = new FireButtonPressedEvent();
        public static FireButtonReleasedEvent FireButtonReleasedEvent = new FireButtonReleasedEvent();

        public static InteractButtonPressedEvent InteractButtonPressedEvent = new InteractButtonPressedEvent();

        public Vector2 MoveVector { get; protected set; }
        public bool IsFireHold { get; protected set; }
        public bool IsFirePressed { get; protected set; }
        public bool IsFireReleased { get; protected set; }

        protected void FireButtonPressedInvoke()
        {
            FireButtonPressedEvent.Invoke();
        }

        protected void FireButtonReleasedInvoke()
        {
            FireButtonReleasedEvent.Invoke();
        }

        protected void InteractButtonPressedInvoke()
        {
            InteractButtonPressedEvent.Invoke();
        }
    }
}