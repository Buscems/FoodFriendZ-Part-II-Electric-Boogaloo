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
    public TextMeshProUGUI critChanceDisp;

    public TextMeshProUGUI MaxCoolDownDuration;
    public TextMeshProUGUI effectTimer;
    public TextMeshProUGUI curTimer;

    void Update()
    {
        //update text
        mSpeedDisp.text = "mS: " + pScript.speedMultiplier.ToString("00.00");
        aSpeedDisp.text = "aSpd: " + pScript.attackSpeedMultiplier.ToString("00.00");
        aPowDisp.text = "aP: " + pScript.baseDamageMulitplier.ToString("00.00");
        aSizeDisp.text = "aSize: " + pScript.attackSizeMultiplier.ToString("00.00");

        critChanceDisp.text = "crit%: " + pScript.critChanceMultiplier.ToString("00.00");

        if (ItemMangerScript.item != null)
        {
            MaxCoolDownDuration.text = "MaxCD: " + ItemMangerScript.PowerUpScript.maxCoolDownDuration.ToString("00.00");
            effectTimer.text = "effectTimer: " + ItemMangerScript.curEffectTimer.ToString("00.00");
            curTimer.text = "curCDTimer: " + ItemMangerScript.curCDTimer.ToString("00.00");
        }
    }
}
