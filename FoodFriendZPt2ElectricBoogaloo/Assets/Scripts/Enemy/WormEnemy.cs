using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : MonoBehaviour
{
    public BaseEnemy baseEnemy;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        rb.AddForce(new Vector2(baseEnemy.speed, baseEnemy.speed));
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
