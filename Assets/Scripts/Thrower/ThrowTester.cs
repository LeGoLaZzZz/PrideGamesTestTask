using UnityEngine;

namespace Thrower
{
    public class ThrowTester : MonoBehaviour
    {
        public Projectile prefab;
        public Thrower thrower;
        public Transform dirPoint;

        public void MakeHimThrow()
        {
            thrower.Throw(prefab, dirPoint.position - transform.position);
        }
    }
}