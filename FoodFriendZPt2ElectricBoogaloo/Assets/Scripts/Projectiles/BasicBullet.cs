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
    [HideInInspector]
    public bool isBoomerang;
    [HideInInspector]
    public float timeBeforeReturning;
    //[HideInInspector]
    public Vector3 velocity;

    public BoxCollider2D top;
    public BoxCollider2D bottom;
    public BoxCollider2D left;
    public BoxCollider2D right;

    [HideInInspector]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        velocity = transform.right * bulletSpeed * Time.deltaTime;

        transform.rotation = Quaternion.identity;

        transform.up = velocity;

    }

    private void Update()
    {
        if (!isBoomerang)
        {
            timeTillDespawn -= Time.deltaTime;
            if (timeTillDespawn < 0)
            {
                Destroy(gameObject);
            }
        }


        if (isBoomerang)
        {
            timeBeforeReturning -= Time.deltaTime;
        }
    }

    public void SetVariables(float _bulletSpeed, float _timeTillDespawn, bool _canBounce)
    {
        bulletSpeed = _bulletSpeed;
        timeTillDespawn = _timeTillDespawn;
        canBounce = _canBounce;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBoomerang && timeBeforeReturning < 0)
        {
            Vector3 origRot = player.transform.eulerAngles;
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            float str = Mathf.Min(1 * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            rb.velocity = transform.forward * bulletSpeed;
            transform.eulerAngles = origRot;
        }
        else
        {
            rb.velocity = velocity;
        }
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


        if(collision.gameObject.tag == "Player1")
        {
            if (isBoomerang && timeBeforeReturning < 0)
            {
                Destroy(gameObject);
            }
        }
       

    }
}
