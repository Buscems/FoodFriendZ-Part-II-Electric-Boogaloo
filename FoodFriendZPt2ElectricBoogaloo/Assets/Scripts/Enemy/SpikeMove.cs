using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMove : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            var targ = collision.GetComponent<MainPlayer>();
            targ.GetHit(damage);
            Destroy(this.gameObject);
        }

        if (collision.tag == "TilesHere")
        {
            Destroy(this.gameObject);
        }
    }
}

