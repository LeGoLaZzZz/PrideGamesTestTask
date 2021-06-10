using UnityEngine;

namespace Throwing.Trajectory
{
    public abstract class TrajectoryFormula : ScriptableObject
    {
        [SerializeField] protected ThrowConstants constants;

        public abstract Vector3 GetPosition(Vector3 direction, float speed, Vector3 startPoint, float timeMoment);
    }
}