using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Image panel;
    public Text text;


    // Start is called before the first frame update
    void Start()
    {
        panel.enabled = true;
        text.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string _text)
    {
        panel.enabled = true;
        text.enabled = true;
        text.text = _text;        
    }

    public void Exit()
    {
        panel.enabled = false;
        text.enabled = false;
    }
}
