using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
