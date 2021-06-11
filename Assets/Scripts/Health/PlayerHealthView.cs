using System;
using Fighting;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
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