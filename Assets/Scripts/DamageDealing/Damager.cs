using UnityEngine;

namespace Fighting
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private TeamConfig team;
        public TeamConfig Team => team;

        public void Damage(float rawDamage, DamageReceiver receiver)
        {
            Damage(Team, rawDamage, receiver);
        }

        public void Damage(TeamConfig teamOwner, float rawDamage, DamageReceiver receiver)
        {
            var bundle = new DamageBundle(teamOwner, rawDamage, this);
            receiver.GetDamage(bundle);
        }

        public void SetTeam(TeamConfig teamConfig)
        {
            team = teamConfig;
        }
    }
}