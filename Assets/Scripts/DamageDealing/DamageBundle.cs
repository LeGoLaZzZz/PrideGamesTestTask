using System;

namespace DamageDealing
{
    [Serializable]
    public class DamageBundle
    {
        public TeamConfig teamSource;
        public float rawDamage;
        public Damager damager;

        public DamageBundle(TeamConfig teamSource, float rawDamage)
        {
            this.teamSource = teamSource;
            this.rawDamage = rawDamage;
        }

        public DamageBundle(TeamConfig teamSource, float rawDamage, Damager damager)
        {
            this.teamSource = teamSource;
            this.rawDamage = rawDamage;
            this.damager = damager;
        }
    }
}