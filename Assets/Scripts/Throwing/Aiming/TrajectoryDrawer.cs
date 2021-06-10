using System;
using UnityEditor;
using UnityEngine;

namespace Throwing
{
    [ExecuteAlways]
    public class TrajectoryDrawer : MonoBehaviour
    {
        [Header("Settings")]
        public int lineResolution;
        public float overlapSphereRadius = 0.1f;
        public LayerMask hitLayerMask;
        [Header("Links")]
        [SerializeField] private Aimer aimer;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private GameObject mark;

        [Header("Monitor")]
        [SerializeField] private bool isWallColliding;


        private Vector3 _pointPosition;

        private void Update()
        {
            if (!aimer.isAiming)
            {
                lineRenderer.positionCount = 0;
                HideMark();
                return;
            }

            DrawTrajectory();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.11f, 0.75f, 0.2f, 0.4f);
            Gizmos.DrawSphere(_pointPosition, overlapSphereRadius);
        }

        private void DrawTrajectory()
        {
            lineRenderer.positionCount = lineResolution;

            for (int i = 0; i < lineResolution; i++)
            {
                _pointPosition = aimer.trajectoryFormula.GetPosition(
                    aimer.GetCurrentDirection(),
                    aimer.initialSpeed,
                    aimer.GetSpawnPosition(),
                    (float) i / lineResolution);

                lineRenderer.SetPosition(i, _pointPosition);

                isWallColliding = CheckWall(i) && i > 0;
                if (isWallColliding)
                {
                    lineRenderer.positionCount = i + 1;
                    break;
                }
                else
                {
                    HideMark();
                }
            }
        }

        private bool CheckWall(int pointIndex)
        {
            var point = lineRenderer.GetPosition(pointIndex);
            var colliders = Physics.OverlapSphere(point, overlapSphereRadius, hitLayerMask);

            if (colliders.Length > 0)
            {
                var isHit = FindWallHit(pointIndex, colliders[0], out var hit);
                if (isHit)
                    DrawMark(hit.point, hit.point + hit.normal);
            }

            return colliders.Length > 0;
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