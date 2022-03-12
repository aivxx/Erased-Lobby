using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject uiMenu;
    public AudioSource playSound;

    // Start is called before the first frame update
    void Start()
    {
        uiMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            uiMenu.SetActive(true);
            playSound.Play();
            Cursor.visible = true;
        }
    }


}
