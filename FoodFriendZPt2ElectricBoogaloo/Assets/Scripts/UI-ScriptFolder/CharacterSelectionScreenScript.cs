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
    [Header("Highlighted Character")]
    public Image HighlightedCharacterIMG;
    public TextMeshProUGUI HighlightedCharacterNameDisplay;

    [Space]
    public Image LockedCharacterSprite;

    [Header("Bottom Screen Buttons")]
    public Button BackToTitleScreen;
    public Button PlayGame;

    public void BackToTitleScreenFunction()
    {
        Debug.Log("Loading TitleScreen");
        SceneManager.LoadScene("TitleScreen");
    }

    public void PlayGameFunction()
    {
        Debug.Log("Loading GameplayScene");
        SceneManager.LoadScene("GameplayScreen");
    }
}
