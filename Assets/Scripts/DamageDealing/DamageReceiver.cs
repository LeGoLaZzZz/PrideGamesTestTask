using UnityEngine;

namespace DamageDealing
{
    [RequireComponent(typeof(Health.Health))]
    public class DamageReceiver : MonoBehaviour
    {
        [SerializeField] private TeamConfig team;

        public TeamConfig Team => team;
        
        private Health.Health _health;
        
        
        public void GetDamage(DamageBundle damageBundle)
        {
           
            if (Team.CanFight(damageBundle.teamSource))
            { 
                _health.ReduceHealth(damageBundle.rawDamage);
            }
        }

        private void Awake()
        {
            _health = GetComponent<Health.Health>();
        }
    }
}