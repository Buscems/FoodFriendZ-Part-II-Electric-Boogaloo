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

    [HideInInspector] public bool open;

    private void Start()
    {
       
    }

    private void Update()
    {
       
    }

    public void RestartButtonFunction()
    {
        SceneManager.LoadScene("Dans licc center");
    }

    public void ExitButtonFunction()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("TitleScreen");
    }
}
