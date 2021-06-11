using UnityEngine;

namespace Throwing.Trajectory
{
    [CreateAssetMenu(menuName = "Throw/RangeBallisticTrajectory", fileName = "RangeBallisticTrajectory", order = 0)]
    public class RangeBallisticTrajectory : TrajectoryFormula
    {
        public float maxRange;
        public float maxHeight;

        public override Vector3 GetPositionByAngle(Vector3 direction, Vector3 startPoint, float timeMoment)
        {
            direction.x *= GetSpeedByRange(maxRange);
            direction.y *= GetSpeedByHeight(maxHeight);
            direction.z *= GetSpeedByRange(maxRange);
            return GetPositionByAngle(direction, startPoint, gravity, timeMoment);
        }

        public override Vector3 GetPositionByTarget(Vector3 targetPoint, Vector3 startPoint, float timeMoment)
        {
            SpeedBallisticTrajectory.CalculateGravityAndDirection(startPoint, targetPoint, GetSpeedByRange(maxRange),
                maxHeight, out var direction, out var gravity);

            return GetPositionByAngle(direction, startPoint, gravity, timeMoment);
        }

        private Vector3 GetPositionByAngle(Vector3 direction, Vector3 startPoint, float gravity, float timeMoment)
        {
            return new Vector3(
                GetSimpleMovementFloat(horizontalAcceleration, direction.x,
                    startPoint.x, timeMoment),
                GetSimpleMovementFloat(-gravity, direction.y, startPoint.y,
                    timeMoment),
                GetSimpleMovementFloat(horizontalAcceleration, direction.z,
                    startPoint.z, timeMoment)
            );
        }


        private float GetSimpleMovementFloat(float acceleration, float direction, float start, float time)
        {
            return acceleration * time * time / 2 + direction * time + start;
        }

        private float GetSpeedByRange(float range)
        {
            var radAngle = 45 * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radAngle);
            var sin = Mathf.Sin(radAngle);

            //v=sqrt(lg/sina/cosa/2)
            return Mathf.Sqrt(range * gravity / cos / sin / 2);
        }

        private float GetSpeedByHeight(float height)
        {
            var radAngle = 45 * Mathf.Deg2Rad;
            var sin = Mathf.Sin(radAngle);

            //v=sqrt(h*2*g/sin/sin)
            return Mathf.Sqrt(height * gravity * 2 / sin / sin);
        }

    }
}