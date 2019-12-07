using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

public class GameOverScreenScript : MonoBehaviour
{
    /* [[[TO DO]]]
     *
     */

    

    [Header("Buttons")]
    public Button RestartButton;
    public Button ExitButton;

    public void RestartButtonFunction()
    {
        Debug.Log("Restarting Game");
        SceneManager.LoadScene("GameplayScreen");
    }

    public void ExitButtonFunction()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("TitleScreen");
    }
}
