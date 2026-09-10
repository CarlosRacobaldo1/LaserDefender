using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType type;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPowerUps playerPowerUps = other.GetComponent<PlayerPowerUps>();
        if (playerPowerUps == null) return;

        playerPowerUps.ActivatePowerUp(type);
        Destroy(gameObject);
    }
}
