using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTree : MonoBehaviour
{

    public GameObject secretTree;

    // Start is called before the first frame update
    void Start()
    {
        secretTree.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            secretTree.SetActive(true);

        }
    }




}
