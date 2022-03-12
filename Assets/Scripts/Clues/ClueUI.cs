using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ClueUI : MonoBehaviour
{
    
    public GameObject uiObject;
    public AudioSource playSound;


 


    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
       playSound.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiObject.SetActive(true);
            playSound.Play();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiObject.SetActive(false);
        }
    }

}
