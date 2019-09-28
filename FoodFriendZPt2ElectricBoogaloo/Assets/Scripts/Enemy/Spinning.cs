using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{

    public float rotateSpeed;
    private Vector2 startPos;
    private Vector2 currentPos;
    
    void Start()
    {
        startPos = transform.position;
    }

    
    void Update()
    {
        transform.position = currentPos;
        currentPos.x = startPos.x;
        currentPos.y = startPos.y;

        transform.Rotate(0, 0, rotateSpeed);
    }
}
