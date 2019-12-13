using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolvingBullet : MonoBehaviour
{

    //help from https://www.youtube.com/watch?v=_Z1t7MNk0c4

    Transform target;
    private Vector2 location;

    public GameObject proj;
    public Transform revolveAround;
    public float speed;
    public Vector3 velocity;
    public BaseEnemy baseEnemy;
    EnemyBullet bullet;

    Rigidbody2D rb;

    private Vector3 zAxis = new Vector3(0, 0, 1);

    public bool revolve;
    public bool shoot;

    public float iSeeYou;
    float shootTime;

    public SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        bullet = GetComponent<EnemyBullet>();
        rb = GetComponent<Rigidbody2D>();
        revolve = true;
        shootTime = Random.Range(3f, 5f);
        baseEnemy.aggroScript.aggro = false;
        rend.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (revolve == true)
        {
            transform.RotateAround(revolveAround.position, zAxis, speed);
        }

        if (baseEnemy.aggroScript.aggro == true){
            StartCoroutine(See());
        }

        if (baseEnemy.health <= 0){
            rend.enabled = false;
        }

        if (shoot == true)
        {
           
            //bullet.enabled = true;
            transform.position = Vector2.MoveTowards(transform.position, location, speed * Time.deltaTime);

            if (transform.position.x == location.x && transform.position.y == location.y){
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            Destroy(proj);
            rend.enabled = false;
        }
    }

    IEnumerator See(){
        yield return new WaitForSeconds(iSeeYou);
        StartCoroutine(timeForShot());
    }

    IEnumerator timeForShot(){
        yield return new WaitForSeconds(shootTime);
        revolve = false;
        shoot = true;
        target = GameObject.FindGameObjectWithTag("Player1").transform;
        location = new Vector2(target.position.x, target.position.y);
        speed = 4;
    }
}
