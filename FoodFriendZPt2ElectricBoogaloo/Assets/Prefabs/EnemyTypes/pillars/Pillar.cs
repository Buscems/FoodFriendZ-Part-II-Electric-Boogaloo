using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public PillarEnemy pillar;
    public float speed;
    Vector2 velocity;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pillar = GetComponent<PillarEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(-speed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;

        transform.position = pos;
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
