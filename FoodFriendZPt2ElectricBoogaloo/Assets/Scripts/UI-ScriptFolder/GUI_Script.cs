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

    [Header("ScreenOverLays")]
    public GameObject guiOverlay;
    public GameObject gameOverOverlay;

    public GameObject CharacterCompassParent;

    [Header("TemporaryHearts")]
    public int Temporaryhealth;

    [Header("Hearts")]
    public int health;                        //player HP
    private int maxNumOfHeartContainers;         //Max player HP (<= hearts) 

    public Image[] hearts;                     //Number of ALL heart IMGs in the SCENE
    public Sprite FullHeart;
    public Sprite HalfHeart;
    public Sprite EmptyHeart;
    [Space]
    public Sprite TempHeart;

    private MainPlayer player; 


    [Header("EquiptmentToolBar")]
    public GameObject EquiptMentToolBar;

    [Header("Item")]
    public Image ItemEquiped_Container;

    [Header("CoolDown")]
    public float CoolDown_Value;

    [Header("Money")]
    public int money;
    private TextMeshProUGUI currencyText;
    public TextMeshProUGUI MoneyCountDisplay;

    private void Start()
    {
        maxNumOfHeartContainers = hearts.Length;
        player = GameObject.Find("Player").GetComponent<MainPlayer>();
        currencyText = GameObject.Find("MoneyCount").GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        currencyText.SetText("" + player.currency);
        health = player.health;

        #region hp display system
        //[HEARTS]
        //sets limit on heart containers
        if (health > maxNumOfHeartContainers)
        {
            health = maxNumOfHeartContainers;
            player.health = health;
        }

        //checks to see full hearts based on health
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = FullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }

            //limit on heart containers displayed
            if (i < maxNumOfHeartContainers)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        #endregion
    }

    #region Game over test
    //GameOver Test
    public void GameOverTestButtonFunction()
    {
        guiOverlay.SetActive(false);
        gameOverOverlay.SetActive(true);
    }
    #endregion

    //this public void is used to access player status to update GUI
    public void PlayerCurrentStatusUpdater(float RecievedPlayerHP, int RecievedMoney)
    {
        // health = RecievedPlayerHP;
        // money = RecievedMoney
    }
}
