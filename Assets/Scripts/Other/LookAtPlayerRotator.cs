using UnityEngine;

namespace Other
{
    public class LookAtPlayerRotator : MonoBehaviour
    {
        [SerializeField] private Transform rotationTransform;

        private Transform _playerMarkerTransform;
        private bool _isPlayerMarker;

        private void Start()
        {
            var playerMarker = FindObjectOfType<PlayerMarker>();
            _isPlayerMarker = playerMarker != null;
            if (_isPlayerMarker) _playerMarkerTransform = playerMarker.transform;
        }

        private void Update()
        {
            if (_isPlayerMarker) rotationTransform.LookAt(_playerMarkerTransform);
        }
    }
}