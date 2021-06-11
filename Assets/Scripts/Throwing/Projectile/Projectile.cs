using System;
using Fighting;
using Throwing.Thrower;
using Throwing.Trajectory;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using static Throwing.ProjectileThrowFinishedEventArgs;

namespace Throwing
{
    [Serializable]
    public class ProjectileThrowFinishedEvent : UnityEvent<ProjectileThrowFinishedEventArgs>
    {
    }

    [Serializable]
    public class ProjectileThrowFinishedEventArgs
    {
        public Projectile projectile;
        public ThrowFinishReason reason;

        public ProjectileThrowFinishedEventArgs(Projectile projectile, ThrowFinishReason reason)
        {
            this.projectile = projectile;
            this.reason = reason;
        }

        public enum ThrowFinishReason
        {
            TimePassed,
            ObstacleHit
        }
    }

    [Serializable]
    public class ProjectileThrowStartedEvent : UnityEvent<ProjectileThrowStartedEventArgs>
    {
    }

    public class ProjectileThrowStartedEventArgs
    {
    }


    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        public ProjectileThrowFinishedEvent projectileThrowFinished = new ProjectileThrowFinishedEvent();
        public ProjectileThrowStartedEvent projectileThrowStarted = new ProjectileThrowStartedEvent();

        [Header("Settings")]
        public LayerMask obstaclesLayers;
        public float collisionOverlapSphereRadius = 0.5f;
        public float maxFlyTime;

        [Header("Monitoring")]
        [SerializeField] private Vector3 direction;
        [SerializeField] private bool isMoving;
        [SerializeField] private Collider projectileCollider;
        [SerializeField] private TeamConfig teamOwner;

        private TrajectoryFormula _formula;
        private ThrowAimType _throwAimType;
        private Vector3 _startPoint;
        private Vector3 _endPoint;
        private float _flyTime;
        private Transform _transform;

        public TeamConfig TeamOwner => teamOwner;
        public Collider ProjectileCollider => projectileCollider;
        public float MaxFlyTime => maxFlyTime;

        public void MovementByAngleSetUp(Vector3 direction, TrajectoryFormula formula)
        {
            _throwAimType = ThrowAimType.ByAngle;
            this.direction = direction;
            _formula = formula;
            StartMovement();
        }

        public void MovementByTargetSetUp(Vector3 endPoint, TrajectoryFormula formula)
        {
            _throwAimType = ThrowAimType.ByTarget;
            _endPoint = endPoint;
            _formula = formula;
            StartMovement();
        }

        public void SetOwnerTeam(TeamConfig teamConfig)
        {
            teamOwner = teamConfig;
        }

        public void EndThrow(ThrowFinishReason reason)
        {
            isMoving = false;
            projectileThrowFinished.Invoke(new ProjectileThrowFinishedEventArgs(this, reason));
        }

        private void Awake()
        {
            isMoving = false;
            _transform = transform;
            projectileCollider = GetComponent<Collider>();
        }

        private void Update()
        {
            if (isMoving)
            {
                _flyTime += Time.deltaTime;

                if (_throwAimType == ThrowAimType.ByAngle)
                {
                    _transform.position = _formula.GetPositionByAngle(direction, _startPoint, _flyTime);
                }
                else
                {
                    _transform.position = _formula.GetPositionByTarget(_endPoint, _startPoint, _flyTime);
                }

                if (maxFlyTime <= _flyTime)
                {
                    EndThrow(ThrowFinishReason.TimePassed);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (obstaclesLayers == (obstaclesLayers | (1 << other.gameObject.layer)))
            {
                OnObstacleHit(other);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(255, 0, 0, 0.3f);
            Gizmos.DrawWireSphere(transform.position, collisionOverlapSphereRadius);
        }

        private void OnObstacleHit(Collider obstacle)
        {
            EndThrow(ThrowFinishReason.ObstacleHit);
        }

        private void StartMovement()
        {
            _flyTime = 0;
            _startPoint = _transform.position;
            isMoving = true;
            projectileThrowStarted.Invoke(new ProjectileThrowStartedEventArgs());
        }
    }
}