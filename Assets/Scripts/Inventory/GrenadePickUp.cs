using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Throwing
{
    [Serializable]
    public class PickedUpEvent : UnityEvent<PickUppedEventArgs>
    {
    }

    [Serializable]
    public class PickUppedEventArgs
    {
    }


    public class GrenadePickUp : MonoBehaviour
    {
        [Header("Grenade settings")]
        public GrenadeConfig grenadeConfig;
        public int count = 1;
        public float destroyDelay = 0.1f;

        [Header("View")]
        public string textFormat = "{0} x{1}";
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Canvas canvas;

        public PickedUpEvent pickedUp = new PickedUpEvent();

        private Transform _camera;

        private void Awake()
        {
            _camera = Camera.main.transform;
        }

        private void Start()
        {
            titleText.text = string.Format(textFormat, grenadeConfig.title, count);
        }

        private void Update()
        {
            canvas.transform.LookAt(_camera);
        }

        public void PickUp()
        {
            //Cool view, animations, particles
            pickedUp.Invoke(new PickUppedEventArgs());
            Destroy(gameObject, destroyDelay);
        }
    }
}