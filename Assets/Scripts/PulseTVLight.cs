using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTVLight : MonoBehaviour { 

    private Light TVLight;
    public float pulseSpeed = 1f; //this is in seconds
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        TVLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > pulseSpeed)
        {
            timer = 0;
            TVLight.enabled = !TVLight.enabled;
        }
    }
}
