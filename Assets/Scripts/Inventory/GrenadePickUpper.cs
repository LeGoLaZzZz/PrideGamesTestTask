using UnityEngine;

namespace Inventory
{
    public class GrenadePickUpper : MonoBehaviour
    {
        public Inventory inventory;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<GrenadePickUp>(out var pickUp))
            {
                if (pickUp.TryPickUp())
                {
                    inventory.AddGrenade(pickUp.grenadeConfig, pickUp.count);
                }
            }
        }
    }
}