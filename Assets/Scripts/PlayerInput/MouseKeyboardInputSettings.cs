using UnityEngine;

namespace PlayerInput
{
    [CreateAssetMenu(fileName = "MouseKeyboardInputSettings", menuName = "Input/MouseKeyboardInputSettings", order = 0)]
    public class MouseKeyboardInputSettings : ScriptableObject
    {
        public int fireMouseButtonKey = 0;
        public KeyCode interactButtonKeyCode = KeyCode.E;
    }
}