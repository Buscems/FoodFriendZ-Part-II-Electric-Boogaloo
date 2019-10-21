using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //NEED to access other scenes
using UnityEngine.EventSystems;     //NEED to use for Buttons
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

/*[CITATIONS]
 * (video) START MENU in Unity
 *https://www.youtube.com/watch?v=zc8ac_qUXQY
 */

public class OptionsMenu_Script : MonoBehaviour
{

    AudioSource audioSource;

    [Header("Screen Overlays")]
    public GameObject DefaultScreenOverlay; //Title Screen or GUI
    public GameObject OptionsScreenOverlay;

    public Button startButton;
    public EventSystem es;

    public Slider VolumeSlider;
    public float volume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void BackButtonFunction()
    {
        audioSource.Play();
        es.SetSelectedGameObject(startButton.gameObject);
        DefaultScreenOverlay.SetActive(true);
        OptionsScreenOverlay.SetActive(false);
    }
}
