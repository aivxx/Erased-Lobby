using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOnTrigger : MonoBehaviour
{

    [SerializeField] float fogDensity = 0.01f;
    private bool isSwimming = false;

    

    private void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.clear;
            
    }

    private void Update()
    {
        
        if(isSwimming == true)
        {
            RenderSettings.fogDensity = fogDensity;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            isSwimming = true;
            fogDensity = 0.05f;
            RenderSettings.fogColor = Color.green;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            isSwimming = false;
            fogDensity = 0.01f;
            RenderSettings.fogColor = Color.clear;
        }
    }
}
