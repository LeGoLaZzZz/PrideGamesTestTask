using UnityEngine;

namespace PlayerInput
{
    public class MouseKeyboardInput : InputReader
    {
        public MouseKeyboardInputSettings mouseKeyboardInputSettings;
    
        private int FireMouseButtonKey => mouseKeyboardInputSettings.fireMouseButtonKey;
        private KeyCode InteractButtonKeyCode => mouseKeyboardInputSettings.interactButtonKeyCode;
    
        private Vector2 _moveVector;

        private void Update()
        {
            InteractButtonTick();
            FireButtonsTick();
            MoveVectorTick();
        }


        private void MoveVectorTick()
        {
            _moveVector.x = Input.GetAxis("Horizontal");
            _moveVector.y = Input.GetAxis("Vertical");
            MoveVector = _moveVector;
        }

        private void InteractButtonTick()
        {
            if (Input.GetKeyDown(InteractButtonKeyCode))
            {
                InteractButtonPressedInvoke();
            }
        }

        private void FireButtonsTick()
        {
            IsFirePressed = Input.GetMouseButtonDown(FireMouseButtonKey);
            if (IsFirePressed)
            {
                FireButtonPressedInvoke();
            }

            IsFireHold = Input.GetMouseButton(FireMouseButtonKey);

            IsFireReleased = Input.GetMouseButtonUp(FireMouseButtonKey);
            if (IsFireReleased)
            {
                FireButtonReleasedInvoke();
            }
        }
    }
}