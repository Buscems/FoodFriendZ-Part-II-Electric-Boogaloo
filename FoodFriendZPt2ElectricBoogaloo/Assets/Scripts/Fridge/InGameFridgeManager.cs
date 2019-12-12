using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;
using UnityEngine.EventSystems;

public class InGameFridgeManager : MonoBehaviour
{
    public BasePlayer[] allPlayableCharacters;
    public GameObject[] fridgeCharacterPlaceHolders;
    public GameObject[] selectCharUI;
    public GameObject[] inUseUI;
    public GameObject Fridge;

    private BasePlayer[] selectableCharacters = new BasePlayer[] { null, null, null, null };
    /*
     * order
     * 
     * 0 -> Square
     * 1 -> Triangle
     * 2 -> Cross
     * 3 -> Circle
     * 
     * */

    public CharacterSelectionScreenScript charSelect;

    public GameObject tofu;

    public EventSystem events;

    public GameObject leftArrow;
    public GameObject rightArrow;

    bool playerWithinRange;

    private MainPlayer player;

    private SaveGame saveManager;
    private GameData gameData;
    private int currentScrollNum = 0;

    Color alphaOn = new Color(1, 1, 1, 1);
    Color alphaOff = new Color(1, 1, 1, 0);

    private bool switchCharacterPhase = false;
    private int storedIndex = 0;

    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    private void Awake()
    {
        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);


        //load the data from the save file so you can check if you own stuff or not
        saveManager = GetComponent<SaveGame>();
        saveManager.Load();
        gameData = saveManager.gameData;
    }

    // Start is called before the first frame update
    void Start()
    {
        charSelect = GameObject.Find("***CharacterSelectionMenu").GetComponent<CharacterSelectionScreenScript>();
        charSelect.fridgeManager = this.GetComponent<InGameFridgeManager>();

        events = charSelect.events;

        Fridge = charSelect.transform.GetChild(0).gameObject;
        Fridge.SetActive(false);

        for (int i = 0; i < fridgeCharacterPlaceHolders.Length; i++)
        {
            fridgeCharacterPlaceHolders[i].SetActive(false);
        }
        for (int i = 0; i < selectCharUI.Length; i++)
        {
            selectCharUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
        }
        for (int i = 0; i < inUseUI.Length; i++)
        {
            inUseUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
        }

        rightArrow.SetActive(false);
        leftArrow.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (playerWithinRange && myPlayer.GetButtonDown("Interact"))
        {
            Debug.Log("Yer");
            Fridge.SetActive(true);

            events.SetSelectedGameObject(charSelect.startCharacter.gameObject);

            Time.timeScale = 0;

            //scroll to the right
            /*if (Input.GetKeyDown(KeyCode.T) || myPlayer.GetButtonDown("RightDPad"))
            {
                if (currentScrollNum + 4 < allPlayableCharacters.Length)
                {
                    currentScrollNum += 4;
                    Scroll();
                    leftArrow.GetComponent<SpriteRenderer>().color = alphaOn;
                }

                if(currentScrollNum + 4 >= allPlayableCharacters.Length)
                {
                    rightArrow.GetComponent<SpriteRenderer>().color = alphaOff;
                }
                else
                {
                    rightArrow.GetComponent<SpriteRenderer>().color = alphaOn;
                }
            }

            //scroll to the left
            if (Input.GetKeyDown(KeyCode.R) || myPlayer.GetButtonDown("LeftDPad"))
            {
                if (currentScrollNum - 4 >= 0)
                {
                    currentScrollNum -= 4;
                    Scroll();
                    rightArrow.GetComponent<SpriteRenderer>().color = alphaOn;
                }
                if (currentScrollNum == 0)
                {
                    leftArrow.GetComponent<SpriteRenderer>().color = alphaOff;
                }
                else
                {
                    leftArrow.GetComponent<SpriteRenderer>().color = alphaOn;
                }
            }

            //array index 0
            if (myPlayer.GetButtonDown("Square"))
            {
                if (switchCharacterPhase == false)
                {
                    //check id this character index is owned so you can swap
                    if (selectableCharacters[0] != null)
                    {
                        switchCharacterPhase = true;
                        storedIndex = currentScrollNum;
                    }
                }
                else
                {
                    //actually do the swap and destroy itself afterward
                    if (player.square != null)
                    {
                        player.GetComponent<MainPlayer>().CharacterSwap(allPlayableCharacters[storedIndex], "Square");
                        Destroy(gameObject);
                    }
                }
            }
            //array index 1
            if (myPlayer.GetButtonDown("Triangle"))
            {
                if (switchCharacterPhase == false)
                {
                    if (selectableCharacters[1] != null)
                    {
                        switchCharacterPhase = true;
                        storedIndex = currentScrollNum + 1;
                    }
                }
                else
                {
                    if (player.triangle != null)
                    {
                        player.GetComponent<MainPlayer>().CharacterSwap(allPlayableCharacters[storedIndex], "Triangle");
                        Destroy(gameObject);
                    }
                }
            }
            //array index 2
            if (myPlayer.GetButtonDown("Cross"))
            {
                if (switchCharacterPhase == false)
                {
                    if (selectableCharacters[2] != null)
                    {
                        switchCharacterPhase = true;
                        storedIndex = currentScrollNum + 2;
                    }
                }
                else
                {
                    if (player.cross != null)
                    {
                        player.GetComponent<MainPlayer>().CharacterSwap(allPlayableCharacters[storedIndex], "Cross");
                        Destroy(gameObject);
                    }
                }
            }
            //array index 3
            if (myPlayer.GetButtonDown("Circle"))
            {
                if (switchCharacterPhase == false)
                {
                    if (selectableCharacters[3] != null)
                    {
                        switchCharacterPhase = true;
                        storedIndex = currentScrollNum + 3;
                    }
                }
                else
                {
                    if (player.circle != false)
                    {
                        player.GetComponent<MainPlayer>().CharacterSwap(allPlayableCharacters[storedIndex], "Circle");
                        Destroy(gameObject);
                    }
                }
            }



    */
        }

        if (myPlayer.GetButtonDown("Circle"))
        {
            Fridge.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SwapCharacter(int Num)
    {

            if (selectableCharacters[Num] != null)
            {
                switchCharacterPhase = true;
                storedIndex = currentScrollNum;
            }

            else
            {
                //actually do the swap and destroy itself afterward
                if (player.square != null)
                {
                    player.GetComponent<MainPlayer>().CharacterSwap(allPlayableCharacters[storedIndex], "Square");
                    Destroy(gameObject);

                }
            }
        
    }

    private void Scroll()
    {

        //cycles through 4 times for eack character place holder
        for (int i = 0; i < 4; i++)
        {
            if (currentScrollNum  + i < allPlayableCharacters.Length)
            {
                //see where in gamedata array this specific character is so we can check if we own the character
                int index = FindCharacterIndex(currentScrollNum + i);
                //if character is even in the GameData script
                if (index != -1)
                {
                    fridgeCharacterPlaceHolders[i].GetComponent<SpriteRenderer>().color = alphaOn;
                    fridgeCharacterPlaceHolders[i].GetComponent<SpriteRenderer>().sprite = allPlayableCharacters[currentScrollNum + i].hudIcon;
                    //if you dont own the character set sprite to be black
                    if (gameData.CharacterList[index] == false)
                    {
                        fridgeCharacterPlaceHolders[i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                        selectCharUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
                        selectableCharacters[i] = null;
                        inUseUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
                    }
                    //else if you own the character, display the character
                    else
                    {
                        fridgeCharacterPlaceHolders[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                        if (player.cross == allPlayableCharacters[currentScrollNum + i] || player.triangle == allPlayableCharacters[currentScrollNum + i] || player.square == allPlayableCharacters[currentScrollNum + i] || player.circle == allPlayableCharacters[currentScrollNum + i])
                        {
                            selectableCharacters[i] = null;
                            selectCharUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
                            inUseUI[i].GetComponent<SpriteRenderer>().color = alphaOn;
                        }
                        else
                        {
                            selectCharUI[i].GetComponent<SpriteRenderer>().color = alphaOn;
                            selectableCharacters[i] = allPlayableCharacters[currentScrollNum + i];
                            inUseUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
                        }
                    }
                }
                //if character is not in the GameData script
                else
                {
                    fridgeCharacterPlaceHolders[i].GetComponent<SpriteRenderer>().color = alphaOff;
                    selectCharUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
                }
            }
            else
            {
                fridgeCharacterPlaceHolders[i].GetComponent<SpriteRenderer>().color = alphaOff;
                selectCharUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
            }
        }
    }

    private int FindCharacterIndex(int curIndex)
    {
        int index = -1;
        for (int i = 0; i < gameData.CharacterListNames.Length; i++)
        {
            if (allPlayableCharacters[curIndex].characterName.ToLower() == gameData.CharacterListNames[i].ToLower())
            {
                index = i;
            }
        }
        return index;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            for (int i = 0; i < selectCharUI.Length; i++)
            {
                selectCharUI[i].GetComponent<SpriteRenderer>().color = alphaOn;
            }
            for (int i = 0; i < fridgeCharacterPlaceHolders.Length; i++)
            {
                fridgeCharacterPlaceHolders[i].SetActive(true);
            }
            for (int i = 0; i < inUseUI.Length; i++)
            {
                inUseUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
            }

            player = other.gameObject.GetComponent<MainPlayer>();
            playerWithinRange = true;
            currentScrollNum = 0;
            saveManager.Load();
            rightArrow.SetActive(true);
            leftArrow.SetActive(true);
            leftArrow.GetComponent<SpriteRenderer>().color = alphaOff;
            rightArrow.GetComponent<SpriteRenderer>().color = alphaOn;
            Scroll();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        for (int i = 0; i < fridgeCharacterPlaceHolders.Length; i++)
        {
            fridgeCharacterPlaceHolders[i].SetActive(false);
        }
        for (int i = 0; i < selectCharUI.Length; i++)
        {
            selectCharUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
        }
        for (int i = 0; i < inUseUI.Length; i++)
        {
            inUseUI[i].GetComponent<SpriteRenderer>().color = alphaOff;
        }
        switchCharacterPhase = false;

        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        if (other.gameObject.tag == "Player1")
        {
            playerWithinRange = false;
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
