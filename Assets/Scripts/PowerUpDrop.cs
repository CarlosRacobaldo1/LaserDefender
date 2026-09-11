using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDrop : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private float dropChance;
    [SerializeField] private float powerUpLifeTime = 5f;
    
    [SerializeField] private PowerUp[] powerUps;


    public void DropPowerUp(Vector3 position)
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= dropChance)
        {
            int randomIndex = Random.Range(0, powerUps.Length);
            PowerUp selectedPowerUp = powerUps[randomIndex];
            GameObject powerUpInstance = Instantiate(selectedPowerUp.gameObject, position, Quaternion.identity);
            Destroy(powerUpInstance, powerUpLifeTime);
        }
    }
}
