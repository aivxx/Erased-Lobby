using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{

    public Material LobbySky;
    public Material SwampSky;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = LobbySky;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
