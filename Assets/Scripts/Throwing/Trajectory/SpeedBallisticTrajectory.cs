using UnityEngine;

namespace Throwing.Trajectory
{
    [CreateAssetMenu(menuName = "Throw/BallisticTrajectory", fileName = "BallisticTrajectory", order = 0)]
    public class SpeedBallisticTrajectory : TrajectoryFormula
    {
        public float speed;
        public float peakY;

        public static bool CalculateGravityAndDirection(Vector3 startPoint, Vector3 endPoint, float speed, float maxHeight,
            out Vector3 direction, out float gravity)
        {
            direction = Vector3.zero;
            gravity = float.NaN;

            Vector3 diff = endPoint - startPoint;
            Vector3 diffXZ = new Vector3(diff.x, 0f, diff.z);
            float lateralDist = diffXZ.magnitude;

            if (lateralDist == 0)
                return false;

            float time = lateralDist / speed;

            direction = diffXZ.normalized * speed;

            //https://habr.com/ru/post/538952/  
            gravity = -4 * ( startPoint.y - 2 * maxHeight + endPoint.y) / (time * time);
            direction.y = -(3 *  startPoint.y - 4 * maxHeight + endPoint.y) / time;

            return true;
        }


        public override Vector3 GetPositionByAngle(Vector3 direction, Vector3 startPoint, float time)
        {
            return GetPositionByAngleAndGravity(direction* speed, startPoint, gravity, time);
        }

        public override Vector3 GetPositionByTarget(Vector3 targetPoint, Vector3 startPoint, float timeMoment)
        {
            CalculateGravityAndDirection(startPoint, targetPoint, speed, peakY, out var direction, out var gravity);
            return GetPositionByAngleAndGravity(direction, startPoint, gravity, timeMoment);
        }

        private Vector3 GetPositionByAngleAndGravity(Vector3 direction, Vector3 startPoint, float gravity, float time)
        {
            return new Vector3(
                GetSimpleMovementFloat(horizontalAcceleration, direction.x , startPoint.x, time),
                GetSimpleMovementFloat(-gravity, direction.y , startPoint.y, time),
                GetSimpleMovementFloat(horizontalAcceleration, direction.z , startPoint.z, time)
            );
        }


        private float GetSimpleMovementFloat(float acceleration, float speed, float start, float time)
        {
            if (acceleration > 0) acceleration *= -1;
            return acceleration * time * time / 2 + speed * time + start;
        }
    }
}