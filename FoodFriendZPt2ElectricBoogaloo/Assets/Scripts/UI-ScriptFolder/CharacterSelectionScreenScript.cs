using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro
using Rewired;
using Rewired.ControllerExtensions;
using System.IO;

/* [[[TO DO]]]
*
   */

public class CharacterSelectionScreenScript : MonoBehaviour
{

    AudioSource audioSource;

    [Header("Highlighted Character")]
    public Image HighlightedCharacterIMG;
    public TextMeshProUGUI HighlightedCharacterNameDisplay;

    [Space]
    public Image LockedCharacterSprite;

    [Header("Bottom Screen Buttons")]
    public Button BackToTitleScreen;
    public Button PlayGame;

    public EventSystem events;

    public Sprite[] characterSprites;
    public Image[] characterButtons;

    [Header("Description Variables")]
    public TextMeshProUGUI descriptionHeader;
    public TextMeshProUGUI descriptionBody;
    public TextAsset descriptionText;
    public string[] descriptionSections;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Header("Scrollbar Variables")]
    public Scrollbar scrollbar;
    public float scrollSpeed;
    public float joystickThreshold;

    private void Awake()
    {
        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);
    }

    private void Start()
    {
        //setting up the text file
        descriptionSections = descriptionText.ToString().Split('\n');
        audioSource = GetComponent<AudioSource>();

        PlayerPrefs.SetFloat("startCharacter", 0);

        for (int i = 0; i < characterButtons.Length; i++)
        {
            characterButtons[i].sprite = characterSprites[i];
        }

        HighlightedCharacterIMG.sprite = characterSprites[0];
        HighlightedCharacterNameDisplay.text = "Tofu";
        string[] descText = descriptionSections[0].Split(';');
        descriptionHeader.text = descText[0];
        descriptionBody.text = descText[1];

    }

    private void Update()
    {
        if(Mathf.Abs(myPlayer.GetAxis("DirectionVertical")) >= joystickThreshold)
        {
            scrollbar.value += myPlayer.GetAxis("DirectionVertical") * scrollSpeed * Time.deltaTime;
        }
    }

    public void BackToTitleScreenFunction()
    {
        audioSource.Play();
        Debug.Log("Loading TitleScreen");
        SceneManager.LoadScene("TitleScreen");
    }

    public void PlayGameFunction()
    {
        audioSource.Play();
        Debug.Log("Loading GameplayScene");
        SceneManager.LoadScene("Dans licc center");
    }

    public void Tofu()
    {
        HighlightedCharacterIMG.sprite = characterSprites[0];
        HighlightedCharacterNameDisplay.text = "Tofu";
        PlayerPrefs.SetInt("startCharacter", 1);
    }
    public void Onigiri()
    {
        HighlightedCharacterIMG.sprite = characterSprites[1];
        HighlightedCharacterNameDisplay.text = "Onigiri";
        PlayerPrefs.SetInt("startCharacter", 2);
    }
    public void Takoyaki()
    {
        HighlightedCharacterIMG.sprite = characterSprites[2];
        HighlightedCharacterNameDisplay.text = "Takoyaki";
        PlayerPrefs.SetInt("startCharacter", 3);
    }
    public void Cherry()
    {
        HighlightedCharacterIMG.sprite = characterSprites[3];
        HighlightedCharacterNameDisplay.text = "Cherry";
        PlayerPrefs.SetInt("startCharacter", 4);
    }
    public void Cannoli()
    {
        HighlightedCharacterIMG.sprite = characterSprites[4];
        HighlightedCharacterNameDisplay.text = "Cannoli";
        PlayerPrefs.SetInt("startCharacter", 5);
    }
    public void Burger()
    {
        HighlightedCharacterIMG.sprite = characterSprites[5];
        HighlightedCharacterNameDisplay.text = "Burger";
        PlayerPrefs.SetInt("startCharacter", 6);
    }
    public void Sashimi()
    {
        HighlightedCharacterIMG.sprite = characterSprites[6];
        HighlightedCharacterNameDisplay.text = "Sashimi";
        PlayerPrefs.SetInt("startCharacter", 7);
    }
    public void Fries()
    {
        HighlightedCharacterIMG.sprite = characterSprites[7];
        HighlightedCharacterNameDisplay.text = "Fries";
        PlayerPrefs.SetInt("startCharacter", 8);
    }
    public void Taco()
    {
        HighlightedCharacterIMG.sprite = characterSprites[8];
        HighlightedCharacterNameDisplay.text = "Taco";
        PlayerPrefs.SetInt("startCharacter", 9);
    }
    public void Donut()
    {
        HighlightedCharacterIMG.sprite = characterSprites[9];
        HighlightedCharacterNameDisplay.text = "Donut";
        PlayerPrefs.SetInt("startCharacter", 10);
    }
    public void Hotdog()
    {
        HighlightedCharacterIMG.sprite = characterSprites[10];
        HighlightedCharacterNameDisplay.text = "Hot Dog";
        PlayerPrefs.SetInt("startCharacter", 11);
    }
    public void Napoleon()
    {
        HighlightedCharacterIMG.sprite = characterSprites[11];
        HighlightedCharacterNameDisplay.text = "Napoleon";
        PlayerPrefs.SetInt("startCharacter", 12);
    }
    public void Muffin()
    {
        HighlightedCharacterIMG.sprite = characterSprites[12];
        HighlightedCharacterNameDisplay.text = "Blueberry \nMuffin";
        PlayerPrefs.SetInt("startCharacter", 13);
    }
    public void MintChip()
    {
        HighlightedCharacterIMG.sprite = characterSprites[13];
        HighlightedCharacterNameDisplay.text = "Mint \nChip";
        PlayerPrefs.SetInt("startCharacter", 14);
    }
    public void LobsterTail()
    {
        HighlightedCharacterIMG.sprite = characterSprites[14];
        HighlightedCharacterNameDisplay.text = "Lobster \nTail";
        PlayerPrefs.SetInt("startCharacter", 15);
    }
    public void Samosa()
    {
        HighlightedCharacterIMG.sprite = characterSprites[15];
        HighlightedCharacterNameDisplay.text = "Samosa";
        PlayerPrefs.SetInt("startCharacter", 16);
    }
    public void TunaCan()
    {
        HighlightedCharacterIMG.sprite = characterSprites[16];
        HighlightedCharacterNameDisplay.text = "Tuna \nCan";
        PlayerPrefs.SetInt("startCharacter", 17);
    }


    //[REWIRED METHODS]
    //these two methods are for ReWired, if any of you guys have any questions about it I can answer them, but you don't need to worry about this for working on the game - Buscemi
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
