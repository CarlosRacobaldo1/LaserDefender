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
    
     [Header("Enemy")]
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minFireRate = 0.1f;
    [SerializeField] bool isAI;

    [HideInInspector]public bool isFiring;

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
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && fireCoroutine !=null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
        
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifeTime);
           
            float fireInterval= Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
            fireInterval = Mathf.Clamp(fireInterval, minFireRate, float.MaxValue);
            
            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(fireInterval);
        }
        
    }
}
