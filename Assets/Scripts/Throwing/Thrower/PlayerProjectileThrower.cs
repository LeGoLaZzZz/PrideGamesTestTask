using PlayerInput;
using Throwing.Aiming;
using UnityEngine;

namespace Throwing.Thrower
{
    public class PlayerProjectileThrower : ProjectileThrower
    {
        [SerializeField] private Aimer aimer;
        [SerializeField] private Inventory.Inventory inventory;

        public override void Throw()
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