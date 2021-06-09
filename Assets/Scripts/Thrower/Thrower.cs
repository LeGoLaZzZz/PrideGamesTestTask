using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thrower
{
    public class Thrower : MonoBehaviour
    {
        public TrajectoryFormula trajectoryFormula;

        [SerializeField] private Transform projectileSpawnPoint;

        public void Throw(Projectile projectilePrefab, Vector3 direction)
        {
            var projectile = InstantiateProjectile(projectilePrefab);
            projectile.SetUp(direction.normalized, trajectoryFormula);
        }


        private Projectile InstantiateProjectile(Projectile projectilePrefab)
        {
            var newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = projectileSpawnPoint.position;
            return newProjectile;
        }
    }
}