using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Health
{
    public class HealthView : MonoBehaviour
    {
        [Header("Links")]
        public Health health;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Pop ups")]
        public Vector3 spawnRandomRange;
        [SerializeField] private PopUpHealthNumber popUpHealthNumberPrefab;
        [SerializeField] private RectTransform popUpParent;

        private void OnEnable()
        {
            health.HealthChanged.AddListener(OnHealthChanged);
        }

        protected virtual void Start()
        {
            UpdateHealth();
        }

        private void OnDisable()
        {
            health.HealthChanged.RemoveListener(OnHealthChanged);
        }

        private void OnHealthChanged(HealthChangedEventArgs arg0)
        {
            UpdateHealth();
            SpawnPopUp(arg0.changeAmount);
        }

        private void UpdateHealth()
        {
            healthText.text = health.CurrentHealth.ToString();
        }

        private void SpawnPopUp(float amount)
        {
            var popUpHealthNumber = Instantiate(popUpHealthNumberPrefab, popUpParent, false);
            popUpHealthNumber.transform.position = transform.position + new Vector3(
                Random.Range(-spawnRandomRange.x, spawnRandomRange.x),
                Random.Range(-spawnRandomRange.y, spawnRandomRange.y),
                Random.Range(-spawnRandomRange.z, spawnRandomRange.z)
            );
            popUpHealthNumber.SetUp(amount);
        }
    }
}