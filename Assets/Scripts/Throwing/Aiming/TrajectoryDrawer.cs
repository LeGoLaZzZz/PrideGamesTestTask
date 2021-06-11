using System;
using Throwing.Thrower;
using Throwing.Trajectory;
using UnityEditor;
using UnityEngine;

namespace Throwing
{
    [ExecuteAlways]
    public abstract class TrajectoryDrawer : MonoBehaviour
    {
        [Header("Settings")]
        public int maxLineResolution;
        public float timeBetweenPoints = 0.1f;
        public LayerMask hitLayerMask;
        [Header("Links")]
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private GameObject mark;

        [Header("Monitor")]
        [SerializeField] private bool isWallColliding;
        [Header("Debug")]
        [SerializeField] private bool debugAim;


        private Vector3 _pointPosition;
        private float _curTimePoint;
        private RaycastHit _wallHit;
        private Collider[] _collisions;
        private int _maxCollisions = 10;


        protected abstract ThrowAimType ThrowAimType(out Vector3 targetPoint);
        protected abstract Vector3 GetDirection();
        protected abstract Vector3 GetStartPosition();
        protected abstract TrajectoryFormula GetTrajectoryFormula();
        protected abstract Projectile GetProjectile();
        protected abstract bool NeedDrawTrajectory();

        private void Awake()
        {
            _collisions = new Collider[_maxCollisions];
        }

        private void Start()
        {
            lineRenderer.positionCount = 0;
            HideMark();
        }

        private void Update()
        {
            if (debugAim) DrawTrajectory();
            else
            {
                if (!NeedDrawTrajectory())
                {
                    lineRenderer.positionCount = 0;
                    HideMark();
                    return;
                }

                DrawTrajectory();
            }
        }

        private void OnDrawGizmos()
        {
            if (!NeedDrawTrajectory()) return;
            Gizmos.color = new Color(0.11f, 0.75f, 0.2f, 0.4f);

            for (var i = 0; i < lineRenderer.positionCount; i++)
            {
                Gizmos.DrawSphere(lineRenderer.GetPosition(i),
                    GetProjectile().collisionOverlapSphereRadius);
            }
        }

        private void DrawTrajectory()
        {
            _curTimePoint = 0;
            lineRenderer.positionCount = maxLineResolution;

            for (int i = 0; i < maxLineResolution; i++)
            {
                DrawPoint(i, _curTimePoint);
                _curTimePoint += timeBetweenPoints;

                if (TryDrawMark(i) || CheckEndMaxFlyTime(_curTimePoint))
                {
                    lineRenderer.positionCount = i + 1;
                    break;
                }
            }
        }

        private bool CheckEndMaxFlyTime(float curTimePoint)
        {
            return GetProjectile().MaxFlyTime <= curTimePoint;
        }

        private bool TryDrawMark(int pointIndex)
        {
            isWallColliding = CheckWallRaycast(pointIndex, GetProjectile(), out _wallHit) && pointIndex > 0;

            if (!isWallColliding)
            {
                isWallColliding =
                    CheckWallOverlap(pointIndex, GetProjectile(), out _wallHit);
            }

            if (isWallColliding) DrawMark(_wallHit.point, _wallHit.point + _wallHit.normal);
            else HideMark();

            return isWallColliding;
        }


        private void DrawPoint(int pointIndex, float timeMoment)
        {
            if (ThrowAimType(out var targetPoint)==Thrower.ThrowAimType.ByTarget)
            {
                _pointPosition = GetTrajectoryFormula().GetPositionByTarget(
                    targetPoint,
                    GetStartPosition(),
                    timeMoment);
            }
            else
            {
                _pointPosition = GetTrajectoryFormula().GetPositionByAngle(
                    GetDirection(),
                    GetStartPosition(),
                    timeMoment);
            }


            lineRenderer.SetPosition(pointIndex, _pointPosition);
        }

        private bool CheckWallRaycast(int pointIndex, Projectile projectile, out RaycastHit wallHit)
        {
            if (pointIndex <= 0)
            {
                wallHit = default;
                return false;
            }

            var curPoint = lineRenderer.GetPosition(pointIndex);
            var prevPoint = lineRenderer.GetPosition(pointIndex - 1);
            var dir = curPoint - prevPoint;

            return Physics.SphereCast(prevPoint, projectile.collisionOverlapSphereRadius, dir, out wallHit,
                dir.magnitude, hitLayerMask); //true if wall
        }


        private bool CheckWallOverlap(int pointIndex, Projectile projectile, out RaycastHit wallHit)
        {
            var point = lineRenderer.GetPosition(pointIndex);
            var size = Physics.OverlapSphereNonAlloc(point, projectile.collisionOverlapSphereRadius, _collisions,
                hitLayerMask);

            wallHit = default;

            if (size > 0)
            {
                var isHit = FindWallHit(pointIndex, _collisions[0], out wallHit);
                if (isHit) return true;
            }

            return false;
        }

        private bool FindWallHit(int pointIndex, Collider wall, out RaycastHit hit)
        {
            Vector3 closestWallPoint;
            Vector3 linePoint;

            for (int i = pointIndex; i >= 0; i--)
            {
                linePoint = lineRenderer.GetPosition(i);
                closestWallPoint = wall.ClosestPoint(linePoint);


                if (Physics.Raycast(linePoint, closestWallPoint - linePoint, out hit, hitLayerMask))
                {
                    return true;
                }
            }

            hit = default(RaycastHit);
            return false;
        }


        private void DrawMark(Vector3 position, Vector3 looAt)
        {
            mark.SetActive(true);
            mark.transform.position = position;
            mark.transform.LookAt(looAt);
        }

        private void HideMark()
        {
            mark.SetActive(false);
        }
    }
}