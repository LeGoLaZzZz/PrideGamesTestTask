using PlayerInput;
using Throwing.Thrower;
using UnityEngine;

namespace Throwing
{
    public class ProjectileStarter : MonoBehaviour
    {
        public LayerMask obstacleMask;
        
        public void StartProjectile(ProjectileThrowSettings settings)
        {
            var projectile = settings.projectileProvider.GetProjectileObject();
            projectile.SetOwnerTeam(settings.teamOwner);
            projectile.transform.position = settings.startPosition;

            projectile.obstaclesLayers = obstacleMask; 
            if (settings.throwAimType == ThrowAimType.ByAngle)
            {
                projectile.MovementByAngleSetUp(settings.direction.normalized, settings.trajectoryFormula);
            }
            else
            {
                projectile.MovementByTargetSetUp(settings.endPosition, settings.trajectoryFormula);
            }
        }
    }
}