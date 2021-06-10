using UnityEngine;

namespace Throwing.Trajectory
{
    [CreateAssetMenu(menuName = "Throw/ThrowConstants", fileName = "ThrowConstants", order = 0)]
    public class ThrowConstants : ScriptableObject
    {
        public float gravity = 9.7f;
        public float horizontalAcceleration = 0f;
    }
}