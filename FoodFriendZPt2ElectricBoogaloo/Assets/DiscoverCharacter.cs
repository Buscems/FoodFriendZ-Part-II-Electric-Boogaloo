using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverCharacter : MonoBehaviour
{

    private Animator anim;
    private BasePlayer currentChar;

    public BasePlayer[] characters;

    public bool spawnThis;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (spawnThis)
        {
            currentChar = characters[index];
        }
        else
        {
            int randNum = (int)Random.Range(0, characters.Length);
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
        if(other.gameObject.tag == "Player1")
        {
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
