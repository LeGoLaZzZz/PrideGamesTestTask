using Other;

namespace Health
{
    public class PlayerHealthView : HealthView
    {
        private void Awake()
        {
            var playerMarker = FindObjectOfType<PlayerMarker>();
            if (playerMarker)
            {
                   health = playerMarker.PlayerHealth;
            }
        }

    }
}