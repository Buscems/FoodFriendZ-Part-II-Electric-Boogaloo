using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemy : MonoBehaviour
{
    //code from https://answers.unity.com/questions/1359733/moving-an-enemy-randomly.html

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 3f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    public CircleCollider2D alert;

    public GameObject water;

    public float spawnTime;

    void Start()
    {
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
        StartCoroutine(spawnWater());
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TilesHere")
        {
            Debug.Log("CHANGE");
            calcuateNewMovementVector();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TilesHere"){
            Debug.Log("change");
            calcuateNewMovementVector();
        }
    }

    IEnumerator spawnWater(){
        var temp = Instantiate(water, transform.position, transform.rotation);
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(spawnWater());
    }
}
