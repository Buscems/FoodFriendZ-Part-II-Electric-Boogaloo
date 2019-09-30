using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public float timeTillDespawn;
    [HideInInspector]
    public bool canBounce;
    //[HideInInspector]
    public Vector3 velocity;

    public BoxCollider2D top;
    public BoxCollider2D bottom;
    public BoxCollider2D left;
    public BoxCollider2D right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        velocity = transform.right * bulletSpeed * Time.deltaTime;

        transform.rotation = Quaternion.identity;

    }

    private void Update()
    {
        timeTillDespawn -= Time.deltaTime;

        if (timeTillDespawn < 0)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBounce && collision.gameObject.tag != "Player1" && collision.gameObject.tag != "Projectile")
        {
            if (top.IsTouching(collision))
            {
                velocity.y *= -1;
            }
            if (bottom.IsTouching(collision))
            {
                velocity.y *= -1;
            }
            if (right.IsTouching(collision))
            {
                velocity.x *= -1;
            }
            if (left.IsTouching(collision))
            {
                velocity.x *= -1;
            }
        }

        if(!canBounce && collision.gameObject.tag == "TilesHere")
        {
            Destroy(this.gameObject);
        }

    }
}
