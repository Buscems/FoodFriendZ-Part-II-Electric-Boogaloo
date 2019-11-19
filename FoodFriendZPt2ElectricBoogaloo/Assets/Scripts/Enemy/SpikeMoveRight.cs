using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMoveRight : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private Vector3 velocity;

    public float destroyTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = transform.right * -1 * speed;
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + velocity * Time.deltaTime);
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
