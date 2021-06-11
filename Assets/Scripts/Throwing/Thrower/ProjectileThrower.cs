using Fighting;
using PlayerInput;
using UnityEngine;

namespace Throwing
{
    public abstract class ProjectileThrower : MonoBehaviour
    {
        public TeamConfig teamOwner;
        [SerializeField] protected ProjectileStarter projectileStarter;


        public abstract void Throw();


    }
}