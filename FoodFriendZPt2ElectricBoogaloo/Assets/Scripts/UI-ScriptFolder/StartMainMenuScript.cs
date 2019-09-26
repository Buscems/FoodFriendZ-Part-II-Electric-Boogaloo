using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

public class StartMainMenuScript : MonoBehaviour
{
    /* [[[TO DO]]]
     * 
     */

    static float t = 0.0f; //starting value for lerp

    [Header("Lerping Colors")]
    public Color TitleInstruction_StartColor;
    public Color TitleInstruction_EndColor;

    [Header("Title Screen")]
    public TextMeshProUGUI TitleCard;
    public TextMeshProUGUI TitleInscruction;

    [Header("Parent GameObjects")]
    public GameObject TitleButtonParent;
    public GameObject CreditScreenParent;

    [Header("Buttons")]
    public Button StartButton;
    public Button OptionsButton;
    public Button CreditsButton;
    public Button LogButton;
    public Button QuitButton;

    bool IsOnTitleScreen;

    void Awake()
    {
        //Default Title Screen elements
        TitleCard.enabled = true;
        TitleButtonParent.SetActive(false);
        TitleInscruction.enabled = true;

        CreditScreenParent.SetActive(false);
        IsOnTitleScreen = true;

        t = 0.0f;
    }

    void Update()
    {

        //Game Juice (TitleInstruction)
        TitleInscruction.color = Color.Lerp(TitleInstruction_StartColor, TitleInstruction_EndColor, Mathf.Sin(Time.time));       //color

        if (Input.anyKey && IsOnTitleScreen)
        {
            TitleInscruction.enabled = false;

            TitleButtonParent.SetActive(true);

            IsOnTitleScreen = false;
        }
    }

    //Start Button
    public void StartButtonFunction()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene("CharacterSelectionScreen");
    }

    //LogBook Button
    public void LogBookButtonFunction()
    {
        Debug.Log("Opening Log Book");
    }

    //Options Button
    public void OptionsButtonFunction()
    {
        Debug.Log("Opening options menu");
    }

    //Credits Button
    public void CreditsButtonFunction()
    {
        CreditScreenParent.SetActive(true);

        //turn off all other elements
        TitleButtonParent.SetActive(false);
        TitleCard.enabled = false;
    }

    //Quit Button
    public void QuitButtonFunction()
    {
        Application.Quit();     //This should work in the build. Doesn't work in editor because it would close Unity
        Debug.Log("Quitting Game");
    }
}
