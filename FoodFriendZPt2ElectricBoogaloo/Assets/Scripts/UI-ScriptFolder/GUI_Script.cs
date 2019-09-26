using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //NEEDS to use UI
using TMPro;                        //NEEDS to use TextMeshPro

public class GUI_Script : MonoBehaviour
{
    /*[CITATIONS]
     * HOW TO MAKE HEART/HEALTH SYSTEM - UNITY TUTORIAL
     * https://www.youtube.com/watch?v=3uyolYVsiWc
     */

    public GameObject CharacterCompassParent;

    [Header("TemporaryHearts")]
    public TextMeshProUGUI TemporaryHeartNumDisplay;
    public int Temporaryhealth;

    [Header("Hearts")]
    public int health;              //player HP
    public int numOfHearts;         //Max player HP (<= hearts) 

    public Image[] hearts;          //Number of ALL heart IMGs in the SCENE
    public Sprite FullHeart;
    public Sprite EmptyHeart;


    [Header("EquiptmentToolBar")]
    public GameObject EquiptMentToolBar;

    [Header("Item")]
    public Image ItemEquiped_Container;

    [Header("CoolDown")]
    public float CoolDown_Value;

    [Header("Money")]
    public int money;
}
