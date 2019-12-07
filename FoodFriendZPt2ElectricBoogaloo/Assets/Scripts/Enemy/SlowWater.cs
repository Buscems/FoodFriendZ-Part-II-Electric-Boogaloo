using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowWater : MonoBehaviour
{
    //code from https://answers.unity.com/questions/1359733/moving-an-enemy-randomly.html

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 3f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    public Rigidbody2D rb;

    public CircleCollider2D alert;


    public float spawnTime;

    Vector3 dir;

    BulletPool bulletPooler;

    void Start()
    {
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
        rb = GetComponent<Rigidbody2D>();
        bulletPooler = BulletPool.Instance;
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        //move enemy: 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));


        BulletPool.Instance.SpawnFromPool("Slow", transform.position, Quaternion.identity);

    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TilesHere")
        {
            Debug.Log("CHANGE");
            movementPerSecond = -movementPerSecond;
            movementDirection = -movementDirection;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TilesHere")
        {
            Debug.Log("yeet");
            Vector2 inNormal = collision.contacts[0].normal;
            dir = Vector2.Reflect(rb.velocity, inNormal);

            rb.velocity = dir * movementPerSecond;
        }

        if (collision.gameObject.tag == "Water")
        {
            Vector2 inNormal = collision.contacts[0].normal;
            dir = Vector2.Reflect(rb.velocity, inNormal);

            rb.velocity = dir * movementPerSecond;
        }
    }


}
