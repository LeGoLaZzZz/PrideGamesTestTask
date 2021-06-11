using System;
using Fighting;
using Throwing.Trajectory;
using UnityEngine;

namespace Throwing
{
    [Serializable]
    public class ProjectileThrowSettings
    {
        public IProjectileProvider projectileProvider;
        public Vector3 direction;
        public Vector3 startPosition;
        public TrajectoryFormula trajectoryFormula;
        public TeamConfig teamOwner;

        public ProjectileThrowSettings(IProjectileProvider projectileProvider, Vector3 direction, Vector3 startPosition, TrajectoryFormula trajectoryFormula, TeamConfig teamOwner)
        {
            this.projectileProvider = projectileProvider;
            this.direction = direction;
            this.startPosition = startPosition;
            this.trajectoryFormula = trajectoryFormula;
            this.teamOwner = teamOwner;
        }
    }
}