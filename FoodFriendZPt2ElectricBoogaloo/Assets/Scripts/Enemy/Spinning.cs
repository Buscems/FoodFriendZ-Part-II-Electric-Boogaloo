using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{

    public float rotateSpeed;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(rotateSpeed, 0, 0);
    }
}
