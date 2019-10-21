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
    public TextMeshProUGUI mspeedDisp;
    public TextMeshProUGUI aspeedDisp;
    public TextMeshProUGUI ApowDisp;

    //variables
    float mspeedMult;
    float aspeedMult;
    float apowMult;

    void Start()
    {

    }

    void Update()
    {

    }
}
