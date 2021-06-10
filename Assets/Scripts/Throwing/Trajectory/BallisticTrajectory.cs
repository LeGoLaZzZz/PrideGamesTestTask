using UnityEngine;

namespace Throwing.Trajectory
{
    [CreateAssetMenu(menuName = "Throw/BallisticTrajectory", fileName = "BallisticTrajectory", order = 0)]
    public class BallisticTrajectory : TrajectoryFormula
    {
        public override Vector3 GetPosition(Vector3 direction, float speed, Vector3 startPoint, float time)
        {
            return new Vector3(
                GetSimpleMovementFloat(constants.horizontalAcceleration, direction.x * speed, startPoint.x, time),
                GetSimpleMovementFloat(-constants.gravity, direction.y * speed, startPoint.y, time),
                GetSimpleMovementFloat(constants.horizontalAcceleration, direction.z * speed, startPoint.z, time)
            );
        }

       
        
        public float BallisticRange(float angle, float speed, float gravity, float startY)
        {
            // Derivation
            //   (1) x = speed * time * cos O
            //   (2) y = initial_height + (speed * time * sin O) - (.5 * gravity*time*time)
            //   (3) via quadratic: t = (speed*sin O)/gravity + sqrt(speed*speed*sin O + 2*gravity*initial_height)/gravity    [ignore smaller root]
            //   (4) solution: range = x = (speed*cos O)/gravity * sqrt(speed*speed*sin O + 2*gravity*initial_height)    [plug t back into x=speed*time*cos O]
            float radAngle = angle * Mathf.Deg2Rad;
            float cos = Mathf.Cos(radAngle);
            float sin = Mathf.Sin(radAngle);

            float range = (speed * cos / gravity) *
                          (speed * sin + Mathf.Sqrt(speed * speed * sin * sin + 2 * gravity * startY));
            return range;
        }

        private float GetSimpleMovementFloat(float acceleration, float speed, float start, float time)
        {
            return acceleration * time * time / 2 + speed * time + start;
        }
    }
}