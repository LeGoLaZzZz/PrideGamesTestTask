using UnityEngine;

namespace Throwing.Trajectory
{
    public abstract class TrajectoryFormula : ScriptableObject
    {
        [SerializeField] protected ThrowConstants constants;

        public abstract Vector3 GetPosition(Vector3 direction, Vector3 startPoint, float timeMoment);
        public abstract float GetMaxHigh(Vector3 direction, Vector3 startPoint);
        public abstract float GetMaxRange(Vector3 direction, Vector3 startPoint);
        
    }
}