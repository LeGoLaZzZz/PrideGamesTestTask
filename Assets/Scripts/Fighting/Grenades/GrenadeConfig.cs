using UnityEngine;

namespace Throwing
{
    public abstract class GrenadeConfig : ScriptableObject, IProjectileProvider
    {
        public string title;

        [Multiline]
        public string description;
        
        public Color icon;

        public abstract Grenade GetGrenadePrefab();
        public abstract Grenade GetGrenadeObject();
        public abstract Projectile GetProjectileObject();
        public abstract Projectile GetProjectilePrefab();
    }

    public abstract class GrenadeConfig<T> : GrenadeConfig where T : Grenade
    {
        public T prefab;

        protected abstract T SetUpGrenade(T grenadeObject);

        public override Grenade GetGrenadeObject()
        {
            var grenadeObject = Instantiate(prefab);
            return SetUpGrenade(grenadeObject);
        }

        public override Grenade GetGrenadePrefab()
        {
            return prefab;
        }

        public override Projectile GetProjectileObject()
        {
            var grenadeObject = GetGrenadeObject();
            return grenadeObject.GetComponent<Projectile>();
        }       
        
        public override Projectile GetProjectilePrefab()
        {
            var grenadeObject = GetGrenadePrefab();
            return grenadeObject.GetComponent<Projectile>();
        }
    }
}