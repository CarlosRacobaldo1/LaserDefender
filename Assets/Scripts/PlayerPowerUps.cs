using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Shield,
    PiercingShot,
    Heal,
    DoubleShot
}

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Shooter))]

public class PlayerPowerUps : MonoBehaviour
{
    [Header("Durações")]
    [SerializeField] float piercingDuration = 8f;
    [SerializeField] float doubleShotDuration = 8f;

    [Header("Cura")]
    [SerializeField] int healAmount = 20;

    Health health;
    Shooter shooter;

    Coroutine piercingRoutine;
    Coroutine doubleShotRoutine;

    void Awake()
    {
        health = GetComponent<Health>();
        shooter = GetComponent<Shooter>();
    }

    public void ActivatePowerUp(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Shield:
                health.ActivateShield();
                break;

            case PowerUpType.PiercingShot:
                if (piercingRoutine != null)
                {
                    StopCoroutine(piercingRoutine);
                } 
                piercingRoutine = StartCoroutine(PiercingShotRoutine());
                break;

            case PowerUpType.Heal:
                health.Heal(healAmount);
                break;

            case PowerUpType.DoubleShot:
                if (doubleShotRoutine != null)
                {
                    StopCoroutine(doubleShotRoutine);
                } 
                doubleShotRoutine = StartCoroutine(DoubleShotRoutine());
                break;
        }
    }

    IEnumerator PiercingShotRoutine()
    {
        shooter.piercingActive = true;
        yield return new WaitForSeconds(piercingDuration);
        shooter.piercingActive = false;
        piercingRoutine = null;
    }

    IEnumerator DoubleShotRoutine()
    {
        shooter.doubleShotActive = true;
        yield return new WaitForSeconds(doubleShotDuration);
        shooter.doubleShotActive = false;
        doubleShotRoutine = null;
    }
}
