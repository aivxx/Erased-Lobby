using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slybox : MonoBehaviour
{

    public Material LobbySky;
    public Material MazeSky;
    public Material InteractionsSky;
    public Material TreeSky;

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
