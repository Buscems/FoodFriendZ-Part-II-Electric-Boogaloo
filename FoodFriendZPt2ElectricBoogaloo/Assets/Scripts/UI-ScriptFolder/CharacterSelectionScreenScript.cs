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

        for (int i = 0; i < characterButtons.Length; i++)
        {
            characterButtons[i].sprite = characterSprites[i];
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
    }
    public void Onigiri()
    {
        HighlightedCharacterIMG.sprite = characterSprites[1];
        HighlightedCharacterNameDisplay.text = "Onigiri";
    }
    public void Takoyaki()
    {
        HighlightedCharacterIMG.sprite = characterSprites[2];
        HighlightedCharacterNameDisplay.text = "Takoyaki";
    }
    public void Cherry()
    {
        HighlightedCharacterIMG.sprite = characterSprites[3];
        HighlightedCharacterNameDisplay.text = "Cherry";
    }
    public void Cannoli()
    {
        HighlightedCharacterIMG.sprite = characterSprites[4];
        HighlightedCharacterNameDisplay.text = "Cannoli";
    }
    public void Fries()
    {
        HighlightedCharacterIMG.sprite = characterSprites[5];
        HighlightedCharacterNameDisplay.text = "Fries";
    }
    public void Burger()
    {
        HighlightedCharacterIMG.sprite = characterSprites[6];
        HighlightedCharacterNameDisplay.text = "Burger";
    }
    public void Sashimi()
    {
        HighlightedCharacterIMG.sprite = characterSprites[7];
        HighlightedCharacterNameDisplay.text = "Sashimi";
    }
}
