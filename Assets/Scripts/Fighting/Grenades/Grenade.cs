using System;
using Fighting;
using UnityEngine;
using UnityEngine.Events;

namespace Throwing
{
    [Serializable]
    public class GrenadeBlewUpEvent : UnityEvent<GrenadeBlewUpEventArgs>
    {
    }

    [Serializable]
    public class GrenadeBlewUpEventArgs
    {
        public Grenade grenade;

        public GrenadeBlewUpEventArgs(Grenade grenade)
        {
            this.grenade = grenade;
        }
    }


    [RequireComponent(typeof(Projectile))]
    public abstract class Grenade : MonoBehaviour
    {
        [SerializeField] private ParticleSystem blowUpParticles;
        protected Projectile Projectile;

        public GrenadeBlewUpEvent grenadeBlewUpEvent = new GrenadeBlewUpEvent();

        protected abstract void BlowUpAction(Vector3 blowUpPoint);
        
        public TeamConfig TeamOwner => Projectile.TeamOwner;

        public void BlowUp()
        {
            BlowUpAction(transform.position);
            ViewBlowUp(transform.position);


            grenadeBlewUpEvent.Invoke(new GrenadeBlewUpEventArgs(this));
            Destroy(gameObject, 0.05f);
        }


        protected virtual void Awake()
        {
            Projectile = GetComponent<Projectile>();
        }

        private void OnEnable()
        {
            Projectile.projectileThrowFinished.AddListener(OnFlightFinished);
            Projectile.projectileThrowStarted.AddListener(OnFlightStart);
        }

        private void OnDisable()
        {
            Projectile.projectileThrowFinished.RemoveListener(OnFlightFinished);
            Projectile.projectileThrowStarted.RemoveListener(OnFlightStart);
        }

        protected virtual void OnFlightStart(ProjectileThrowStartedEventArgs arg0)
        {
        }

        protected virtual void OnFlightFinished(ProjectileThrowFinishedEventArgs arg0)
        {
            BlowUp();
        }

        private void ViewBlowUp(Vector3 blowUpPoint) //TODO view take out
        {
            var particle = Instantiate(blowUpParticles);
            particle.transform.position = blowUpPoint;
        }
    }
}