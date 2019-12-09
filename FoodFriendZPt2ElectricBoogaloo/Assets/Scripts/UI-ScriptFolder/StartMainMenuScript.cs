using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro
using Rewired;
using Rewired.ControllerExtensions;

public class StartMainMenuScript : MonoBehaviour
{
    /* [[[TO DO]]]
     * 
     */

    [SerializeField] Animator myAnimationController;

    static float t = 0.0f; //starting value for lerp

    public GameObject ControllerMessage;
    bool controllerConnected;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Header("Lerping Colors")]
    public Color TitleInstruction_StartColor;
    public Color TitleInstruction_EndColor;

    [Header("Title Screen")]
    public Image TitleCard;
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
    public Button optionsBack;
    public Button creditsBack;
    public Button logBookCharacters;
    public Image characterHighlight;
    public Sprite[] allCharacterSprites;
    public static Sprite currentHighlightSprite;
    public float offset;
    bool mouse;

    [Header("Confetti Variables")]
    public GameObject Confetti1;
    public GameObject Confetti2;
    public GameObject Confetti3;
    public Transform spawn1;
    public Transform spawn2;
    bool spawnConfetti;

    public EventSystem es;

    bool IsOnTitleScreen;
    bool didPlayerPressAnyKey;

    void Awake()
    {
        Cursor.visible = false;
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        //Default Title Screen elements
        TitleCard.enabled = true;
        TitleInscruction.enabled = true;

        CreditScreenParent.SetActive(false);
        IsOnTitleScreen = true;

        t = 0.0f;

        var randNum = Random.Range(0, allCharacterSprites.Length);
        currentHighlightSprite = allCharacterSprites[randNum];
        characterHighlight.sprite = currentHighlightSprite;

        try
        {
            if (!controllerConnected)
            {
                ControllerMessage.SetActive(true);
            }
            else
            {
                ControllerMessage.SetActive(false);
            }
        }
        catch { }
    }

    void Update()
    {
        Debug.Log(controllerConnected);
        try
        {
            if (!controllerConnected)
            {
                ControllerMessage.SetActive(true);
            }
            else
            {
                ControllerMessage.SetActive(false);
            }
        }
        catch { }
        if (controllerConnected)
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

            if (!mouse)
            {
                if (es.currentSelectedGameObject == StartButton.gameObject)
                {
                    characterHighlight.gameObject.transform.position = new Vector3(characterHighlight.rectTransform.position.x, StartButton.transform.position.y + offset);
                }
                if (es.currentSelectedGameObject == OptionsButton.gameObject)
                {
                    characterHighlight.gameObject.transform.position = new Vector3(characterHighlight.rectTransform.position.x, OptionsButton.transform.position.y + offset);
                }
                if (es.currentSelectedGameObject == CreditsButton.gameObject)
                {
                    characterHighlight.gameObject.transform.position = new Vector3(characterHighlight.rectTransform.position.x, CreditsButton.transform.position.y + offset);
                }
                if (es.currentSelectedGameObject == LogButton.gameObject)
                {
                    characterHighlight.gameObject.transform.position = new Vector3(characterHighlight.rectTransform.position.x, LogButton.transform.position.y + offset);
                }
                if (es.currentSelectedGameObject == QuitButton.gameObject)
                {
                    characterHighlight.gameObject.transform.position = new Vector3(characterHighlight.rectTransform.position.x, QuitButton.transform.position.y + offset);
                }
            }

            if (MainMenuParent.activeInHierarchy && !spawnConfetti)
            {
                StartCoroutine(SpawnConfetti());
            }
        }
    }

    IEnumerator SpawnConfetti()
    {
        spawnConfetti = true;
        var randNum = Random.Range(0, 2);
        if(randNum == 0)
        {
            var c = Instantiate(Confetti1, new Vector3(Random.Range(spawn1.position.x, spawn2.position.x), spawn1.position.y, 0), Quaternion.identity);
            c.GetComponent<Confetti>().MainMenuScreen = MainMenuParent;
            c.name = "Confetti";
        }
        if (randNum == 1)
        {
            var c = Instantiate(Confetti2, new Vector3(Random.Range(spawn1.position.x, spawn2.position.x), spawn1.position.y, 0), Quaternion.identity);
            c.GetComponent<Confetti>().MainMenuScreen = MainMenuParent;
            c.name = "Confetti";
        }
        if (randNum == 2)
        {
            var c = Instantiate(Confetti3, new Vector3(Random.Range(spawn1.position.x, spawn2.position.x), spawn1.position.y, 0), Quaternion.identity);
            c.GetComponent<Confetti>().MainMenuScreen = MainMenuParent;
            c.name = "Confetti";
        }
        yield return new WaitForSeconds(Random.Range(0, .25f));
        spawnConfetti = false;
    }

    public void StartHighlight()
    {
        mouse = true;
        characterHighlight.gameObject.transform.position = new Vector3(characterHighlight.rectTransform.position.x, StartButton.transform.position.y + offset);
    }

    public void LeaveButton()
    {
        mouse = false;
    }

    #region all buttons
    //Start Button
    public void StartButtonFunction()
    {
        Debug.Log("Starting Game");
        //this is just for now so that the game can be played, have to add back character select
        //SceneManager.LoadScene("Dans licc center");
        SceneManager.LoadScene("CharacterSelectionScreen");
    }

    //LogBook Button
    public void LogBookButtonFunction()
    {
        Debug.Log("Opening Log Book");
        es.SetSelectedGameObject(logBookCharacters.gameObject);
        LogBookParent.SetActive(true);
        MainMenuParent.SetActive(false);
    }

    public void TutorialButtonFunction()
    {
        Debug.Log("Tutorial");
        SceneManager.LoadScene("Tutorial");
    }

    //Options Button
    public void OptionsButtonFunction()
    {
        OptionsMenuParent.SetActive(true);
        MainMenuParent.SetActive(false);
        es.SetSelectedGameObject(optionsBack.gameObject);
        Debug.Log("Opening options menu");
    }

    //Credits Button
    public void CreditsButtonFunction()
    {
        CreditScreenParent.SetActive(true);
        es.SetSelectedGameObject(creditsBack.gameObject);
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

    void OnControllerConnected(ControllerStatusChangedEventArgs arg)
    {
       controllerConnected = true;
    }

}
