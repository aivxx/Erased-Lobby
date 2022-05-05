using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logs : MonoBehaviour
{

    public GameObject logs;
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        logs.transform.localScale += scaleChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Logs"))
        {
            scaleChange = -scaleChange;
            GameObject.Instantiate(logs);
        }
    }
}
