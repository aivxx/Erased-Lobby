using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOnTrigger : MonoBehaviour
{

    public LightingSettings Water;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            Water = enabled;
        }
    }
}
