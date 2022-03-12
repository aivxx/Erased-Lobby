using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    PauseAction action;
 public static bool GamePaused = false;
    public GameObject PauseMenuUI;

    


    private void Awake()
    {
        action = new PauseAction();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

   

    // Update is called once per frame
    private void Start()
    {
        action.Pause.PauseGame.performed += _ => DeterminePause();
        PauseMenuUI.SetActive(false);
    }

    private void DeterminePause()
    {
        if (GamePaused)
            ResumeGame();
        else
            PauseGame();
    }


    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        GamePaused = false;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        GamePaused = true;
        Cursor.visible = true;
        PauseMenuUI.SetActive(true);
        
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

