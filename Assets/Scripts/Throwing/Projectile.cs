using Throwing.Trajectory;
using UnityEngine;

namespace Throwing
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Vector3 direction;
        [SerializeField] private bool isMoving;

        private TrajectoryFormula _formula;
        private Vector3 _startPoint;
        private float _flyTime;
        private Transform _transform;

        public void SetUp(Vector3 direction, TrajectoryFormula formula)
        {
            this.direction = direction;
            _formula = formula;
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
                _transform.position =
                    _formula.GetPosition(direction,  _startPoint, _flyTime);
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