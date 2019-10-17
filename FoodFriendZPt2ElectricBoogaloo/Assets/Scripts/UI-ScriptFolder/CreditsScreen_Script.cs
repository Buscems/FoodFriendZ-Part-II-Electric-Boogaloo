using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

public class CreditsScreen_Script : MonoBehaviour
{
    public GameObject TitleScreenOverlay;
    public GameObject CreditsScreenOverlay;

    public EventSystem es;
    public Button startButton;

    public void BackButtonFunction()
    {
        es.SetSelectedGameObject(startButton.gameObject);
        TitleScreenOverlay.SetActive(true);
        CreditsScreenOverlay.SetActive(false);
    }
}
