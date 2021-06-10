using PlayerInput;
using UnityEngine;

namespace Throwing
{
    public class ProjectileStarter : MonoBehaviour
    {
        public void StartProjectile(ProjectileThrowSettings settings)
        {
            var projectile = InstantiateProjectile(settings.prefab, settings.startPosition);
            projectile.SetUp(settings.direction.normalized, settings.initialSpeed, settings.trajectoryFormula);
        }

        private Projectile InstantiateProjectile(Projectile projectilePrefab, Vector3 spawnPosition)
        {
            var newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = spawnPosition;
            return newProjectile;
        }
    }
}