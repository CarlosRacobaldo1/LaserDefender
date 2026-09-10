using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
   
    [SerializeField] int damage = 10;
    [SerializeField] bool canPierce;

    public int GetDamage()
    {
        return damage;
    }

    public void SetPierce(bool pierce)
    {
        canPierce = pierce;
    }

    public void Hit()
    {
        if (!canPierce)
        {
            Destroy(gameObject);
        }
        
    }
}
