using System;
using UnityEngine;

namespace PlayerInput
{
    /// <summary>
    ///  For simple testing. To provide static unity events of InputReader in inspector.
    /// </summary>
    public class InputEventsListener : MonoBehaviour
    {
        public FireButtonPressedEvent fireButtonPressedEvent = new FireButtonPressedEvent();
        public FireButtonReleasedEvent fireButtonReleasedEvent = new FireButtonReleasedEvent();
        public InteractButtonPressedEvent interactButtonPressedEvent = new InteractButtonPressedEvent();
        public InventoryKeyPressedEvent inventoryKeyPressedEvent = new InventoryKeyPressedEvent();


        private void OnEnable()
        {
            InputReader.FireButtonPressedEvent.AddListener(OnFireButtonPressed);
            InputReader.FireButtonReleasedEvent.AddListener(OnFireButtonReleased);
            InputReader.InteractButtonPressedEvent.AddListener(OnInteractButtonReleased);
            InputReader.InventoryKeyPressedEvent.AddListener(OnInventoryKeyPressed);
        }

        private void OnDisable()
        {
            InputReader.FireButtonPressedEvent.RemoveListener(OnFireButtonPressed);
            InputReader.FireButtonReleasedEvent.RemoveListener(OnFireButtonReleased);
            InputReader.InteractButtonPressedEvent.RemoveListener(OnInteractButtonReleased);
            InputReader.InventoryKeyPressedEvent.RemoveListener(OnInventoryKeyPressed);
        }

        private void OnInventoryKeyPressed(InventoryKeyEventArgs arg0)
        {
            inventoryKeyPressedEvent.Invoke(arg0);
        }

        private void OnInteractButtonReleased()
        {
            interactButtonPressedEvent.Invoke();
        }

        private void OnFireButtonReleased()
        {
            fireButtonReleasedEvent.Invoke();
        }

        private void OnFireButtonPressed()
        {
            fireButtonPressedEvent.Invoke();
        }
    }
}