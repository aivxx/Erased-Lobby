using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnTrigger : MonoBehaviour
{

    public AudioSource source;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Whiteboard"))
        {
            source.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Whiteboard"))
        {
            source.Stop();
        }
    }
}
