using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DEBUG_PlayerStatsDisplayScript : MonoBehaviour
{
    [Header("Scripts")]
    public MainPlayer pScript;

    [Header("Text")]
    public TextMeshProUGUI mSpeedDisp;
    public TextMeshProUGUI aSpeedDisp;
    public TextMeshProUGUI aPowDisp;
    public TextMeshProUGUI aSizeDisp;

    void Start()
    {

    }

    void Update()
    {
        //update text
        mSpeedDisp.text = "mS: " + pScript.speedMultiplier.ToString("00.00");
        aSpeedDisp.text = "aSpd: " + pScript.attackSpeedMultiplier.ToString("00.00");
        aPowDisp.text = "aP: " + pScript.maxDamageMultiplier.ToString("00.00");
        aSizeDisp.text = "aSize: " + pScript.attackSizeMultiplier.ToString("00.00");
    }
}
