using Throwing.Thrower;
using Throwing.Trajectory;
using UnityEngine;

namespace Throwing
{
    public class PlayerTrajectoryDrawer : TrajectoryDrawer
    {
        [Header("Player Links")]
        [SerializeField] private Aimer aimer;
        [SerializeField] private Inventory inventory;


        //player aims by angle, not by target
        protected override ThrowAimType ThrowAimType(out Vector3 targetPoint)
        {
            targetPoint = default;
            return Thrower.ThrowAimType.ByAngle;
        }

        protected override Vector3 GetDirection()
        {
            return aimer.GetCurrentDirection();
        }

        protected override Vector3 GetStartPosition()
        {
            return aimer.GetSpawnPosition();
        }

        protected override TrajectoryFormula GetTrajectoryFormula()
        {
            return aimer.trajectoryFormula;
        }

        protected override Projectile GetProjectile()
        {
            return inventory.GetSelected().GetProjectilePrefab();
        }

        protected override bool NeedDrawTrajectory()
        {
            return !inventory.IsProjectilesEmpty && aimer.isAiming;
        }
    }
}