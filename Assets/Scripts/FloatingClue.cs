using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingClue : MonoBehaviour
{

    // Makes objects float up and down

    // User inputs
    public float frequency = 1f;
    public float amplitude = 0.5f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position of object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
