using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotateSpeed;
    public bool clockwise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!clockwise)
        {
            transform.eulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles -= new Vector3(0, 0, rotateSpeed * Time.deltaTime);
        }
    }
}
