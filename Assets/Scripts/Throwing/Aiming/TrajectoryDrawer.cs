using UnityEngine;

namespace Throwing
{
    [ExecuteAlways]
    public class TrajectoryDrawer : MonoBehaviour
    {
        public int lineResolution;
        [SerializeField] private Aimer aimer;
        [SerializeField] private LineRenderer lineRenderer;


        private Vector3 _pointPosition;

        private void Update()
        {
            if (!aimer.isAiming)
            {
                lineRenderer.positionCount = 0;
                return;
            }
            
            lineRenderer.positionCount = lineResolution;
            
            for (int i = 0; i < lineResolution; i++)
            {
                _pointPosition = aimer.trajectoryFormula.GetPosition(
                    aimer.GetCurrentDirection(),
                    aimer.initialSpeed,
                    aimer.GetSpawnPosition(),
                    (float) i / lineResolution);

                lineRenderer.SetPosition(i, _pointPosition);
            }
        }
    }
}