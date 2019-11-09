using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    ItemExtension ieScript;

    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public int damage;

    Rigidbody2D rb;

    [HideInInspector] public bool slowBullet;

    [HideInInspector] public bool returnToSender;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //assigned script
        ieScript = GameObject.Find("Player").GetComponent<ItemExtension>();
    }

    void FixedUpdate()
    {
        if (!returnToSender)
        {
            rb.MovePosition(transform.position + velocity * speed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(transform.position - velocity * speed * 2 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1" || collision.tag == "Player2")
        {
            if (ieScript.needProjectileScript)
            {
                ieScript.projectileScripts = this;
                ieScript.hasProjectileHitPlayer = true;
            }

            else
            {
                var targ = collision.GetComponent<MainPlayer>();
                targ.GetHit(damage);
                if (slowBullet)
                {
                    targ.StartCoroutine(targ.Slow(3));
                }
                Destroy(this.gameObject);
            }
        }

        if (collision.tag == "TilesHere")
        {
            Destroy(this.gameObject);
        }

        if (collision.tag == "Enemy" && returnToSender)
        {
            var targ = collision.GetComponent<BaseEnemy>();
            targ.TakeDamage(damage);

            Destroy(this.gameObject);
        }
    }
}
