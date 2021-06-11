namespace Throwing.Projectile
{
    public interface IProjectileProvider
    {
        public Projectile GetProjectileObject();
        public Projectile GetProjectilePrefab();
    }
}