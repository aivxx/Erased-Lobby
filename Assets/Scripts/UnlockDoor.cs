using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{

    public GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        Door.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Key"))
        {
            //unlock!
            if(Door.activeInHierarchy)
            Door.SetActive(false);
        }
    }
}
