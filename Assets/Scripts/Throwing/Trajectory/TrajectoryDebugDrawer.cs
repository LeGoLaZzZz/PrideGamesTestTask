using Throwing.Thrower;
using UnityEngine;

namespace Throwing.Trajectory
{
    public class TrajectoryDebugDrawer : TrajectoryDrawer
    {
        public Transform target;
        public Transform directionPoint;
        public TrajectoryFormula formula;
        public GrenadeConfig grenadeConfig;
        public bool needDraw = true;
        public ThrowAimType throwAimType;


        protected override ThrowAimType ThrowAimType(out Vector3 targetPoint)
        {
            targetPoint = target.position;
            return throwAimType;
        }

        protected override Vector3 GetDirection()
        {
            return directionPoint.forward;
        }

        protected override Vector3 GetStartPosition()
        {
            return directionPoint.position;
        }

        protected override TrajectoryFormula GetTrajectoryFormula()
        {
            return formula;
        }

        protected override Projectile GetProjectile()
        {
            return grenadeConfig.GetProjectilePrefab();
        }

        protected override bool NeedDrawTrajectory()
        {
            return needDraw;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.ArrowHandleCap(GUIUtility.GetControlID(FocusType.Keyboard),
                directionPoint.position,
                Quaternion.LookRotation(directionPoint.forward), 1, EventType.Repaint);
        }
#endif
    }
}