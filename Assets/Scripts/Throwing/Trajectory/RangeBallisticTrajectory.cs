using UnityEngine;

namespace Throwing.Trajectory
{
    [CreateAssetMenu(menuName = "Throw/RangeBallisticTrajectory", fileName = "RangeBallisticTrajectory", order = 0)]
    public class RangeBallisticTrajectory : TrajectoryFormula
    {
        public float maxRange;
        public float maxHeight;

        public override Vector3 GetPosition(Vector3 direction, Vector3 startPoint, float timeMoment)
        {
            return new Vector3(
                startPoint.x +
                (GetSimpleMovementFloat(constants.horizontalAcceleration, direction.x ,startPoint.x, timeMoment) - startPoint.x) *
                maxRange / SpeedBallisticTrajectory.GetMaxRange(1, constants.gravity, startPoint),
                
                startPoint.y +
                (GetSimpleMovementFloat(-constants.gravity, direction.y ,startPoint.y, timeMoment) - startPoint.y) *
                maxHeight / SpeedBallisticTrajectory.GetMaxHigh(1, constants.gravity, direction),
                
                startPoint.z +
                (GetSimpleMovementFloat(constants.horizontalAcceleration, direction.z ,startPoint.z, timeMoment) - startPoint.z) *
                maxRange / SpeedBallisticTrajectory.GetMaxRange(1, constants.gravity, startPoint)
            );
        }

        public override float GetMaxHigh(Vector3 direction, Vector3 startPoint)
        {
            return maxHeight;
        }

        public override float GetMaxRange(Vector3 direction, Vector3 startPoint)
        {
            return maxRange;
        }

        private float GetSimpleMovementFloat(float acceleration,float direction, float start, float time)
        {
            return acceleration * time * time / 2 + direction * time + start;
        }
    }
}