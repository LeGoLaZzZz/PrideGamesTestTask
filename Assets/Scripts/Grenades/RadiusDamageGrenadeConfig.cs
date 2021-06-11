using UnityEngine;

namespace Grenades
{
    [CreateAssetMenu(fileName = "RadiusDamageGrenadeConfig", menuName = "Fighting/RadiusDamageGrenadeConfig",
        order = 0)]
    public class RadiusDamageGrenadeConfig : GrenadeConfig<RadiusDamageGrenade>
    {
        public float damage;
        public float radius;


        protected override RadiusDamageGrenade SetUpGrenade(RadiusDamageGrenade grenadeObject)
        {
            grenadeObject.damage = damage;
            grenadeObject.radius = radius;

            return grenadeObject;
        }
    }
}