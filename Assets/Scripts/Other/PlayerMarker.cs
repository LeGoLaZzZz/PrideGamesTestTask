using UnityEngine;

namespace Other
{
    public class PlayerMarker : MonoBehaviour
    {
       [SerializeField] private Health.Health playerHealth;

       public Health.Health PlayerHealth => playerHealth;
    }
}