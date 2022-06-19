using DamageDealing;
using UnityEngine;

namespace Throwing.Thrower
{
    public abstract class ProjectileThrower : MonoBehaviour
    {
        public TeamConfig teamOwner;
        [SerializeField] protected ProjectileStarter projectileStarter;


        public abstract void Throw();


    }
}