using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTreeSkybox : MonoBehaviour
{
    public Material LobbySky;
    public Material DefaultSky;
    public Material TreeSky;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = TreeSky;
    }

}
