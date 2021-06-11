using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class WorldUiRotator : MonoBehaviour
    {
        [SerializeField] private Transform canvas;

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
            if (_isPlayerMarker) canvas.LookAt(_playerMarkerTransform);
        }
    }
}