using UnityEngine;

namespace Throwing.Trajectory
{
    [CreateAssetMenu(menuName = "Throw/BallisticTrajectory", fileName = "BallisticTrajectory", order = 0)]
    public class SpeedBallisticTrajectory : TrajectoryFormula
    {
        public float speed;

        public static float GetMaxHigh(float speed, float gravitation, Vector3 direction)
        {
            var angle = Vector3.Angle(direction, Vector3.forward);
            var sin2 = Mathf.Sin(angle * Mathf.Deg2Rad);
            return speed * speed * sin2 / 2 / gravitation;
        }

        public static float GetMaxRange(float speed, float gravitation, Vector3 startPoint)
        {
            var radAngle = 45 * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radAngle);
            var sin = Mathf.Sin(radAngle);

            var range = (speed * cos / gravitation) *
                        (speed * sin + Mathf.Sqrt(speed * speed * sin * sin + 2 * gravitation * startPoint.y));
            return range;
        }


        public override Vector3 GetPosition(Vector3 direction, Vector3 startPoint, float time)
        {
            return new Vector3(
                GetSimpleMovementFloat(constants.horizontalAcceleration, direction.x * speed, startPoint.x, time),
                GetSimpleMovementFloat(-constants.gravity, direction.y * speed, startPoint.y, time),
                GetSimpleMovementFloat(constants.horizontalAcceleration, direction.z * speed, startPoint.z, time)
            );
        }

        public override float GetMaxHigh(Vector3 direction, Vector3 startPoint)
        {
            return GetMaxHigh(speed, constants.gravity, direction);
        }

        public override float GetMaxRange(Vector3 direction, Vector3 startPoint)
        {
            return GetMaxRange(speed, constants.gravity, startPoint);
        }


        private float GetSimpleMovementFloat(float acceleration, float speed, float start, float time)
        {
            return acceleration * time * time / 2 + speed * time + start;
        }
    }
}