using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuUI;  //i use this so that i may have the overlay off in the scene window when i work

    public static bool IsGamePaused = false;   //used to tell other scripts

    [Header("Overlay Elements")]
    public GameObject PauseMenuOverlay;
    public GameObject OptionsOverlay;

    [Header("Parent GameObjects")]
    public GameObject PauseButtonParent;

    [Header("Buttons")]
    public Button OptionsButton;
    public Button ResumeGameButton;
    public Button ResetRunButton;
    public Button ExitGameButton;

    private void Awake()
    {
        PauseMenuUI.SetActive(false); //makes sure the pause screen is off upon loading
    }

    void Update()
    {
        //Pause toggle input
        if (Input.GetKeyDown(KeyCode.Escape))
        //Input
        {
            //Pause check
            if (IsGamePaused)
            {
                ResumeGameButtonFunction();
            }
            else
            {
                Pause();
            }
        }
    }


    #region all button functions
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;    //freezes time
        IsGamePaused = true;
    }

    public void OptionsButtonFunction()
    {
        Debug.Log("Calling Options Menu");
        PauseMenuOverlay.SetActive(false);
        OptionsOverlay.SetActive(true);
    }

    public void ResumeGameButtonFunction()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;    //resumes time
        IsGamePaused = false;
    }

    public void ResetRunButtonFunction()
    {
        Time.timeScale = 1f;            //Sets time back to normal before loading another scene [May affect animator]
        Debug.Log("Reseting Run");
        SceneManager.LoadScene("GameplayScreen");
    }

    public void ExitGameButtonFunction()
    {
        Time.timeScale = 1f;            //Sets time back to normal before loading another scene
        Debug.Log("Going to MainMenu");
        SceneManager.LoadScene("TitleScreen");
    }
    #endregion
}
