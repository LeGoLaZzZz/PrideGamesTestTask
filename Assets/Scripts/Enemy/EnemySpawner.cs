using Health;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Settings")]
        public float cooldown;
        public GameObject enemyPrefab;
        
        [Header("Links")]
        public Transform spawnPoint;
        
        [Header("Monitoring")]
        [SerializeField] private GameObject spawned;
        [SerializeField] private bool isSpawned;

        private float _lastCooldownTime;

        public void Spawn()
        {
            var enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnPoint.position;
            spawned = enemy;
            isSpawned = true;
            spawned.GetComponent<Health.Health>().deathEvent.AddListener(OnDeath);
        }


        private void Start()
        {
            Spawn();
        }

        private void Update()
        {
            if (isSpawned) return;

            if (Time.time - _lastCooldownTime > cooldown)
            {
                Spawn();
                _lastCooldownTime = Time.time;
            }
        }

        private void OnDeath(DeathEventArgs arg0)
        {
            spawned.GetComponent<Health.Health>().deathEvent.RemoveListener(OnDeath);
            spawned = null;
            isSpawned = false;
            _lastCooldownTime = Time.time;
        }
    }
}