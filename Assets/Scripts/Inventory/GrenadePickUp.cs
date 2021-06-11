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


        public PickedUpEvent pickedUp = new PickedUpEvent();

        private bool _isPickedUp = false;

        private void Start()
        {
            titleText.text = string.Format(textFormat, grenadeConfig.title, count);
        }


        public bool TryPickUp()
        {
            if (_isPickedUp) return false;
            //Cool view, animations, particles
            pickedUp.Invoke(new PickUppedEventArgs());
            _isPickedUp = true;
            Destroy(gameObject, destroyDelay);
            return true;
        }
    }
}