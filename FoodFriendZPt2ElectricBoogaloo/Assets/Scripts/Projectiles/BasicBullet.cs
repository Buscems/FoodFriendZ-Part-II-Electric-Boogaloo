using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;

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

    public GameObject instantiateOnDestroy;

    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public bool stopMoving = false;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        velocity = transform.right * bulletSpeed;

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

    public void SetStatusEffects(float[] _burn, float[] _fire, float[] _poison, float _freeze, float _stun)
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBoomerang && timeBeforeReturning < 0)
        {
            GetComponent<Animator>().enabled = false;
            float step = bulletSpeed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

        }
        else if(stopMoving == false)
        {
            rb.MovePosition(transform.position + velocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            if (isBoomerang && timeBeforeReturning < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canBounce && collision.gameObject.tag == "TilesHere")
        {
            velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
        }
    }

    private void OnDestroy()
    {
        try
        {
            Instantiate(instantiateOnDestroy, transform.position, Quaternion.identity);
        }
        catch { }
    }
}
