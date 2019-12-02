using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1").GetComponent<MainPlayer>();
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
                    if(c.GetComponent<DiscoverCharacter>().currentChar == characters[randNum])
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

    // Update is called once per frame
    void Update()
    {

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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            Instantiate(pickUpSound, transform.position, Quaternion.identity);
            MainPlayer player = other.gameObject.GetComponent<MainPlayer>();
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
}
