using UnityEngine;

namespace Throwing.Trajectory
{
    public abstract class TrajectoryFormula : ScriptableObject
    {
        public float gravity = 9.7f;
        public float horizontalAcceleration = 0f;

        public abstract Vector3 GetPositionByAngle(Vector3 direction, Vector3 startPoint, float timeMoment);
        public abstract Vector3 GetPositionByTarget(Vector3 targetPoint, Vector3 startPoint, float timeMoment);
        
    }
}