using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public int damage;

    Rigidbody2D rb;

    [HideInInspector] public bool slowBullet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + velocity * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            var targ = collision.GetComponent<MainPlayer>();
            targ.GetHit(damage);
            if (slowBullet)
            {
                targ.StartCoroutine(targ.Slow(3));
            }
            Destroy(this.gameObject);
        }

        if (collision.tag == "TilesHere")
        {
            Destroy(this.gameObject);
        }
    }
}
