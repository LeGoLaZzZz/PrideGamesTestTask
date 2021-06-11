using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class PickUpSpawner : MonoBehaviour
    {
        public GrenadePickUp[] pickUps;
        public float cooldown;
        public Transform spawnPoint;

        public GrenadePickUp spawned;
        public bool isSpawned;

        private float _lastCooldownTime;

        public void SpawnPickUp()
        {
            var randomPickUpPrefab = GetRandomPickUpPrefab();
            var grenadePickUp = Instantiate(randomPickUpPrefab);
            grenadePickUp.transform.position = spawnPoint.position;
            spawned = grenadePickUp;
            isSpawned = true;
            spawned.pickedUp.AddListener(OnPickedUp);
        }


        private void Start()
        {
            SpawnPickUp();
        }

        private void Update()
        {
            if (isSpawned) return;

            if (Time.time - _lastCooldownTime > cooldown)
            {
                SpawnPickUp();
                _lastCooldownTime = Time.time;
            }
        }

        private void OnPickedUp(PickUppedEventArgs arg0)
        {
            spawned.pickedUp.RemoveListener(OnPickedUp);
            spawned = null;
            isSpawned = false;
            _lastCooldownTime = Time.time;
        }

        private GrenadePickUp GetRandomPickUpPrefab()
        {
            return pickUps[Random.Range(0, pickUps.Length)];
        }
    }
}