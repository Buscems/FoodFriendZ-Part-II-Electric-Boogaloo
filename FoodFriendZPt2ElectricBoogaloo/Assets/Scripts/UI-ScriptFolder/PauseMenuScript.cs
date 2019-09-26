using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

/* [[[TO DO]]]
    *
   /*
        
    / * [CITATIONS]
 * (Video) PAUSE MENU in Unity
 * https://www.youtube.com/watch?v=JivuXdrIHK0
 */

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuUI;  //i use this so that i may have the overlay off in the scene window when i work

    public static bool IsGamePaused = false;   //used to tell other scripts

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
        //Inpuy 
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

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;    //freezes time
        IsGamePaused = true;
    }

    public void OptionsButtonFunction()
    {
        Debug.Log("Calling Options Menu");
    }

    public void ResumeGameButtonFunction()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;    //resumes time
        IsGamePaused = false;
    }

    public void ResetRunButtonFunction()
    {
        Time.timeScale = 1f;            //Sets time back to normal before loading another scene
        Debug.Log("Reseting Run");
    }

    public void ExitGameButtonFunction()
    {
        Time.timeScale = 1f;            //Sets time back to normal before loading another scene
        Debug.Log("Going to MainMenu");
    }
}
