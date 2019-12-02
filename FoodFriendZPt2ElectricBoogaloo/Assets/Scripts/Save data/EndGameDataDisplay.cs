using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameDataDisplay : MonoBehaviour
{

    //Enemy Variables
    public TextMeshProUGUI damageDealtText;
    public TextMeshProUGUI enemiesKilledText;
    public static float damageDealt;
    public static int enemiesKilled;

    //Player Variables
    public TextMeshProUGUI damageTakenText;
    public TextMeshProUGUI healthRecoveredText;
    public TextMeshProUGUI meleeAttacksText;
    public TextMeshProUGUI rangedAttacksText;
    public static float damageTaken;
    public static int healthRecovered;
    public static int meleeAttacks;
    public static int rangedAttacks;

    //Money Variables
    public TextMeshProUGUI moneySpentText;
    public TextMeshProUGUI moneyGeneratedText;
    public TextMeshProUGUI chestsOpenedText;
    public static int moneyGenerated;
    public static int moneySpent;
    public static int chestsOpened;

    //Equipment Variables


    //Item Variables

   
    //Game Data(Levels, Time, Info)

    
    void Update()
    {
        damageDealtText.text = "Damage Dealt: " + damageDealt;
        enemiesKilledText.text = "Enemies Killed: " + enemiesKilled;
        damageTakenText.text = "Damage Taken: " + damageTaken;
        healthRecoveredText.text = "Health Restored: " + healthRecovered;
        moneySpentText.text = "Money Spent: " + moneySpent;
        moneyGeneratedText.text = "Money Generated: " + moneyGenerated;
        chestsOpenedText.text = "Chests Opened: " + chestsOpened;
        meleeAttacksText.text = "Melee Attacks Used: " + meleeAttacks;
        rangedAttacksText.text = "Ranged Attacks Used: " + rangedAttacks;
    }
}
