using System;
using UnityEngine;

namespace Fighting
{
    [CreateAssetMenu(fileName = "TeamConfig", menuName = "Fighting/TeamConfig", order = 0)]
    public class TeamConfig : ScriptableObject
    {
        public Team team;
        public Team enemyTeams;

        public bool CanFight(TeamConfig other)
        {
            return CanFight((int) other.team);
        }

        public bool CanFight(int other)
        {
            return (int) enemyTeams == ((int) enemyTeams | (1 << other));
        }
    }
}