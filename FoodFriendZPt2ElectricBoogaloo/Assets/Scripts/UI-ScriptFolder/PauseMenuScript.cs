using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro
using Rewired;
using Rewired.ControllerExtensions;

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

    [Header("Event System")]
    public EventSystem es;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    public CharacterSelectionScreenScript charSelect;

    private void Awake()
    {
        PauseMenuUI.SetActive(false); //makes sure the pause screen is off upon loading
        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);
    }

    void Update()
    {
        //Pause toggle input
        if (myPlayer.GetButtonDown("Pause") && !charSelect.fridge.activeInHierarchy)
        //Input
        {
            //Pause check
            if (IsGamePaused)
            {
                ResumeGameButtonFunction();
                PauseMenuUI.SetActive(false);
            }
            else
            {
                PauseMenuUI.SetActive(true);
                Pause();
            }
        }

        if (myPlayer.GetButtonDown("Circle") && IsGamePaused)
        //Input
        {
            ResumeGameButtonFunction();
            PauseMenuUI.SetActive(false);
        }

    }


    #region all button functions
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        es.SetSelectedGameObject(ResumeGameButton.gameObject);

        Time.timeScale = 0f;    //freezes time
        IsGamePaused = true;
    }

    public void OptionsButtonFunction()
    {
        //need to fix
        /*
        Debug.Log("Calling Options Menu");
        PauseMenuOverlay.SetActive(false);
        OptionsOverlay.SetActive(true);
        */
    }

    public void ResumeGameButtonFunction()
    {
        es.SetSelectedGameObject(null);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;    //resumes time
        IsGamePaused = false;
    }

    public void ResetRunButtonFunction()
    {
        Time.timeScale = 1f;            //Sets time back to normal before loading another scene [May affect animator]
        Debug.Log("Reseting Run");
        SceneManager.LoadScene("Dans licc center");
    }

    public void ExitGameButtonFunction()
    {
        Time.timeScale = 1f;            //Sets time back to normal before loading another scene
        Debug.Log("Going to MainMenu");
        SceneManager.LoadScene("TitleScreen");
    }
    #endregion
    //rewired
    void OnControllerConnected(ControllerStatusChangedEventArgs arg)
    {
        CheckController(myPlayer);
    }
    void CheckController(Player player)
    {
        foreach (Joystick joyStick in player.controllers.Joysticks)
        {
            var ds4 = joyStick.GetExtension<DualShock4Extension>();
            if (ds4 == null) continue;//skip this if not DualShock4
            switch (playerNum)
            {
                case 4:
                    ds4.SetLightColor(Color.yellow);
                    break;
                case 3:
                    ds4.SetLightColor(Color.green);
                    break;
                case 2:
                    ds4.SetLightColor(Color.blue);
                    break;
                case 1:
                    ds4.SetLightColor(Color.red);
                    break;
                default:
                    ds4.SetLightColor(Color.white);
                    Debug.LogError("Player Num is 0, please change to a number > 0");
                    break;
            }
        }
    }
}
