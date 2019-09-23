using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    MainPlayer Player;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 velocity;

    public int damage;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.GetComponent<MainPlayer>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + velocity * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player.health = Player.health - damage;
        }
    }
}
