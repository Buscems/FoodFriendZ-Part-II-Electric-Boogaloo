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
    public TextMeshProUGUI TemporaryHeartNumDisplay;
    public int Temporaryhealth;

    [Header("Hearts")]
    public float health;                        //player HP
    public int MaxNumOfHeartContainers;         //Max player HP (<= hearts) 

    public Image[] hearts;                     //Number of ALL heart IMGs in the SCENE
    public Sprite FullHeart;
    public Sprite HalfHeart;
    public Sprite EmptyHeart;
    [Space]
    public Sprite TempHeart;


    [Header("EquiptmentToolBar")]
    public GameObject EquiptMentToolBar;

    [Header("Item")]
    public Image ItemEquiped_Container;

    [Header("CoolDown")]
    public float CoolDown_Value;

    [Header("Money")]
    public int money;
    public TextMeshProUGUI MoneyCountDisplay;

    public void Update()
    {
        #region hp display system
        //[HEARTS]
        //sets limit on heart containers
        if (health > MaxNumOfHeartContainers)
        {
            health = MaxNumOfHeartContainers;
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
            if (i < MaxNumOfHeartContainers)
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
