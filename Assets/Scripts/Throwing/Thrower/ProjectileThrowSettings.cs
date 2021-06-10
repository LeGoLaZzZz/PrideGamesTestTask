using System;
using Throwing.Trajectory;
using UnityEngine;

namespace Throwing
{
    [Serializable]
    public class ProjectileThrowSettings
    {
        public Projectile prefab;
        public Vector3 direction;
        public Vector3 startPosition;
        public float initialSpeed;
        public TrajectoryFormula trajectoryFormula;

        public ProjectileThrowSettings(Projectile prefab, Vector3 direction, Vector3 startPosition, float initialSpeed, TrajectoryFormula trajectoryFormula)
        {
            this.prefab = prefab;
            this.direction = direction;
            this.startPosition = startPosition;
            this.initialSpeed = initialSpeed;
            this.trajectoryFormula = trajectoryFormula;
        }
    }
}