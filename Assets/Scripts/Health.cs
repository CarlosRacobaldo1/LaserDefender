using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHp = 50;
    int currentHp;
    [SerializeField] int points = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool isPlayer;
    bool hasShield;
    public event Action<bool> OnShieldStateChanged;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        currentHp = maxHp;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer == null) return;

        if (hasShield)
        {
            hasShield = false;
            OnShieldStateChanged?.Invoke(false);
            damageDealer.Hit();
            return;
        }

        TakeDamage(damageDealer.GetDamage());
        PlayHitEffect();
        audioPlayer.PlayDamageClip();
        ShakeCamera();
        damageDealer.Hit();
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
           Death();
        }
    }

    public void Heal(int amount)
    {
        currentHp = Mathf.Min(currentHp + amount, maxHp);
    }

    void Death()
    {   
        if (!isPlayer)
        {
            scoreKeeper.AddScore(points);
        }
        else{ levelManager.GameOver(); }
         Destroy(gameObject);
         audioPlayer.PlayExplosionClip();
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public void ActivateShield()
    {
        hasShield = true;
        OnShieldStateChanged?.Invoke(true);
    }

    void ShakeCamera()
    {
        if(cameraShake!=null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return currentHp;
    }
    public int GetMaxHealth() 
    { 
        return maxHp; 
    }
}
