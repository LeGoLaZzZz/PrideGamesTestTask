using System;
using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    [Serializable]
    public class DeathEvent : UnityEvent<DeathEventArgs>
    {
    }


    [Serializable]
    public class DeathEventArgs
    {
        public Health health;

        public DeathEventArgs(Health health)
        {
            this.health = health;
        }
    }

    [Serializable]
    public class HealthChangedEvent : UnityEvent<HealthChangedEventArgs>
    {
    }

    [Serializable]
    public class HealthChangedEventArgs
    {
        public Health health;
        public float changeAmount;

        public HealthChangedEventArgs(Health health, float changeAmount)
        {
            this.health = health;
            this.changeAmount = changeAmount;
        }
    }

    public class Health : MonoBehaviour
    {
        public DeathEvent deathEvent = new DeathEvent();
        public HealthChangedEvent HealthChanged = new HealthChangedEvent();

        public bool destroyOnDeath = true;
        public float destroyDelay = 0.5f;


        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;


        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;

        public void ReduceHealth(float amount)
        {
            if (amount < 0)
            {
                throw new Exception("Cant reduce health by negative amount");
            }

            if (currentHealth - amount <= 0)
            {
                currentHealth = 0;
                Die();
            }
            else
            {
                currentHealth -= amount;
            }

            HealthChanged.Invoke(new HealthChangedEventArgs(this, -amount));
        }

        public void Die()
        {
            deathEvent.Invoke(new DeathEventArgs(this));
            if (destroyOnDeath)
            {
                Destroy(gameObject, destroyDelay);
            }
        }
    }
}