﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DiscoverCharacter : MonoBehaviour
{

    private Animator anim;
    [HideInInspector]
    public BasePlayer currentChar;

    public BasePlayer[] characters;

    public bool spawnThis;
    public int index;

    public int[] excludeIndex;

    private MainPlayer player;
    public GameObject pickUpSound;

    public bool chooseSwap = false;
    public bool fridgeSwap = false;

    [HideInInspector]
    public GameObject lastButton;

    public EventSystem events;

    public Image overlay;

    public Image upCharacter;

    public Image downCharacter;

    public Image leftCharacter;

    public Image rightCharacter;

    public Text choose;

    public bool isFridge;

    // Start is called before the first frame update
    void Start()
    {
        if (upCharacter == null)
        {
            upCharacter = GameObject.Find("CD_Top").GetComponent<Image>();
        }
        if (downCharacter == null)
        {
            downCharacter = GameObject.Find("CD_Bottom").GetComponent<Image>();
        }
        if (leftCharacter == null)
        {
            leftCharacter = GameObject.Find("CD_Left").GetComponent<Image>();
        }
        if (rightCharacter == null)
        {
            rightCharacter = GameObject.Find("CD_Right").GetComponent<Image>();
        }
        if (choose == null)
        {
            choose = GameObject.Find("Choose").GetComponent<Text>();
        }
        if(overlay == null)
        {
            overlay = GameObject.Find("Overlay").GetComponent<Image>();
        }
        upCharacter.enabled = false;
        downCharacter.enabled = false;
        leftCharacter.enabled = false;
        rightCharacter.enabled = false;
        choose.enabled = false;
        overlay.enabled = false;

        player = GameObject.FindGameObjectWithTag("Player1").GetComponent<MainPlayer>();
        if (!isFridge)
        {
            anim = GetComponent<Animator>();


            if (spawnThis)
            {
                currentChar = characters[index];
            }
            else
            {
                int randNum = (int)Random.Range(0, characters.Length);
                bool check = false;

                do
                {
                    check = false;

                    //dont spawn one i want to exclude
                    for (int i = 0; i < excludeIndex.Length; i++)
                    {
                        if (randNum == excludeIndex[i])
                        {
                            check = true;
                        }
                    }

                    //make sure doesnt spawn one character has
                    if (characters[randNum] == player.cross)
                    {
                        check = true;
                    }
                    if (characters[randNum] == player.triangle)
                    {
                        check = true;
                    }
                    if (characters[randNum] == player.circle)
                    {
                        check = true;
                    }
                    if (characters[randNum] == player.square)
                    {
                        check = true;
                    }

                    //make sure two dont spawn the same
                    GameObject[] characterDiscover;
                    characterDiscover = GameObject.FindGameObjectsWithTag("CharacterDiscover");
                    foreach (GameObject c in characterDiscover)
                    {
                        if (c.GetComponent<DiscoverCharacter>().currentChar == characters[randNum])
                        {
                            check = true;
                        }
                    }

                    if (check)
                    {
                        randNum = (int)Random.Range(0, characters.Length);
                    }

                } while (check == true);

                currentChar = characters[randNum];
            }
            AnimationHandler();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (chooseSwap)
        {

            if (player.myPlayer.GetButtonDown("Square"))
            {
                player.CharacterSwap(currentChar, "Square");
                player.AddCharacterToSaveFile(currentChar.characterName);
                //player.square = currentChar;
                upCharacter.enabled = false;
                downCharacter.enabled = false;
                leftCharacter.enabled = false;
                rightCharacter.enabled = false;
                choose.enabled = false;
                overlay.enabled = false;
                if (!isFridge)
                {
                    Destroy(gameObject);
                }
                else
                {
                    fridgeSwap = false;
                    chooseSwap = false;
                    events.SetSelectedGameObject(lastButton);
                }
            }
            else if (player.myPlayer.GetButtonDown("Triangle"))
            {
                player.CharacterSwap(currentChar, "Triangle");
                player.AddCharacterToSaveFile(currentChar.characterName);
                //player.triangle = currentChar;
                upCharacter.enabled = false;
                downCharacter.enabled = false;
                leftCharacter.enabled = false;
                rightCharacter.enabled = false;
                choose.enabled = false;
                overlay.enabled = false;
                if (!isFridge)
                {
                    Destroy(gameObject);
                }
                else
                {
                    fridgeSwap = false;
                    chooseSwap = false;
                    events.SetSelectedGameObject(lastButton);
                }
            }
            else if (player.myPlayer.GetButtonDown("Circle"))
            {
                player.CharacterSwap(currentChar, "Circle");
                player.AddCharacterToSaveFile(currentChar.characterName);
                //player.circle = currentChar;
                upCharacter.enabled = false;
                downCharacter.enabled = false;
                leftCharacter.enabled = false;
                rightCharacter.enabled = false;
                choose.enabled = false;
                overlay.enabled = false;
                if (!isFridge)
                {
                    Destroy(gameObject);
                }
                else
                {
                    fridgeSwap = false;
                    chooseSwap = false;
                    events.SetSelectedGameObject(lastButton);
                }
            }
            else if (player.myPlayer.GetButtonDown("Cross"))
            {
                player.CharacterSwap(currentChar, "Cross");
                player.AddCharacterToSaveFile(currentChar.characterName);
                //player.circle = currentChar;
                upCharacter.enabled = false;
                downCharacter.enabled = false;
                leftCharacter.enabled = false;
                rightCharacter.enabled = false;
                choose.enabled = false;
                overlay.enabled = false;
                if (!isFridge)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Work, yeah?");
                    fridgeSwap = false;
                    chooseSwap = false;
                    events.SetSelectedGameObject(lastButton);
                }
            }
        }

        if (fridgeSwap)
        {
            events.SetSelectedGameObject(null);
            if (player.triangle != null)
            {
                upCharacter.sprite = player.triangle.hudIcon;
            }
            else
            {
                upCharacter.sprite = player.cross.hudIcon;
                upCharacter.color = Color.black;
            }
            //this will always be a character
            downCharacter.sprite = player.cross.hudIcon;
            if (player.square != null)
            {
                leftCharacter.sprite = player.square.hudIcon;
            }
            else
            {
                leftCharacter.sprite = player.cross.hudIcon;
                leftCharacter.color = Color.black;
            }
            if (player.circle != null)
            {
                rightCharacter.sprite = player.circle.hudIcon;
            }
            else
            {
                rightCharacter.sprite = player.cross.hudIcon;
                rightCharacter.color = Color.black;
            }
            chooseSwap = true;
            upCharacter.enabled = true;
            downCharacter.enabled = true;
            leftCharacter.enabled = true;
            rightCharacter.enabled = true;
            choose.enabled = true;
            overlay.enabled = true;
        }

    }

    //[MOVEMENT AND ANIMATION METHODS]
    private void AnimationHandler()
    {
        //handles which character the player currently is, in terms of animation
        switch (currentChar.characterName)
        {
            case "tofu":
                anim.SetInteger("characterID", 1);
                break;

            case "onigiri":
                anim.SetInteger("characterID", 2);
                break;

            case "takoyaki":
                anim.SetInteger("characterID", 3);
                break;

            case "cherry":
                anim.SetInteger("characterID", 4);
                break;

            case "cannoli":
                anim.SetInteger("characterID", 5);
                break;

            case "burger":
                anim.SetInteger("characterID", 6);
                break;

            case "sashimi":
                anim.SetInteger("characterID", 7);
                break;

            case "fries":
                anim.SetInteger("characterID", 8);
                break;
            case "taco":
                anim.SetInteger("characterID", 9);
                break;
            case "donut":
                anim.SetInteger("characterID", 10);
                break;
            case "hotdog":
                anim.SetInteger("characterID", 11);
                break;
            case "napoleon":
                anim.SetInteger("characterID", 12);
                break;
            case "blueberryMuffin":
                anim.SetInteger("characterID", 13);
                break;
            case "cone":
                anim.SetInteger("characterID", 14);
                break;
            case "lobsterTail":
                anim.SetInteger("characterID", 15);
                break;
            case "samosa":
                anim.SetInteger("characterID", 16);
                break;
            case "tunaCan":
                anim.SetInteger("characterID", 17);
                break;
            case "Orb":
                anim.SetInteger("characterID", 15);
                break;
            case "5RoundBurst":
                anim.SetInteger("characterID", 13);
                break;
            case "pancakes":
                anim.SetInteger("characterID", 18);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            chooseSwap = false;
            upCharacter.enabled = false;
            downCharacter.enabled = false;
            leftCharacter.enabled = false;
            rightCharacter.enabled = false;
            choose.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            try
            {
                Instantiate(pickUpSound, transform.position, Quaternion.identity);
            }
            catch { }
            MainPlayer player = other.gameObject.GetComponent<MainPlayer>();
            if (player.cross != null && player.triangle != null && player.circle != null && player.square != null)
            {
                chooseSwap = true;
                upCharacter.sprite = player.triangle.hudIcon;
                downCharacter.sprite = player.cross.hudIcon;
                leftCharacter.sprite = player.square.hudIcon;
                rightCharacter.sprite = player.circle.hudIcon;
                upCharacter.enabled = true;
                downCharacter.enabled = true;
                leftCharacter.enabled = true;
                rightCharacter.enabled = true;
                choose.enabled = true;
            }
            else
            if (player.square != currentChar && player.triangle != currentChar && player.circle != currentChar && player.cross != currentChar)
            {
                if (player.square == null)
                {
                    player.AddCharacter(currentChar, "Square");
                    player.AddCharacterToSaveFile(currentChar.characterName);
                    //player.square = currentChar;
                    Destroy(gameObject);
                }
                else if (player.triangle == null)
                {
                    player.AddCharacter(currentChar, "Triangle");
                    player.AddCharacterToSaveFile(currentChar.characterName);
                    //player.triangle = currentChar;
                    Destroy(gameObject);
                }
                else if (player.circle == null)
                {
                    player.AddCharacter(currentChar, "Circle");
                    player.AddCharacterToSaveFile(currentChar.characterName);
                    //player.circle = currentChar;
                    Destroy(gameObject);
                }
            }
        }
    }

    public void EnableCharSwap()
    {
        fridgeSwap = true;

    }

}
