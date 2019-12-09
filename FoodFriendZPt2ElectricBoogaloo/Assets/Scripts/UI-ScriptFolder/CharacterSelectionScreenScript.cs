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

    public InGameFridgeManager fridgeManager;
    [HideInInspector] public int savedNum;

    public Button startCharacter;

    //saving stuff
    private SaveGame saveManager;
    private GameData gameData;

    AudioSource audioSource;

    bool canPlay;
    bool turnOn;

    [Header("Highlighted Character")]
    public Image HighlightedCharacterIMG;
    public TextMeshProUGUI HighlightedCharacterNameDisplay;

    [Space]
    public Image LockedCharacterSprite;

    public EventSystem events;

    public Sprite[] characterSprites;
    public Image[] characterButtons;

    [Header("Description Variables")]
    public TextMeshProUGUI descriptionHeader;
    public TextMeshProUGUI descriptionBody;
    public TextAsset descriptionText;
    public string[] descriptionSections;

    [Header("Stat Sliders")]
    public Image damage;
    public Image movementSpeed;
    public Image attackSpeed;
    public Vector3[] stats;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Header("Scrolling Variables")]
    public GameObject[] characterSets;
    public GameObject[] switchCharacter;
    public GameObject firstChar;
    public float joystickThreshold;
    bool hasScrolled;

    private void Awake()
    {
        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);
    }

    private void Start()
    {
        //load the data from the save file so you can check if you own stuff or not
        saveManager = GetComponent<SaveGame>();
        saveManager.Load();
        gameData = saveManager.gameData;

        //buttonSpot.position = new Vector3(buttonSpot.position.x, scrollbarValues[0], 0);

        //setting up the text file
        descriptionSections = descriptionText.ToString().Split('\n');
        audioSource = GetComponent<AudioSource>();

        PlayerPrefs.SetFloat("startCharacter", 0);
        saveManager.Load();
        for (int i = 0; i < characterButtons.Length; i++)
        {
            print(gameData.CharacterListNames[i] + ", " + gameData.CharacterList[i]);
            characterButtons[i].sprite = characterSprites[i];
            if(!gameData.CharacterList[i])
            {
                characterButtons[i].color = Color.black;
            }
        }
        try
        {
            events.SetSelectedGameObject(startCharacter.gameObject);
        }
        catch { }
        HighlightedCharacterIMG.sprite = characterSprites[0];
        HighlightedCharacterNameDisplay.text = "Tofu";
        string[] descText = descriptionSections[0].Split(';');
        descriptionHeader.text = descText[0];
        descriptionBody.text = descText[1];
        for(int i = 0; i < characterSets.Length; i++)
        {
            if(i != 0)
            {
                characterSets[i].SetActive(false);
            }
        }

        
    }

    private void Update()
    {

        if (turnOn)
        {
            TurningOn();
        }
        
        //this is handling all of the scrolling code
        if (Mathf.Abs(myPlayer.GetAxis("DirectionVertical")) >= joystickThreshold)
        {
            for (int i = 0; i < characterSets.Length; i++)
            {
                if (!hasScrolled)
                {
                    if (characterSets[i].activeSelf)
                    {
                        //if going down
                        if (myPlayer.GetAxisRaw("DirectionVertical") < 0 && i != characterSets.Length - 1)
                        {
                            characterSets[i].SetActive(false);
                            characterSets[i + 1].SetActive(true);
                            events.SetSelectedGameObject(switchCharacter[i + 1]);
                            audioSource.Play();
                            hasScrolled = true;
                        }
                        //if going up
                        if (myPlayer.GetAxisRaw("DirectionVertical") > 0 && i != 0)
                        {
                            characterSets[i].SetActive(false);
                            characterSets[i - 1].SetActive(true);
                            events.SetSelectedGameObject(switchCharacter[i - 1]);
                            audioSource.Play();
                            hasScrolled = true;
                        }
                        //if nothing is clicked
                        if(events.currentSelectedGameObject == null)
                        {
                            events.SetSelectedGameObject(switchCharacter[i]);
                        }
                    }
                }
            }
        }
        else
        {
            hasScrolled = false;
        }
        
        /*
        if (Mathf.Abs(myPlayer.GetAxis("MoveVertical")) >= joystickThreshold)
        {
            //if going down
            if (myPlayer.GetAxis("MoveVertical") < 0)
            {
                try
                {
                    if (events.currentSelectedGameObject == switchCharacter[0] || events.currentSelectedGameObject == switchCharacter[1])
                    {
                        characterSets[0].SetActive(false);
                        characterSets[1].SetActive(true);
                        events.SetSelectedGameObject(switchCharacter[2]);
                    }
                    if (events.currentSelectedGameObject == switchCharacter[4] || events.currentSelectedGameObject == switchCharacter[5])
                    {
                        characterSets[1].SetActive(false);
                        characterSets[2].SetActive(true);
                        events.SetSelectedGameObject(switchCharacter[6]);
                    }
                    if (events.currentSelectedGameObject == switchCharacter[8] || events.currentSelectedGameObject == switchCharacter[9])
                    {
                        characterSets[2].SetActive(false);
                        characterSets[3].SetActive(true);
                        events.SetSelectedGameObject(switchCharacter[10]);
                    }
                }
                catch
                {

                }
            }
            //if going up
            if (myPlayer.GetAxis("DirectionVertical") > 0)
            {
                try
                {
                    if (events.currentSelectedGameObject == switchCharacter[10] || events.currentSelectedGameObject == switchCharacter[11])
                    {
                        characterSets[3].SetActive(false);
                        characterSets[2].SetActive(true);
                        events.SetSelectedGameObject(switchCharacter[6]);
                    }
                    if (events.currentSelectedGameObject == switchCharacter[6] || events.currentSelectedGameObject == switchCharacter[7])
                    {
                        characterSets[2].SetActive(false);
                        characterSets[1].SetActive(true);
                        events.SetSelectedGameObject(switchCharacter[2]);
                    }
                    if (events.currentSelectedGameObject == switchCharacter[2] || events.currentSelectedGameObject == switchCharacter[3])
                    {
                        characterSets[1].SetActive(false);
                        characterSets[0].SetActive(true);
                        events.SetSelectedGameObject(firstChar);
                    }
                }
                catch { }
            }
        }
        */

    }

    public void SwapOut(int Num)
    {

        if (gameData.CharacterList[Num])
        {
            damage.fillAmount = stats[Num].x;
            movementSpeed.fillAmount = stats[Num].y;
            attackSpeed.fillAmount = stats[Num].z;
            HighlightedCharacterIMG.sprite = characterSprites[Num];
            HighlightedCharacterNameDisplay.text = gameData.CharacterListNames[Num];
            string[] descText = descriptionSections[Num].Split(';');
            savedNum = Num;
            turnOn = true;
            try
            {
                descriptionHeader.text = descText[Num];
                descriptionBody.text = descText[Num];
            } catch { }
           
        }
        else
        {
            turnOn = false;
        }
    }

    public void TurningOn()
    {
        if (turnOn)
        {
            Debug.Log("swapping");
            if (myPlayer.GetButtonDown("Cross"))
            {
                fridgeManager.SwapCharacter(savedNum);
                turnOn = false;
            }
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
        if (canPlay)
        {
            audioSource.Play();
            Debug.Log("Loading GameplayScene");
            SceneManager.LoadScene("Dans licc center");
        }
    }

    public void Tofu()
    {
        if (gameData.CharacterList[0])
        {
            damage.fillAmount = stats[0].x;
            movementSpeed.fillAmount = stats[0].y;
            attackSpeed.fillAmount = stats[0].z;
            HighlightedCharacterIMG.sprite = characterSprites[0];
            HighlightedCharacterNameDisplay.text = "Tofu";
            PlayerPrefs.SetInt("startCharacter", 1);
            string[] descText = descriptionSections[0].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            //damage.fillAmount
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Onigiri()
    {
        if (gameData.CharacterList[1])
        {
            damage.fillAmount = stats[1].x;
            movementSpeed.fillAmount = stats[1].y;
            attackSpeed.fillAmount = stats[1].z;
            HighlightedCharacterIMG.sprite = characterSprites[1];
            HighlightedCharacterNameDisplay.text = "Onigiri";
            PlayerPrefs.SetInt("startCharacter", 2);
            string[] descText = descriptionSections[1].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Takoyaki()
    {
        if (gameData.CharacterList[2])
        {
            damage.fillAmount = stats[2].x;
            movementSpeed.fillAmount = stats[2].y;
            attackSpeed.fillAmount = stats[2].z;
            HighlightedCharacterIMG.sprite = characterSprites[2];
            HighlightedCharacterNameDisplay.text = "Takoyaki";
            PlayerPrefs.SetInt("startCharacter", 3);
            string[] descText = descriptionSections[2].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Cherry()
    {
        if (gameData.CharacterList[3])
        {
            damage.fillAmount = stats[3].x;
            movementSpeed.fillAmount = stats[3].y;
            attackSpeed.fillAmount = stats[3].z;
            HighlightedCharacterIMG.sprite = characterSprites[3];
            HighlightedCharacterNameDisplay.text = "Cherry";
            PlayerPrefs.SetInt("startCharacter", 4);
            string[] descText = descriptionSections[3].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Cannoli()
    {
        if (gameData.CharacterList[1])
        {
            damage.fillAmount = stats[4].x;
            movementSpeed.fillAmount = stats[4].y;
            attackSpeed.fillAmount = stats[4].z;
            HighlightedCharacterIMG.sprite = characterSprites[4];
            HighlightedCharacterNameDisplay.text = "Cannoli";
            PlayerPrefs.SetInt("startCharacter", 5);
            string[] descText = descriptionSections[4].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Burger()
    {
        if (gameData.CharacterList[5])
        {
            damage.fillAmount = stats[5].x;
            movementSpeed.fillAmount = stats[5].y;
            attackSpeed.fillAmount = stats[5].z;
            HighlightedCharacterIMG.sprite = characterSprites[5];
            HighlightedCharacterNameDisplay.text = "Burger";
            PlayerPrefs.SetInt("startCharacter", 6);
            string[] descText = descriptionSections[5].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Sashimi()
    {
        if (gameData.CharacterList[6])
        {
            damage.fillAmount = stats[6].x;
            movementSpeed.fillAmount = stats[6].y;
            attackSpeed.fillAmount = stats[6].z;
            HighlightedCharacterIMG.sprite = characterSprites[6];
            HighlightedCharacterNameDisplay.text = "Sashimi";
            PlayerPrefs.SetInt("startCharacter", 7);
            string[] descText = descriptionSections[6].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Fries()
    {
        if (gameData.CharacterList[7])
        {
            damage.fillAmount = stats[7].x;
            movementSpeed.fillAmount = stats[7].y;
            attackSpeed.fillAmount = stats[7].z;
            HighlightedCharacterIMG.sprite = characterSprites[7];
            HighlightedCharacterNameDisplay.text = "Fries";
            PlayerPrefs.SetInt("startCharacter", 8);
            string[] descText = descriptionSections[7].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Taco()
    {
        if (gameData.CharacterList[8])
        {
            damage.fillAmount = stats[8].x;
            movementSpeed.fillAmount = stats[8].y;
            attackSpeed.fillAmount = stats[8].z;
            HighlightedCharacterIMG.sprite = characterSprites[8];
            HighlightedCharacterNameDisplay.text = "Taco";
            PlayerPrefs.SetInt("startCharacter", 9);
            string[] descText = descriptionSections[8].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true; 
        }
        else
        {
            canPlay = false;
        }
    }
    public void Donut()
    {
        if (gameData.CharacterList[9])
        {
            damage.fillAmount = stats[9].x;
            movementSpeed.fillAmount = stats[9].y;
            attackSpeed.fillAmount = stats[9].z;
            HighlightedCharacterIMG.sprite = characterSprites[9];
            HighlightedCharacterNameDisplay.text = "Donut";
            PlayerPrefs.SetInt("startCharacter", 10);
            string[] descText = descriptionSections[9].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Hotdog()
    {
        if (gameData.CharacterList[10])
        {
            damage.fillAmount = stats[10].x;
            movementSpeed.fillAmount = stats[10].y;
            attackSpeed.fillAmount = stats[10].z;
            HighlightedCharacterIMG.sprite = characterSprites[10];
            HighlightedCharacterNameDisplay.text = "Hot Dog";
            PlayerPrefs.SetInt("startCharacter", 11);
            string[] descText = descriptionSections[10].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Napoleon()
    {
        if (gameData.CharacterList[11])
        {
            damage.fillAmount = stats[11].x;
            movementSpeed.fillAmount = stats[11].y;
            attackSpeed.fillAmount = stats[11].z;
            HighlightedCharacterIMG.sprite = characterSprites[11];
            HighlightedCharacterNameDisplay.text = "Napoleon";
            PlayerPrefs.SetInt("startCharacter", 12);
            string[] descText = descriptionSections[11].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Muffin()
    {
        if (gameData.CharacterList[12])
        {
            damage.fillAmount = stats[12].x;
            movementSpeed.fillAmount = stats[12].y;
            attackSpeed.fillAmount = stats[12].z;
            HighlightedCharacterIMG.sprite = characterSprites[12];
            HighlightedCharacterNameDisplay.text = "Blueberry \nMuffin";
            PlayerPrefs.SetInt("startCharacter", 13);
            string[] descText = descriptionSections[12].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void MintChip()
    {
        if (gameData.CharacterList[13])
        {
            damage.fillAmount = stats[13].x;
            movementSpeed.fillAmount = stats[13].y;
            attackSpeed.fillAmount = stats[13].z;
            HighlightedCharacterIMG.sprite = characterSprites[13];
            HighlightedCharacterNameDisplay.text = "Mint \nChip";
            PlayerPrefs.SetInt("startCharacter", 14);
            string[] descText = descriptionSections[13].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void LobsterTail()
    {
        if (gameData.CharacterList[14])
        {
            damage.fillAmount = stats[14].x;
            movementSpeed.fillAmount = stats[14].y;
            attackSpeed.fillAmount = stats[14].z;
            HighlightedCharacterIMG.sprite = characterSprites[14];
            HighlightedCharacterNameDisplay.text = "Bubble \nTea";
            PlayerPrefs.SetInt("startCharacter", 15);
            string[] descText = descriptionSections[14].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Samosa()
    {
        if (gameData.CharacterList[15])
        {
            damage.fillAmount = stats[15].x;
            movementSpeed.fillAmount = stats[15].y;
            attackSpeed.fillAmount = stats[15].z;
            HighlightedCharacterIMG.sprite = characterSprites[15];
            HighlightedCharacterNameDisplay.text = "Samosa";
            PlayerPrefs.SetInt("startCharacter", 16);
            string[] descText = descriptionSections[15].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void TunaCan()
    {
        if (gameData.CharacterList[16])
        {
            damage.fillAmount = stats[16].x;
            movementSpeed.fillAmount = stats[16].y;
            attackSpeed.fillAmount = stats[16].z;
            HighlightedCharacterIMG.sprite = characterSprites[16];
            HighlightedCharacterNameDisplay.text = "Tuna \nCan";
            PlayerPrefs.SetInt("startCharacter", 17);
            string[] descText = descriptionSections[16].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void Pancake()
    {
        if (gameData.CharacterList[17])
        {
            damage.fillAmount = stats[17].x;
            movementSpeed.fillAmount = stats[17].y;
            attackSpeed.fillAmount = stats[17].z;
            HighlightedCharacterIMG.sprite = characterSprites[17];
            HighlightedCharacterNameDisplay.text = "Pancakes";
            PlayerPrefs.SetInt("startCharacter", 18);
            string[] descText = descriptionSections[17].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }
    public void BubbleTea()
    {
        if (gameData.CharacterList[18])
        {
            damage.fillAmount = stats[18].x;
            movementSpeed.fillAmount = stats[18].y;
            attackSpeed.fillAmount = stats[18].z;
            HighlightedCharacterIMG.sprite = characterSprites[18];
            HighlightedCharacterNameDisplay.text = "BubbleTea";
            PlayerPrefs.SetInt("startCharacter", 19);
            string[] descText = descriptionSections[18].Split(';');
            descriptionHeader.text = descText[0];
            descriptionBody.text = descText[1];
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
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
