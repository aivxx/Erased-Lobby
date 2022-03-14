using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameMaze : MonoBehaviour
{

    public GameObject winMessage;
    public GameObject winZone;

    // Start is called before the first frame update
    void Start()
    {
        winMessage.SetActive(false);
        winZone.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("MainCamera"))
        {
            if(!winMessage.activeInHierarchy)
            winMessage.SetActive(true);
            winZone.SetActive(false);
        }
    }
}
