using UnityEngine;

namespace PlayerInput
{
    [CreateAssetMenu(fileName = "MouseKeyboardInputSettings", menuName = "Input/MouseKeyboardInputSettings", order = 0)]
    public class MouseKeyboardInputSettings : ScriptableObject
    {
        public int fireMouseButtonKey = 0;
        public KeyCode interactKeyCode = KeyCode.E;
        public KeyCode inventorySwipeLeftKeyCode = KeyCode.LeftArrow;
        public KeyCode inventorySwipeRightKeyCode = KeyCode.RightArrow;
    }
}