using Fighting;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerMarker : MonoBehaviour
    {
       [SerializeField] private Health playerHealth;

       public Health PlayerHealth => playerHealth;
    }
}