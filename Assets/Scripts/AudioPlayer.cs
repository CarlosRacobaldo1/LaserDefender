using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] AudioClip damageSFX;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;
    [Header("Explosion")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0f, 1f)] float explosionVolume = 1f;
    [Header("Shooting")]
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;
    static AudioPlayer instance;
    
    void Awake()
    {
        ManageSingleton();
    }

    void PlayClip(AudioClip audio, float volume)
    {
        if(audio != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(audio, cameraPos, volume);
        }
    }
   void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance =this;
            DontDestroyOnLoad(gameObject);
        }
    }

     public void PlayDamageClip()
    {
        PlayClip(damageSFX, damageVolume);
    }

    public void PlayExplosionClip()
    {
        PlayClip(explosionSFX, explosionVolume);
    }

   public void PlayShootingClip()
   {
        PlayClip(shootingSFX, shootingVolume);
   }
}
