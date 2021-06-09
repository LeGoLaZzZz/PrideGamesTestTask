using UnityEngine;

namespace PlayerInput
{
    public class MouseKeyboardInput : InputReader
    {
        public MouseKeyboardInputSettings mouseKeyboardInputSettings;

        private int FireMouseButtonKey => mouseKeyboardInputSettings.fireMouseButtonKey;
        private KeyCode InteractKeyCode => mouseKeyboardInputSettings.interactKeyCode;
        private KeyCode InventorySwipeRightKeyCode => mouseKeyboardInputSettings.inventorySwipeRightKeyCode;
        private KeyCode InventorySwipeLeftKeyCode => mouseKeyboardInputSettings.inventorySwipeLeftKeyCode;

        private Vector2 _moveVector;
        private Vector2 _viewRotation;

        private void Update()
        {
            InteractButtonTick();
            FireButtonsTick();
            MoveVectorTick();
            InventoryKeysTick();
            ViewRotationTick();
        }

        private void ViewRotationTick()
        {
            _viewRotation.x = Input.GetAxis("Mouse X");
            _viewRotation.y = Input.GetAxis("Mouse Y");
            ViewRotation = _viewRotation;
        }

        private void InventoryKeysTick()
        {
            if (Input.GetKeyDown(InventorySwipeLeftKeyCode)) InventoryKeyPressedInvoke(InventoryAction.SwipeLeft);
            if (Input.GetKeyDown(InventorySwipeRightKeyCode)) InventoryKeyPressedInvoke(InventoryAction.SwipeRight);
        }

        private void MoveVectorTick()
        {
            _moveVector.x = Input.GetAxis("Horizontal");
            _moveVector.y = Input.GetAxis("Vertical");
            MoveVector = _moveVector;
        }

        private void InteractButtonTick()
        {
            if (Input.GetKeyDown(InteractKeyCode)) InteractButtonPressedInvoke();
        }

        private void FireButtonsTick()
        {
            IsFirePressed = Input.GetMouseButtonDown(FireMouseButtonKey);
            if (IsFirePressed) FireButtonPressedInvoke();

            IsFireHold = Input.GetMouseButton(FireMouseButtonKey);

            IsFireReleased = Input.GetMouseButtonUp(FireMouseButtonKey);
            if (IsFireReleased) FireButtonReleasedInvoke();
        }
    }
}