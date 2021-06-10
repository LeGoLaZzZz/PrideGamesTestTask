using Throwing.Trajectory;
using UnityEngine;

namespace Throwing
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Vector3 direction;
        [SerializeField] private bool isMoving;

        private float _speed;
        private TrajectoryFormula _formula;
        private Vector3 _startPoint;
        private float _flyTime;
        private Transform _transform;

        public void SetUp(Vector3 direction, float speed, TrajectoryFormula formula)
        {
            this.direction = direction;
            _formula = formula;
            _speed = speed;
            StartMovement();
        }


        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (isMoving)
            {
                _flyTime += Time.deltaTime;
                _transform.position = _formula.GetPosition(direction, _speed, _startPoint, _flyTime);
            }
        }

        private void StartMovement()
        {
            _flyTime = 0;
            _startPoint = _transform.position;
            isMoving = true;
        }
    }
}