using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newSize = transform.parent.GetComponent<RectTransform>().rect.height/130;
        //newSize is equal to .43f (verified by Debug.Log
        GetComponent<RectTransform>().localScale = new Vector3(newSize, newSize, 1);
    }
}
