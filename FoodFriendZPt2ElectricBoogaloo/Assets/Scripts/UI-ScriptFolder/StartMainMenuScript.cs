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

    [SerializeField] Animator myAnimationController;

    static float t = 0.0f; //starting value for lerp

    [Header("Lerping Colors")]
    public Color TitleInstruction_StartColor;
    public Color TitleInstruction_EndColor;

    [Header("Title Screen")]
    public TextMeshProUGUI TitleCard;
    public TextMeshProUGUI TitleInscruction;

    [Header("Parent GameObjects")]
    public GameObject OptionsMenuParent;
    public GameObject MainMenuParent;
    public GameObject CreditScreenParent;
    public GameObject LogBookParent;

    [Header("Buttons")]
    public GameObject TitleButtonParent;
    [Space]
    public Button StartButton;
    public Button OptionsButton;
    public Button CreditsButton;
    public Button LogButton;
    public Button QuitButton;

    public EventSystem es;

    bool IsOnTitleScreen;
    bool didPlayerPressAnyKey;

    void Awake()
    {
        //Default Title Screen elements
        TitleCard.enabled = true;
        TitleInscruction.enabled = true;

        CreditScreenParent.SetActive(false);
        IsOnTitleScreen = true;

        t = 0.0f;
    }

    void Update()
    {
        //bool logic
        if (IsOnTitleScreen)
        {
            didPlayerPressAnyKey = false;
        }
        else
        {
            didPlayerPressAnyKey = true;
        }

        //animations
        myAnimationController.SetBool("playerPressedAnyKey", didPlayerPressAnyKey);  //sets bool in the animator

        //Game Juice (TitleInstruction)
        TitleInscruction.color = Color.Lerp(TitleInstruction_StartColor, TitleInstruction_EndColor, Mathf.Sin(Time.time));       //color

        if (Input.anyKey && IsOnTitleScreen)
        {
            TitleInscruction.enabled = false;
            IsOnTitleScreen = false;
            es.SetSelectedGameObject(StartButton.gameObject);
        }
    }

    #region all buttons
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
        LogBookParent.SetActive(true);
        MainMenuParent.SetActive(false);
    }

    //Options Button
    public void OptionsButtonFunction()
    {
        OptionsMenuParent.SetActive(true);
        MainMenuParent.SetActive(false);
        Debug.Log("Opening options menu");
    }

    //Credits Button
    public void CreditsButtonFunction()
    {
        CreditScreenParent.SetActive(true);

        //turn off all other elements
        MainMenuParent.SetActive(false);
    }

    //Quit Button
    public void QuitButtonFunction()
    {
        Application.Quit();     //This should work in the build. Doesn't work in editor because it would close Unity
        Debug.Log("Quitting Game");
    }
    #endregion
}
