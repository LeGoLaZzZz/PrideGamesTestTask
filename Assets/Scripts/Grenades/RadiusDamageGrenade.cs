using Fighting;
using UnityEngine;

namespace Throwing
{
    [RequireComponent(typeof(Projectile))]
    [RequireComponent(typeof(Damager))]
    public class RadiusDamageGrenade : Grenade
    {
        [Header("Monitoring")]
        public float damage;
        public float radius;

        private Damager _damager;

        protected override void Awake()
        {
            base.Awake();
            _damager = GetComponent<Damager>();
        }

        protected override void BlowUpAction(Vector3 blowUpPoint)
        {
            var colliders = Physics.OverlapSphere(blowUpPoint, radius);
            foreach (var colllider in colliders)
            {
                if (colllider.gameObject.TryGetComponent<DamageReceiver>(out var receiver))
                {
                    _damager.Damage(Projectile.TeamOwner, damage, receiver);
                }
            }
        }
    }
}