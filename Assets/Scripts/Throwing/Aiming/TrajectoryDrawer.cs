using System;
using UnityEditor;
using UnityEngine;

namespace Throwing
{
    [ExecuteAlways]
    public class TrajectoryDrawer : MonoBehaviour
    {
        [Header("Settings")]
        public int maxLineResolution;
        public float overlapSphereRadius = 0.1f;
        public float timeBetweenPoints = 0.1f;
        public LayerMask hitLayerMask;
        [Header("Links")]
        [SerializeField] private Aimer aimer;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private GameObject mark;

        [Header("Monitor")]
        [SerializeField] private bool isWallColliding;


        private Vector3 _pointPosition;
        private float _curTimePoint;
        private RaycastHit _wallHit;

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
            _curTimePoint = 0;
            lineRenderer.positionCount = maxLineResolution;

            for (int i = 0; i < maxLineResolution; i++)
            {
                DrawPoint(i, _curTimePoint);
                _curTimePoint += timeBetweenPoints;

                if (TryDrawMark(i))
                {
                    lineRenderer.positionCount = i + 1;
                    break;
                }
            }
        }

        private bool TryDrawMark(int pointIndex)
        {
            isWallColliding = CheckWallRaycast(pointIndex, out _wallHit) && pointIndex > 0;
            if (isWallColliding) DrawMark(_wallHit.point, _wallHit.point + _wallHit.normal);
            else HideMark();

            return isWallColliding;
        }


        private void DrawPoint(int pointIndex, float timeMoment)
        {
            _pointPosition = aimer.trajectoryFormula.GetPosition(
                aimer.GetCurrentDirection(),
                aimer.GetSpawnPosition(),
                timeMoment);

            lineRenderer.SetPosition(pointIndex, _pointPosition);
        }

        private bool CheckWallRaycast(int pointIndex, out RaycastHit wallHit)
        {
            if (pointIndex <= 0)
            {
                wallHit = default;
                return false;
            }

            var curPoint = lineRenderer.GetPosition(pointIndex);
            var prevPoint = lineRenderer.GetPosition(pointIndex - 1);
            var dir = curPoint - prevPoint;

            return Physics.Raycast(prevPoint, dir, out wallHit, dir.magnitude, hitLayerMask); //true if wall
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