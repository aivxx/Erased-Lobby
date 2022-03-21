using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinGameMaze : MonoBehaviour
{
    public UnityEvent onWin = new UnityEvent();
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
            onWin?.Invoke();
            if(!winMessage.activeInHierarchy)
            winMessage.SetActive(true);
            winZone.SetActive(false);
        }
    }
}
