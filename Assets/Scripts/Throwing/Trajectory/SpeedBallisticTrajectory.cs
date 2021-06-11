using UnityEngine;

namespace Throwing.Trajectory
{
    [CreateAssetMenu(menuName = "Throw/BallisticTrajectory", fileName = "BallisticTrajectory", order = 0)]
    public class SpeedBallisticTrajectory : TrajectoryFormula
    {
        public float speed;
        public float peakY;

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

        public static bool CalculateGravityAndYSpeed(Vector3 startPoint, Vector3 endPoint, float speed, float maxHeight,
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

            // System of equations. Hit max_height at t=.5*time. Hit target at t=time.
            //
            // peak = y0 + vertical_speed*halfTime + .5*gravity*halfTime^2
            // end = y0 + vertical_speed*time + .5*gravity*time^s
            // Wolfram Alpha: solve b = a + .5*v*t + .5*g*(.5*t)^2, c = a + vt + .5*g*t^2 for g, v
            float a = startPoint.y; // initial
            float b = maxHeight; // peak
            float c = endPoint.y; // final

            gravity = -4 * (a - 2 * b + c) / (time * time);
            direction.y = -(3 * a - 4 * b + c) / time;

            return true;
        }


        public override Vector3 GetPositionByAngle(Vector3 direction, Vector3 startPoint, float time)
        {
            return GetPositionByAngleAndGravity(direction* speed, startPoint, gravity, time);
        }

        public override Vector3 GetPositionByTarget(Vector3 targetPoint, Vector3 startPoint, float timeMoment)
        {
            CalculateGravityAndYSpeed(startPoint, targetPoint, speed, peakY, out var direction, out var gravity);
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