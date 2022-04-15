using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSkybox : MonoBehaviour
{
    public Material LobbySky;
    public Material DefaultSky;
    public Material SwampSky;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = DefaultSky;
    }

}
