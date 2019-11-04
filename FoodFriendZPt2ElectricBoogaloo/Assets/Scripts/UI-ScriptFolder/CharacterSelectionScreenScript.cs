using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayerPrefs.SetFloat("startCharacter", 0);

        for (int i = 0; i < characterButtons.Length; i++)
        {
            characterButtons[i].sprite = characterSprites[i];
        }

        HighlightedCharacterIMG.sprite = characterSprites[0];
        HighlightedCharacterNameDisplay.text = "Tofu";

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
        HighlightedCharacterNameDisplay.text = "Napoleon";
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
        HighlightedCharacterNameDisplay.text = "Blueberry Muffin";
        PlayerPrefs.SetInt("startCharacter", 13);
    }
    public void MintChip()
    {
        HighlightedCharacterIMG.sprite = characterSprites[13];
        HighlightedCharacterNameDisplay.text = "Mint Chocolate Chip";
        PlayerPrefs.SetInt("startCharacter", 14);
    }
    public void LobsterTail()
    {
        HighlightedCharacterIMG.sprite = characterSprites[14];
        HighlightedCharacterNameDisplay.text = "Lobster Tail";
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
        HighlightedCharacterNameDisplay.text = "Tuna Can";
        PlayerPrefs.SetInt("startCharacter", 17);
    }
}
