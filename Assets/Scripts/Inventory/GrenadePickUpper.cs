using System;
using UnityEngine;

namespace Throwing
{
    public class GrenadePickUpper : MonoBehaviour
    {
        public Inventory inventory;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<GrenadePickUp>(out var pickUp))
            {
                inventory.AddGrenade(pickUp.grenadeConfig, pickUp.count);
                pickUp.PickUp();
            }
        }
    }
}