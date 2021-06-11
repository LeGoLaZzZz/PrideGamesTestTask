using PlayerInput;
using UnityEngine;

namespace Throwing
{
    public class ProjectileStarter : MonoBehaviour
    {
        public void StartProjectile(ProjectileThrowSettings settings)
        {
            var projectile = settings.projectileProvider.GetProjectileObject();
            projectile.SetOwnerTeam(settings.teamOwner);
            projectile.transform.position = settings.startPosition;
            projectile.MovementSetUp(settings.direction.normalized, settings.trajectoryFormula);
        }
    }
}