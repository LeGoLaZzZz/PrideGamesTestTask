using UnityEngine;

namespace Thrower
{
    public abstract class TrajectoryFormula : ScriptableObject
    {
        [SerializeField] protected ThrowConstants constants;

        public abstract Vector3 Move(Vector3 direction, float speed, Vector3 startPoint, float timeMoment);
    }
}