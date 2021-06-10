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

    [Serializable]
    public class InventoryKeyPressedEvent : UnityEvent<InventoryKeyEventArgs>
    {
    }

    [Serializable]
    public class InventoryKeyEventArgs
    {
        public InventoryAction inventoryAction;

        public InventoryKeyEventArgs(InventoryAction inventoryAction)
        {
            this.inventoryAction = inventoryAction;
        }
    }

    public abstract class InputReader : MonoBehaviour
    {
        private static InputReader _instance = null;
        public static InputReader Instance
        {
            get
            {
                if (_instance == null) _instance = (InputReader) FindObjectOfType(typeof(InputReader));

#if PLATFORM_STANDALONE
                if (_instance == null) _instance = new GameObject("InputReader").AddComponent<MouseKeyboardInput>();
#endif

                return _instance;
            }
        }

        public static FireButtonPressedEvent FireButtonPressedEvent = new FireButtonPressedEvent();
        public static FireButtonReleasedEvent FireButtonReleasedEvent = new FireButtonReleasedEvent();
        public static InteractButtonPressedEvent InteractButtonPressedEvent = new InteractButtonPressedEvent();
        public static InventoryKeyPressedEvent InventoryKeyPressedEvent = new InventoryKeyPressedEvent();

        public Vector2 MoveVector { get; protected set; }
        public bool IsFireHold { get; protected set; }
        public bool IsFirePressed { get; protected set; }
        public bool IsFireReleased { get; protected set; }

        public Vector2 ViewRotation { get; protected set; }

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

        protected void InventoryKeyPressedInvoke(InventoryAction action)
        {
            InventoryKeyPressedEvent.Invoke(new InventoryKeyEventArgs(action));
        }
    }
}