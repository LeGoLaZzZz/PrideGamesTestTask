using PlayerInput;
using UnityEngine;

namespace Throwing
{
    public class ProjectileThrower : MonoBehaviour
    {
        [SerializeField] private ProjectileStarter projectileStarter;
        [SerializeField] private Aimer aimer;
        [SerializeField] private ProjectileInventory inventory;


        public void Throw()
        {
            var newSettings = new ProjectileThrowSettings(
                inventory.GetSelectedProjectile(),
                aimer.GetCurrentDirection(),
                aimer.GetSpawnPosition(),
                aimer.initialSpeed,
                aimer.trajectoryFormula
            );

            projectileStarter.StartProjectile(newSettings);
        }

        private void OnEnable()
        {
            InputReader.FireButtonReleasedEvent.AddListener(OnFireReleased);
        }

        private void OnDisable()
        {
            InputReader.FireButtonReleasedEvent.RemoveListener(OnFireReleased);
        }

        private void OnFireReleased()
        {
            Throw();
        }
    }
}