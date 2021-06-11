using Fighting;
using PlayerInput;
using UnityEngine;

namespace Throwing
{
    public class ProjectileThrower : MonoBehaviour
    {
        public TeamConfig teamOwner;
        [SerializeField] private ProjectileStarter projectileStarter;
        [SerializeField] private Aimer aimer;
        [SerializeField] private Inventory inventory;


        public void Throw()
        {

            if (!inventory.TryTakeSelected(out var provider)) return;
            
            var newSettings = new ProjectileThrowSettings(
                provider,
                aimer.GetCurrentDirection(),
                aimer.GetSpawnPosition(),
                aimer.trajectoryFormula,
                teamOwner
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