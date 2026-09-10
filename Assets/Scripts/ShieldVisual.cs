using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShieldVisual : MonoBehaviour
{
     [SerializeField] Health health;
     SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (health == null)
            health = GetComponentInParent<Health>();

        spriteRenderer = GetComponent<SpriteRenderer>(); 
        spriteRenderer.enabled = false; 
    }

    void OnEnable()
    {
        if (health != null)
            health.OnShieldStateChanged += HandleShieldStateChanged;
    }

    void OnDisable()
    {
        if (health != null)
            health.OnShieldStateChanged -= HandleShieldStateChanged;
    }

    void HandleShieldStateChanged(bool isActive)
    {
        spriteRenderer.enabled = isActive;
    }
}
