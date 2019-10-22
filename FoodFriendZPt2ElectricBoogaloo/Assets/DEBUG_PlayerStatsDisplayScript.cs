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

    void Start()
    {

    }

    void Update()
    {
        //update text
        mSpeedDisp.text = pScript.speedMultiplier.ToString("00.00");
        aSpeedDisp.text = pScript.attackSpeedMultiplier.ToString("00.00");
        aPowDisp.text = pScript.maxDamageMultiplier.ToString("00.00");
    }
}
