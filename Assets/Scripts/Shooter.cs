using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFireRate = 0.2f;

    [Header("Power Ups")]
    [SerializeField] float doubleShotOffset = 0.3f;
    
    [Header("Enemy")]
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minFireRate = 0.1f;
    [SerializeField] bool isAI;

    [HideInInspector]public bool isFiring;
    [HideInInspector] public bool piercingActive;  
    [HideInInspector] public bool doubleShotActive; 

    AudioPlayer audioPlayer;
    Coroutine fireCoroutine;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if (isAI)
        {
           isFiring = true; 
        }
    }

    
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Time.timeScale == 0f)
        {
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
                fireCoroutine = null;
            }
            return;
        }

        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

   IEnumerator FireContinuously()
    {
        while (true)
        {
            if (Time.timeScale > 0f)
            {
                if (doubleShotActive)
                {
                    SpawnProjectile(transform.position + transform.right * doubleShotOffset);
                    SpawnProjectile(transform.position - transform.right * doubleShotOffset);
                }
                else
                {
                    SpawnProjectile(transform.position);
                }

                audioPlayer.PlayShootingClip();
            }

            float fireInterval = Random.Range(
                baseFireRate - fireRateVariance,
                baseFireRate + fireRateVariance
            );
            fireInterval = Mathf.Clamp(fireInterval, minFireRate, float.MaxValue);

            yield return new WaitForSeconds(fireInterval);
        }
    }

    void SpawnProjectile(Vector3 position)
    {
        if (Time.timeScale == 0f) return;

        GameObject instance = Instantiate(projectilePrefab, position, Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = transform.up * projectileSpeed;

        if (piercingActive)
        {
            DamageDealer damageDealer = instance.GetComponent<DamageDealer>();
            if (damageDealer != null) damageDealer.SetPierce(true);
        }

        Destroy(instance, projectileLifeTime);
    }
}
