using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DEBUG_PlayerStatsDisplayScript : MonoBehaviour
{
    [Header("Scripts")]
    public MainPlayer pScript;
    public ItemManager ItemMangerScript;

    [Header("Text")]
    public TextMeshProUGUI mSpeedDisp;
    public TextMeshProUGUI aSpeedDisp;
    public TextMeshProUGUI aPowDisp;
    public TextMeshProUGUI aSizeDisp;
    [Space]
    public TextMeshProUGUI critChanceDisp;
    public TextMeshProUGUI stunChanceDisp;
    public TextMeshProUGUI bleedChanceDisp;
    public TextMeshProUGUI burnChanceDisp;
    public TextMeshProUGUI poisonChanceDisp;
    public TextMeshProUGUI freezeChanceDisp;
    [Space]
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI MaxCoolDownDuration;
    public TextMeshProUGUI effectTimer;
    public TextMeshProUGUI curTimer;

    void Update()
    {
        //update text
        mSpeedDisp.text = "mS: " + pScript.speedMultiplier.ToString("0.00");
        aSpeedDisp.text = "aSpd: " + pScript.attackSpeedMultiplier.ToString("0.00");
        aPowDisp.text = "aP: " + pScript.baseDamageMulitplier.ToString("0.00");
        aSizeDisp.text = "aSize: " + pScript.attackSizeMultiplier.ToString("0.00");

        critChanceDisp.text = "crit%: " + pScript.critChanceMultiplier.ToString("0.00");
        stunChanceDisp.text = "stun%: " + pScript.inflictStunChance.ToString("0.00");
        bleedChanceDisp.text = "bleed%: " + pScript.inflictBleedChance.ToString("0.00");
        burnChanceDisp.text = "bleed%: " + pScript.inflictBurnChance.ToString("0.00");
        poisonChanceDisp.text = "poison%: " + pScript.inflictPoisonChance.ToString("0.00");
        freezeChanceDisp.text = "freeze%: " + pScript.inflictFreezeChance.ToString("0.00");

        if (ItemMangerScript.item != null)
        {
            itemName.text = ItemMangerScript.item.name;
            MaxCoolDownDuration.text = "MaxCD: " + ItemMangerScript.PowerUpScript.maxCoolDownDuration.ToString("00.00");
            effectTimer.text = "effectTimer: " + ItemMangerScript.curEffectTimer.ToString("00.00");
            curTimer.text = "curCDTimer: " + ItemMangerScript.curCDTimer.ToString("00.00");
        }
    }
}
