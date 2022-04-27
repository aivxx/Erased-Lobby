using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Consumable : MonoBehaviour
{

    [SerializeField] GameObject[] portions;
    [SerializeField] int index = 0;

    public bool IsFinished => index == portions.Length;

    AudioSource _audioSource;



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        SetVisuals();
    }

    private void OnValidate()
    {
        SetVisuals();
    }

    public void Consume()
    {
        if(!IsFinished)
        {
            index ++;
            SetVisuals();
            _audioSource.Play();
        }
    }

    void SetVisuals()
    {
        for (int i = 0; i < portions.Length; i++)
        {
            portions[i].SetActive(i == index);
        }
    }

}
