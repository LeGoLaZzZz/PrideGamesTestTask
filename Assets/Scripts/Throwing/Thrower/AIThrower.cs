using Grenades;
using Other;
using Throwing.Trajectory;
using UnityEngine;

namespace Throwing.Thrower
{
    public class AIThrower : ProjectileThrower
    {
        [SerializeField] private GrenadeConfig grenadeConfig;
        [SerializeField] private TrajectoryFormula trajectory;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform directionPoint;
        [SerializeField] private float cooldown;
        private float _lastCooldownTime;

        private Transform _playerTransform;

        private void Start()
        {
            var playerMarker = FindObjectOfType<PlayerMarker>();
            if (playerMarker != null) _playerTransform = playerMarker.transform;
        }

        private void Update()
        {
            if (!_playerTransform) return;

            if (Time.time - _lastCooldownTime > cooldown)
            {
                Throw();
                _lastCooldownTime = Time.time;
            }
        }

        public override void Throw()
        {
            var settings = new ProjectileThrowSettings(grenadeConfig, spawnPosition.position,
                trajectory, teamOwner, _playerTransform.position);

            projectileStarter.StartProjectile(settings);
        }


        private Vector3 GetDirection()
        {
            return directionPoint.forward;
        }
    }
}