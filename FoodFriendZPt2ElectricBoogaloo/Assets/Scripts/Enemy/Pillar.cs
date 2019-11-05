using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public PillarEnemy pillar;
    public float speed;
    Rigidbody2D rb;
    Vector3 velocityX;
    Vector3 velocityY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pillar = GetComponent<PillarEnemy>();
        velocityX = transform.up * speed;
        velocityY = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        if (pillar.spawnDown == true)
        {
            rb.MovePosition(Vector2.down * speed * Time.deltaTime);
        }

        if (pillar.spawnRight == true)
        {
            rb.MovePosition(Vector2.left * speed * Time.deltaTime);
        }
        if (pillar.spawnUp == true)
        {
            rb.MovePosition(Vector2.up * speed * Time.deltaTime);
        }
        if (pillar.spawnRight == true)
        {
            rb.MovePosition(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1"){
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "TilesHere"){
            Destroy(gameObject);
        }
    }
}
