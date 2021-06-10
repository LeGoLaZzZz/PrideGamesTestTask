using System.Linq;
using UnityEngine;

namespace Throwing
{
    public class ProjectileInventory : MonoBehaviour
    {
        public Projectile[] projectiles;

        //TODO
        public Projectile GetSelectedProjectile()
        {
            return projectiles.First();
        }
    }
}