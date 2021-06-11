using System;
using Fighting;
using Throwing.Thrower;
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
        public ThrowAimType throwAimType;
        public Vector3 endPosition;

        public ProjectileThrowSettings(IProjectileProvider projectileProvider, Vector3 direction, Vector3 startPosition,
            TrajectoryFormula trajectoryFormula, TeamConfig teamOwner)
        {
            this.projectileProvider = projectileProvider;
            this.direction = direction;
            this.startPosition = startPosition;
            this.trajectoryFormula = trajectoryFormula;
            this.teamOwner = teamOwner;

            throwAimType = ThrowAimType.ByAngle;
        }

        public ProjectileThrowSettings(IProjectileProvider projectileProvider, Vector3 startPosition,
            TrajectoryFormula trajectoryFormula, TeamConfig teamOwner, Vector3 endPosition)
        {
            this.projectileProvider = projectileProvider;
            this.startPosition = startPosition;
            this.trajectoryFormula = trajectoryFormula;
            this.teamOwner = teamOwner;
            this.endPosition = endPosition;
            
            throwAimType = ThrowAimType.ByTarget;
        }
    }
}