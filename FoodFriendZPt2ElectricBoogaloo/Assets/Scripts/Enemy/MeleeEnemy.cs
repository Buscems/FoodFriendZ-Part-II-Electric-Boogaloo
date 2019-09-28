using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    BaseEnemy BaseEnemy;
    Tracking Tracking;
    MainPlayer MainPlayer;
    Rigidbody2D rb;

    [Tooltip("Knockback amount on X and Y")]
    public Vector3 knock;
    [Tooltip("Speed of knockback")]
    public float speed;

    //public GameObject Melee;
    private Vector2 startPos;
    private Vector2 hitPos;

    private bool force;

    public int damage;

    [Tooltip("Amount of Time enemy is knocked back")]
    public float KnockbackTime;

    void Start()
    {
        Tracking = this.GetComponent<Tracking>();
        BaseEnemy = this.GetComponent<BaseEnemy>();
        MainPlayer = this.GetComponent<MainPlayer>();
        rb = this.GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    
    void Update()
    {

        if (BaseEnemy.aggroScript.aggro)
        {
            if (Tracking.follow)
            {
                BaseEnemy.speed = BaseEnemy.speed * 2;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            //hitPos = transform.position;
            collision.GetComponent<MainPlayer>().health -= damage;
            StartCoroutine(Knockback());
        }
    }

    IEnumerator Knockback()
    {
        Debug.Log("Starting knockback");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x + knock.x, transform.position.y + knock.y);
        //rb.MovePosition(transform.position + knock * speed * Time.deltaTime);
        yield return new WaitForSeconds(KnockbackTime);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
