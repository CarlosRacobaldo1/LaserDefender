using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public SpriteRenderer sp;

    // Update is called once per frame
    void Update()
    {
        if(sp != null)
        {
            sp.enabled = !sp.enabled;
        }
    }
}
