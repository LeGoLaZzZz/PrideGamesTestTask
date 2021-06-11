using UnityEngine;

namespace DamageDealing
{
    [CreateAssetMenu(fileName = "TeamConfig", menuName = "Fighting/TeamConfig", order = 0)]
    public class TeamConfig : ScriptableObject
    {
        public Team team;
        public Team enemyTeams;

        public bool CanFight(TeamConfig other)
        {
            return CanFight(other.team);
        }

        public bool CanFight(Team other)
        {
            return enemyTeams.HasFlag(other);
        }
    }
}