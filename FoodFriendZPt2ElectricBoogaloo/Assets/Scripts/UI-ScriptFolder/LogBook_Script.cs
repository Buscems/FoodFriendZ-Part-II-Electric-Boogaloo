using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

/*[[TO DO]]
 *
 */

public class LogBook_Script : MonoBehaviour
{

    AudioSource audioSource;

    public TextMeshProUGUI CurrentTab_Display;

    public GameObject TitleScreenOverlay;
    public GameObject LogBookScreenOverlay;

    [Header("Buttons")]
    public Button CharactersButton;
    public Button ItemButton;
    public Button EquipmentButton;
    public Button FloorsButton;
    public Button EnemiesButton;

    [Header("Panels")]
    public GameObject CharacterPanel;
    public GameObject ItemPanel;
    public GameObject EquipmentPanel;
    public GameObject FloorsPanel;
    public GameObject EnemiesPanel;

    public EventSystem es;

    public Button startButton;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //as default
        CurrentTab_Display.text = ("Characters");
    }

    #region all buttons
    //Character tab
    public void CharacterButtonFunction()
    {
        audioSource.Play();
        Debug.Log("Opening Character Tab");
        CurrentTab_Display.text = ("Characters");
    }

    //Items tab
    public void ItemButtonFunction()
    {
        audioSource.Play();
        Debug.Log("Opening Item Tab");
        CurrentTab_Display.text = ("Items");
    }

    //Equipment tab
    public void EquipmentButtonFunction()
    {
        audioSource.Play();
        Debug.Log("Opening Equipment Tab");
        CurrentTab_Display.text = ("Equipment");
    }

    //Floors tab
    public void FloorsButtonFunction()
    {
        audioSource.Play();
        Debug.Log("Opening Floors Tab");
        CurrentTab_Display.text = ("Floors");
    }

    //Enemies tab
    public void EnemiesButtonFunction()
    {
        audioSource.Play();
        Debug.Log("Opening Enemies Tab");
        CurrentTab_Display.text = ("Enemies");
    }

    //Back button tab
    public void BackButtonFunction()
    {
        audioSource.Play();
        es.SetSelectedGameObject(startButton.gameObject);
        TitleScreenOverlay.SetActive(true);
        LogBookScreenOverlay.SetActive(false);
    }
    #endregion
}
